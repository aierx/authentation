using System.Security.Cryptography;

namespace webapi.util;

public abstract  class HashPasswordBase: IHashPassword
{
    /// <summary>
    ///     获取哈希字节长度
    /// </summary>
    public int HashByteLength
    {
        get { return GetHashByteLength(); }
    }
    
    public string Generate( byte[] salt,string password)
    {
        byte[] hash = Generate(password, salt);
        return string.Format("{0}:{1}", Convert.ToBase64String(salt), Convert.ToBase64String(hash));
    }

    public bool Validate(string password, string hashValue)
    {
        string[] splits = hashValue.Split(':');
        if (splits.Length == 2)
        {
            byte[] salt = Convert.FromBase64String(splits[0]);
            byte[] hash = Convert.FromBase64String(splits[1]);

            return Validate(password, salt, hash);
        }

        return false;
    }
    
    protected abstract byte[] ComputeHash(byte[] buffer);
    
    protected byte[] Generate(string password, byte[] salt)
    {
        byte[] bytes = BlockCopy(password);
        byte[] combined = Combine(salt, bytes);
        return ComputeHash(combined);
    }
    
    public byte[] GenerateSalt()
    {
        return RandomSalt(HashByteLength);
    }
    
    protected abstract int GetHashByteLength();
    
    
    protected bool Validate(string password, byte[] salt, byte[] goodHash)
    {
        byte[] hash = Generate(password, salt);
        return SlowEquals(hash, goodHash);
    }

    private static byte[] BlockCopy(string input)
    {
        var bytes = new byte[input.Length*sizeof (char)];
        Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }
    
    private static byte[] Combine(byte[] a, byte[] b)
    {
        return a.Concat(b).ToArray();
    }

    private static byte[] RandomSalt(int length)
    {
        var rng = new RNGCryptoServiceProvider();
        var salt = new byte[length];
        rng.GetBytes(salt);
        return salt;
    }

    private static bool SlowEquals(byte[] a, byte[] b)
    {
        uint diff = (uint) a.Length ^ (uint) b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
        {
            diff |= (uint) (a[i] ^ b[i]);
        }
        return diff == 0;
    }
}