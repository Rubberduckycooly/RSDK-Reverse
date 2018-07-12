using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    class SonicNexusGlobalObjects
    {
        MapObject Blank = new MapObject();

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


        public Dictionary<Point, MapObject> GlobalObjects = new Dictionary<Point, MapObject>();

        public SonicNexusGlobalObjects()
        {
            GlobalObjects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            GlobalObjects.Add(new Point(StageSetup.ID, StageSetup.SubType), StageSetup);
            GlobalObjects.Add(new Point(HUD.ID, HUD.SubType), HUD);
            GlobalObjects.Add(new Point(TitleCard.ID, TitleCard.SubType), TitleCard);
            GlobalObjects.Add(new Point(ActFinish.ID, ActFinish.SubType), ActFinish);
            GlobalObjects.Add(new Point(Ring.ID, Ring.SubType), Ring);
            GlobalObjects.Add(new Point(Ring2.ID, Ring2.SubType), Ring2);
            GlobalObjects.Add(new Point(RingSparkle.ID, RingSparkle.SubType), RingSparkle);
            GlobalObjects.Add(new Point(Monitor.ID, Monitor.SubType), Monitor);
            GlobalObjects.Add(new Point(BrokenMonitor.ID, BrokenMonitor.SubType), BrokenMonitor);
            GlobalObjects.Add(new Point(SpringRed.ID, SpringRed.SubType), SpringRed);
            GlobalObjects.Add(new Point(SpringYellow.ID, SpringYellow.SubType), SpringYellow);
            GlobalObjects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);
            GlobalObjects.Add(new Point(StarPost.ID, StarPost.SubType), StarPost);
            GlobalObjects.Add(new Point(Explosion.ID, Explosion.SubType), Explosion);
            GlobalObjects.Add(new Point(PlaneSwitchA.ID, PlaneSwitchA.SubType), PlaneSwitchA);
            GlobalObjects.Add(new Point(PlaneSwitchB.ID, PlaneSwitchB.SubType), PlaneSwitchB);
            GlobalObjects.Add(new Point(PlaneSwitchLoop.ID, PlaneSwitchLoop.SubType), PlaneSwitchLoop);
            GlobalObjects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);
            GlobalObjects.Add(new Point(Invincibility.ID, Invincibility.SubType), Invincibility);
            GlobalObjects.Add(new Point(DeathEvent.ID, DeathEvent.SubType), DeathEvent);
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
