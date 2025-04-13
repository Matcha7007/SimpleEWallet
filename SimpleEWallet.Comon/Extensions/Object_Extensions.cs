using System.Diagnostics.CodeAnalysis;

namespace SimpleEWallet.Comon.Extensions
{
	/// <summary>
	/// Extension for object
	/// </summary>
	public static partial class Object_Extensions
	{
		/// <summary>
		/// Return true when object is null
		/// </summary>
		/// <param name="extended"></param>
		/// <returns></returns>
		public static bool IsNull([NotNullWhen(false)] this object? extended) =>
			extended == null;
		/// <summary>
		/// Return true when object is not null
		/// </summary>
		/// <param name="extended"></param>
		/// <returns></returns>
		public static bool IsNotNull([NotNullWhen(true)] this object? extended) =>
			!extended.IsNull();
	}
}