using MediatR;

using SimpleEWallet.Comon.Models.Wallet;

namespace SimpleEWallet.Wallet.Features.Commands
{
	public record TopupRequestCommand(TopupRequestParameters Parameters) : IRequest<TopupTransferRequestResponse>;
}
