using System.Security.Cryptography;

namespace SimpleEWallet.Auth.Tools
{
	public static class HashTool
	{

		private static int SaltSize => 16; // 128 bits
		private static int KeySize => 32; // 256 bits
		private static int Iterations => 50000;
		private static HashAlgorithmName Algorithm => HashAlgorithmName.SHA256;

		private static char SegmentDelimiter => ':';

		public static string DoHash(string data)
		{
			byte[] bytesSalt = RandomNumberGenerator.GetBytes(SaltSize);
			byte[] bytesHash = Rfc2898DeriveBytes.Pbkdf2(
				data,
				bytesSalt,
				Iterations,
				Algorithm,
				KeySize
			);
			return string.Join(
				SegmentDelimiter,
				Convert.ToHexString(bytesHash),
				Convert.ToHexString(bytesSalt),
				Iterations,
				Algorithm
			);
		}

		public static bool Verify(string dataToVerify, string hashString)
		{
			string[] segments = hashString.Split(SegmentDelimiter);
			byte[] bytesHash = Convert.FromHexString(segments[0]);
			byte[] bytesSalt = Convert.FromHexString(segments[1]);
			int iterations = int.Parse(segments[2]);
			HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);
			byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
				dataToVerify,
				bytesSalt,
				iterations,
				algorithm,
				bytesHash.Length
			);
			return CryptographicOperations.FixedTimeEquals(inputHash, bytesHash);
		}
	}
}