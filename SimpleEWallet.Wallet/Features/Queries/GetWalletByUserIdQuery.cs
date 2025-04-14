using MediatR;

using SimpleEWallet.Comon.Models.Wallet;

namespace SimpleEWallet.Wallet.Features.Queries
{
	public record GetWalletByUserIdQuery(GetWalletByUserIdParameters Parameters) : IRequest<GetWalletByUserIdResponse>;
}
