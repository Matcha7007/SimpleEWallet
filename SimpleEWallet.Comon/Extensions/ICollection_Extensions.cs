using System.Collections;

namespace SimpleEWallet.Comon.Extensions
{
	/// <summary>
	/// Provides a set of extension methods for <see cref="ICollection"/> and <see cref="ICollection{T}"/> that extend its functionality.
	/// </summary>
	/// <remarks>
	/// The <c>ICollection_Extensions</c> class includes various utility methods for working with ICollection.
	/// </remarks>
	public static class ICollection_Extensions
	{
		/// <summary>
		/// Check whether a collection is null or empty.
		/// </summary>
		/// <param name="collection">the collection to check.</param>
		/// <returns>true if the collection is null or have no elements; otherwise false.</returns>
		public static bool IsNullOrEmpty(this ICollection collection) => collection == null ? true : collection.Count == 0;

		/// <summary>
		/// Check whether a collection is not null and not empty.
		/// </summary>
		/// <param name="collection">the collection to check.</param>
		/// <returns>false if the collection is null or have no elements; otherwise true.</returns>
		public static bool IsNotNullOrEmpty(this ICollection collection) => !collection.IsNullOrEmpty();
	}
}
