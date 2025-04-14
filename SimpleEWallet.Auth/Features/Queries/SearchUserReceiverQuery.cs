using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Queries
{
	public record SearchUserReceiverQuery(UserReceiverSearchParameters Parameters, Guid? UserSenderId) : IRequest<UserReceiverSearchResponse>;
}
