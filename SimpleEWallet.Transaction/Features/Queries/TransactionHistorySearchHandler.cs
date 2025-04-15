using MediatR;

using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Models.Transaction;
using SimpleEWallet.Transaction.Domain;
using SimpleEWallet.Transaction.Persistence;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace SimpleEWallet.Transaction.Features.Queries
{
	public class TransactionHistorySearchHandler(TransactionDbContext context, IMapper mapper) : IRequestHandler<TransactionHistorySearchQuery, TransactionHistorySearchResponse?>
	{
		public async Task<TransactionHistorySearchResponse?> Handle(TransactionHistorySearchQuery request, CancellationToken cancellationToken)
		{
			TransactionHistorySearchResponse response = new();
			try
			{
				IQueryable<VwTransactionListDatum> query = context.VwTransactionListData
					.Where(x => x.WalletId == request.Parameters.Filter.WalletId)
					.AsNoTracking();

				#region Filtering
				DateOnly? periodFrom = request.Parameters.Filter.FilterPeriodFrom;
				DateOnly? periodTo = request.Parameters.Filter.FilterPeriodTo;

				// Convert DateOnly? to DateTime? for comparison
				DateTime? periodFromDateTime = periodFrom?.ToDateTime(TimeOnly.MinValue);
				DateTime? periodToDateTime = periodTo?.ToDateTime(TimeOnly.MaxValue);

				query = query
					.Where(x => x.TransactionDate >= periodFromDateTime && x.TransactionDate <= periodToDateTime);
				#endregion

				#region Sorting Paging
				BaseFuncListResult<VwTransactionListDatum> listData = await query.ToListWithSortingAndPagingAsync(request!.Parameters);
				response.Data.TotalRecords = listData.TotalRecords;
				response.Data.ListData = mapper.Map<List<TransactionListItemDto>>(listData.ListData);
				response.Data.FillCurrentPageAndPageSize(request.Parameters).CalculatePages();
				response.Message = "Search Transaction History";
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
