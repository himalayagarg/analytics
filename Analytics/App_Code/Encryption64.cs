using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for Encryption64
/// </summary>
public class Encryption64
{    
    private string sEncryptionKey = string.Empty;
    private byte[] key = new byte[] { };
    private byte[] IV = new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

    public Encryption64()
    {
        // TODO: Add constructor logic here
        sEncryptionKey = "!#$a54?3";  //private key
    }

    public string Decrypt(string stringToDecrypt)
    {
        byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(stringToDecrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = System.Text.Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string Encrypt(string stringToEncrypt)
    {
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}