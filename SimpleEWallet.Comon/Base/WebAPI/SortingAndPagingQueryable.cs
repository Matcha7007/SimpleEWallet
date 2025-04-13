namespace SimpleEWallet.Comon.Base.WebAPI
{
	/// <summary>
	/// Class.
	/// </summary>
	/// <remarks>
	/// Function.
	/// </remarks>
	public class SortingAndPagingQueryable<T>(IQueryable<T> queryBeforePaging, IQueryable<T> queryAfterPaging)
	{
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public IQueryable<T> QueryBeforePaging { get; protected set; } = queryBeforePaging;
		/// <summary>
		/// Gets or sets the identifier of the person who performed the action.
		/// </summary>
		public IQueryable<T> QueryAfterPaging { get; protected set; } = queryAfterPaging;
	}
}
