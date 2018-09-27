using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Globalization;

namespace RetroED.Tools.CollisionEditor
{
    public static class Extensions
    {
        public static T GetValueOrDefault<T>(this T[,] array, int x, int y)
        {
            if (x < array.GetLength(0) & y < array.GetLength(1))
                return array[x, y];
            return default(T);
        }

        public static byte[] ReadNybbles(this BinaryReader br, int count)
        {
            int bytes = count / 2;
            if ((count & 1) == 1) bytes++;
            return br.ReadBytes(bytes);
        }

        public static Color FindNearestMatch(this Color col, out int distance, params Color[] palette)
        {
            if (Array.IndexOf(palette, col) != -1)
            {
                distance = 0;
                return col;
            }
            Color nearest_color = Color.Empty;
            distance = int.MaxValue;
            foreach (Color o in palette)
            {
                int test_red = o.R - col.R;
                test_red *= test_red;
                int test_green = o.G - col.G;
                test_green *= test_green;
                int test_blue = o.B - col.B;
                test_blue *= test_blue;
                int temp = test_blue + test_green + test_red;
                if (temp == 0)
                    return o;
                else if (temp < distance)
                {
                    distance = temp;
                    nearest_color = o;
                }
            }
            return nearest_color;
        }

        public static Color FindNearestMatch(this Color col, params Color[] palette)
        {
            return FindNearestMatch(col, out int distance, palette);
        }

        /// <summary>
        /// Resizes the <see cref="Bitmap" />, maintaining the original aspect ratio.
        /// </summary>
        public static Bitmap Resize(this Bitmap image, Size newsize)
        {
            Bitmap bmp = new Bitmap(newsize.Width, newsize.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.Clear(Color.Transparent);
            int mywidth = image.Width;
            int myheight = image.Height;
            while (myheight > newsize.Height | mywidth > newsize.Width)
            {
                if (mywidth > newsize.Width)
                {
                    mywidth = newsize.Width;
                    myheight = (int)(image.Height * ((double)newsize.Width / image.Width));
                }
                else if (myheight > newsize.Height)
                {
                    myheight = newsize.Height;
                    mywidth = (int)(image.Width * ((double)newsize.Height / image.Height));
                }
            }
            gfx.DrawImage(image, (int)(((double)newsize.Width - mywidth) / 2), (int)(((double)newsize.Height - myheight) / 2), mywidth, myheight);
            return bmp;
        }

        /// <summary>
        /// Sets options to enable faster rendering.
        /// </summary>
        public static void SetOptions(this Graphics gfx)
        {
            gfx.CompositingQuality = CompositingQuality.HighQuality;
            gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
            gfx.PixelOffsetMode = PixelOffsetMode.None;
            gfx.SmoothingMode = SmoothingMode.HighSpeed;
        }

        public static Bitmap Copy(this Bitmap bmp)
        {
            BitmapData bmpd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            Bitmap newbmp = new Bitmap(bmpd.Width, bmpd.Height, bmpd.PixelFormat);
            BitmapData newbmpd = newbmp.LockBits(new Rectangle(0, 0, bmpd.Width, bmpd.Height), ImageLockMode.WriteOnly, bmpd.PixelFormat);
            byte[] bytes = new byte[Math.Abs(bmpd.Stride) * bmpd.Height];
            Marshal.Copy(bmpd.Scan0, bytes, 0, bytes.Length);
            Marshal.Copy(bytes, 0, newbmpd.Scan0, bytes.Length);
            bmp.UnlockBits(bmpd);
            newbmp.UnlockBits(newbmpd);
            newbmp.Palette = bmp.Palette;
            return newbmp;
        }

        public static Rectangle Flip(this Rectangle rect, bool xflip, bool yflip)
        {
            if (xflip)
                rect.X = -rect.Right;
            if (yflip)
                rect.Y = -rect.Bottom;
            return rect;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
        {
            if (dict.TryGetValue(key, out TValue output))
                return output;
            return @default;
        }

        public static TKey GetKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, TValue value)
        {
            bool found = false;
            TKey result = default(TKey);
            foreach (KeyValuePair<TKey, TValue> item in dict)
                if (item.Value.Equals(value))
                {
                    found = true;
                    result = item.Key;
                }
            if (found) return result;
            throw new KeyNotFoundException();
        }

        public static TKey GetKeyOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TValue value, TKey @default)
        {
            foreach (KeyValuePair<TKey, TValue> item in dict)
                if (item.Value.Equals(value))
                    @default = item.Key;
            return @default;
        }

        public static Dictionary<TValue, TKey> Swap<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            Dictionary<TValue, TKey> result = new Dictionary<TValue, TKey>(dict.Count);
            foreach (KeyValuePair<TKey, TValue> item in dict)
                result.Add(item.Value, item.Key);
            return result;
        }

        public static string MakeIdentifier(this string name)
        {
            StringBuilder result = new StringBuilder();
            foreach (char item in name)
                if ((item >= '0' & item <= '9') | (item >= 'A' & item <= 'Z') | (item >= 'a' & item <= 'z') | item == '_')
                    result.Append(item);
            if (result[0] >= '0' & result[0] <= '9')
                result.Insert(0, '_');
            return result.ToString();
        }

        public static string ToHex68k(this byte number)
        {
            if (number < 10)
                return number.ToString(NumberFormatInfo.InvariantInfo);
            else
                return "$" + number.ToString("X");
        }

        public static string ToHex68k(this sbyte number)
        {
            if (number > -1)
                if (number < 10)
                    return number.ToString(NumberFormatInfo.InvariantInfo);
                else
                    return "$" + number.ToString("X");
            else if (number == sbyte.MinValue)
                return "$80";
            else
                return "-" + Math.Abs(number).ToHex68k();
        }

        public static string ToHex68k(this ushort number)
        {
            if (number < 10)
                return number.ToString(NumberFormatInfo.InvariantInfo);
            else
                return "$" + number.ToString("X");
        }

        public static string ToHex68k(this short number)
        {
            if (number > -1)
                if (number < 10)
                    return number.ToString(NumberFormatInfo.InvariantInfo);
                else
                    return "$" + number.ToString("X");
            else if (number == short.MinValue)
                return "$8000";
            else
                return "-" + Math.Abs(number).ToHex68k();
        }

        public static bool ArrayEqual<T>(this T[] arr1, T[] arr2)
        {
            if (arr1 == arr2) return true;
            if (arr1.Length != arr2.Length) return false;
            for (int i = 0; i < arr1.Length; i++)
                if (!arr1[i].Equals(arr2[i]))
                    return false;
            return true;
        }

        public static bool ListEqual<T>(this IList<T> lst1, IList<T> lst2)
        {
            if (lst1 == lst2) return true;
            if (lst1.Count != lst2.Count) return false;
            for (int i = 0; i < lst1.Count; i++)
                if (!lst1[i].Equals(lst2[i]))
                    return false;
            return true;
        }

        public static void Fill<T>(this T[] arr, T item, int startIndex, int length)
        {
            if (length == 0) return;
            if (startIndex < 0 || startIndex >= arr.Length) throw new ArgumentOutOfRangeException("startIndex");
            if (length < 0 || startIndex + length > arr.Length) throw new ArgumentOutOfRangeException("length");
            for (int i = startIndex; i < startIndex + length; i++)
                arr[i] = item;
        }

        public static void Fill<T>(this T[] arr, T item)
        {
            for (int i = 0; i < arr.Length; i++)
                arr[i] = item;
        }
    }
}
