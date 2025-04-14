using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Queries
{
	public record ClaimTokenQuery(ControllerBase ControllerBase) : IRequest<ClaimTokenResponse>;
}
