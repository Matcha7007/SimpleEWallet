using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Auth
{
    public class UserReceiverSearchFilters : BaseListFilters
	{
		public string? Keyword { get; set; }
	}
}
