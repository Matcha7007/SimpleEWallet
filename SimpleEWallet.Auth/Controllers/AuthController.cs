using MediatR;

using Microsoft.AspNetCore.Mvc;

using SimpleEWallet.Auth.Features.Commands;
using SimpleEWallet.Auth.Features.Queries;
using SimpleEWallet.Comon.Base.Controller;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Controllers
{
	[ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController(IMediator _mediator) : BaseAPIController
    {
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginParameters parameters)
		{
			LoginResponse response = new();
			try
			{
				response = await _mediator.Send(new LoginCommand(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("claim-token")]
		public async Task<IActionResult> ClaimToken()
		{
			ClaimTokenResponse response = new();
			try
			{
				response = await _mediator.Send(new ClaimTokenQuery(this));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}
	}
}
