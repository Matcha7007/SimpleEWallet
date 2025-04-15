using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Transaction
{
    public class TransactionHistorySearchParameters : BaseListParameters<TransactionHistorySearchFilters>
	{
		public TransactionHistorySearchParameters() : base()
		{ }
	}
}
