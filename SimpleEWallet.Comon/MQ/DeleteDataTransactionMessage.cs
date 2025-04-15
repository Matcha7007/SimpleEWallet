namespace SimpleEWallet.Comon.MQ
{
    public class DeleteDataTransactionMessage
    {
		public Guid UserId { get; set; }
		public Guid WalletId { get; set; }
	}
}
