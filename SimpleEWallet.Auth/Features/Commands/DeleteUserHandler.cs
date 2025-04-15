using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using MassTransit;
using SimpleEWallet.Comon.MQ;

namespace SimpleEWallet.Auth.Features.Commands
{
	public class DeleteUserHandler(AuthDbContext context, ISendEndpointProvider send) : IRequestHandler<DeleteUserCommand, UserResponse?>
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
				user.LastModifiedAt = DateTime.Now;
				user.LastModifiedBy = request.Parameters.UserId;
				await context.SaveChangesAsync(cancellationToken);

				DeleteDataWalletMessage message = new()
				{
					UserId = request.Parameters.UserId
				};
				ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Wallet.DeleteDataWallet));
				await sendEndpoint.Send(message, cancellationToken);

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
