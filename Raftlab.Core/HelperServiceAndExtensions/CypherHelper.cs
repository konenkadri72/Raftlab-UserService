using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Raftlab.Core.HelperServiceAndExtensions
{
    public static class CypherHelper
    {

        /// <summary>
        /// This Method Will Encrypt input string
        /// Note : Output may contains Special characters
        /// </summary>
        /// <param name="input">string parameter</param>
        /// <param name="key">optional Salt</param>
        /// <returns></returns>
        public static string EncryptWithSpecialCharacter(this string input, string key = "sblw-3hn8-sqoy19")
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        /// <summary>
        /// This Method Will Encrypt input string
        /// Note : Output may contains Special characters
        /// </summary>
        /// <param name="input">string parameter</param>
        /// <param name="key">optional Salt Same as Encrypt</param>
        /// <returns></returns>
        public static string DecryptSpecialCharacter(this string input, string key = "sblw-3hn8-sqoy19")
        {
            byte[] inputArray = Convert.FromBase64String(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }


        /// <summary>
        /// This Method will Encrypt Given string Using Base64 Logic
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptUsingBase64(this string input)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));
        }
        /// <summary>
        /// This Method will Encrypt Given string Using Base64 Logic
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DecryptUsingBase64(this string input)
        {
            return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input));
        }


        /// <summary>
        /// This Method will Encrypt Given string without any special character contains
        /// </summary>
        /// <param name="stringvalue"></param>
        /// <returns></returns>
        public static string EncryptWithoutSpecialCharacter(this string stringvalue)
        {
            System.Text.Encoding encoding = Encoding.UTF8;
            Byte[] stringBytes = encoding.GetBytes(stringvalue);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }
        /// <summary>
        /// This Method will Decrypt Given string without any special character contains
        /// </summary>
        /// <param name="stringvalue"></param>
        /// <returns></returns>
        public static string DecryptWithoutSpecialCharacter(this string hexvalue)
        {
            System.Text.Encoding encoding = Encoding.UTF8;
            int CharsLength = hexvalue.Length;
            byte[] bytesarray = new byte[CharsLength / 2];
            for (int i = 0; i < CharsLength; i += 2)
            {
                bytesarray[i / 2] = Convert.ToByte(hexvalue.Substring(i, 2), 16);
            }
            return encoding.GetString(bytesarray);
        }

    }
}
