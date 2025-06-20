using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Raftlab.Core.HelperServiceAndExtensions
{
    /// <summary>
    /// this is Helper Extensions
    /// </summary>
    public static class HelperExtension
    {
        /// <summary>
        /// check string is null or whitespace if yes than it return true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// check object is null if yes than it return true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNull(this object input)
        {
            return input == null;
        }

        /// <summary>
        /// check string is not null if yes than it return true
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object input)
        {
            return input != null;
        }

        /// <summary>
        /// join array of objects with seprator and return string
        /// </summary>
        /// <param name="input"></param>
        /// <param name="seprator"></param>
        /// <returns></returns>
        public static string Join(this object[] input, string seprator)
        {
            return string.Join(seprator, input);
        }

        public static string MemoryStreamToString(this MemoryStream stream)
        {
            if (stream.Position != 0)
                stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static string MemoryStreamToString(this Stream stream)
        {
            if (stream.Position != 0)
                stream.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static Stream StringToMemoryStream(this string input)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(input));
        }

        public static string GetBase64String(this byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static string GetUtf8String(this byte[] data)
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public static byte[] GetUtf8StringToBytes(this string data)
        {
            return System.Text.Encoding.UTF8.GetBytes(data);
        }

        public static TEnum ParseEnum<TEnum>(this string input)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), input, true);
        }
    }
}
