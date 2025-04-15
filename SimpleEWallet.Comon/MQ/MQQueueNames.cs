namespace SimpleEWallet.Comon.MQ
{
	public static class MQQueueNames
	{
		public static class Wallet
		{
			public static string InitializeWallet => "simple_ewallet_wallet_initialize";
			public static string UpdateStatusTopup => "simple_ewallet_update_status_topup";
			public static string UpdateStatusTransfer => "simple_ewallet_update_status_transfer";
			public static string UpdateTransactionId => "simple_ewallet_update_transaction_id";
			public static string DeleteDataWallet => "simple_ewallet_delete_data_wallet";
		}

		public static class Transaction
		{
			public static string AddTransaction => "simple_ewallet_add_transaction";
			public static string DeleteDataTransaction => "simple_ewallet_delete_data_transaction";
		}
	}
}
