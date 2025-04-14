using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Auth.Tools;

namespace SimpleEWallet.Auth.Features.Commands
{
	public class UpdateUserHandler(AuthDbContext context) : IRequestHandler<UpdateUserCommand, UserResponse?>
	{
		public async Task<UserResponse?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			UserResponse response = new();
			try
			{
				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Password))
				{
					response.SetValidationMessage("Password is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Email))
				{
					response.SetValidationMessage("Email is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Phone))
				{
					response.SetValidationMessage("Phone is null or empty");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Pin))
				{
					response.SetValidationMessage("PIN is null or empty");
					return response;
				}
				if (request.Parameters.Pin.Length != 6)
				{
					response.SetValidationMessage("PIN must be 6 digit");
					return response;
				}
				#endregion

				#region Check User
				MstUser? user = await context.MstUsers
					.FirstOrDefaultAsync(x => x.Id == request.Parameters.UserId && x.IsActive == true, cancellationToken);
				if (user == null)
				{
					response.SetValidationMessage("User not found");
					return response;
				}
				#endregion

				#region Update User
				if (!string.IsNullOrEmpty(request.Parameters.FullName))
				{
					user.FullName = request.Parameters.FullName!;
				}
				user.Email = request.Parameters.Email;
				user.Phone = request.Parameters.Phone;
				user.PasswordHash = HashTool.DoHash(request.Parameters.Password);
				user.PinHash = HashTool.DoHash(request.Parameters.Pin);
				user.LastModifiedAt = DateTime.Now;
				user.LastModifiedBy = request.Parameters.UserId;
				await context.SaveChangesAsync(cancellationToken);
				response.Message = "User has been updated";
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
