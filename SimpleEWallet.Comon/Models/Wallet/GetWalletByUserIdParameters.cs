using SimpleEWallet.Comon.Base.WebAPI;

namespace SimpleEWallet.Comon.Models.Wallet
{
    public class GetWalletByUserIdParameters : BaseMethodParameters
	{
		public Guid UserId { get; set; }
	}
}
