using MediatR;

using Microsoft.EntityFrameworkCore;

using SimpleEWallet.Transaction.Persistence;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public class GetConfigByKeyIdHandler(TransactionDbContext context) : IRequestHandler<GetConfigByKeyIdQuery, int>
	{
		public async Task<int> Handle(GetConfigByKeyIdQuery request, CancellationToken cancellationToken)
		{
			int config = await context.MstConfigs
				.Where(x => x.ConfigKey.ToLower() == request.ConfigKey.ToLower() && x.IsActive == true)
				.Select(x => int.Parse(x.ConfigValue!))
				.SingleOrDefaultAsync(cancellationToken: cancellationToken);

			return config;
		}
	}
}
