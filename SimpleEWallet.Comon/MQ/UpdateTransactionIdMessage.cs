namespace SimpleEWallet.Comon.MQ
{
    public class UpdateTransactionIdMessage
    {
		public Guid UserId { get; set; }
		public Guid TransactionId { get; set; }
		public Guid TransactionRequestId { get; set; }
		public int TransactionTypeId { get; set; }
	}
}
