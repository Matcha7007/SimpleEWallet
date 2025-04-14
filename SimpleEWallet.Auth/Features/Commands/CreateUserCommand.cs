using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Commands
{
	public record CreateUserCommand(UserParameters Parameters) : IRequest<UserResponse>;
}
