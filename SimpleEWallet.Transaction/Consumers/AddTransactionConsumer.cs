using MassTransit;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Transaction.Domain;
using SimpleEWallet.Transaction.Persistence;

namespace SimpleEWallet.Transaction.Consumers
{
	public class AddTransactionConsumer(TransactionDbContext _dbContext, ISendEndpointProvider send) : IConsumer<AddTransactionMessage>
	{
		public async Task Consume(ConsumeContext<AddTransactionMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				AddTransactionMessage data = context.Message;

				TrnTransaction transaction = new()
				{
					WalletId = data.WalletId,
					TransactionTypeId = data.TransactionTypeId,
					Amount = data.Amount,
					Reference = data.Reference,
					IsActive = true,
					CreatedBy = data.UserId,
					CreatedAt = DateTime.Now,
					LastModifiedBy = data.UserId,
					LastModifiedAt = DateTime.Now
				};

				await _dbContext.TrnTransactions.AddAsync(transaction);
				await _dbContext.SaveChangesAsync();

				UpdateTransactionIdMessage message = new()
				{
					UserId = data.UserId,
					TransactionRequestId = data.TransactionRequestId,
					TransactionId = transaction.Id,
					TransactionTypeId = data.TransactionTypeId
				};
				ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Wallet.UpdateTransactionId));
				await sendEndpoint.Send(message);

				await _dbContext.Database.CommitTransactionAsync();
			}
			catch
			{
				await _dbContext.Database.RollbackTransactionAsync();
			}
		}
	}
}
