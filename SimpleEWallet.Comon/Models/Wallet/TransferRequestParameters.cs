using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Wallet
{
    public class TransferRequestParameters : BaseMethodParameters
    {
		public Guid? UserId { get; set; }
		public string WalletNumberReceiver { get; set; } = string.Empty;
		public decimal Amount { get; set; } = 0;
		public string Pin { get; set; } = string.Empty;
		public string? Description { get; set; } = string.Empty;
	}
}
