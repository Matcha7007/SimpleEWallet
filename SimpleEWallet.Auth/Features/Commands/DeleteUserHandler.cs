using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;

namespace SimpleEWallet.Auth.Features.Commands
{
	public class DeleteUserHandler(AuthDbContext context) : IRequestHandler<DeleteUserCommand, UserResponse?>
	{
		public async Task<UserResponse?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
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
				if (request.Parameters.UserId == Guid.Empty)
				{
					response.SetValidationMessage("User Id is null or empty");
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

				#region Delete User
				user.IsActive = false;
				user.LastModifiedAt = DateTime.UtcNow;
				user.LastModifiedBy = request.Parameters.UserId;
				await context.SaveChangesAsync(cancellationToken);
				response.Message = "User has been deleted";
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
