using MediatR;

using SimpleEWallet.Comon.Models;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public record GetConfigByKeyQuery(string ConfigKey) : IRequest<ConfigDto>;
}
