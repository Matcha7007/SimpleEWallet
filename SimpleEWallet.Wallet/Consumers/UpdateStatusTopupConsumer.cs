using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.Enumerations;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Domain;
using SimpleEWallet.Wallet.Persistence;

namespace SimpleEWallet.Wallet.Consumers
{
	public class UpdateStatusTopupConsumer(WalletDbContext _dbContext, ISendEndpointProvider send) : IConsumer<UpdateStatusTopupMessage>
	{
		public async Task Consume(ConsumeContext<UpdateStatusTopupMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				UpdateStatusTopupMessage data = context.Message;

				TrnTopupRequest? dataTopup = await _dbContext.TrnTopupRequests
					.Where(x => x.Id == data.TopupRequestId && x.IsActive == true)
					.Include(x => x.Wallet)
					.FirstOrDefaultAsync();

				if (dataTopup == null)
				{
					await Task.Delay(TimeSpan.FromMinutes(1));
					dataTopup!.StatusId = StatusConstants.Success.ToInt32();
					dataTopup.LastModifiedAt = DateTime.Now;
					dataTopup.LastModifiedBy = data.UserId;
					dataTopup.Wallet.Balance += dataTopup.Amount;
					dataTopup.Wallet.LastModifiedAt = DateTime.Now;
					dataTopup.Wallet.LastModifiedBy = data.UserId;
					await _dbContext.SaveChangesAsync();

					// Send message to transaction service
					AddTransactionMessage message = new()
					{
						UserId = data.UserId,
						WalletId = dataTopup.WalletId,
						TransactionTypeId = TransactionTypeConstants.Topup.ToInt32(),
						Amount = dataTopup.Amount,
						TransactionRequestId = dataTopup.Id
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Transaction.AddTransaction));
					await sendEndpoint.Send(message);

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
