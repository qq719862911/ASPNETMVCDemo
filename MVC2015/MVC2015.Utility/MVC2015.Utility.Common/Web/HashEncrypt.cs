using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MVC2015.Utility
{
    public class HashEncrypt
    {

        public static string MD5Encrypt(string password)
        {
            byte[] tmpByte;
            MD5 md5 = new MD5CryptoServiceProvider();
            tmpByte = md5.ComputeHash(GetKeyByteArray(password));
            md5.Clear();

            return GetStringValue(tmpByte);

        }

        public static string MD5(String str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(str);
            byte[] result = md5.ComputeHash(data);
            String ret = "";
            for (int i = 0; i < result.Length; i++)
                ret += result[i].ToString("x").PadLeft(2, '0');
            return ret;
        }

        public static string SHA1Encrypt(string password)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            tmpByte = sha1.ComputeHash(GetKeyByteArray(password));
            sha1.Clear();

            return GetStringValue(tmpByte);

        }
        public static string SHA256Encrypt(string password)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();

            tmpByte = sha256.ComputeHash(GetKeyByteArray(password));
            sha256.Clear();

            return GetStringValue(tmpByte);

        }
        public static string SHA512Encrypt(string password)
        {
            //string strIN = getstrIN(strIN);
            byte[] tmpByte;
            SHA512 sha512 = new SHA512Managed();

            tmpByte = sha512.ComputeHash(GetKeyByteArray(password));
            sha512.Clear();

            return Convert.ToBase64String(tmpByte);

        }

        /**/
        /// <summary>
        /// Use DES Encrypt
        /// </summary>
        /// <param name="password">Before encrypted string</param>
        /// <param name="key">The key (Max Length 8)</param>
        /// <param name="IV">The IV(Max Length 8)</param>
        /// <returns>After encrypted string</returns>
        public static string DESEncrypt(string password, string key, string IV)
        {
            //initialize key and IV
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(password);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Convert.ToBase64String(ms.ToArray());
        }
        public static string DESEncrypt(string originalValue, string key)
        {
            return DESEncrypt(originalValue, key, key);
        }

        /// <summary>
        /// Use DES Decrypt
        /// </summary>
        /// <param name="password">Before decrypted string</param>
        /// <param name="key">The key (Max Length 8)</param>
        /// <param name="IV">The IV(Max Length 8)</param>
        /// <returns>After decrypted string</returns>
        public static string DESDecrypt(string encryptedValue, string key, string IV)
        {
            //initialize key and IV
            key += "12345678";
            IV += "12345678";
            key = key.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(key);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ct = sa.CreateDecryptor();

            byt = Convert.FromBase64String(encryptedValue);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }

        public static string DESDecrypt(string encryptedValue, string key)
        {
            return DESDecrypt(encryptedValue, key, key);
        }

        private static string GetStringValue(byte[] Byte)
        {
            string tmpString = "";

            ASCIIEncoding Asc = new ASCIIEncoding();
            tmpString = Asc.GetString(Byte);

            //int iCounter;
            //for (iCounter = 0; iCounter < Byte.Length; iCounter++)
            //{
            //    tmpString = tmpString + Byte[iCounter].ToString();
            //}

            return tmpString;
        }

        private static byte[] GetKeyByteArray(string strKey)
        {

            ASCIIEncoding Asc = new ASCIIEncoding();

            int tmpStrLen = strKey.Length;
            byte[] tmpByte = new byte[tmpStrLen - 1];

            tmpByte = Asc.GetBytes(strKey);

            return tmpByte;

        }
    }
}
