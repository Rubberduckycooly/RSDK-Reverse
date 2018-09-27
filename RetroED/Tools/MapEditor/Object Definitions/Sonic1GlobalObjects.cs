using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    public class Sonic1Objects
    {
        MapObject Blank = new MapObject("Blank Object", 0, 0, "Blank Objects Don't Need Sprites Lmao", 0, 0, 0, 0);

        MapObject Player = new MapObject("Player Spawn", 1, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TailsTail = new MapObject("Tails' Tail", 2, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Player2 = new MapObject("Player 2", 3, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject StageSetup = new MapObject("Stage Setup", 4, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject HUD = new MapObject("HUD", 5, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TitleCard = new MapObject("Title Card", 6, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject DeathEvent = new MapObject("Death Event", 7, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ActFinish = new MapObject("Act Finish", 8, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject debugMode = new MapObject("Debug Mode", 9, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Ring = new MapObject("Ring", 10, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Ring2 = new MapObject("Lose Ring", 11, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject RingSparkle = new MapObject("Ring Sparkle", 12, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BlankMonitor = new MapObject("Blank Monitor", 13, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject RingMonitor = new MapObject("Ring Monitor", 13, 1, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ShieldMonitor = new MapObject("Shield Monitor", 13, 2, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject InvincMonitor = new MapObject("Invincibility Monitor", 13, 3, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpeedMonitor = new MapObject("Speed Shoes Monitor", 13, 4, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject OneUPsMonitor = new MapObject("1UP (Sonic) Monitor", 13, 5, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject OneUPtMonitor = new MapObject("1UP (Tails) Monitor", 13, 6, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject OneUPkMonitor = new MapObject("1UP (Knuckles) Monitor", 13, 7, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SuperMonitor = new MapObject("Super Monitor", 13, 8, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BubbleMonitor = new MapObject("Bubble Shield Monitor", 13, 9, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject FireMonitor = new MapObject("Fire Shield Monitor", 13, 10, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject LightningMonitor = new MapObject("Lightning Shield Monitor", 13, 11, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject EggmanMonitor = new MapObject("Eggman Monitor", 13, 12, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TeleportMonitor = new MapObject("Teleport Monitor", 13, 13, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject RandomMonitor = new MapObject("Random Monitor", 13, 14, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ShieldModeMonitor = new MapObject("Shield Mode Toggle Monitor", 13, 15, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject EmeraldToggleMonitor = new MapObject("Emerald Toggle Monitor", 13, 16, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject MonitorBroken = new MapObject("Broken Monitor", 14, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject RedSpring = new MapObject("Red Spring", 15, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject YellowSpring = new MapObject("Yellow Spring", 16, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Checkpoint = new MapObject("Checkpoint", 17, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SmokePuff = new MapObject("Smoke Puff", 18, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject DustPuff = new MapObject("Dust Puff", 19, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SignPost = new MapObject("SignPost", 20, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject EggPrison = new MapObject("Animal Prison", 21, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpecialRing = new MapObject("Special Ring", 22, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Explosion = new MapObject("Explosion", 23, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BlueShield = new MapObject("Blue Shield", 24, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BlueShield2 = new MapObject("Blue Shield (S2)", 25, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject InstaShield = new MapObject("Insta Shield", 26, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BubbleShield = new MapObject("Bubble Shield", 27, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject FireShield = new MapObject("Fire Shield", 28, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject LightningShield = new MapObject("Lightning Shield", 29, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject LightningSpark = new MapObject("Lightning Spark", 30, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Invincibility = new MapObject("Invincibility", 31, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Invincibility2 = new MapObject("Invincibility (S2)", 32, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SuperSpark = new MapObject("Super Spark", 33, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BoundsMarker = new MapObject("Bounds Marker", 34, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Spikes = new MapObject("Spikes", 35, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ObjectScore = new MapObject("Object Score", 36, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BonusPoints = new MapObject("Bonus Points", 37, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject MusicEvent = new MapObject("Music Event", 38, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject PlaneSwitchH = new MapObject("Plane Switch H", 39, 0, "Objects\\General.gfx", 0, 0, 16, 16); //Unsure About these ones
        //MapObject PlaneSwitchV = new MapObject("Plane Switch V", 40, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject InvisibleBlock = new MapObject("Invisible Block", 41, 0, "Objects\\General.gfx", 0, 0, 16, 16);


        public Dictionary<Point, MapObject> Objects = new Dictionary<Point, MapObject>();

        public Sonic1Objects()
        {
            Objects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            Objects.Add(new Point(Player.ID, Player.SubType), Player);
            Objects.Add(new Point(TailsTail.ID, TailsTail.SubType), TailsTail);
            Objects.Add(new Point(Player2.ID, Player2.SubType), Player2);
            Objects.Add(new Point(StageSetup.ID, StageSetup.SubType), StageSetup);
            Objects.Add(new Point(HUD.ID, HUD.SubType), HUD);
            Objects.Add(new Point(TitleCard.ID, TitleCard.SubType), TitleCard);
            Objects.Add(new Point(DeathEvent.ID, DeathEvent.SubType), DeathEvent);
            Objects.Add(new Point(ActFinish.ID, ActFinish.SubType), ActFinish);
            Objects.Add(new Point(debugMode.ID, debugMode.SubType), debugMode);
            Objects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            Objects.Add(new Point(Ring2.ID, Ring2.SubType), Ring2);
            Objects.Add(new Point(RingSparkle.ID, RingSparkle.SubType), RingSparkle);

            Objects.Add(new Point(BlankMonitor.ID, BlankMonitor.SubType), BlankMonitor);
            Objects.Add(new Point(RingMonitor.ID, RingMonitor.SubType), RingMonitor);
            Objects.Add(new Point(ShieldMonitor.ID, ShieldMonitor.SubType), ShieldMonitor);
            Objects.Add(new Point(InvincMonitor.ID, InvincMonitor.SubType), InvincMonitor);
            Objects.Add(new Point(SpeedMonitor.ID, SpeedMonitor.SubType), SpeedMonitor);
            Objects.Add(new Point(OneUPsMonitor.ID, OneUPsMonitor.SubType), OneUPsMonitor);
            Objects.Add(new Point(OneUPtMonitor.ID, OneUPtMonitor.SubType), OneUPtMonitor);
            Objects.Add(new Point(OneUPkMonitor.ID, OneUPkMonitor.SubType), OneUPkMonitor);
            Objects.Add(new Point(BubbleMonitor.ID, BubbleMonitor.SubType), BubbleMonitor);
            Objects.Add(new Point(FireMonitor.ID, FireMonitor.SubType), FireMonitor);
            Objects.Add(new Point(LightningMonitor.ID, LightningMonitor.SubType), LightningMonitor);
            Objects.Add(new Point(EggmanMonitor.ID, EggmanMonitor.SubType), EggmanMonitor);
            Objects.Add(new Point(TeleportMonitor.ID, TeleportMonitor.SubType), TeleportMonitor);
            Objects.Add(new Point(RandomMonitor.ID, RandomMonitor.SubType), RandomMonitor);
            Objects.Add(new Point(ShieldModeMonitor.ID, ShieldModeMonitor.SubType), ShieldModeMonitor);
            Objects.Add(new Point(EmeraldToggleMonitor.ID, EmeraldToggleMonitor.SubType), EmeraldToggleMonitor);
            Objects.Add(new Point(MonitorBroken.ID, MonitorBroken.SubType), MonitorBroken);

            Objects.Add(new Point(RedSpring.ID, RedSpring.SubType), RedSpring);
            Objects.Add(new Point(YellowSpring.ID, YellowSpring.SubType), YellowSpring);

            Objects.Add(new Point(Checkpoint.ID, Checkpoint.SubType), Checkpoint);
            Objects.Add(new Point(SmokePuff.ID, SmokePuff.SubType), SmokePuff);
            Objects.Add(new Point(DustPuff.ID, DustPuff.SubType), DustPuff);

            Objects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            Objects.Add(new Point(EggPrison.ID, EggPrison.SubType), EggPrison);
            Objects.Add(new Point(Explosion.ID, Explosion.SubType), Explosion);
            Objects.Add(new Point(SpecialRing.ID, SpecialRing.SubType), SpecialRing);

            Objects.Add(new Point(BlueShield.ID, BlueShield.SubType), BlueShield);
            Objects.Add(new Point(BlueShield2.ID, BlueShield2.SubType), BlueShield2);
            Objects.Add(new Point(InstaShield.ID, InstaShield.SubType), InstaShield);
            Objects.Add(new Point(BubbleShield.ID, BubbleShield.SubType), BubbleShield);
            Objects.Add(new Point(FireShield.ID, FireShield.SubType), FireShield);
            Objects.Add(new Point(LightningShield.ID, LightningShield.SubType), LightningShield);
            Objects.Add(new Point(LightningSpark.ID, LightningSpark.SubType), LightningSpark);
            Objects.Add(new Point(Invincibility.ID, Invincibility.SubType), Invincibility);
            Objects.Add(new Point(Invincibility2.ID, Invincibility2.SubType), Invincibility2);
            Objects.Add(new Point(SuperSpark.ID, SuperSpark.SubType), SuperSpark);

            Objects.Add(new Point(BoundsMarker.ID, BoundsMarker.SubType), BoundsMarker);
            Objects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);
            Objects.Add(new Point(ObjectScore.ID, ObjectScore.SubType), ObjectScore);
            Objects.Add(new Point(BonusPoints.ID, BonusPoints.SubType), BonusPoints);
            Objects.Add(new Point(MusicEvent.ID, MusicEvent.SubType), MusicEvent);
            //Objects.Add(new Point(PlaneSwitchH.ID, PlaneSwitchH.SubType), PlaneSwitchH);
            //Objects.Add(new Point(PlaneSwitchV.ID, PlaneSwitchV.SubType), PlaneSwitchV);
            //Objects.Add(new Point(InvisibleBlock.ID, InvisibleBlock.SubType), InvisibleBlock);
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
