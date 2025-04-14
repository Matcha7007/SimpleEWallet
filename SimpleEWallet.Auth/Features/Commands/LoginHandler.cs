using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Auth.Tools;

namespace SimpleEWallet.Auth.Features.Commands
{
	public class LoginHandler(AuthDbContext context) : IRequestHandler<LoginCommand, LoginResponse?>
	{
		public async Task<LoginResponse?> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			LoginResponse response = new();
			try
			{
				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Phone))
				{
					response.SetValidationMessage("Phone is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Password))
				{
					response.SetValidationMessage("Password is null or empty");
					return response;
				}				
				#endregion

				#region Check User
				MstUser? user = await context.MstUsers
					.FirstOrDefaultAsync(x => x.Phone == request.Parameters.Phone && x.IsActive == true, cancellationToken);
				if (user == null || !HashTool.Verify(request.Parameters.Password, user.PasswordHash))
				{
					response.SetValidationMessage("Phone or Password is incorrect");
					return response;
				}
				#endregion

				#region Generate Bearer Token
				response.BearerToken = BearerTokenTool.GenerateBearerToken(user.Id);
				response.Message = "Login success";
				#endregion
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return response;
		}
	}
}
