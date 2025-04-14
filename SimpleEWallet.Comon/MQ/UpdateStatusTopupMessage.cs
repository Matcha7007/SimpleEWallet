namespace SimpleEWallet.Comon.MQ
{
    public class UpdateStatusTopupMessage
    {
		public Guid UserId { get; set; }
		public Guid TopupRequestId { get; set; }
		public DateTime TopupTime { get; set; }
	}
}
