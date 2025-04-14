namespace SimpleEWallet.Wallet.Tools
{
	public static class Status
	{
		public static string GetRandomStatusString()
		{
			return GetRandomStatus() switch
			{
				1 => "Pending",
				2 => "Success",
				3 => "Failed",
				_ => "Unknown"
			};
		}

		public static int GetRandomStatus()
		{
			Random random = new();
			return random.Next(1, 4);
		}
	}
}
