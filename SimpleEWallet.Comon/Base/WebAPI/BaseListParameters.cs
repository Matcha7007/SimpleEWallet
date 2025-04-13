using SimpleEWallet.Comon.Base.WebAPI;
using SimpleEWallet.Comon.Extensions;

namespace SimpleEWallet.Comon.Base.WebAPI
{
	/// <summary>
	/// Provides a base parameters class for a list request.
	/// </summary>
	/// <typeparam name="TFilters">The type of the filter used for a list request.</typeparam>
	public class BaseListParameters<TFilters> : BaseListParameters
        where TFilters : BaseListFilters, new()
    {
		/// <summary>
		/// The filters used for a list request.
		/// </summary>
		public TFilters Filter { get; set; }

		/// <summary>
		/// Provides a default constructor.
		/// </summary>
        public BaseListParameters()
            : base()
        {
			Filter = new TFilters();
        }
    }

	/// <summary>
	/// Provides a base parameters class for a list request.
	/// </summary>
	public class BaseListParameters : BaseMethodParameters
    {
		/// <summary>
		/// The current page number.
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		/// The size of each page. Use zero (0) if you want to return data without paging.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// The column name used for sorting data.
		/// </summary>
		public string? OrderBy { get; set; }

		/// <summary>
		/// Determines whether the data returned will be sorted ascendingly or descendingly.
		/// </summary>
		public bool IsAscending { get; set; } = true;

		/// <summary>
		/// Provides a default constructor.
		/// </summary>
		public BaseListParameters()
            : base()
        {
        }
    }

	/// <summary>
	/// Provides a set of extension methods for <see cref="BaseListParameters"/> that extend its functionality.
	/// </summary>
	public static class BaseListParameters_Extensions
    {
		/// <summary>
		/// Copy the paging data from one parameters to another parameter.
		/// </summary>
		/// <param name="source">The source of the paging data.</param>
		/// <param name="destination">The object where the paging data will be copied to</param>
		/// <returns>The source object.</returns>
		public static BaseListParameters CopyPagingTo(this BaseListParameters source, BaseListParameters destination)
		{
			destination.PageNumber = source.PageNumber;
			destination.PageSize = source.PageSize;
			return source;
		}

		/// <summary>
		/// Copy the sorting data from one parameters to another parameter.
		/// </summary>
		/// <param name="source">The source of the sorting data.</param>
		/// <param name="destination">The object where the sorting data will be copied to</param>
		/// <returns>The source object.</returns>
		public static BaseListParameters CopyOrderByTo(this BaseListParameters source, BaseListParameters destination)
		{
			destination.OrderBy = source.OrderBy;
			destination.IsAscending = source.IsAscending;
			return source;
		}

		public static BaseFuncResult<IQueryable<T>> Paging<T>(this IQueryable<T> query, BaseListParameters listParameters)
		{
			BaseFuncResult<IQueryable<T>> baseFuncResult = new BaseFuncResult<IQueryable<T>>();
			baseFuncResult.ListData = query;
			if (listParameters == null)
			{
				baseFuncResult.SetErrorMessage("Paging parameters is null.");
				return baseFuncResult;
			}

			try
			{
				baseFuncResult.ListData = baseFuncResult.ListData.Paging(listParameters.PageNumber, listParameters.PageSize);
			}
			catch (Exception errorMessage)
			{
				baseFuncResult.SetErrorMessage(errorMessage);
			}

			return baseFuncResult;
		}

		public static BaseFuncResult<IQueryable<TSource>> Sorting<TSource>(this IQueryable<TSource> query, BaseListParameters listParameters)
		{
			BaseFuncResult<IQueryable<TSource>> baseFuncResult = new()
			{
				ListData = query
			};
			if (listParameters == null)
			{
				baseFuncResult.SetErrorMessage("Sorting parameters is null.");
				return baseFuncResult;
			}

			try
			{
				baseFuncResult.ListData = baseFuncResult.ListData.Sorting(listParameters.OrderBy, listParameters.IsAscending);
			}
			catch (Exception errorMessage)
			{
				baseFuncResult.SetErrorMessage(errorMessage);
			}

			return baseFuncResult;
		}

		public static BaseFuncResult<SortingAndPagingQueryable<T>> SortingAndPaging<T>(this IQueryable<T> query, BaseListParameters listParameters)
		{
			BaseFuncResult<SortingAndPagingQueryable<T>> baseFuncResult = new BaseFuncResult<SortingAndPagingQueryable<T>>();
			IQueryable<T> listData = query.Sorting(listParameters).CopyTo(baseFuncResult).ListData;
			if (!baseFuncResult.IsSuccess)
			{
				return baseFuncResult;
			}

			IQueryable<T> listData2 = listData.Paging(listParameters).CopyTo(baseFuncResult).ListData;
			if (!baseFuncResult.IsSuccess)
			{
				return baseFuncResult;
			}

			baseFuncResult.ListData = new SortingAndPagingQueryable<T>(listData, listData2);
			return baseFuncResult;
		}
	}
}