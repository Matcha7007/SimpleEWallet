using Microsoft.EntityFrameworkCore;

using SimpleEWallet.Comon.Extensions;

namespace SimpleEWallet.Comon.Base.WebAPI
{
	/// <summary>
	/// Function.
	/// </summary>
	public class BaseFuncListResult<ListDataType> : BaseFuncResult<List<ListDataType>>
	{
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public int TotalRecords { get; set; }
		/// <summary>
		/// Constructor.
		/// </summary>
		public BaseFuncListResult()
			: base()
		{
			ListData = [];
		}
	}
	/// <summary>
	/// Extension.
	/// </summary>
	public static class BaseFuncListResult_Extensions
	{
		/// <summary>
		/// Function.
		/// </summary>
		public static async Task<BaseFuncListResult<T>> ToListWithSortingAndPagingAsync<T>(this IQueryable<T> query, BaseListParameters listParameters)
		{
			BaseFuncListResult<T> result = new()
			{
				TotalRecords = await query.CountAsync()
			};

			if (listParameters.OrderBy.IsNotNullOrWhiteSpace())
			{
				query = query.Sorting(listParameters).CopyTo(result).ListData;
			}

			if (listParameters.PageNumber > 0 && listParameters.PageSize > 0)
			{
				query = query.Paging(listParameters).CopyTo(result).ListData;
			}
			result.ListData = await query.ToListAsync();
			return result;
		}


		public static async Task<BaseFuncListResult<T>> ToListWithSortingAndPaging<T>(this IQueryable<T> query, BaseListParameters listParameters)
		{
			BaseFuncListResult<T> result = new BaseFuncListResult<T>();
			result.TotalRecords = query.Count();

			if (listParameters.OrderBy.IsNotNullOrWhiteSpace())
			{
				query = query.Sorting(listParameters).CopyTo(result).ListData;
			}

			if (listParameters.PageNumber > 0 && listParameters.PageSize > 0)
			{
				query = query.Paging(listParameters).CopyTo(result).ListData;
			}
			result.ListData = query.ToList();
			return result;
		}
	}
}
