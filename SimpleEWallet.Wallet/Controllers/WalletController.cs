using MediatR;

using Microsoft.AspNetCore.Mvc;

using SimpleEWallet.Comon.Base.Controller;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Models.Wallet;
using SimpleEWallet.Wallet.Features.Queries;

namespace SimpleEWallet.Wallet.Controllers
{
	[ApiController]
    [Route("api/v1/[controller]")]
    public class WalletController(IMediator _mediator) : BaseAPIController
    {
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();

		[HttpPost("get-by-user-id")]
		public async Task<IActionResult> GetByUserId([FromBody] GetWalletByUserIdParameters parameters)
		{
			GetWalletByUserIdResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new GetWalletByUserIdQuery(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}
	}
}
