using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace App.Core.Common;

/// <summary>
///     Tạo code OTP
/// </summary>
public class OtpAuthenticator : IDisposable
{
    private readonly HMAC HMAC;

    private readonly OtpType Type;

    public OtpAuthenticator(OtpType type, OtpAlgorithm algorithm = OtpAlgorithm.SHA1, byte[] key = null)
    {
        HMAC = GetHMAC(algorithm, key ?? GenerateKey(algorithm));
        Type = type;
    }

    public ICachingProvider CachingProvider { get; set; }
    public int CodeLength { get; set; } = 6;
    public long HotpCounter { get; set; }
    public int TotpInterval { get; set; } = 30;
    public long TotpTimeOffsetSeconds { get; set; } = 0;

    public void Dispose()
    {
        HMAC.Dispose();
    }

    /// <summary>
    ///     Xác thực OTP
    /// </summary>
    /// <param name="code"></param>
    /// <param name="challenge"></param>
    /// <param name="forward"></param>
    /// <param name="back"></param>
    /// <returns></returns>
    public bool VerifyOtp(string code, byte[] challenge = null, int forward = 1, int back = 1)
    {
        if (string.IsNullOrEmpty(code) || code.Length != CodeLength ||
            (CachingProvider != null && !CachingProvider.ValidateToken(code))) return false;
        var State = GetState(-back);
        var Tried = new List<string>();
        for (var I = 0; I <= forward + back; I++)
        {
            var Code = GetOtp(State + I, challenge);
            Tried.Add(code);
            if (Code == code)
            {
                if (CachingProvider != null) CachingProvider.CancelTokens(Type, Tried);
                if (Type == OtpType.HOTP) HotpCounter = State + I + 1;
                return true;
            }
        }

        return false;
    }

    private long GetState(int offset)
    {
        return (Type == OtpType.HOTP
            ? HotpCounter
            : (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + TotpTimeOffsetSeconds) / TotpInterval) + offset;
    }

    /// <summary>
    ///     Lấy OTP
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="challenge"></param>
    /// <returns></returns>
    public string GetOtp(int offset = 0, byte[] challenge = null)
    {
        return GetOtp(GetState(offset), challenge);
    }

    private string GetOtp(long state, byte[] challenge = null)
    {
        var Input = BitConverter.GetBytes(state);
        //if (BitConverter.IsLittleEndian) 
        Array.Reverse(Input);

        if (challenge != null)
        {
            Array.Resize(ref Input, Input.Length + challenge.Length);
            Buffer.BlockCopy(challenge, 0, Input, 8, challenge.Length);
        }

        var Hash = HMAC.ComputeHash(Input);
        var offset = Hash[Hash.Length - 1] & 0xf;
        var binary = ((Hash[offset] & 0x7f) << 24) | ((Hash[offset + 1] & 0xff) << 16) |
                     ((Hash[offset + 2] & 0xff) << 8) | (Hash[offset + 3] & 0xff);
        return (binary % (int)Math.Pow(10, CodeLength)).ToString(new string('0', CodeLength));
    }

    public string GetIntegrityValue()
    {
        return GetOtp();
    }

    public static string GetUri(OtpType type, byte[] key, string accountName, string issuer = "",
        OtpAlgorithm algorithm = OtpAlgorithm.SHA1,
        int codeLength = 6, long counter = 0, int period = 30)
    {
        var SB = new StringBuilder();
        SB.AppendFormat("otpauth://{0}/", type.ToString().ToLower());
        if (!string.IsNullOrEmpty(issuer))
            SB.AppendFormat("{0}:{1}?issuer={0}&", Uri.EscapeUriString(issuer), Uri.EscapeUriString(accountName));
        else SB.AppendFormat("{0}?", Uri.EscapeUriString(accountName));
        SB.AppendFormat("secret={0}&algorithm={1}&digits={2}&", Base32.Encode(key), algorithm, codeLength);
        if (type == OtpType.HOTP) SB.AppendFormat("counter={0}", counter);
        else SB.AppendFormat("period={0}", period);
        return SB.ToString();
    }

    public string GetUri(string accountName, string issuer = "")
    {
        return GetUri(Type, HMAC.Key, accountName, issuer = "",
            (OtpAlgorithm)Enum.Parse(typeof(OtpAlgorithm), HMAC.HashName),
            CodeLength, HotpCounter, TotpInterval);
    }

    public override string ToString()
    {
        return GetUri("OtpGenerator");
    }

    public static byte[] GenerateKey(OtpAlgorithm algorithm)
    {
        return GenerateKey(GetHashLength(algorithm));
    }

    public static byte[] GenerateKey(int length)
    {
        using (var RNG = RandomNumberGenerator.Create())
        {
            var Output = new byte[length];
            RNG.GetBytes(Output);
            return Output;
        }
    }

    private static HMAC GetHMAC(OtpAlgorithm algorithm, byte[] key)
    {
        switch (algorithm)
        {
            case OtpAlgorithm.MD5: return new HMACMD5(key);
            case OtpAlgorithm.SHA1: return new HMACSHA1(key);
            case OtpAlgorithm.SHA256: return new HMACSHA256(key);
            case OtpAlgorithm.SHA512: return new HMACSHA512(key);
        }

        throw new InvalidOperationException();
    }

    private static int GetHashLength(OtpAlgorithm algorithm)
    {
        switch (algorithm)
        {
            case OtpAlgorithm.MD5: return 32;
            case OtpAlgorithm.SHA1: return 20;
            case OtpAlgorithm.SHA256: return 32;
            case OtpAlgorithm.SHA512: return 64;
        }

        throw new InvalidOperationException();
    }
}

public enum OtpType
{
    /// <summary>
    ///     https://datatracker.ietf.org/doc/html/rfc6238
    /// </summary>
    HOTP = 0,

    /// <summary>
    ///     https://datatracker.ietf.org/doc/html/rfc4226
    /// </summary>
    TOTP = 1
}

public enum OtpAlgorithm
{
    MD5 = 10,
    SHA1 = 1,
    SHA256 = 2,
    SHA512 = 3
}

public interface ICachingProvider
{
    void CancelToken(OtpType type, string token);
    void CancelTokens(OtpType type, IEnumerable<string> tokens);
    bool ValidateToken(string token);
}

public class LocalCachingProvider : ICachingProvider
{
    private readonly List<string> Used = new();

    public void CancelToken(OtpType type, string token)
    {
        Used.Add(token);
    }

    public void CancelTokens(OtpType type, IEnumerable<string> tokens)
    {
        Used.AddRange(tokens);
    }

    public bool ValidateToken(string token)
    {
        return !Used.Contains(token);
    }
}