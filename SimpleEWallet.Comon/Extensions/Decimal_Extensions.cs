using System.Globalization;

namespace SimpleEWallet.Comon.Extensions
{
	public static class Decimal_Extensions
	{
		public static string ToDisplayMoney(this decimal money)
		{
			return money.ToString("#,##0.00", new CultureInfo("id-ID"));
		}

		public static string ToDisplayMoneyCustom(this decimal money) => money.ToString("#,##0.00", CultureInfo.InvariantCulture).Replace(",", "_").Replace(".", ",").Replace("_", ".");

		public static string ToDisplayQuantity(this decimal qty)
		{
			return qty.ToString("#,##0", new CultureInfo("id-ID"));
		}

		public static string ToDisplayPercentage(this decimal? percentage)
		{
			return percentage.HasValue ? percentage.Value.ToDisplayPercentage() : 0m.ToDisplayPercentage();
		}

		public static string ToDisplayPercentage(this decimal percentage)
		{
			return percentage.ToString("##0.00", new CultureInfo("id-ID")) + " %";
		}
	}
}
