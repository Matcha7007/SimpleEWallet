using MediatR;

using SimpleEWallet.Comon.Models.Wallet;

namespace SimpleEWallet.Wallet.Features.Commands
{
	public record TransferRequestCommand(TransferRequestParameters Parameters, string Token) : IRequest<TopupTransferRequestResponse>;
}
