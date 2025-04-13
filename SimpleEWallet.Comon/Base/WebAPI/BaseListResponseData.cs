namespace SimpleEWallet.Comon.Base.WebAPI
{
	/// <summary>
	/// Provides a base response data class for a list request response.
	/// </summary>
	/// <typeparam name="DataType">The type of the data returned by the request.</typeparam>
	public class BaseListResponseData<DataType> : BaseResponseData
	{
		/// <summary>
		/// The current page number.
		/// </summary>
		public int PageNumber { get; set; }

		/// <summary>
		/// The current page size.
		/// </summary>
		public int PageSize { get; set; }

		/// <summary>
		/// The number of total pages for all the data.
		/// </summary>
		public int TotalPages { get; set; }

		/// <summary>
		/// The number of total records.
		/// </summary>
		public int TotalRecords { get; set; }

		/// <summary>
		/// The list of the data returned.
		/// </summary>
		public List<DataType> ListData { get; set; }

		/// <summary>
		/// Provides a default constructor.
		/// </summary>
		public BaseListResponseData()
			: base()
		{
			ListData = new List<DataType>();
		}

		/// <summary>
		/// Calculate TotalPages based on TotalRecords.
		/// </summary>
		public BaseListResponseData<DataType> CalculatePages()
		{
			if (PageSize == 0)
			{
				TotalPages = 1;
			}
			else
			{
				if (TotalRecords == 0)
				{
					TotalPages = 1;
				}
				else
				{
					int remain = TotalRecords % PageSize;
					TotalPages = remain == 0 ? TotalRecords / PageSize : (TotalRecords - remain) / PageSize + 1;
				}
			}
			return this;
		}

		public BaseListResponseData<DataType> FillCurrentPageAndPageSize(BaseListParameters parameters)
		{
			PageNumber = parameters.PageNumber;
			PageSize = parameters.PageSize;
			return this;
		}
	}
}