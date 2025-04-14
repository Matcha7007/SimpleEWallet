using MediatR;

using Microsoft.AspNetCore.Mvc;
using SimpleEWallet.Comon.Base.Controller;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Models.Auth;

namespace QuickAcq.Svc.Auth.Controllers
{
	[ApiController]
    [Route("api/v1/[controller]")]
    public class WalletController(IMediator _mediator) : BaseAPIController
    {
		[HttpGet("is-online")]
		public string IsOnline() => this.IsOnlineMessage();

		[HttpPost("get-by-id")]
		public async Task<IActionResult> GetById([FromBody] GetUserByIdParameters parameters)
		{
			GetUserByIdResponse response = new();
			try
			{
				//ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				//_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new GetUserByIdQuery(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return this.ResponseToActionResult(response);
		}
	}
}
