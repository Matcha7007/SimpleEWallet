using MediatR;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public record GetConfigByKeyValueQuery(string ConfigKey) : IRequest<string>;
}
