using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    public class SonicNexusObjects
    {
        MapObject Blank = new MapObject("Blank Object", 0, 0, "Blank Objects Don't Need Sprites Lmao", 0, 0, 0, 0);

        MapObject StageSetup = new MapObject("Stage Setup", 1, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject HUD = new MapObject("HUD", 2, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TitleCard = new MapObject("Title Card", 3, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ActFinish = new MapObject("Act Finish", 4, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Ring = new MapObject("Ring", 5, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Ring2 = new MapObject("Lose Ring", 6, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject RingSparkle = new MapObject("Ring Sparkle", 7, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Monitor = new MapObject("Blank Monitor", 8, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BrokenMonitor = new MapObject("Broken Monitor", 9, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpringRed = new MapObject("Red Spring", 10, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpringYellow = new MapObject("Yellow Spring", 11, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Spikes = new MapObject("Spikes", 12, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject StarPost = new MapObject("StarPost", 13, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Explosion = new MapObject("Explosion", 14, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PlaneSwitchA = new MapObject("PlaneSwitch_A", 15, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PlaneSwitchB = new MapObject("PlaneSwitch_B", 16, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PlaneSwitchLoop = new MapObject("PlaneSwitch_Loop", 17, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SignPost = new MapObject("Sign Post", 18, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Invincibility = new MapObject("Invincibility", 19, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject DeathEvent = new MapObject("DeathEvent", 20, 0, "Objects\\General.gfx", 0, 0, 16, 16);


        public Dictionary<Point, MapObject> Objects = new Dictionary<Point, MapObject>();

        public SonicNexusObjects()
        {
            Objects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            Objects.Add(new Point(StageSetup.ID, StageSetup.SubType), StageSetup);
            Objects.Add(new Point(HUD.ID, HUD.SubType), HUD);
            Objects.Add(new Point(TitleCard.ID, TitleCard.SubType), TitleCard);
            Objects.Add(new Point(ActFinish.ID, ActFinish.SubType), ActFinish);
            Objects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            Objects.Add(new Point(Ring2.ID, Ring2.SubType), Ring2);
            Objects.Add(new Point(RingSparkle.ID, RingSparkle.SubType), RingSparkle);
            Objects.Add(new Point(Monitor.ID, Monitor.SubType), Monitor);
            Objects.Add(new Point(BrokenMonitor.ID, BrokenMonitor.SubType), BrokenMonitor);
            Objects.Add(new Point(SpringRed.ID, SpringRed.SubType), SpringRed);
            Objects.Add(new Point(SpringYellow.ID, SpringYellow.SubType), SpringYellow);
            Objects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);
            Objects.Add(new Point(StarPost.ID, StarPost.SubType), StarPost);
            Objects.Add(new Point(Explosion.ID, Explosion.SubType), Explosion);
            Objects.Add(new Point(PlaneSwitchA.ID, PlaneSwitchA.SubType), PlaneSwitchA);
            Objects.Add(new Point(PlaneSwitchB.ID, PlaneSwitchB.SubType), PlaneSwitchB);
            Objects.Add(new Point(PlaneSwitchLoop.ID, PlaneSwitchLoop.SubType), PlaneSwitchLoop);
            Objects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            Objects.Add(new Point(Invincibility.ID, Invincibility.SubType), Invincibility);
            Objects.Add(new Point(DeathEvent.ID, DeathEvent.SubType), DeathEvent);
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
