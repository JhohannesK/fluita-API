using System.Security.Cryptography;

namespace fb_API.Services
{
	public class PasswordHasher
	{
		private const int _saltSize = 16;
		private const int _keySize = 32;
		private const int iterations = 10000;
		private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA512;

		private const char _delimiter = '.';

		public static string Hash(string password)
		{
			byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);
			byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithmName, _keySize);

			return string.Join(_delimiter, iterations, Convert.ToBase64String(salt), Convert.ToBase64String(hash), hashAlgorithmName);
		}

		public static bool Verify(string password, string hashedPassword)
		{
			string[] segments = hashedPassword.Split(_delimiter);
			byte[] salt = Convert.FromBase64String(segments[1]);
			byte[] hash = Convert.FromBase64String(segments[2]);
			int iterations = int.Parse(segments[0]);
			HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);

			byte[] newHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, hash.Length);

			return CryptographicOperations.FixedTimeEquals(hash, newHash);

		}
	}
}
