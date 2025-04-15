using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Queries
{
	public record VerifyPinQuery(VerifyPinParameters Parameters, string Token) : IRequest<VerifyPinResponse>;
}
