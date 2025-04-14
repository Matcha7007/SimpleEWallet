using MediatR;

namespace SimpleEWallet.Wallet.Features.Queries
{
	public record GetConfigByKeyIdQuery(string ConfigKey) : IRequest<int>;
}
