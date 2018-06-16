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

namespace RSDKv3
{
    public class Animation : IAnimation
    {
        public int Version => 3;

        public List<string> SpriteSheets { get; }

        public List<AnimationEntry> Animations { get; }

        public List<HitboxEntry> Hitboxes { get; }

        public IEnumerable<string> HitboxTypes => null;

        public Animation(BinaryReader reader)
        {
            int spriteSheetsCount = reader.ReadByte();
            SpriteSheets = new List<string>(spriteSheetsCount);
            while (spriteSheetsCount-- > 0)
                SpriteSheets.Add(StringEncoding.GetString(reader));

            var animationsCount = reader.ReadByte();
            Animations = new List<AnimationEntry>(animationsCount);
            while (animationsCount-- > 0)
                Animations.Add(new AnimationEntry(reader));

            var collisionBoxesCount = reader.ReadByte();
            Hitboxes = new List<HitboxEntry>(collisionBoxesCount);
            while (collisionBoxesCount-- > 0)
                Hitboxes.Add(new HitboxEntry(reader));
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
            return Hitboxes.Select(x => (IHitboxEntry)x);
        }

        public void SetHitboxes(IEnumerable<IHitboxEntry> hitboxes)
        {
            Hitboxes.Clear();
            Hitboxes.AddRange(hitboxes
                .Select(x => x as HitboxEntry)
                .Where(x => x != null));
        }
        public void SetHitboxTypes(IEnumerable<string> hitboxTypes)
        { }


        public void SaveChanges(BinaryWriter writer)
        {
            var spriteSheetsCount = (byte)Math.Min(SpriteSheets.Count, byte.MaxValue);
            writer.Write(spriteSheetsCount);
            for (int i = 0; i < spriteSheetsCount; i++)
            {
                var item = SpriteSheets[i];
                writer.Write(StringEncoding.GetBytes(item));
            }

            var animationsCount = (byte)Math.Min(Animations.Count, byte.MaxValue);
            writer.Write(animationsCount);
            for (int i = 0; i < animationsCount; i++)
            {
                Animations[i].SaveChanges(writer);
            }

            var collisionBoxesCount = (byte)Math.Min(Hitboxes.Count, byte.MaxValue);
            writer.Write(collisionBoxesCount);
            for (int i = 0; i < collisionBoxesCount; i++)
            {
                Hitboxes[i].SaveChanges(writer);
            }
        }
    }
}
