using SimpleEWallet.Comon.Extensions;
using System.Security.Claims;

namespace SimpleEWallet.Auth.Tools
{
	public static class BearerTokenTool
	{
		public static string GenerateBearerToken(Guid userId)
		{
			List<Claim> listClaim = [];
			listClaim.Add(new Claim("UserId", userId.ToString()));

			return JWTTokenTool.GenerateJWTToken(
				"Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx"
				, "JWTAuthenticationSimpleEWallet"
				, "JWTServiceSimpleEWalletClient"
				, 5 > 0 ? DateTime.Now.AddMinutes(5) : null
				, listClaim
			);
		}

		public static bool ValidateBearerToken(string token)
		{
			return JWTTokenTool.ValidateJWTToken(
				"Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx"
				, "JWTAuthenticationSimpleEWallet"
				, "JWTServiceSimpleEWalletClient"
				, token);
		}

		public static Guid? GetUserId(string token)
		{
			string strUsedId = JWTTokenTool.GetJWTClaimData(token, "UserId");
			Guid? result = Guid.TryParse(strUsedId, out Guid parseResult)
				? parseResult
				: throw new FormatException("Error when getting user id from token.");
			return result;
		}

		public static string GetTokenValue(string fullBearerToken)
		{
			string token = string.Empty;
			if (fullBearerToken.IsNotNullOrWhiteSpace())
			{
				if (fullBearerToken.StartsWith("BEARER ", StringComparison.OrdinalIgnoreCase))
				{
					token = fullBearerToken.Substring(7);
				}
			}
			return token;
		}
	}
}