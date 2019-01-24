using System;
using System.Collections.Generic;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    public class ObjectDefinitions
    {
        MapObject Blank = new MapObject("Blank Object", 0, 0, "Blank Objects Don't Need Sprites Lmao",0,0,0,0);

        MapObject Ring = new MapObject("Ring", 1, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Ring2 = new MapObject("Ring (Dropped)", 2, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject RingSparkle = new MapObject("Ring Sparkle", 3, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BlankItemBox = new MapObject("Blank Item Box", 4, 0, "Objects\\General.gfx", 24, 0, 30, 32);
        MapObject RingBox = new MapObject("Ring Box", 4, 1, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BlueShield = new MapObject("Blue Shield", 4, 2, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject MagnetShield = new MapObject("Magnet Shield", 4, 3, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject FireShield = new MapObject("Fire Shield", 4, 4, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BubbleShield = new MapObject("Bubble Shield", 4, 5, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject InvincibilityMonitor = new MapObject("Invincibility Monitor", 4, 6, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject SpeedShoesMonitor = new MapObject("Speed Shoes Monitor", 4, 7, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject EggmanMonitor = new MapObject("Eggman Monitor", 4, 8, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject oneUP = new MapObject("1UP", 4, 9, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BlueRing = new MapObject("Blue Ring", 4, 10, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BrokenItemBox = new MapObject("Broken Item Box", 5, 0, "Objects\\General.gfx", 24, 0, 30, 32);

        MapObject SpringYellow = new MapObject("Yellow Spring", 6, 0, "Objects\\General.gfx", 86, 16, 32, 16);
        MapObject SpringRed = new MapObject("Red Spring", 7, 0, "Objects\\General.gfx", 86, 16, 32, 16);

        MapObject Spikes = new MapObject("Spike", 8, 0, "Objects\\General.gfx", 118, 160, 32, 32);

        MapObject Checkpoint = new MapObject("Checkpoint", 9, 0, "Objects\\General.gfx", 240, 0, 16, 48);

        MapObject SignPost = new MapObject("SignPost", 18, 0, "Objects\\General2.gfx", 64, 0, 48, 48);
        MapObject EggPrison = new MapObject("Egg Prison", 19, 0, "Objects\\General2.gfx", 64, 0, 48, 48);

        MapObject ExplodeSmall = new MapObject("Explosion Small", 20, 0, "Objects\\General2.gfx", 64, 0, 48, 48);
        MapObject ExplodeBig = new MapObject("Explosion Big", 21, 0, "Objects\\General2.gfx", 64, 0, 48, 48);
        MapObject EPDebris = new MapObject("Egg Prison Debris", 22, 0, "Objects\\General2.gfx", 64, 0, 48, 48);
        MapObject Animal = new MapObject("Animal", 23, 0, "Objects\\General2.gfx", 64, 0, 48, 48);

        MapObject BigRing = new MapObject("Big Ring", 26, 0, "Objects\\General.gfx", 256, 0, 64, 64);

        MapObject WaterSplash = new MapObject("Water Splash", 27, 0, "Objects\\General.gfx", 256, 0, 64, 64);
        MapObject BubbleSpawn = new MapObject("Air Bubble Spawner", 28, 0, "Objects\\General.gfx", 256, 0, 64, 64);
        MapObject BubbleSmall = new MapObject("Bubble Small", 29, 0, "Objects\\General.gfx", 256, 0, 64, 64);
        MapObject SmokePuff = new MapObject("Smoke Puff", 30, 0, "Objects\\General.gfx", 256, 0, 64, 64);
        MapObject WaterTrigger = new MapObject("Water Trigger", 31, 0, "Objects\\General.gfx", 256, 0, 64, 64);

        public Dictionary<Point,MapObject> Objects = new Dictionary<Point, MapObject>();

        public ObjectDefinitions()
        {

        }

        public ObjectDefinitions(int RS)
        {

            Objects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            Objects.Add(new Point(Ring2.ID, Ring2.SubType), Ring2);
            Objects.Add(new Point(RingSparkle.ID, RingSparkle.SubType), RingSparkle);

            Objects.Add(new Point(BlankItemBox.ID, BlankItemBox.SubType), BlankItemBox);
            Objects.Add(new Point(RingBox.ID, RingBox.SubType), RingBox);
            Objects.Add(new Point(BlueShield.ID, BlueShield.SubType), BlueShield);
            Objects.Add(new Point(MagnetShield.ID, MagnetShield.SubType), MagnetShield);
            Objects.Add(new Point(FireShield.ID, FireShield.SubType), FireShield);
            Objects.Add(new Point(BubbleShield.ID, BubbleShield.SubType), BubbleShield);
            Objects.Add(new Point(InvincibilityMonitor.ID, InvincibilityMonitor.SubType), InvincibilityMonitor);
            Objects.Add(new Point(SpeedShoesMonitor.ID,SpeedShoesMonitor.SubType), SpeedShoesMonitor);
            Objects.Add(new Point(EggmanMonitor.ID, EggmanMonitor.SubType), EggmanMonitor);
            Objects.Add(new Point(oneUP.ID, oneUP.SubType), oneUP);
            Objects.Add(new Point(BlueRing.ID, BlueRing.SubType), BlueRing);
            Objects.Add(new Point(BrokenItemBox.ID, BrokenItemBox.SubType), BrokenItemBox);

            Objects.Add(new Point(SpringYellow.ID, SpringYellow.SubType), SpringYellow);
            Objects.Add(new Point(SpringRed.ID, SpringRed.SubType), SpringRed);

            Objects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);

            Objects.Add(new Point(Checkpoint.ID, Checkpoint.SubType), Checkpoint);

            Objects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            Objects.Add(new Point(EggPrison.ID, EggPrison.SubType), EggPrison);

            Objects.Add(new Point(ExplodeSmall.ID, ExplodeSmall.SubType), ExplodeSmall);
            Objects.Add(new Point(ExplodeBig.ID, ExplodeBig.SubType), ExplodeBig);

            Objects.Add(new Point(EPDebris.ID, EPDebris.SubType), EPDebris);
            Objects.Add(new Point(Animal.ID, Animal.SubType), Animal);

            Objects.Add(new Point(BigRing.ID, BigRing.SubType), BigRing);

            Objects.Add(new Point(WaterSplash.ID, WaterSplash.SubType), WaterSplash);
            Objects.Add(new Point(BubbleSpawn.ID, BubbleSpawn.SubType), BubbleSpawn);
            Objects.Add(new Point(BubbleSmall.ID, BubbleSmall.SubType), BubbleSmall);
            Objects.Add(new Point(SmokePuff.ID, SmokePuff.SubType), SmokePuff);
            Objects.Add(new Point(WaterTrigger.ID, WaterTrigger.SubType), WaterTrigger);
        }

        public MapObject GetObjectByType(int Type, int Subtype)
        {
            MapObject mo = null;
            try
            {
                mo = Objects[new Point(Type, Subtype)];
            }
            catch (Exception)
            {
                mo = new MapObject();
            }

            return mo;
        }

        public void LoadObjList(string filePath)
        {
            Objects.Clear();
            System.IO.StreamReader reader = new System.IO.StreamReader(filePath);
            string Name = "";
            string Type = "";
            string SubType = "";
            string ImagePath = "";
            string SpriteImgXpos = "";
            string SpriteImgYpos = "";
            string SpriteWidth = "";
            string SpriteHeight = "";
            string pivotX = "";
            string pivotY = "";
            string flip = "";
            char buf = '>';
            int T = 0;
            int ST = 0;
            int Xpos = 0;
            int Ypos = 0;
            int Width = 0;
            int Height = 0;
            int PivotX = 0;
            int PivotY = 0;
            int Flip = 0;


            while (!reader.EndOfStream)
            {
                Name = "";
                Type = "";
                SubType = "";
                ImagePath = "";
                SpriteImgXpos = "";
                SpriteImgYpos = "";
                SpriteWidth = "";
                SpriteHeight = "";
                pivotX = "";
                pivotY = "";
                flip = "";
                buf = '>';
                T = 0;
                ST = 0;
                Xpos = 0;
                Ypos = 0;
                Width = 0;
                Height = 0;

                while (buf != ',') //Load The name
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    Name = Name + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object Type
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    Type = Type + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object SubType
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    SubType = SubType + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object's SpriteSheet
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    ImagePath = ImagePath + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object's Spritesheet Xpos
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    SpriteImgXpos = SpriteImgXpos + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object's Spritesheet Ypos
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    SpriteImgYpos = SpriteImgYpos + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object Sprite's Width
                {
                    buf = (char)reader.Read();
                    if (buf == ',') { break; }
                    SpriteWidth = SpriteWidth + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                while (buf != ',') //Load The Object Sprite's Height
                {
                    buf = (char)reader.Read();
                    if (buf == ';' || buf == ',') { break; } //detect if the line is over
                    SpriteHeight = SpriteHeight + buf;
                }
                bool Legacy = true;
                if (buf != ';') { buf = '>'; Legacy = false; } //That char shouldn't show up so change the buffer to that!

                while (buf != ',' && buf != ';' && !Legacy) //Load The Object Sprite's Height
                {
                    buf = (char)reader.Read();
                    if (buf == ';' || buf == ',') { break; } //detect if the line is over
                    pivotX = pivotX + buf;
                }
                if (buf != ';') { buf = '>'; } //That char shouldn't show up so change the buffer to that!

                while (buf != ',' && buf != ';' && !Legacy) //Load The Object Sprite's Height
                {
                    buf = (char)reader.Read();
                    if (buf == ';' || buf == ',') { break; } //detect if the line is over
                    pivotY = pivotY + buf;
                }
                if (buf != ';') { buf = '>'; } //That char shouldn't show up so change the buffer to that!  

                while (buf != ',' && buf != ';' && !Legacy) //Load The Object Sprite's Height
                {
                    buf = (char)reader.Read();
                    if (buf == ';') { break; } //detect if the line is over
                    flip = flip + buf;
                }
                buf = '>'; //That char shouldn't show up so change the buffer to that!

                T = Int32.Parse(Type);
                ST = Int32.Parse(SubType);
                Xpos = Int32.Parse(SpriteImgXpos);
                Ypos = Int32.Parse(SpriteImgYpos);
                Width = Int32.Parse(SpriteWidth);
                Height = Int32.Parse(SpriteHeight);
                if (!Legacy)
                {
                    PivotX = Int32.Parse(pivotX);
                    PivotY = Int32.Parse(pivotY);
                    Flip = Int32.Parse(flip);
                }
                MapObject MapObj;
                if (Legacy)
                {
                    MapObj = new MapObject(Name, T, ST, ImagePath, Xpos, Ypos, Width, Height);
                    Objects.Add(new Point(MapObj.ID, MapObj.SubType), MapObj);
                }
                else if (!Legacy)
                {
                    MapObj = new MapObject(Name, T, ST, ImagePath, Xpos, Ypos, Width, Height, PivotX, PivotY, Flip);
                    Objects.Add(new Point(MapObj.ID, MapObj.SubType), MapObj);
                }

                reader.ReadLine();
            }
            reader.Close();
        }

    }
}
