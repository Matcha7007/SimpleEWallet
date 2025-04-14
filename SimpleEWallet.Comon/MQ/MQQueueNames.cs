namespace SimpleEWallet.Comon.MQ
{
	public static class MQQueueNames
	{
		public static class Wallet
		{
			public static string InitializeWallet => "simple_ewallet_wallet_initialize";
			public static string UpdateStatusTopup => "simple_ewallet_update_status_topup";
		}

		public static class Transaction
		{
			public static string AddTransaction => "simple_ewallet_add_transaction";
		}
	}
}
