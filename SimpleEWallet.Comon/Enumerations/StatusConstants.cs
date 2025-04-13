namespace SimpleEWallet.Comon.Enumerations
{
	public enum StatusConstants
	{
		Pending = 1,
		Success = 2,
		Failed = 3
	}
	public static class StatusConstantsExtensions
	{
		public static int ToInt32(this StatusConstants value) => (int)value;
	}
}
