namespace webapi.util;

public interface IHashPassword
{

    // 获取哈希字节长度
    int HashByteLength { get; }


    byte[] GenerateSalt();
    // 生成
    string Generate(byte[] salt,string passwordString);

    // 验证
    bool Validate(string passwordString, string hashValue);
}