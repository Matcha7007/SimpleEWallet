using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public record ClaimTokenQuery(ControllerBase ControllerBase) : IRequest<ClaimTokenResponse>;
}
