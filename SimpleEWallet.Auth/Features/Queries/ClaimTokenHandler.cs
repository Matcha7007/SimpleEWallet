using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Helpers;
using SimpleEWallet.Auth.Tools;

namespace SimpleEWallet.Auth.Features.Queries
{
	public class ClaimTokenHandler : IRequestHandler<ClaimTokenQuery, ClaimTokenResponse?>
	{
		public Task<ClaimTokenResponse?> Handle(ClaimTokenQuery request, CancellationToken cancellationToken)
		{
			ClaimTokenResponse response = new();
			try
			{
				string? token = HttpHelper.ExtractBearerToken(request.ControllerBase);
				if (string.IsNullOrEmpty(token))
				{
					return Task.FromResult<ClaimTokenResponse?>(response.SetUnauthorized());
				}
				Guid? userId = BearerTokenTool.GetUserId(token!);
				bool isValid = BearerTokenTool.ValidateBearerToken(token!);
				if (userId == null || !isValid)
				{					
					return Task.FromResult<ClaimTokenResponse?>(response.SetUnauthorized());
				}
				response.Data = new()
				{
					UserId = (Guid)userId,
					Token = token!,
					IsTokenValid = isValid
				};
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return Task.FromResult<ClaimTokenResponse?>(response);
		}
	}
}
