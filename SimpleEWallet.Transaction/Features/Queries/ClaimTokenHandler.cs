using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Enumerations;
using SimpleEWallet.Comon.Helpers;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public class ClaimTokenHandler(IMediator _mediator) : IRequestHandler<ClaimTokenQuery, ClaimTokenResponse?>
	{
		public async Task<ClaimTokenResponse?> Handle(ClaimTokenQuery request, CancellationToken cancellationToken)
		{
			ClaimTokenResponse response = new();
			try
			{
				string? bearerToken = HttpHelper.ExtractBearerToken(request.ControllerBase);
				if (!string.IsNullOrEmpty(bearerToken))
				{
					string url = await _mediator.Send(new GetConfigByKeyValueQuery(GlobalConstant.Transaction.UrlClaimToken), cancellationToken);
					response = await HttpHelper.PostJsonAsync<ClaimTokenResponse>(bearerToken, url);
				}
				else
				{
					response.SetUnauthorized();
				}
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return response;
		}
	}
}
