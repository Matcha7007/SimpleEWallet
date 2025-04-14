namespace SimpleEWallet.Comon.MQ
{
    public class AddTransactionMessage
    {
		public Guid UserId { get; set; }
		public Guid WalletId { get; set; }
		public int TransactionTypeId { get; set; }
		public decimal Amount { get; set; }
		public string? Reference { get; set; }
	}
}
