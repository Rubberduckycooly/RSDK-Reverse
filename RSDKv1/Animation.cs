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

using RSDK;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSDK1
{
    public class Animation : IAnimation
    {
        public int Version => 1;

        public List<string> SpriteSheets { get; }

        public byte PlayerType = 0;

        public List<AnimationEntry> Animations { get; }

        public List<HitboxEntry> Hitboxes { get; }

        public IEnumerable<string> HitboxTypes => null;

        public Animation(BinaryReader reader, bool RSDC)
        {
            reader.ReadByte(); //skip this byte, as it seems unused
            PlayerType = reader.ReadByte(); //Tells the engine what player is selected; It is 0 for sonic, 1 for tails & 2 for Knux, so maybe it specifies a player value?
            int spriteSheetsCount = 3;
            if (RSDC) //The Dreamcast Demo of retro-sonic only had 2 spritesheets per animation...
            {
                spriteSheetsCount = 2;
            }
            else //But the PC demo has 3 spritesheets per animation! so we set that here!
            {
                spriteSheetsCount = 3;
            }
            var animationsCount = reader.ReadByte();

            SpriteSheets = new List<string>(spriteSheetsCount);

            byte[] byteBuf = null;

            for (int i = 0; i < spriteSheetsCount; i++)
            {
                int sLen = reader.ReadByte();
                byteBuf = new byte[sLen];

                byteBuf = reader.ReadBytes(sLen);

                string result = System.Text.Encoding.UTF8.GetString(byteBuf);

                SpriteSheets.Add(result);
                Console.WriteLine(result);
            }
            byteBuf = null;

            // Read number of animations		
            Animations = new List<AnimationEntry>(animationsCount);

            for (int i = 0; i < animationsCount; i++)
            {
                // In the 3 bytes:
                // byte 1 - Number of frames
                // byte 2 - Animation speed
                // byte 3 - Frame to start looping from, when looping

                // read frame count	
                int frameCount = reader.ReadByte();
                //read Animation Speed
                int animationSpeed = reader.ReadByte() * 4;
                //read Loop Index
                int loopFrom = reader.ReadByte();

                //The Retro Sonic Animation Files Don't Have Names, so let's give them "ID's" instead
                Animations.Add(new AnimationEntry(("Retro Sonic Animation #" + (i+1)), frameCount, animationSpeed,
                    loopFrom, false, false, reader));
                }
            }

        public void Factory(out IAnimationEntry o) { o = new AnimationEntry(); }
        public void Factory(out IFrame o) { o = new Frame(); }
        public void Factory(out IHitboxEntry o) { o = new HitboxEntry(); }

        public IEnumerable<IAnimationEntry> GetAnimations()
        {
            return Animations.Select(x => (IAnimationEntry)x);
        }

        public void SetAnimations(IEnumerable<IAnimationEntry> animations)
        {
            Animations.Clear();
            Animations.AddRange(animations
                .Select(x => x as AnimationEntry)
                .Where(x => x != null));
        }

        public IEnumerable<IHitboxEntry> GetHitboxes()
        {
            //return Hitboxes.Select(x => (IHitboxEntry)x);
            return null;
        }

        public void SetHitboxes(IEnumerable<IHitboxEntry> hitboxes)
        {           
            /*Hitboxes.Clear();
            Hitboxes.AddRange(hitboxes
                .Select(x => x as HitboxEntry)
                .Where(x => x != null));*/
        }
        public void SetHitboxTypes(IEnumerable<string> hitboxTypes)
        { }

        public void SaveChanges(BinaryWriter writer)
        {
            writer.Write((byte)0);
            writer.Write((byte)0);
            var animationsCount = (byte)Math.Min(Animations.Count, byte.MaxValue);
            writer.Write(animationsCount);

            var spriteSheetsCount = (byte)Math.Min(SpriteSheets.Count, byte.MaxValue);
            for (int i = 0; i < spriteSheetsCount; i++)
            {
                var item = SpriteSheets[i];
                writer.Write(StringEncoding.GetBytes(item));
            }

            for (int i = 0; i < animationsCount; i++)
            {
                Animations[i].SaveChanges(writer);
            }
        }
    }
}
