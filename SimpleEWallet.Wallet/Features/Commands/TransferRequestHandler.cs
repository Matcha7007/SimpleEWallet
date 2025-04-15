using MediatR;

using SimpleEWallet.Comon.Base.WebAPI;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Wallet.Persistence;
using SimpleEWallet.Comon.Models.Wallet;
using SimpleEWallet.Wallet.Domain;
using SimpleEWallet.Comon.Enumerations;
using SimpleEWallet.Comon.Extensions;
using SimpleEWallet.Comon.MQ;
using MassTransit;
using SimpleEWallet.Comon.Helpers;
using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Wallet.Features.Queries;

namespace SimpleEWallet.Wallet.Features.Commands
{
	public class TransferRequestHandler(WalletDbContext context, ISendEndpointProvider send, IMediator _mediator) : IRequestHandler<TransferRequestCommand, TopupTransferRequestResponse?>
	{
		public async Task<TopupTransferRequestResponse?> Handle(TransferRequestCommand request, CancellationToken cancellationToken)
		{
			TopupTransferRequestResponse response = new();
			try
			{
				await context.Database.BeginTransactionAsync(cancellationToken);

				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.WalletNumberReceiver))
				{
					response.SetValidationMessage("Wallet Number Receiver is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Pin))
				{
					response.SetValidationMessage("PIN is required to process this action.");
					return response;
				}
				if (request.Parameters.UserId == Guid.Empty)
				{
					response.SetValidationMessage("Token is invalid");
					return response;
				}
				#endregion

				#region Verify PIN
				string url = await _mediator.Send(new GetConfigByKeyValueQuery(GlobalConstant.Wallet.UrlVerifyPin), cancellationToken);
				VerifyPinParameters verifyPinParameters = new()
				{
					UserId = (Guid)request.Parameters.UserId!,
					Pin = request.Parameters.Pin
				};
				VerifyPinResponse verifyPinResponse = await HttpHelper.PostJsonAsync<VerifyPinParameters, VerifyPinResponse>(request.Token, verifyPinParameters, url);
				
				if (string.IsNullOrEmpty(verifyPinResponse.Result) || (AESEncryptionTool.Decrypt(request.Token, verifyPinResponse.Result) != "true"))
				{
					response.SetValidationMessage("PIN is incorrect");
					return response;
				}
				#endregion

				#region Check Wallet Sender
				MstWallet? walletUser = await context.MstWallets
					.Where(x => x.UserId == request.Parameters.UserId && x.IsActive == true)
					.FirstOrDefaultAsync(cancellationToken);

				if (walletUser == null)
				{
					response.SetValidationMessage("Wallet not found");
					return response;
				}
				if (walletUser.Balance < request.Parameters.Amount)
				{
					response.SetValidationMessage("Insufficient balance");
					return response;
				}
				#endregion

				#region Check Wallet Receiver
				MstWallet? walletReceiver = await context.MstWallets
					.Where(x => x.WalletNumber == request.Parameters.WalletNumberReceiver && x.IsActive == true)
					.FirstOrDefaultAsync(cancellationToken);

				if (walletReceiver == null)
				{
					response.SetValidationMessage("Receiver not found");
					return response;
				}
				#endregion

				#region Transfer Request
				TrnTransfer transferRequest = new()
				{
					SenderWalletId = walletUser.Id,
					ReceiverWalletId = walletReceiver.Id,
					Amount = request.Parameters.Amount,
					Description = request.Parameters.Description,
					StatusId = Tools.Status.GetRandomStatus(),
					CreatedAt = DateTime.Now,
					CreatedBy = request.Parameters.UserId,
					LastModifiedAt = DateTime.Now,
					LastModifiedBy = request.Parameters.UserId,
				};
				await context.TrnTransfers.AddAsync(transferRequest, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);

				if (transferRequest.StatusId.Equals(StatusConstants.Pending.ToInt32()))
				{
					UpdateStatusTransferMessage message = new()
					{
						UserId = (Guid)request.Parameters.UserId!,
						TransferId = transferRequest.Id,
						TransferTime = (DateTime)transferRequest.CreatedAt,
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Wallet.UpdateStatusTransfer));
					await sendEndpoint.Send(message, cancellationToken);
					response.Message = "Transfer is Pending";
				}
				else if (transferRequest.StatusId.Equals(StatusConstants.Success.ToInt32()))
				{
					// Update wallet balance
					walletUser.Balance -= transferRequest.Amount;
					walletUser.LastModifiedAt = DateTime.Now;
					walletUser.LastModifiedBy = request.Parameters.UserId;

					walletReceiver.Balance += transferRequest.Amount;
					walletReceiver.LastModifiedAt = DateTime.Now;
					walletReceiver.LastModifiedBy = request.Parameters.UserId;
					await context.SaveChangesAsync(cancellationToken);

					// Send message to transaction service
					AddTransactionMessage messageOut = new()
					{
						UserId = (Guid)request.Parameters.UserId!,
						WalletId = walletUser.Id,
						TransactionTypeId = TransactionTypeConstants.TransferOut.ToInt32(),
						Amount = transferRequest.Amount,
						Reference = transferRequest.Description,
						TransactionRequestId = transferRequest.Id
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Transaction.AddTransaction));
					await sendEndpoint.Send(messageOut, cancellationToken);

					AddTransactionMessage messageIn = new()
					{
						UserId = (Guid)request.Parameters.UserId!,
						WalletId = walletReceiver.Id,
						TransactionTypeId = TransactionTypeConstants.TransferIn.ToInt32(),
						Amount = transferRequest.Amount,
						Reference = transferRequest.Description,
						TransactionRequestId = transferRequest.Id
					};

					await sendEndpoint.Send(messageIn, cancellationToken);
					response.Message = "Transfer is Success";
				}
				else
				{
					response.Message = "Transfer is Failed";
				}

				#endregion
				await context.Database.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				await context.Database.RollbackTransactionAsync(cancellationToken);
			}
			return response;
		}
	}
}
