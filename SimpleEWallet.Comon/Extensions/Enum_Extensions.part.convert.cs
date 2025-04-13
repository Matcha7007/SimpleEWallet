namespace SimpleEWallet.Comon.Extensions
{
	public static partial class Enum_Extensions
	{
		/// <summary>
		/// Convert an enum into it's 32-bit signed integer representation.
		/// </summary>
		/// <typeparam name="EnumType">The type of enum to convert.</typeparam>
		/// <param name="extended">The enum extended to convert.</param>
		/// <returns>The 32-bit signed integer representation of the enum extended.</returns>
		/// <exception cref="System.OverflowException">extended represents a number that is less than System.Int32.MinValue or greater than System.Int32.MaxValue.</exception>
		public static int ToInt32<EnumType>(this EnumType extended)
			where EnumType : struct, Enum
		{
			try
			{
				return Convert.ToInt32(extended);
			}
			catch (OverflowException ovflEx)
			{
				throw new OverflowException("'" + extended + "' is not within a valid range.", ovflEx);
			}
		}

		/// <summary>
		/// Convert an enum into it's 32-bit signed integer representation.
		/// </summary>
		/// <typeparam name="EnumType">The type of enum to convert.</typeparam>
		/// <param name="extended">The enum extended to convert.</param>
		/// <returns>null if extended is null; otherwise the 32-bit signed integer representation of the enum extended.</returns>
		/// <exception cref="System.OverflowException">extended represents a number that is less than System.Int32.MinValue or greater than System.Int32.MaxValue.</exception>
		public static int? ToNullableInt32<EnumType>(this EnumType? extended)
			where EnumType : struct, Enum
			=> extended.HasValue
				? extended.Value.ToInt32()
				: (int?)null;

		/// <summary>
		/// Convert an enum into it's 64-bit unsigned integer representation.
		/// </summary>
		/// <typeparam name="EnumType">The type of enum to convert.</typeparam>
		/// <param name="extended">The enum extended to convert.</param>
		/// <returns>The 64-bit unsigned integer representation of the enum extended.</returns>
		/// <exception cref="System.OverflowException">extended represents a number that is less than System.UInt64.MinValue or greater than System.UInt64.MaxValue.</exception>
		public static ulong ToUInt64<EnumType>(this EnumType extended)
			where EnumType : struct, Enum
		{
			try
			{
				return Convert.ToUInt64(extended);
			}
			catch (OverflowException ovflEx)
			{
				throw new OverflowException("'" + extended + "' is not within a valid range.", ovflEx);
			}
		}

		/// <summary>
		/// Convert an enum into it's 64-bit unsigned integer representation.
		/// </summary>
		/// <typeparam name="EnumType">The type of enum to convert.</typeparam>
		/// <param name="extended">The enum extended to convert.</param>
		/// <returns>null if extended is null; otherwise the 64-bit unsigned integer representation of the enum extended.</returns>
		/// <exception cref="System.OverflowException">extended represents a number that is less than System.UInt64.MinValue or greater than System.UInt64.MaxValue.</exception>
		public static ulong? ToNullableUInt64<EnumType>(this EnumType? extended)
			where EnumType : struct, Enum
			=> extended.HasValue
				? extended.Value.ToUInt64()
				: (ulong?)null;
	}
}