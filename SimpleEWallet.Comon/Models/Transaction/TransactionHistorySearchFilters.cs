using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Transaction
{
	public class TransactionHistorySearchFilters : BaseListFilters
	{
		public Guid? UserId { get; set; }
		public Guid? WalletId { get; set; }
		public DateOnly? FilterPeriodFrom { get; set; }
		public DateOnly? FilterPeriodTo { get; set; }

		public TransactionHistorySearchFilters()
		{
			var today = DateTime.Now;
			var firstDayOfMonth = new DateOnly(today.Year, today.Month, 1);
			var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

			FilterPeriodFrom = firstDayOfMonth;
			FilterPeriodTo = lastDayOfMonth;
		}
	}
}
