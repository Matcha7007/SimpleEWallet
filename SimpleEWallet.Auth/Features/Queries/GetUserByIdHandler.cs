﻿using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace SimpleEWallet.Auth.Features.Queries
{
	public class GetUserByIdHandler(AuthDbContext context, IMapper mapper) : IRequestHandler<GetUserByIdQuery, GetUserByIdResponse?>
	{
		public async Task<GetUserByIdResponse?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			GetUserByIdResponse response = new();
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
					response.Data = null!;
					response.SetValidationMessage("User not found");
					return response;
				}
				#endregion

				#region Mapping User
				response.Data = mapper.Map<UserDto>(user);
				response.Message = "Get User Data by Id";
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
