using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NSRetail.Helper_Code.OAuth2
{
    public class AesBase64Wrapper
    {
        private static string IV = "4FEDC00C6319F334";
        private static string PASSWORD = "CategisSoftwareGmbH@BengaluruKA";
        private static string SALT = "9FCB0B9D0FA22A74DF61E012D7C5B0AB";

        public static string EncryptAndEncode(string raw)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                string encrypted = Convert.ToBase64String(output);
                return encrypted;
            }
        }

        public static string DecodeAndDecrypt(string encrypted)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var d = GetCryptoTransform(csp, false);
                byte[] output = Convert.FromBase64String(encrypted);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);
                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(PASSWORD), Encoding.UTF8.GetBytes(SALT), 65536);
            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(IV);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }
       
        private static byte[] Encrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("NSoftSol", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        private static byte[] Decrypt(byte[] input)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes("NSoftSol", new byte[] { 0x43, 0x87, 0x23, 0x72, 0x45, 0x56, 0x68, 0x14, 0x62, 0x84 });
            MemoryStream ms = new MemoryStream();
            Aes aes = new AesManaged();
            aes.Key = pdb.GetBytes(aes.KeySize / 8);
            aes.IV = pdb.GetBytes(aes.BlockSize / 8);
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(input, 0, input.Length);
            cs.Close();
            return ms.ToArray();
        }
        public static string Encrypt(string input)
        {
            return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(input)));
        }
        public static string Decrypt(string input)
        {
            return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(input)));
        }
    }
}