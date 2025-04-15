using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.Enumerations;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Domain;
using SimpleEWallet.Wallet.Persistence;

namespace SimpleEWallet.Wallet.Consumers
{
	public class UpdateStatusTransferConsumer(WalletDbContext _dbContext, ISendEndpointProvider send) : IConsumer<UpdateStatusTransferMessage>
	{
		public async Task Consume(ConsumeContext<UpdateStatusTransferMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				UpdateStatusTransferMessage data = context.Message;

				TrnTransfer? dataTransfer = await _dbContext.TrnTransfers
					.Where(x => x.Id == data.TransferId && x.IsActive == true)
					.Include(x => x.SenderWallet)
					.Include(x => x.ReceiverWallet)
					.FirstOrDefaultAsync();

				if (dataTransfer == null)
				{
					await Task.Delay(TimeSpan.FromMinutes(1));
					dataTransfer!.StatusId = StatusConstants.Success.ToInt32();
					dataTransfer.LastModifiedAt = DateTime.Now;
					dataTransfer.LastModifiedBy = data.UserId;
					dataTransfer.SenderWallet.Balance -= dataTransfer.Amount;
					dataTransfer.SenderWallet.LastModifiedAt = DateTime.Now;
					dataTransfer.SenderWallet.LastModifiedBy = data.UserId;
					dataTransfer.ReceiverWallet.Balance += dataTransfer.Amount;
					dataTransfer.ReceiverWallet.LastModifiedAt = DateTime.Now;
					dataTransfer.ReceiverWallet.LastModifiedBy = data.UserId;
					await _dbContext.SaveChangesAsync();

					// Send message to transaction service
					AddTransactionMessage messageOut = new()
					{
						UserId = data.UserId,
						WalletId = dataTransfer.SenderWalletId,
						TransactionTypeId = TransactionTypeConstants.TransferOut.ToInt32(),
						Amount = dataTransfer.Amount,
						Reference = dataTransfer.Description
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Transaction.AddTransaction));
					await sendEndpoint.Send(messageOut);

					AddTransactionMessage messageIn = new()
					{
						UserId = data.UserId,
						WalletId = dataTransfer.ReceiverWalletId,
						TransactionTypeId = TransactionTypeConstants.TransferIn.ToInt32(),
						Amount = dataTransfer.Amount,
						Reference = dataTransfer.Description
					};

					await sendEndpoint.Send(messageIn);

					await _dbContext.Database.CommitTransactionAsync();
				}
			}
			catch
			{
				await _dbContext.Database.RollbackTransactionAsync();
			}
		}
	}
}
