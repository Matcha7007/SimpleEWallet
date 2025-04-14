using MediatR;

namespace SimpleEWallet.Wallet.Features.Queries
{
	public record GetConfigByKeyValueQuery(string ConfigKey) : IRequest<string>;
}
