using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Wallet
{
    public class TopupRequestParameters : BaseMethodParameters
	{
		public Guid? UserId { get; set; }
		public string WalletNumber { get; set; } = string.Empty;
		public decimal Amount { get; set; } = 0;
	}
}
