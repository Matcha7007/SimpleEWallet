using MediatR;

using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Comon.Models;
using SimpleEWallet.Transaction.Domain;
using SimpleEWallet.Transaction.Persistence;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public class GetConfigByKeyHandler(TransactionDbContext context) : IRequestHandler<GetConfigByKeyQuery, ConfigDto?>
	{
		public async Task<ConfigDto?> Handle(GetConfigByKeyQuery request, CancellationToken cancellationToken)
		{
			MstConfig? config = await context.MstConfigs
				.Where(x => x.ConfigKey.ToLower() == request.ConfigKey.ToLower() && x.IsActive == true)
				.SingleOrDefaultAsync(cancellationToken: cancellationToken);

			return config is null ? null : new ConfigDto(config.Id, config.ConfigKey, config.ConfigValue!, config.IsActive == true);
		}
	}
}
