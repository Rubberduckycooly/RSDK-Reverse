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
        MapObject StageSetup = new MapObject("Stage Setup", 4, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject HUD = new MapObject("HUD", 5, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject TitleCard = new MapObject("Title Card", 6, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Ring = new MapObject("Ring", 10, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Monitor = new MapObject("Blank Monitor", 13, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject MonitorBroken = new MapObject("Broken Monitor", 14, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject RedSpring = new MapObject("Red Spring", 15, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject YellowSpring = new MapObject("Yellow Spring", 16, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject Spikes = new MapObject("Spikes", 17, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject Checkpoint = new MapObject("Checkpoint", 18, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SignPost = new MapObject("SignPost", 21, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject EggPrison = new MapObject("Egg Prison", 22, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject BoundsMarker = new MapObject("Bounds Marker", 32, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject MusicEvent = new MapObject("Music Event", 34, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject PlaneSwitch = new MapObject("Plane Switch", 35, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject NextStageTrigger = new MapObject("Next Stage Trigger", 37, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject InvisibleBlock = new MapObject("Invisible Block", 38, 0, "Objects\\General.gfx", 0, 0, 16, 16);
        MapObject SpringBoard = new MapObject("SpringBoard", 39, 0, "Objects\\General.gfx", 0, 0, 16, 16);


        public Dictionary<Point, MapObject> GlobalObjects = new Dictionary<Point, MapObject>();

        public Sonic2GlobalObjects()
        {
            GlobalObjects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            GlobalObjects.Add(new Point(Player.ID, Player.SubType), Player);
            GlobalObjects.Add(new Point(StageSetup.ID, StageSetup.SubType), StageSetup);
            GlobalObjects.Add(new Point(HUD.ID, HUD.SubType), HUD);
            GlobalObjects.Add(new Point(TitleCard.ID, TitleCard.SubType), TitleCard);
            GlobalObjects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            GlobalObjects.Add(new Point(Monitor.ID, Monitor.SubType), Monitor);
            GlobalObjects.Add(new Point(MonitorBroken.ID, MonitorBroken.SubType), MonitorBroken);
            GlobalObjects.Add(new Point(RedSpring.ID, RedSpring.SubType), RedSpring);
            GlobalObjects.Add(new Point(YellowSpring.ID, YellowSpring.SubType), YellowSpring);
            GlobalObjects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);
            GlobalObjects.Add(new Point(Checkpoint.ID, Checkpoint.SubType), Checkpoint);
            GlobalObjects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            GlobalObjects.Add(new Point(EggPrison.ID, EggPrison.SubType), EggPrison);
            GlobalObjects.Add(new Point(BoundsMarker.ID, BoundsMarker.SubType), BoundsMarker);
            GlobalObjects.Add(new Point(MusicEvent.ID, MusicEvent.SubType), MusicEvent);
            GlobalObjects.Add(new Point(PlaneSwitch.ID, PlaneSwitch.SubType), PlaneSwitch);
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
