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
    public class UserController(IMediator _mediator) : BaseAPIController
    {
		[HttpGet("is-online")]
		public string IsOnline() => IsOnlineMessage();

		[HttpPost("get-by-id")]
		public async Task<IActionResult> GetById([FromBody] GetUserByIdParameters parameters)
		{
			GetUserByIdResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new GetUserByIdQuery(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("get-self-data")]
		public async Task<IActionResult> GetSelf()
		{
			GetUserByIdResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				GetUserByIdParameters parameters = new() { UserId = claimTokenResponse.Data.UserId };
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new GetUserByIdQuery(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create([FromBody] UserParameters parameters)
		{
			UserResponse response = new();
			try
			{
				response = await _mediator.Send(new CreateUserCommand(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("update")]
		public async Task<IActionResult> Update([FromBody] UserParameters parameters)
		{
			UserResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new UpdateUserCommand(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("delete")]
		public async Task<IActionResult> Delete([FromBody] GetUserByIdParameters parameters)
		{
			UserResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new DeleteUserCommand(parameters));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}

		[HttpPost("search-receiver")]
		public async Task<IActionResult> SearchReceiver([FromBody] UserReceiverSearchParameters parameters)
		{
			UserReceiverSearchResponse response = new();
			try
			{
				ClaimTokenResponse claimTokenResponse = await _mediator.Send(new ClaimTokenQuery(this));
				_ = !claimTokenResponse.IsValid ? response.SetUnauthorized() : response = await _mediator.Send(new SearchUserReceiverQuery(parameters, claimTokenResponse.Data.UserId));
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return ResponseToActionResult(response);
		}
	}
}
