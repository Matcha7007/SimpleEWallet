namespace SimpleEWallet.Comon.Enumerations
{
	public enum TransactionTypeConstants
	{
		Topup = 1,
		TransferIn = 2,
		TransferOut = 3
	}
	public static class TransactionTypeConstantsExtensions
	{
		public static int ToInt32(this TransactionTypeConstants value) => (int)value;
	}
}
