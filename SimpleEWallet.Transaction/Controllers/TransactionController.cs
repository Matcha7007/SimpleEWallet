using MediatR;

using Microsoft.AspNetCore.Mvc;

using SimpleEWallet.Comon.Base.Controller;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Wallet.Controllers
{
	[ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionController(IMediator _mediator) : BaseAPIController
    {
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();
	}
}
