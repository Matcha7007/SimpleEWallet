using Microsoft.IdentityModel.Tokens;
using SimpleEWallet.Comon.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleEWallet.Auth.Tools
{
	public static class JWTTokenTool
	{

		public static string GenerateJWTToken(string secretKey, string issuer, string audience, DateTime? expireDate, List<Claim> listClaim)
		{
			SymmetricSecurityKey jwtSecurityKey = new(Encoding.ASCII.GetBytes(secretKey));

			JwtSecurityTokenHandler tokenHandler = new();
			SecurityTokenDescriptor tokenDescriptor = new()
			{
				Subject = new ClaimsIdentity([.. listClaim]),
				Expires = expireDate,
				Issuer = issuer,
				Audience = audience,
				SigningCredentials = new SigningCredentials(jwtSecurityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public static bool ValidateJWTToken(string secretKey, string issuer, string audience, string token)
		{
			SymmetricSecurityKey jwtSecurityKey = new(Encoding.ASCII.GetBytes(secretKey));

			JwtSecurityTokenHandler tokenHandler = new();
			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = jwtSecurityKey
				}, out SecurityToken validatedToken);
			}
			catch
			{
				return false;
			}
			return true;
		}

		public static string GetJWTClaimData(string token, string claimType)
		{
			if (token.Trim().IsNullOrEmpty())
			{
				throw new Exception("Token cannot empty.");
			}
			JwtSecurityTokenHandler tokenHandler = new();
			JwtSecurityToken? securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken
				?? throw new Exception("An error occured when reading token.");
			string stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
			return stringClaimValue;
		}
	}
}