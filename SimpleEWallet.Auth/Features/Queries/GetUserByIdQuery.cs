using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Queries
{
	public record GetUserByIdQuery(GetUserByIdParameters Parameters) : IRequest<GetUserByIdResponse>;
}
