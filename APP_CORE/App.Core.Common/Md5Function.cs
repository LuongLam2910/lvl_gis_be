using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Common
{
    public class Md5Function
    {
        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009XBTssdd12@123";
        private static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }

        }
        private static string Decrypt(string cipher)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateDecryptor())
                    {
                        byte[] cipherBytes = Convert.FromBase64String(cipher);
                        byte[] bytes = transform.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
                        return UTF8Encoding.UTF8.GetString(bytes);
                    }
                }
            }
        }


        public static string Encrypt_Cccd(string Cccd)
        {
            try
            {
                if (Cccd!=null && Cccd.Length <= 13) return Encrypt(Cccd);
                else return Cccd;
            }
            catch (Exception e)
            {
                throw;
            }

            return Cccd;
        }

        public static string Encrypt_phone(string phone)
        {
            try
            {
                if (phone!=null && phone.Length <= 13) return Encrypt(phone);
                else return phone;
            }
            catch (Exception e)
            {
                throw;
            }
            return phone;
        }

        public static string Decrypt_Cccd(string Cccd)
        {
           
            try
            {
                if (Cccd!=null &&  Cccd.Length > 15) return Decrypt(Cccd);
                else return Cccd;
            }
            catch (Exception e)
            {
                throw;
            }
            return Cccd;
        }

        public static string Decrypt_phone(string phone)
        {
            
            try
            {
                if (phone!=null && phone.Length > 15) return Decrypt(phone);
                else return phone;
            }
            catch (Exception e)
            {
                throw;
            }
            return phone;
        }


        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            var result = md5.Hash;

            var strBuilder = new StringBuilder();
            for (var i = 0; i < result.Length; i++)
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));

            return strBuilder.ToString();
        }
    }
}
