using MediatR;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public record GetConfigByKeyIdQuery(string ConfigKey) : IRequest<int>;
}
