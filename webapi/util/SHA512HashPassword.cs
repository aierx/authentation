using System.Security.Cryptography;

namespace webapi.util;

public class SHA512HashPassword : HashPasswordBase
{
    #region 常量

    /// <summary>
    ///     哈希字节长度常量
    /// </summary>
    public const int HashByteLengthConst = 64;

    #endregion

    #region 方法

    /// <summary>
    ///     计算hash
    /// </summary>
    /// <param name="buffer">
    ///     缓冲区
    /// </param>
    protected override byte[] ComputeHash(byte[] buffer)
    {
        SHA512 service;

        try
        {
            service = new SHA512CryptoServiceProvider();
        }
        catch (PlatformNotSupportedException)
        {
            service = new SHA512Managed();
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