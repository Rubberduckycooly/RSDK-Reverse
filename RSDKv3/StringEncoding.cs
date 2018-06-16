// MIT License
// 
// Copyright(c) 2017 Luciano (Xeeynamo) Ciccariello
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    public static class StringEncoding
    {
        public static byte[] GetBytes(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var bytesList = new List<byte>(bytes.Length + 1)
            {
                (byte)bytes.Length
            };
            bytesList.AddRange(bytes);
            return bytesList.ToArray();
        }

        public static string GetString(BinaryReader reader)
        {
            int length = reader.ReadByte();
            return GetString(reader.ReadBytes(length));
        }

        public static string GetString(byte[] data)
        {
            int length = data.Length;
            if (length == 0)
                return string.Empty;
            if (data[length - 1] == '\0')
                length--;
            return Encoding.UTF8.GetString(data, 0, length);
        }
    }
}
