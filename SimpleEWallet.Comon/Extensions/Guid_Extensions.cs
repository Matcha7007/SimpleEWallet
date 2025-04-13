using System.Diagnostics.CodeAnalysis;

namespace SimpleEWallet.Comon.Extensions
{
	public static class Guid_Extensions
	{
		public static bool IsEmpty(this Guid extended)
		{
			return extended == Guid.Empty;
		}

		public static bool IsNotEmpty(this Guid extended)
		{
			return !extended.IsEmpty();
		}

		public static bool IsNullOrEmpty([NotNullWhen(false)] this Guid? extended)
		{
			return !extended.HasValue || extended.Value.IsEmpty();
		}

		public static bool IsNotNullOrEmpty([NotNullWhen(false)] this Guid? extended)
		{
			return !extended.IsNullOrEmpty();
		}
	}
}
