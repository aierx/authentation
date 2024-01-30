using System.Security.Cryptography;

namespace webapi.util;

public class SHA256HashPassword : HashPasswordBase
{
    #region Constants

    /// <summary>
    ///     哈希字节长度常量
    /// </summary>
    public const int HashByteLengthConst = 32;

    #endregion

    #region Methods

    /// <summary>
    ///     计算hash
    /// </summary>
    /// <param name="buffer">
    ///     缓冲区
    /// </param>
    protected override byte[] ComputeHash(byte[] buffer)
    {
        SHA256 service;

        try
        {
            service = new SHA256CryptoServiceProvider();
        }
        catch (PlatformNotSupportedException)
        {
            service = new SHA256Managed();
        }

        return service.ComputeHash(buffer);
    }

    /// <summary>
    ///     获取哈希字节长度
    /// </summary>
    protected override int GetHashByteLength()
    {
        return HashByteLengthConst;
    }

    #endregion
}