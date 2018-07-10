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



        public Dictionary<Point, MapObject> GlobalObjects = new Dictionary<Point, MapObject>();

        public SonicCDGlobalObjects()
        {
            GlobalObjects.Add(new Point(Blank.ID, Blank.SubType), Blank);
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
