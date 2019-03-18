using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
//using IniParser.Model;

namespace RSDKv5
{
    public class Objects
    {
        public static Dictionary<string, string> ObjectNames = new Dictionary<string, string>();
        public static Dictionary<string, string> AttributeNames = new Dictionary<string, string>();

        public static void InitObjectNames(StreamReader reader, bool closeReader = true)
        {
            MD5 md5hash = MD5.Create();
            ObjectNames.Clear();
            while (!reader.EndOfStream)
            {
                try
                {
                    string input = reader.ReadLine();
                    string hash = GetMd5HashString(input);
                    if (!ObjectNames.ContainsKey(hash))
                    {
                        ObjectNames.Add(hash, input);
                    }
                }
                catch (Exception ex)
                {
                    //oop
                }
            }
            if (closeReader) reader.Close();
        }

        public static void InitAttributeNames(StreamReader reader, bool closeReader = true)
        {
            MD5 md5hash = MD5.Create();
            AttributeNames.Clear();
            while (!reader.EndOfStream)
            {
                try
                {
                    string input = reader.ReadLine();
                    string hash = GetMd5HashString(input);
                    if (!AttributeNames.ContainsKey(hash))
                    {
                        AttributeNames.Add(hash, input);
                    }

                }
                catch (Exception ex)
                {
                    //oop
                }
            }
            if (closeReader) reader.Close();
        }

        public static string GetObjectName(NameIdentifier name)
        {
            string res = name.HashString();
            ObjectNames.TryGetValue(name.HashString(), out res);
            return res;
        }

        public static string GetAttributeName(NameIdentifier name)
        {
            string res = name.HashString();
            AttributeNames.TryGetValue(name.HashString(), out res);
            return res;
        }

        public static string GetMd5HashString(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
