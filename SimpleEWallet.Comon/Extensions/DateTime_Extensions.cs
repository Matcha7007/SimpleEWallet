namespace SimpleEWallet.Comon.Extensions
{
	public static class DateTime_Extensions
	{
		//
		// Summary:
		//     Get the date only part of a System.DateTime.
		//
		// Parameters:
		//   value:
		//     the DateTime which the date only value will be derived from.
		//
		// Returns:
		//     null if extended is null; otherwise the DateOnly part of the extended.
		public static DateOnly? ToDateOnly(this DateTime? value)
		{
			return value?.ToDateOnly();
		}

		//
		// Summary:
		//     Get the date only part of a System.DateTime.
		//
		// Parameters:
		//   value:
		//     the DateTime which the date only value will be derived from.
		//
		// Returns:
		//     the DateOnly part of the extended.
		public static DateOnly ToDateOnly(this DateTime value)
		{
			return DateOnly.FromDateTime(value);
		}

		public static string ToDisplayDate(this DateTime? value)
		{
			return value.HasValue ? value.Value.ToDisplayDate() : string.Empty;
		}

		public static string ToDisplayDate(this DateTime value)
		{
			return value.ToString("dd MMM yyyy");
		}

		public static string ToDisplayDateTime(this DateTime? value)
		{
			return value.HasValue ? value.Value.ToDisplayDateTime() : string.Empty;
		}

		public static string ToDisplayDateTime(this DateTime value)
		{
			return value.ToString("dd MMM yyyy HH:mm:ss");
		}
	}
}
