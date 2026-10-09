using System.Security.Cryptography;
using System.Text;

namespace SonarNet;

/// <summary>
/// Базова логіка аутентифікації користувачів (зберігання в пам'яті).
/// Паролі зберігаються як PBKDF2-хеш із випадковою сіллю.
/// </summary>
public class UserAuth
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    private readonly Dictionary<string, (byte[] Salt, byte[] Hash)> _users =
        new(StringComparer.OrdinalIgnoreCase);

    public bool Register(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            return false;
        if (_users.ContainsKey(username.Trim()))
            return false;

        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        _users[username.Trim()] = (salt, HashPassword(password, salt));
        return true;
    }

    public bool Login(string username, string password)
    {
        if (string.IsNullOrWhiteSpace(username) || password is null)
            return false;
        if (!_users.TryGetValue(username.Trim(), out var record))
            return false;

        byte[] hash = HashPassword(password, record.Salt);
        return CryptographicOperations.FixedTimeEquals(hash, record.Hash);
    }

    private static byte[] HashPassword(string password, byte[] salt) =>
        Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, Iterations, HashAlgorithmName.SHA256, HashSize);
}
