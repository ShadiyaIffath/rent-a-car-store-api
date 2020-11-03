using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace UtilityLibrary.Utils
{
    public class EncryptUtil
    {

        private const string _vector = "cipherVector";
        private const string _key = "E546C8DF278CD5931069B522E695D4F2";

        public static string EncryptString(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            try
            {
                var keyByte = Encoding.UTF8.GetBytes(_key);

                using (var aesAlg = Aes.Create())
                {
                    using (var encryptor = aesAlg.CreateEncryptor(keyByte, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(value);
                            }

                            var iv = aesAlg.IV;

                            var decryptedContent = msEncrypt.ToArray();

                            var result = new byte[iv.Length + decryptedContent.Length];

                            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

                            var str = Convert.ToBase64String(result);
                            var fullCipher = Convert.FromBase64String(str);
                            return str;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string DecryptString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            try
            {
                var fullCipher = Convert.FromBase64String(cipherText);

                var iv = new byte[16];
                var cipher = new byte[fullCipher.Length - iv.Length];

                Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);
                var keyByte = Encoding.UTF8.GetBytes(_key);

                using (var aesAlg = Aes.Create())
                {
                    using (var decryptor = aesAlg.CreateDecryptor(keyByte, iv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(cipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return cipherText;
            }
        }

    }
}
