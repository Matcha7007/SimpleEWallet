using MediatR;
using SimpleEWallet.Comon.Models;

namespace SimpleEWallet.Wallet.Features.Queries
{
	public record GetConfigByKeyQuery(string ConfigKey) : IRequest<ConfigDto>;
}
