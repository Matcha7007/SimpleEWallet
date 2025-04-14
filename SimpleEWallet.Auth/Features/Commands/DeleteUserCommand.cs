using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Commands
{
	public record DeleteUserCommand(GetUserByIdParameters Parameters) : IRequest<UserResponse>;
}
