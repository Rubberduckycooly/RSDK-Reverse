using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    class SonicCDGlobalObjects
    {
        MapObject Blank = new MapObject();

        MapObject Player = new MapObject("Player Spawn", 1, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject StageSetup = new MapObject("Stage Setup", 2, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject HUD = new MapObject("HUD", 3, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TitleCard = new MapObject("Title Card", 4, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject DeathEvent = new MapObject("Death Event", 5, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TailsTail = new MapObject("Tails' Tail", 6, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PauseMenu = new MapObject("Pause Menu", 7, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ActFinish = new MapObject("Act Finish", 8, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Ring = new MapObject("Ring", 9, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Ring2 = new MapObject("Lose Ring", 10, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject RingSparkle = new MapObject("Ring Sparkle", 11, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BlankMonitor = new MapObject("Blank Monitor", 12, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject RingMonitor = new MapObject("Ring Monitor", 13, 1, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject ShieldMonitor = new MapObject("Shield Monitor", 13, 2, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject InvincMonitor = new MapObject("Invincibility Monitor", 13, 3, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject SpeedMonitor = new MapObject("Speed Shoes Monitor", 13, 4, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject OneUPsMonitor = new MapObject("1UP (Sonic) Monitor", 13, 5, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject OneUPtMonitor = new MapObject("1UP (Tails) Monitor", 13, 6, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject OneUPkMonitor = new MapObject("1UP (Knuckles) Monitor", 13, 7, "Objects\\General.gfx", 0, 0, 16, 16);
        //MapObject EggmanMonitor = new MapObject("Eggman Monitor", 13, 12, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject MonitorBroken = new MapObject("Broken Monitor", 13, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject RedSpring = new MapObject("Red Spring", 14, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject YellowSpring = new MapObject("Yellow Spring", 15, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Checkpoint = new MapObject("Checkpoint", 16, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject FuturePost = new MapObject("Future Post", 17, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PastPost = new MapObject("Past Post", 18, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SignPost = new MapObject("Sign Post", 19, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject GoalPost = new MapObject("Goal Post", 20, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpecialRing = new MapObject("Special Ring", 21, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Spikes = new MapObject("Spikes", 22, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SmokePuff = new MapObject("Smoke Puff", 23, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Explosion = new MapObject("Explosion", 24, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject DustPuff = new MapObject("Dust Puff", 25, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BlueShield = new MapObject("Blue Shield", 26, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Invincibility = new MapObject("Invincibility", 27, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject WarpStar = new MapObject("Warp Star", 28, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TimeWarp = new MapObject("Time Warp", 29, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject WarpSonic = new MapObject("Warp Sonic", 30, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Transporter = new MapObject("Transporter", 31, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject MSProjector = new MapObject("Metal Sonic Projector", 32, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ObjectScore = new MapObject("Object Score", 33, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject debugMode = new MapObject("Debug Mode", 34, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        public Dictionary<Point, MapObject> GlobalObjects = new Dictionary<Point, MapObject>();

        public SonicCDGlobalObjects()
        {
            GlobalObjects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            GlobalObjects.Add(new Point(Player.ID, Player.SubType), Player);
            GlobalObjects.Add(new Point(StageSetup.ID, StageSetup.SubType), StageSetup);
            GlobalObjects.Add(new Point(HUD.ID, HUD.SubType), HUD);
            GlobalObjects.Add(new Point(TitleCard.ID, TitleCard.SubType), TitleCard);
            GlobalObjects.Add(new Point(DeathEvent.ID, DeathEvent.SubType), DeathEvent);
            GlobalObjects.Add(new Point(TailsTail.ID, TailsTail.SubType), TailsTail);
            GlobalObjects.Add(new Point(ActFinish.ID, ActFinish.SubType), ActFinish);

            GlobalObjects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            GlobalObjects.Add(new Point(Ring2.ID, Ring2.SubType), Ring2);
            GlobalObjects.Add(new Point(RingSparkle.ID, RingSparkle.SubType), RingSparkle);

            GlobalObjects.Add(new Point(BlankMonitor.ID, BlankMonitor.SubType), BlankMonitor);
            //GlobalObjects.Add(new Point(RingMonitor.ID, RingMonitor.SubType), RingMonitor);
            //GlobalObjects.Add(new Point(ShieldMonitor.ID, ShieldMonitor.SubType), ShieldMonitor);
            //GlobalObjects.Add(new Point(InvincMonitor.ID, InvincMonitor.SubType), InvincMonitor);
            //GlobalObjects.Add(new Point(SpeedMonitor.ID, SpeedMonitor.SubType), SpeedMonitor);
            //GlobalObjects.Add(new Point(OneUPsMonitor.ID, OneUPsMonitor.SubType), OneUPsMonitor);
            //GlobalObjects.Add(new Point(OneUPtMonitor.ID, OneUPtMonitor.SubType), OneUPtMonitor);
            GlobalObjects.Add(new Point(MonitorBroken.ID, MonitorBroken.SubType), MonitorBroken);

            GlobalObjects.Add(new Point(RedSpring.ID, RedSpring.SubType), RedSpring);
            GlobalObjects.Add(new Point(YellowSpring.ID, YellowSpring.SubType), YellowSpring);

            GlobalObjects.Add(new Point(Checkpoint.ID, Checkpoint.SubType), Checkpoint);
            GlobalObjects.Add(new Point(FuturePost.ID, FuturePost.SubType), FuturePost);
            GlobalObjects.Add(new Point(PastPost.ID, PastPost.SubType), PastPost);
            GlobalObjects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            GlobalObjects.Add(new Point(GoalPost.ID, GoalPost.SubType), GoalPost);
            GlobalObjects.Add(new Point(SpecialRing.ID, SpecialRing.SubType), SpecialRing);

            GlobalObjects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);
            GlobalObjects.Add(new Point(SmokePuff.ID, SmokePuff.SubType), SmokePuff);
            GlobalObjects.Add(new Point(Explosion.ID, Explosion.SubType), Explosion);
            GlobalObjects.Add(new Point(DustPuff.ID, DustPuff.SubType), DustPuff);

            GlobalObjects.Add(new Point(BlueShield.ID, BlueShield.SubType), BlueShield);
            GlobalObjects.Add(new Point(Invincibility.ID, Invincibility.SubType), Invincibility);
            GlobalObjects.Add(new Point(WarpStar.ID, WarpStar.SubType), WarpStar);
            GlobalObjects.Add(new Point(TimeWarp.ID, TimeWarp.SubType), TimeWarp);
            GlobalObjects.Add(new Point(WarpSonic.ID, WarpSonic.SubType), WarpSonic);

            GlobalObjects.Add(new Point(Transporter.ID, Transporter.SubType), Transporter);
            GlobalObjects.Add(new Point(MSProjector.ID, MSProjector.SubType), MSProjector);
            GlobalObjects.Add(new Point(ObjectScore.ID, ObjectScore.SubType), ObjectScore);
            GlobalObjects.Add(new Point(debugMode.ID, debugMode.SubType), debugMode);
        }

        public MapObject GetObjectByType(int Type, int Subtype)
        {
            MapObject mo = null;
            try
            {
                mo = GlobalObjects[new Point(Type, Subtype)];
            }
            catch (Exception)
            {
                Console.WriteLine("That Object Wasn't on the list!");
                mo = new MapObject();
            }

            return mo;
        }
    }
}
