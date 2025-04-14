using MassTransit;

using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.MQ;
using SimpleEWallet.Wallet.Domain;
using SimpleEWallet.Wallet.Persistence;

namespace SimpleEWallet.Wallet.Consumers
{
	public class InitializeWalletConsumer(WalletDbContext _dbContext) : IConsumer<InitializeWalletMessage>
	{
		public async Task Consume(ConsumeContext<InitializeWalletMessage> context)
		{
			try
			{
				await _dbContext.Database.BeginTransactionAsync();

				InitializeWalletMessage data = context.Message;

				MstWallet? existingWallet = await _dbContext.MstWallets
					.FirstOrDefaultAsync(x => x.UserId == data.UserId && x.IsActive == true);

				if (existingWallet == null)
				{
					MstWallet wallet = new()
					{
						UserId = data.UserId,
						WalletNumber = data.WalletNumber,
						WalletName = data.WalletName,
						Balance = 0,
						IsActive = true,
						CreatedAt = DateTime.UtcNow,
						CreatedBy = data.UserId,
						LastModifiedAt = DateTime.UtcNow,
						LastModifiedBy = data.UserId
					};

					_dbContext.MstWallets.Add(wallet);
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
