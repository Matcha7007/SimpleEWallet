using MediatR;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Wallet.Persistence;

namespace SimpleEWallet.Wallet.Features.Queries
{
	public class GetConfigByKeyValueHandler(WalletDbContext context) : IRequestHandler<GetConfigByKeyValueQuery, string?>
	{
		public async Task<string?> Handle(GetConfigByKeyValueQuery request, CancellationToken cancellationToken)
		{
			string? config = await context.MstConfigs
				.Where(x => x.ConfigKey.ToLower() == request.ConfigKey.ToLower() && x.IsActive == true)
				.Select(x => x.ConfigValue)
				.SingleOrDefaultAsync(cancellationToken: cancellationToken);

			return config is null ? null : config;
		}
	}
}
