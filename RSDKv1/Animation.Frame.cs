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
using RSDK;

namespace RSDK1
{
    public class Frame : IFrame
    {
        public int SpriteSheet { get; set; }

        public int CollisionBox { get; set; }

        public int Id { get => 0; set { } }

        public int flag1 { get; set; }

        public int flag2 { get; set; }

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

        public int[] Hitbox;

        public int[] PivotVals;

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
            // byte 1 - Image's X Position
            // byte 2 - Image's Y Position		
            // byte 3 - Width
            // byte 4 - Height
            // byte 5 - Image Number
            // byte 6 - Hitbox Left
            // byte 7 - Hitbox Top
            // byte 8 - Hitbox Right
            // byte 9 - Hitbox Bottom
            // byte 10 - Center X
            // byte 11 - Center Y
            X = reader.ReadByte();
            Y = reader.ReadByte();
            Width = reader.ReadByte();
            Height = reader.ReadByte();
            SpriteSheet = reader.ReadByte();
            Id = 0;

            Hitbox = new int[4];
            PivotVals = new int[2];

            for (int k = 0; k < 4; k++)
            { Hitbox[k] = reader.ReadByte(); }

            for (int k = 0; k < 2; k++)
            { PivotVals[k] = reader.ReadByte(); }

            // Compute hotspot displacements
            CenterX = Hitbox[2] - PivotVals[0]; //PivotVal[0] is the true Value, this calculation is just done so the animation looks right upon playback
            CenterY = Hitbox[3] - PivotVals[1]; //PivotVal[1] is the true Value, this calculation is just done so the animation looks right upon playback
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write((byte)X);
            writer.Write((byte)Y);
            writer.Write((byte)Width);
            writer.Write((byte)Height);
            writer.Write((byte)SpriteSheet);
            writer.Write((byte)Hitbox[0]); //Hitbox values, Left
            writer.Write((byte)Hitbox[1]); //Hitbox values, Top
            writer.Write((byte)Hitbox[2]); //Hitbox values, Right
            writer.Write((byte)Hitbox[3]); //Hitbox values, Bottom
            writer.Write((byte)PivotVals[0]); 
            writer.Write((byte)PivotVals[1]);
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
