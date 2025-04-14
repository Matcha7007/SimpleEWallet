using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using SimpleEWallet.Auth.Tools;
using SimpleEWallet.Comon.MQ;
using MassTransit;

namespace SimpleEWallet.Auth.Features.Commands
{
	public class CreateUserHandler(AuthDbContext context, ISendEndpointProvider send) : IRequestHandler<CreateUserCommand, UserResponse?>
	{
		public async Task<UserResponse?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
		{
			UserResponse response = new();
			try
			{
				await context.Database.BeginTransactionAsync(cancellationToken);

				#region Validation
				if (request.Parameters == null)
				{
					response.SetValidationMessage("Parameters is null");
					return response;
				}
				if (string.IsNullOrEmpty(request.Parameters.Username))
				{
					response.SetValidationMessage("Username is null or empty");
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
				if (string.IsNullOrEmpty(request.Parameters.FullName))
				{
					request.Parameters.FullName = request.Parameters.Username;
				}
				#endregion

				#region Check User
				MstUser? user = await context.MstUsers
					.FirstOrDefaultAsync(x => x.Username.ToLower() == request.Parameters.Username.ToLower() ||
						x.Email.ToLower() == request.Parameters.Email.ToLower() ||
						x.Phone.ToLower() == request.Parameters.Phone.ToLower(), cancellationToken);
				if (user != null)
				{
					response.SetValidationMessage("Username or email or phone already exists");
					return response;
				}
				#endregion

				#region Create User
				MstUser newUser = new()
				{
					Username = request.Parameters.Username,
					FullName = request.Parameters.FullName,
					Email = request.Parameters.Email,
					Phone = request.Parameters.Phone,
					PasswordHash = HashTool.DoHash(request.Parameters.Password),
					PinHash = HashTool.DoHash(request.Parameters.Pin),
					CreatedAt = DateTime.UtcNow,
					LastModifiedAt = DateTime.UtcNow
				};
				context.MstUsers.Add(newUser);
				await context.SaveChangesAsync(cancellationToken);
				response.Message = "User has been created";
				#endregion

				#region Create Wallet
				InitializeWalletMessage walletMessage = new()
				{
					UserId = newUser.Id,
					WalletNumber = request.Parameters.Phone,
					WalletName = request.Parameters.FullName
				};
				ISendEndpoint sendEndpoint = await send.GetSendEndpoint(new Uri("queue:" + MQQueueNames.Wallet.InitializeWallet));
				await sendEndpoint.Send(walletMessage, cancellationToken);
				#endregion

				await context.Database.CommitTransactionAsync(cancellationToken);
			}
			catch (Exception ex)
			{
				response.SetErrorMessage(ex.Message);
				await context.Database.RollbackTransactionAsync(cancellationToken);
			}
			return response;
		}
	}
}
