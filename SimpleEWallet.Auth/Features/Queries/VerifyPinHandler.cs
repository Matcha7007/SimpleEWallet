using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Auth.Tools;
using SimpleEWallet.Comon.Helpers;

namespace SimpleEWallet.Auth.Features.Queries
{
	public class VerifyPinHandler(AuthDbContext context) : IRequestHandler<VerifyPinQuery, VerifyPinResponse?>
	{
		public async Task<VerifyPinResponse?> Handle(VerifyPinQuery request, CancellationToken cancellationToken)
		{
			VerifyPinResponse response = new();
			try
			{
				MstUser? user = await context.MstUsers
					.Where(u => u.Id == request.Parameters.UserId)
					.FirstOrDefaultAsync(cancellationToken);

				if (user == null || !HashTool.Verify(request.Parameters.Pin, user.PinHash!))
				{
					response.SetValidationMessage("PIN is incorrect");
					return response;
				}

				response.Result = AESEncryptionTool.Encrypt(request.Token, "true");
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
			}
			return response;
		}
	}
}
