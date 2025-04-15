using MediatR;

using Microsoft.AspNetCore.Mvc;

using SimpleEWallet.Comon.Base.Controller;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Models.Transaction;
using SimpleEWallet.Transaction.Features.Queries;

namespace SimpleEWallet.Transaction.Controllers
{
	[ApiController]
    [Route("api/v1/[controller]")]
    public class TransactionController(IMediator _mediator) : BaseAPIController
    {
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();


		[HttpPost("search-transaction-history")]
		public async Task<IActionResult> SearchTransactionHistory([FromBody] TransactionHistorySearchParameters parameters)
		{
			TransactionHistorySearchResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new TransactionHistorySearchQuery(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}
	}
}
