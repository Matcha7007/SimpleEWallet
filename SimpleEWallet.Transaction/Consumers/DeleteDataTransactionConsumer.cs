using MassTransit;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Transaction.Domain;
using SimpleEWallet.Transaction.Persistence;

namespace SimpleEWallet.Transaction.Consumers
{
	public class DeleteDataTransactionConsumer(TransactionDbContext _dbContext) : IConsumer<DeleteDataTransactionMessage>
	{
		public async Task Consume(ConsumeContext<DeleteDataTransactionMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				DeleteDataTransactionMessage data = context.Message;

				TrnTransaction? transaction = await _dbContext.TrnTransactions
					.Where(x => x.WalletId == data.WalletId && x.IsActive == true)					
					.FirstOrDefaultAsync();

				if (transaction != null)
				{
					transaction.IsActive = false;
					transaction.LastModifiedAt = DateTime.UtcNow;
					transaction.LastModifiedBy = data.UserId;
					
					await _dbContext.SaveChangesAsync();
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
