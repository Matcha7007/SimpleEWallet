using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Commands
{
	public record LoginCommand(LoginParameters Parameters) : IRequest<LoginResponse>;
}
