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

namespace SimpleEWallet.Wallet.Features.Commands
{
	public class TopupRequestHandler(WalletDbContext context, ISendEndpointProvider send) : IRequestHandler<TopupRequestCommand, TopupTransferRequestResponse?>
	{
		public async Task<TopupTransferRequestResponse?> Handle(TopupRequestCommand request, CancellationToken cancellationToken)
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
				if (string.IsNullOrEmpty(request.Parameters.WalletNumber))
				{
					response.SetValidationMessage("Wallet Number is null or empty");
					return response;
				}
				if (request.Parameters.Amount < GlobalConstant.Wallet.MinimumTopupAmount)
				{
					response.SetValidationMessage($"Minimum top-up amount must be greater than or equal to IDR {GlobalConstant.Wallet.MinimumTopupAmount.ToDisplayMoney()}");
					return response;
				}
				if (request.Parameters.UserId == Guid.Empty)
				{
					response.SetValidationMessage("Token is invalid");
					return response;
				}
				#endregion

				#region Check Wallet User
				MstWallet? walletUser = await context.MstWallets
					.Where(x => x.UserId == request.Parameters.UserId && x.WalletNumber == request.Parameters.WalletNumber && x.IsActive == true)
					.FirstOrDefaultAsync(cancellationToken);

				if (walletUser == null)
				{
					response.SetValidationMessage("Wallet not found");
					return response;
				}
				#endregion

				#region Topup Request
				TrnTopupRequest topupRequest = new()
				{
					WalletId = walletUser.Id,
					Amount = request.Parameters.Amount,
					StatusId = Tools.Status.GetRandomStatus(),
					CreatedAt = DateTime.Now,
					CreatedBy = request.Parameters.UserId,
					LastModifiedAt = DateTime.Now,
					LastModifiedBy = request.Parameters.UserId,
				};
				await context.TrnTopupRequests.AddAsync(topupRequest, cancellationToken);
				await context.SaveChangesAsync(cancellationToken);

				if (topupRequest.StatusId.Equals(StatusConstants.Pending.ToInt32()))
				{
					UpdateStatusTopupMessage message = new()
					{
						UserId = (Guid)request.Parameters.UserId!,
						TopupRequestId = topupRequest.Id,
						TopupTime = (DateTime)topupRequest.CreatedAt,
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Wallet.UpdateStatusTopup));
					await sendEndpoint.Send(message, cancellationToken);
					response.Message = "Topup request is Pending";
				}
				else if (topupRequest.StatusId.Equals(StatusConstants.Success.ToInt32()))
				{
					// Update wallet balance
					walletUser.Balance += topupRequest.Amount;
					walletUser.LastModifiedAt = DateTime.Now;
					walletUser.LastModifiedBy = request.Parameters.UserId;
					await context.SaveChangesAsync(cancellationToken);

					// Send message to transaction service
					AddTransactionMessage message = new()
					{
						UserId = (Guid)request.Parameters.UserId!,
						WalletId = walletUser.Id,
						TransactionTypeId = TransactionTypeConstants.Topup.ToInt32(),
						Amount = topupRequest.Amount
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Transaction.AddTransaction));
					await sendEndpoint.Send(message, cancellationToken);
					response.Message = "Topup request is Success";
				}
				else
				{
					response.Message = "Topup request is Failed";
				}

				await context.Database.CommitTransactionAsync(cancellationToken);
				#endregion
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
