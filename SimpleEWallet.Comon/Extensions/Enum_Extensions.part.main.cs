namespace SimpleEWallet.Comon.Extensions
{
	/// <summary>
	/// Provides extension methods for working with enumeration types, including checks for FlagsAttribute
	/// and bitwise flag operations.
	/// </summary>
	public static partial class Enum_Extensions
	{
		/// <summary>
		/// Determines whether an enum type has FlagsAttribute or not.
		/// </summary>
		/// <param name="enumType">The enum type to check.</param>
		/// <returns>true if enum type provided has FlagsAttribute; otherwise, false.</returns>
		/// <exception cref="ArgumentException">enumType is not an enum type.</exception>
		public static bool IsFlags(this Type enumType)
			=> enumType.IsAnEnum()
				? enumType.IsDefined(typeof(FlagsAttribute), false)
				: throw new ArgumentException("'" + nameof(enumType) +"' must be an enum type." , nameof(enumType));

		// Technical Requirements: 1_Tch_Basic PreProcessor Directive.

		/// <summary>
		/// Determines whether one or more bit fields are set in the current instance.        
		/// <para>
		/// Reference(s): <seealso href="https://msdn.microsoft.com/en-us/library/system.enum.hasflag"/>
		/// </para>        
		/// </summary>
		/// <typeparam name="EnumType">The enum type to check.</typeparam>
		/// <param name="extended">The value to check.</param>
		/// <param name="flag">The flag to check.</param>
		/// <returns>true if the bit field or bit fields that are set in flag are also set in the value; otherwise, false.</returns>        
		public static bool HasThisFlag<EnumType> ( this EnumType extended, EnumType flag )
			where EnumType : struct, Enum
		{
			ulong valueBit = extended.ToUInt64();
			ulong flagBit = flag.ToUInt64();
			return (valueBit & flagBit) == flagBit;
		}
	}
}