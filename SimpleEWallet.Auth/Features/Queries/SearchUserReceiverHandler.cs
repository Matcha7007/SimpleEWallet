using MediatR;

using SimpleEWallet.Comon.Models.Auth;
using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Auth.Persistence;
using SimpleEWallet.Auth.Domain;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace SimpleEWallet.Auth.Features.Queries
{
	public class SearchUserReceiverHandler(AuthDbContext context, IMapper mapper) : IRequestHandler<SearchUserReceiverQuery, UserReceiverSearchResponse?>
	{
		public async Task<UserReceiverSearchResponse?> Handle(SearchUserReceiverQuery request, CancellationToken cancellationToken)
		{
			UserReceiverSearchResponse response = new();
			try
			{
				IQueryable<MstUser> query = context.MstUsers
					.AsNoTracking()
					.Where(x => x.IsActive == true && x.Id != request.UserSenderId);

				#region Filtering
				string? filterKeyword = request?.Parameters.Filter?.Keyword?.ToLower();
				if (!string.IsNullOrEmpty(filterKeyword))
				{
					query = query
						.Where(row => 
							row.Username!.ToLower().Contains(filterKeyword) ||
							row.FullName!.ToLower().Contains(filterKeyword) ||
							row.Email!.ToLower().Contains(filterKeyword) ||
							row.Phone!.ToLower().Contains(filterKeyword)
						);
				}
				#endregion

				#region Sorting Paging
				BaseFuncListResult<MstUser> listData = await query.ToListWithSortingAndPagingAsync(request!.Parameters);
				response.Data.TotalRecords = listData.TotalRecords;
				response.Data.ListData = mapper.Map<List<UserReceiverDto>>(listData.ListData);
				response.Data.FillCurrentPageAndPageSize(request.Parameters).CalculatePages();
				response.Message = "Search User Receiver";
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
