using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Domain;
using SimpleEWallet.Wallet.Persistence;

namespace SimpleEWallet.Wallet.Consumers
{
	public class DeleteDataWalletConsumer(WalletDbContext _dbContext, ISendEndpointProvider send) : IConsumer<DeleteDataWalletMessage>
	{
		public async Task Consume(ConsumeContext<DeleteDataWalletMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				DeleteDataWalletMessage data = context.Message;

				MstWallet? wallet = await _dbContext.MstWallets
					.Where(x => x.UserId == data.UserId && x.IsActive == true)
					.Include(x => x.TrnTransferSenderWallets)
					.Include(x => x.TrnTransferReceiverWallets)
					.Include(x => x.TrnTopupRequests)
					.FirstOrDefaultAsync();

				if (wallet != null)
				{
					wallet.IsActive = false;
					wallet.LastModifiedAt = DateTime.UtcNow;
					wallet.LastModifiedBy = data.UserId;
					wallet.TrnTransferSenderWallets.ToList().ForEach(x =>
					{
						x.IsActive = false;
						x.LastModifiedAt = DateTime.UtcNow;
						x.LastModifiedBy = data.UserId;
					});
					wallet.TrnTransferReceiverWallets.ToList().ForEach(x =>
					{
						x.IsActive = false;
						x.LastModifiedAt = DateTime.UtcNow;
						x.LastModifiedBy = data.UserId;
					});
					wallet.TrnTopupRequests.ToList().ForEach(x =>
					{
						x.IsActive = false;
						x.LastModifiedAt = DateTime.UtcNow;
						x.LastModifiedBy = data.UserId;
					});
					await _dbContext.SaveChangesAsync();

					DeleteDataTransactionMessage message = new()
					{
						UserId = data.UserId,
						WalletId = wallet.Id
					};
					ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Transaction.DeleteDataTransaction));
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
