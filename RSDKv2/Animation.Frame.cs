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

using System.IO;

namespace RSDKv2
{
    public class Frame : IFrame
    {
        public int SpriteSheet { get; set; }

        public int CollisionBox { get; set; }

        public int Id { get => 0; set { } }

        public bool flag1 { get => false; set { } }

        public bool flag2 { get => false; set { } }

        public int Duration
        {
            get => 256;
            set { }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int CenterX { get; set; }

        public int CenterY { get; set; }

        public IHitbox GetHitbox(int index)
        {
            return new Hitbox();
        }

        public void SaveChanges(BinaryWriter writer)
        {
            Write(writer);
        }

        public void Read(BinaryReader reader)
        {
            // byte 1 - Number of image the frame is located in
            // byte 2 - UnKnown/Useless		
            // byte 3 - X position in image of the frame
            // byte 4 - Y position in image of the frame
            // byte 5 - Width of frame
            // byte 6 - Height of frame
            // byte 7 - Hot spot horizontal displacement
            // byte 8 - Hot spot vertical displacement
            SpriteSheet = reader.ReadByte();
            reader.ReadByte();
            X = reader.ReadByte();
            Y = reader.ReadByte();
            Width = reader.ReadByte();
            Height = reader.ReadByte();
            CenterX = reader.ReadSByte();
            CenterY = reader.ReadSByte();
            flag1 = false; // UNKNOWN
            flag2 = false; // UNKNOWN
            Id = 0;
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)SpriteSheet);
            writer.Write((byte)0);
            writer.Write((byte)X);
            writer.Write((byte)Y);
            writer.Write((byte)Width);
            writer.Write((byte)Height);
            writer.Write((byte)CenterX);
            writer.Write((byte)CenterY);
        }

        public object Clone()
        {
            return new Frame()
            {
                SpriteSheet = SpriteSheet,
                CollisionBox = CollisionBox,
                X = X,
                Y = Y,
                Width = Width,
                Height = Height,
                CenterX = CenterX,
                CenterY = CenterY
            };
        }
    }
}
