using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    class Sonic2GlobalObjects
    {
        MapObject Blank = new MapObject();

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

        MapObject Spikes = new MapObject("Spikes", 17, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Checkpoint = new MapObject("Checkpoint", 18, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SmokePuff = new MapObject("Smoke Puff", 19, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject DustPuff = new MapObject("Dust Puff", 20, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SignPost = new MapObject("SignPost", 21, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject EggPrison = new MapObject("Animal Prison", 22, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Explosion = new MapObject("Explosion", 23, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BlueShield = new MapObject("Blue Shield", 24, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject InstaShield = new MapObject("Insta Shield", 25, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BubbleShield = new MapObject("Bubble Shield", 26, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject FireShield = new MapObject("Fire Shield", 27, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject LightningShield = new MapObject("Lightning Shield", 28, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject LightningSpark = new MapObject("Lightning Spark", 29, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Invincibility = new MapObject("Invincibility", 30, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SuperSpark = new MapObject("Super Spark", 31, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BoundsMarker = new MapObject("Bounds Marker", 32, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject ObjectScore = new MapObject("Object Score", 33, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject MusicEvent = new MapObject("Music Event", 34, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PlaneSwitchH = new MapObject("Plane Switch H", 35, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PlaneSwitchV = new MapObject("Plane Switch V", 36, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject VSGame = new MapObject("VS Game", 37, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject NextStageTrigger = new MapObject("Next Stage Trigger", 38, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject InvisibleBlock = new MapObject("Invisible Block", 39, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpringBoard = new MapObject("SpringBoard", 40, 0, "Objects\\General.gfx", 0, 0, 16, 16);


        public Dictionary<Point, MapObject> GlobalObjects = new Dictionary<Point, MapObject>();

        public Sonic2GlobalObjects()
        {
            GlobalObjects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            GlobalObjects.Add(new Point(Player.ID, Player.SubType), Player);
            GlobalObjects.Add(new Point(TailsTail.ID, TailsTail.SubType), TailsTail);
            GlobalObjects.Add(new Point(Player2.ID, Player2.SubType), Player2);
            GlobalObjects.Add(new Point(StageSetup.ID, StageSetup.SubType), StageSetup);
            GlobalObjects.Add(new Point(HUD.ID, HUD.SubType), HUD);
            GlobalObjects.Add(new Point(TitleCard.ID, TitleCard.SubType), TitleCard);
            GlobalObjects.Add(new Point(DeathEvent.ID, DeathEvent.SubType), DeathEvent);
            GlobalObjects.Add(new Point(ActFinish.ID, ActFinish.SubType), ActFinish);
            GlobalObjects.Add(new Point(debugMode.ID, debugMode.SubType), debugMode);
            GlobalObjects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            GlobalObjects.Add(new Point(Ring2.ID, Ring2.SubType), Ring2);
            GlobalObjects.Add(new Point(RingSparkle.ID, RingSparkle.SubType), RingSparkle);

            GlobalObjects.Add(new Point(BlankMonitor.ID, BlankMonitor.SubType), BlankMonitor);
            GlobalObjects.Add(new Point(RingMonitor.ID, RingMonitor.SubType), RingMonitor);
            GlobalObjects.Add(new Point(ShieldMonitor.ID, ShieldMonitor.SubType), ShieldMonitor);
            GlobalObjects.Add(new Point(InvincMonitor.ID, InvincMonitor.SubType), InvincMonitor);
            GlobalObjects.Add(new Point(SpeedMonitor.ID, SpeedMonitor.SubType), SpeedMonitor);
            GlobalObjects.Add(new Point(OneUPsMonitor.ID, OneUPsMonitor.SubType), OneUPsMonitor);
            GlobalObjects.Add(new Point(OneUPtMonitor.ID, OneUPtMonitor.SubType), OneUPtMonitor);
            GlobalObjects.Add(new Point(OneUPkMonitor.ID, OneUPkMonitor.SubType), OneUPkMonitor);
            GlobalObjects.Add(new Point(BubbleMonitor.ID, BubbleMonitor.SubType), BubbleMonitor);
            GlobalObjects.Add(new Point(FireMonitor.ID, FireMonitor.SubType), FireMonitor);
            GlobalObjects.Add(new Point(LightningMonitor.ID, LightningMonitor.SubType), LightningMonitor);
            GlobalObjects.Add(new Point(EggmanMonitor.ID, EggmanMonitor.SubType), EggmanMonitor);
            GlobalObjects.Add(new Point(TeleportMonitor.ID, TeleportMonitor.SubType), TeleportMonitor);
            GlobalObjects.Add(new Point(RandomMonitor.ID, RandomMonitor.SubType), RandomMonitor);
            GlobalObjects.Add(new Point(ShieldModeMonitor.ID, ShieldModeMonitor.SubType), ShieldModeMonitor);
            GlobalObjects.Add(new Point(EmeraldToggleMonitor.ID, EmeraldToggleMonitor.SubType), EmeraldToggleMonitor);
            GlobalObjects.Add(new Point(MonitorBroken.ID, MonitorBroken.SubType), MonitorBroken);

            GlobalObjects.Add(new Point(RedSpring.ID, RedSpring.SubType), RedSpring);
            GlobalObjects.Add(new Point(YellowSpring.ID, YellowSpring.SubType), YellowSpring);

            GlobalObjects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);

            GlobalObjects.Add(new Point(Checkpoint.ID, Checkpoint.SubType), Checkpoint);
            GlobalObjects.Add(new Point(SmokePuff.ID, SmokePuff.SubType), SmokePuff);
            GlobalObjects.Add(new Point(DustPuff.ID, DustPuff.SubType), DustPuff);

            GlobalObjects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            GlobalObjects.Add(new Point(EggPrison.ID, EggPrison.SubType), EggPrison);
            GlobalObjects.Add(new Point(Explosion.ID, Explosion.SubType), Explosion);

            GlobalObjects.Add(new Point(BlueShield.ID, BlueShield.SubType), BlueShield);
            GlobalObjects.Add(new Point(InstaShield.ID, InstaShield.SubType), InstaShield);
            GlobalObjects.Add(new Point(BubbleShield.ID, BubbleShield.SubType), BubbleShield);
            GlobalObjects.Add(new Point(FireShield.ID, FireShield.SubType), FireShield);
            GlobalObjects.Add(new Point(LightningShield.ID, LightningShield.SubType), LightningShield);
            GlobalObjects.Add(new Point(LightningSpark.ID, LightningSpark.SubType), LightningSpark);
            GlobalObjects.Add(new Point(Invincibility.ID, Invincibility.SubType), Invincibility);
            GlobalObjects.Add(new Point(SuperSpark.ID, SuperSpark.SubType), SuperSpark);

            GlobalObjects.Add(new Point(BoundsMarker.ID, BoundsMarker.SubType), BoundsMarker);
            GlobalObjects.Add(new Point(ObjectScore.ID, ObjectScore.SubType), ObjectScore);
            GlobalObjects.Add(new Point(MusicEvent.ID, MusicEvent.SubType), MusicEvent);
            GlobalObjects.Add(new Point(PlaneSwitchH.ID, PlaneSwitchH.SubType), PlaneSwitchH);
            GlobalObjects.Add(new Point(PlaneSwitchV.ID, PlaneSwitchV.SubType), PlaneSwitchV);
            GlobalObjects.Add(new Point(VSGame.ID, VSGame.SubType), VSGame);
            GlobalObjects.Add(new Point(NextStageTrigger.ID, NextStageTrigger.SubType), NextStageTrigger);
            GlobalObjects.Add(new Point(InvisibleBlock.ID, InvisibleBlock.SubType), InvisibleBlock);
            GlobalObjects.Add(new Point(SpringBoard.ID, SpringBoard.SubType), SpringBoard);
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
