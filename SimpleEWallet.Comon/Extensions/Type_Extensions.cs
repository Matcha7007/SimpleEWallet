namespace SimpleEWallet.Comon.Extensions
{
	/// <summary>
	/// Provides extension methods for the <see cref="Type"/> class, offering additional functionality 
	/// for type checking and metadata access.
	/// </summary>
	public static partial class Type_Extensions
	{
		/// <summary>
		/// Gets a extended indicating whether the extended represents an enumeration.
		/// </summary>
		/// <param name="extended">The Type to check.</param>
		/// <returns>true if the extended represents an enumeration; otherwise, false.</returns>
		public static bool IsAnEnum(this Type extended)
			=> extended.IsEnum;
	}
}