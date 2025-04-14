using System.Security.Cryptography;
using System.Text;

namespace SimpleEWallet.Auth.Tools
{
	public static class AESEncryptionTool
	{

		public static string Encrypt(string key, string data)
		{
			//initialization vector
			byte[] iv = new byte[16];

			byte[] array;

			//Create a new Aes Cryptography
			using (Aes aes = Aes.Create())
			{
				//Encode the key and set it for encryption
				aes.Key = Encoding.UTF8.GetBytes(key);

				//Set the initialization vector (IV)
				aes.IV = iv;

				//Create an encryptor using the Aes Key and IV
				ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

				//Create a memory stream to store the encryption result
				using (MemoryStream memoryStream = new())
				{
					//Create a new CryptoStream that will use encryptor to encrypt data and write the encryption result to memoryStream
					using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
					{
						//Create a StreamWriter that will write data to cryptoStream
						using (StreamWriter streamWriter = new(cryptoStream))
						{
							//Write data to encrypt using streamWriter
							streamWriter.Write(data);
						}

						//Write the encrypted data to the byte array
						array = memoryStream.ToArray();
					}
				}
			}

			//Convert the byte array containing the encrypted data back to string and return the string
			return Convert.ToBase64String(array);
		}

		public static string Decrypt(string key, string encryptedData)
		{
			//initialization vector
			byte[] iv = new byte[16];

			//Convert encypted string to bytes array
			byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

			//Create a new Aes Cryptography
			using (Aes aes = Aes.Create())
			{
				//Encode the key and set it for encryption
				aes.Key = Encoding.UTF8.GetBytes(key);

				//Set the initialization vector (IV)
				aes.IV = iv;

				//Create a decryptor using the Aes Key and IV
				ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

				//Create a memory stream to store the encrypted bytes array
				using (MemoryStream memoryStream = new(encryptedBytes))
				{
					//Create a new CryptoStream that will use decryptor to decrypt data from memoryStream
					using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read))
					{
						//Create a StreamReader that will read decryption result from cryptoStream
						using (StreamReader streamReader = new(cryptoStream))
						{
							//read the decryption result and return it as string
							return streamReader.ReadToEnd();
						}
					}
				}
			}
		}
	}
}