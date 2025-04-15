using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.Enumerations;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Domain;
using SimpleEWallet.Wallet.Persistence;

namespace SimpleEWallet.Wallet.Consumers
{
	public class UpdateTransactionIdConsumer(WalletDbContext _dbContext) : IConsumer<UpdateTransactionIdMessage>
	{
		public async Task Consume(ConsumeContext<UpdateTransactionIdMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				UpdateTransactionIdMessage data = context.Message;

				if (data.TransactionTypeId == TransactionTypeConstants.Topup.ToInt32())
				{
					TrnTopupRequest? dataTopup = await _dbContext.TrnTopupRequests
						.Where(x => x.Id == data.TransactionRequestId && x.IsActive == true)
						.FirstOrDefaultAsync();

					if (dataTopup != null)
					{
						dataTopup.TransactionId = data.TransactionId;
						dataTopup.LastModifiedAt = DateTime.Now;
						dataTopup.LastModifiedBy = data.UserId;
					}
				}
				else
				{
					TrnTransfer? dataTransfer = await _dbContext.TrnTransfers
						.Where(x => x.Id == data.TransactionRequestId && x.IsActive == true)
						.FirstOrDefaultAsync();

					if (dataTransfer != null)
					{
						dataTransfer.TransactionId = data.TransactionId;
						dataTransfer.LastModifiedAt = DateTime.Now;
						dataTransfer.LastModifiedBy = data.UserId;
					}
				}				
			}
			catch
			{
				await _dbContext.Database.RollbackTransactionAsync();
			}
		}
	}
}
