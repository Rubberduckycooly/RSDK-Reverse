using System;
using System.Collections.Generic;
using System.Drawing;

namespace RetroED.Tools.MapEditor.Object_Definitions
{
    class Retro_SonicGlobalObjects
    {
        MapObject Blank = new MapObject();

        MapObject Ring = new MapObject("Ring", 1, 0, "Objects\\General.gfx", 0, 0, 16, 16);

        MapObject BlankItemBox = new MapObject("Blank Item Box", 4, 0, "Objects\\General.gfx", 24, 0, 30, 32);
        MapObject RingBox = new MapObject("Ring Box", 4, 1, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BlueShield = new MapObject("Blue Shield", 4, 2, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject MagnetShield = new MapObject("Magnet Shield", 4, 3, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject FireShield = new MapObject("Fire Shield", 4, 4, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BubbleShield = new MapObject("Bubble Shield", 4, 5, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject EggmanMonitor = new MapObject("Eggman Monitor", 4, 8, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject oneUP = new MapObject("1UP", 4, 9, "Objects\\General.gfx", 0, 0, 14, 16);
        MapObject BlueRing = new MapObject("Blue Ring", 4, 10, "Objects\\General.gfx", 0, 0, 14, 16);

        MapObject Spring = new MapObject("Yellow Spring", 6, 0, "Objects\\General.gfx", 86, 16, 32, 16);

        MapObject Spikes = new MapObject("Spike", 7, 0, "Objects\\General.gfx", 118, 160, 32, 32);

        MapObject Checkpoint = new MapObject("Checkpoint", 9, 0, "Objects\\General.gfx", 240, 0, 16, 48);

        MapObject SignPost = new MapObject("SignPost", 18, 0, "Objects\\General2.gfx", 64, 0, 48, 48);

        MapObject BigRing = new MapObject("Big Ring", 26, 0, "Objects\\General.gfx", 256, 0, 64, 64);

        public Dictionary<Point ,MapObject> GlobalObjects = new Dictionary<Point, MapObject>();

        public Retro_SonicGlobalObjects()
        {
            GlobalObjects.Add(new Point(Blank.ID, Blank.SubType), Blank);

            GlobalObjects.Add(new Point(Ring.ID, Ring.SubType), Ring);

            GlobalObjects.Add(new Point(BlankItemBox.ID, BlankItemBox.SubType), BlankItemBox);
            GlobalObjects.Add(new Point(RingBox.ID, RingBox.SubType), RingBox);
            GlobalObjects.Add(new Point(BlueShield.ID, BlueShield.SubType), BlueShield);
            GlobalObjects.Add(new Point(MagnetShield.ID, MagnetShield.SubType), MagnetShield);
            GlobalObjects.Add(new Point(FireShield.ID, FireShield.SubType), FireShield);
            GlobalObjects.Add(new Point(BubbleShield.ID, BubbleShield.SubType), BubbleShield);
            GlobalObjects.Add(new Point(EggmanMonitor.ID, EggmanMonitor.SubType), EggmanMonitor);
            GlobalObjects.Add(new Point(oneUP.ID, oneUP.SubType), oneUP);
            GlobalObjects.Add(new Point(BlueRing.ID, BlueRing.SubType), BlueRing);

            GlobalObjects.Add(new Point(Spring.ID, Spring.SubType), Spring);

            GlobalObjects.Add(new Point(Spikes.ID, Spikes.SubType), Spikes);

            GlobalObjects.Add(new Point(Checkpoint.ID, Checkpoint.SubType), Checkpoint);

            GlobalObjects.Add(new Point(SignPost.ID, SignPost.SubType), SignPost);

            GlobalObjects.Add(new Point(BigRing.ID, BigRing.SubType), BigRing);
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
