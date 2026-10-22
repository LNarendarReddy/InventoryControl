using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NSRetailPOS.Gateway.Mobikwik
{
    internal static class MobikwikCrypto
    {
        internal static string EncryptBody(string text, string key)
        {
            using (var aes = Aes.Create())
            using (var sha = SHA256.Create())
            {
                aes.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(key)).Take(16).ToArray();
                aes.GenerateIV();
                using (var transform = aes.CreateEncryptor())
                {
                    var plain = Encoding.UTF8.GetBytes(text);
                    return Convert.ToBase64String(aes.IV.Concat(transform.TransformFinalBlock(plain, 0, plain.Length)).ToArray());
                }
            }
        }

        internal static string DecryptBody(string text, string key)
        {
            byte[] bytes = Convert.FromBase64String(text);
            if (bytes.Length < 32 || bytes.Length % 16 != 0) throw new CryptographicException("Invalid encrypted response.");
            using (var aes = Aes.Create())
            using (var sha = SHA256.Create())
            {
                aes.Key = sha.ComputeHash(Encoding.UTF8.GetBytes(key)).Take(16).ToArray();
                aes.IV = bytes.Take(16).ToArray();
                using (var transform = aes.CreateDecryptor())
                    return Encoding.UTF8.GetString(transform.TransformFinalBlock(bytes, 16, bytes.Length - 16));
            }
        }

        internal static RSACryptoServiceProvider ReadKey(string path, bool privateKey)
        {
            byte[] bytes = File.ReadAllBytes(path);
            string pem = Encoding.ASCII.GetString(bytes);
            if (pem.StartsWith("-----BEGIN", StringComparison.Ordinal))
                bytes = Convert.FromBase64String(string.Concat(pem.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Where(l => !l.StartsWith("-----"))));
            var sequence = new DerReader(bytes).Sequence();
            if (privateKey)
            {
                sequence.Integer();
                if (sequence.Tag == 0x30)
                {
                    sequence.Sequence();
                    sequence = new DerReader(sequence.Read(0x04)).Sequence();
                    sequence.Integer();
                }
                var p = new RSAParameters { Modulus = sequence.Integer(), Exponent = sequence.Integer() };
                p.D = Pad(sequence.Integer(), p.Modulus.Length);
                p.P = Pad(sequence.Integer(), p.Modulus.Length / 2);
                p.Q = Pad(sequence.Integer(), p.Modulus.Length / 2);
                p.DP = Pad(sequence.Integer(), p.Modulus.Length / 2);
                p.DQ = Pad(sequence.Integer(), p.Modulus.Length / 2);
                p.InverseQ = Pad(sequence.Integer(), p.Modulus.Length / 2);
                return Import(p);
            }
            if (sequence.Tag == 0x30)
            {
                sequence.Sequence();
                byte[] bitString = sequence.Read(0x03);
                if (bitString.Length < 2 || bitString[0] != 0) throw new CryptographicException("Invalid RSA public key.");
                sequence = new DerReader(bitString.Skip(1).ToArray()).Sequence();
            }
            return Import(new RSAParameters { Modulus = sequence.Integer(), Exponent = sequence.Integer() });
        }

        static RSACryptoServiceProvider Import(RSAParameters p)
        {
            if (p.Modulus.Length != 512) throw new CryptographicException("Mobikwik requires a 4096-bit RSA key.");
            var rsa = new RSACryptoServiceProvider { PersistKeyInCsp = false };
            try { rsa.ImportParameters(p); return rsa; }
            catch { rsa.Dispose(); throw; }
        }
        static byte[] Pad(byte[] bytes, int size)
        {
            if (bytes.Length > size) throw new CryptographicException("Invalid RSA component.");
            var padded = new byte[size];
            Buffer.BlockCopy(bytes, 0, padded, size - bytes.Length, bytes.Length);
            return padded;
        }

        // Parse only the PKCS#1, PKCS#8 and SubjectPublicKeyInfo containers; .NET performs all cryptography.
        sealed class DerReader
        {
            readonly byte[] bytes;
            int position;
            internal DerReader(byte[] bytes) { this.bytes = bytes; }
            internal int Tag => position < bytes.Length ? bytes[position] : -1;
            internal DerReader Sequence() => new DerReader(Read(0x30));
            internal byte[] Integer()
            {
                var value = Read(0x02);
                return value.Length > 1 && value[0] == 0 ? value.Skip(1).ToArray() : value;
            }
            internal byte[] Read(int tag)
            {
                if (position + 2 > bytes.Length || bytes[position++] != tag) throw new CryptographicException("Invalid DER key.");
                int length = bytes[position++];
                if ((length & 0x80) != 0)
                {
                    int count = length & 0x7f;
                    if (count < 1 || count > 4 || position + count > bytes.Length) throw new CryptographicException("Invalid DER length.");
                    length = 0;
                    for (int i = 0; i < count; i++) length = checked(length * 256 + bytes[position++]);
                }
                if (length < 0 || length > bytes.Length - position) throw new CryptographicException("Truncated DER key.");
                byte[] value = new byte[length];
                Buffer.BlockCopy(bytes, position, value, 0, length);
                position += length;
                return value;
            }
        }
    }
}
