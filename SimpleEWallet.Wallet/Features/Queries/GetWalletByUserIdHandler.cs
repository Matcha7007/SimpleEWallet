using MediatR;

using SimpleEWallet.Comon.Base.WebAPI;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SimpleEWallet.Wallet.Persistence;
using SimpleEWallet.Comon.Models.Wallet;
using SimpleEWallet.Wallet.Domain;

namespace SimpleEWallet.Wallet.Features.Queries
{
	public class GetWalletByUserIdHandler(WalletDbContext context, IMapper mapper) : IRequestHandler<GetWalletByUserIdQuery, GetWalletByUserIdResponse?>
	{
		public async Task<GetWalletByUserIdResponse?> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
		{
			GetWalletByUserIdResponse response = new();
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

				#region Check Wallet User
				MstWallet? walletUser = await context.MstWallets
					.Where(x => x.UserId == request.Parameters.UserId && x.IsActive == true)
					.Include(x => x.TrnTopupRequests)
					.Include(x => x.TrnTransferReceiverWallets)
					.Include(x => x.TrnTransferSenderWallets)
					.FirstOrDefaultAsync(cancellationToken);

				if (walletUser == null)
				{
					response.Data = null!;
					response.SetValidationMessage("Wallet not found");
					return response;
				}
				#endregion

				#region Mapping Wallet
				response.Data = mapper.Map<WalletDto>(walletUser);
				response.Message = "Get Walet Data by User Id";
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
