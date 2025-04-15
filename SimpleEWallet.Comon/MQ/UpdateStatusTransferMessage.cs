namespace SimpleEWallet.Comon.MQ
{
    public class UpdateStatusTransferMessage
    {
		public Guid UserId { get; set; }
		public Guid TransferId { get; set; }
		public DateTime TransferTime { get; set; }
	}
}
