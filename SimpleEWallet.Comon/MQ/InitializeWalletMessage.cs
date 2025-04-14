namespace SimpleEWallet.Comon.MQ
{
    public class InitializeWalletMessage
    {
		public Guid UserId { get; set; }
		public string WalletNumber { get; set; } = string.Empty;
		public string WalletName { get; set; } = string.Empty;
	}
}
