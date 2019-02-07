using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;

namespace Retro_Formats
{

    public enum EngineType
    {
        RSDKvB,
        RSDKv2,
        RSDKv1,
        RSDKvRS,
        RSDKv5,
    }

    public class Scene
    {
        /// <summary>
        /// the Stage Name (what the titlecard displays)
        /// </summary>
        public string Title = "Stage";

        /// <summary>
        /// the array of Chunk IDs for the stage
        /// </summary>
        public ushort[][] MapLayout;

        /* Values for the "Display Bytes" */
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer0 = 1; //Usually BG Layer
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer1 = 9; //Unknown
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer2 = 0; //Usually Foreground (Map) Layer
        /// <summary>
        /// Active Layer 0, does ???
        /// </summary>
        public byte ActiveLayer3 = 0; //Usually Foreground (Map) Layer
        /// <summary>
        /// The Midpoint Layer does ???
        /// </summary>
        public byte Midpoint = 3;

        /// <summary>
        /// the list of objects in the stage
        /// </summary>
        public List<Object> objects = new List<Object>();

        /// <summary>
        /// stage width (in chunks)
        /// </summary>
        public ushort width;
        /// <summary>
        /// stage height (in chunks)
        /// </summary>
        public ushort height;

        /// <summary>
        /// the Max amount of objects that can be in a single stage
        /// </summary>
        public int MaxObjectCount
        {
            get
            {
                return 1056;
            }
        }

        /// <summary>
        /// the starting Music ID for the stage
        /// </summary>
        public byte Music; //This is usually Set to 0
        /// <summary>
        /// the displayed Background layer
        /// </summary>
        public byte Background; //This is usually Set to 1 in PC, 0 in DC

        /// <summary>
        /// player's Spawn Xpos
        /// </summary>
        public ushort PlayerXpos;
        /// <summary>
        /// player's Spawn Ypos
        /// </summary>
        public ushort PlayerYPos;

        public EngineType engineType;

        public Scene()
        {
            MapLayout = new ushort[1][];
            MapLayout[0] = new ushort[1];
        }

        public void ImportFrom(EngineType type, string filepath)
        {
            engineType = type;
            switch(engineType)
            {
                case EngineType.RSDKvB:
                    RSDKvB.Scene ScenevB = new RSDKvB.Scene(filepath);
                    ActiveLayer0 = ScenevB.ActiveLayer0;
                    ActiveLayer1 = ScenevB.ActiveLayer1;
                    ActiveLayer2 = ScenevB.ActiveLayer2;
                    ActiveLayer3 = ScenevB.ActiveLayer3;
                    Midpoint = ScenevB.Midpoint;
                    width = ScenevB.width;
                    height = ScenevB.height;
                    Title = ScenevB.Title;
                    MapLayout = ScenevB.MapLayout;
                    for (int i = 0; i < ScenevB.objects.Count; i++)
                    {
                        objects.Add(new Object());
                        objects[i].type = ScenevB.objects[i].type;
                        objects[i].subtype = ScenevB.objects[i].subtype;
                        objects[i].xPos = ScenevB.objects[i].xPos;
                        objects[i].yPos = ScenevB.objects[i].yPos;
                        objects[i].attribute = ScenevB.objects[i].attribute;
                        objects[i].AttributeType = ScenevB.objects[i].AttributeType;
                    }
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.Scene Scenev2 = new RSDKv2.Scene(filepath);
                    ActiveLayer0 = Scenev2.ActiveLayer0;
                    ActiveLayer1 = Scenev2.ActiveLayer1;
                    ActiveLayer2 = Scenev2.ActiveLayer2;
                    ActiveLayer3 = Scenev2.ActiveLayer3;
                    Midpoint = Scenev2.Midpoint;
                    width = Scenev2.width;
                    height = Scenev2.height;
                    Title = Scenev2.Title;
                    MapLayout = Scenev2.MapLayout;
                    for (int i = 0; i < Scenev2.objects.Count; i++)
                    {
                        objects.Add(new Object());
                        objects[i].type = Scenev2.objects[i].type;
                        objects[i].subtype = Scenev2.objects[i].subtype;
                        objects[i].xPos = Scenev2.objects[i].xPos;
                        objects[i].yPos = Scenev2.objects[i].yPos;
                    }
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.Scene Scenev1 = new RSDKv1.Scene(filepath);
                    ActiveLayer0 = Scenev1.ActiveLayer0;
                    ActiveLayer1 = Scenev1.ActiveLayer1;
                    ActiveLayer2 = Scenev1.ActiveLayer2;
                    ActiveLayer3 = Scenev1.ActiveLayer3;
                    Midpoint = Scenev1.Midpoint;
                    width = Scenev1.width;
                    height = Scenev1.height;
                    Title = Scenev1.Title;
                    MapLayout = Scenev1.MapLayout;
                    for (int i = 0; i < Scenev1.objects.Count; i++)
                    {
                        objects.Add(new Object());
                        objects[i].type = Scenev1.objects[i].type;
                        objects[i].type = Scenev1.objects[i].subtype;
                        objects[i].xPos = Scenev1.objects[i].xPos;
                        objects[i].yPos = Scenev1.objects[i].yPos;
                    }
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.Scene ScenevRS = new RSDKvRS.Scene(filepath);
                    PlayerXpos = ScenevRS.PlayerXpos;
                    PlayerYPos = ScenevRS.PlayerYPos;
                    Background = ScenevRS.Background;
                    Music = ScenevRS.Music;
                    width = ScenevRS.width;
                    height = ScenevRS.height;
                    Title = ScenevRS.Title;
                    MapLayout = ScenevRS.MapLayout;
                    for (int i = 0; i < ScenevRS.objects.Count; i++)
                    {
                        objects.Add(new Object());
                        objects[i].type = ScenevRS.objects[i].type;
                        objects[i].type = ScenevRS.objects[i].subtype;
                        objects[i].xPos = ScenevRS.objects[i].xPos;
                        objects[i].yPos = ScenevRS.objects[i].yPos;
                    }
                    break;
            }
        }

        public void ExportTo(EngineType type, string filepath)
        {
            switch (type)
            {
                case EngineType.RSDKvB:
                    RSDKvB.Scene ScenevB = new RSDKvB.Scene();
                    ScenevB.ActiveLayer0 = ActiveLayer0;
                    ScenevB.ActiveLayer1 = ActiveLayer1;
                    ScenevB.ActiveLayer2 = ActiveLayer2;
                    ScenevB.ActiveLayer3 = ActiveLayer3;
                    ScenevB.Midpoint = Midpoint;
                    ScenevB.width = width;
                    ScenevB.height = height;
                    ScenevB.Title = Title;
                    ScenevB.MapLayout = MapLayout;
                    for (int i = 0; i < ScenevB.objects.Count; i++)
                    {
                        ScenevB.objects.Add(new RSDKvB.Object());
                        ScenevB.objects[i].type = objects[i].type;
                        ScenevB.objects[i].type = objects[i].subtype;
                        ScenevB.objects[i].xPos = objects[i].xPos;
                        ScenevB.objects[i].yPos = objects[i].yPos;
                        ScenevB.objects[i].attribute = objects[i].attribute;
                        ScenevB.objects[i].AttributeType = objects[i].AttributeType;
                    }
                    ScenevB.Write(filepath);
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.Scene Scenev2 = new RSDKv2.Scene();
                    Scenev2.ActiveLayer0 = ActiveLayer0;
                    Scenev2.ActiveLayer1 = ActiveLayer1;
                    Scenev2.ActiveLayer2 = ActiveLayer2;
                    Scenev2.ActiveLayer3 = ActiveLayer3;
                    Scenev2.Midpoint = Midpoint;
                    Scenev2.width = width;
                    Scenev2.height = height;
                    Scenev2.Title = Title;
                    Scenev2.MapLayout = MapLayout;
                    for (int i = 0; i < Scenev2.objects.Count; i++)
                    {
                        Scenev2.objects.Add(new RSDKv2.Object());
                        Scenev2.objects[i].type = objects[i].type;
                        Scenev2.objects[i].type = objects[i].subtype;
                        Scenev2.objects[i].xPos = objects[i].xPos;
                        Scenev2.objects[i].yPos = objects[i].yPos;
                    }
                    Scenev2.Write(filepath);
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.Scene Scenev1 = new RSDKv1.Scene();
                    Scenev1.ActiveLayer0 = ActiveLayer0;
                    Scenev1.ActiveLayer1 = ActiveLayer1;
                    Scenev1.ActiveLayer2 = ActiveLayer2;
                    Scenev1.ActiveLayer3 = ActiveLayer3;
                    Scenev1.Midpoint = Midpoint;
                    Scenev1.width = width;
                    Scenev1.height = height;
                    Scenev1.Title = Title;
                    Scenev1.MapLayout = MapLayout;
                    for (int i = 0; i < Scenev1.objects.Count; i++)
                    {
                        Scenev1.objects.Add(new RSDKv1.Object());
                        Scenev1.objects[i].type = objects[i].type;
                        Scenev1.objects[i].type = objects[i].subtype;
                        Scenev1.objects[i].xPos = objects[i].xPos;
                        Scenev1.objects[i].yPos = objects[i].yPos;
                    }
                    Scenev1.Write(filepath);
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.Scene ScenevRS = new RSDKvRS.Scene();
                    ScenevRS.PlayerXpos = PlayerXpos;
                    ScenevRS.PlayerYPos = PlayerYPos;
                    ScenevRS.Background = Background;
                    ScenevRS.Music = Music;
                    ScenevRS.width = width;
                    ScenevRS.height = height;
                    ScenevRS.Title = Title;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (MapLayout[y][x] > 255)
                            {
                                MapLayout[y][x] = 0;
                            }
                        }
                    }

                    ScenevRS.MapLayout = MapLayout;
                    for (int i = 0; i < ScenevRS.objects.Count; i++)
                    {
                        ScenevRS.objects.Add(new RSDKvRS.Object());
                        ScenevRS.objects[i].type = objects[i].type;
                        ScenevRS.objects[i].type = objects[i].subtype;
                        ScenevRS.objects[i].xPos = objects[i].xPos;
                        ScenevRS.objects[i].yPos = objects[i].yPos;
                    }
                    ScenevRS.Write(filepath);
                    break;
            }
        }
    }

    public class Object
    {
        /// <summary>
        /// the Object's Name (used for entity list)
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// The Type of the object
        /// </summary>
        public byte type;
        /// <summary>
        /// The Object's SubType/PropertyValue
        /// </summary>
        public byte subtype;
        /// <summary>
        /// The Object's X Position
        /// </summary>
        public short xPos;
        /// <summary>
        /// The Object's Y Position
        /// </summary>
        public short yPos;
        /// <summary>
        /// how to load the "attribute"?
        /// </summary>
        public ushort AttributeType;
        /// <summary>
        /// the attribute?
        /// </summary>
        public int attribute;

        public EngineType engineType;

        public int id;

        public Object()
        {

        }
        public Object(byte type, byte subtype, short xPos, short yPos, int id, string name)
        {
            this.Name = name;
            this.type = type;
            this.subtype = subtype;
            this.xPos = xPos;
            this.yPos = yPos;
            this.id = id;
        }
    }

    public class Background
    {

        public class ScrollInfo
        {
            /// <summary>
            /// how fast the line moves while the player is moving
            /// </summary>
            public byte RelativeSpeed;
            /// <summary>
            /// How fast the line moves without the player moving
            /// </summary>
            public byte ConstantSpeed;
            /// <summary>
            /// the draw order of the layer
            /// </summary>
            public byte DrawLayer;
            /// <summary>
            /// a special byte that tells the game what "behaviour" property the layer has
            /// </summary>
            public byte Behaviour;

            public ScrollInfo()
            {
                RelativeSpeed = 0;
                ConstantSpeed = 0;
                DrawLayer = 0;
                Behaviour = 0;
            }

            public ScrollInfo(byte r, byte c, byte d, byte b)
            {
                RelativeSpeed = r;
                ConstantSpeed = c;
                DrawLayer = d;
                Behaviour = b;
            }
        }

        public class BackgroundLayer
        {
            /// <summary>
            /// the array of Chunks IDs for the Layer
            /// </summary>
            public ushort[][] MapLayout { get; set; }

            /// <summary>
            /// Layer Width
            /// </summary>
            public ushort width = 0;
            /// <summary>
            /// Layer Height
            /// </summary>
            public ushort height = 0;
            /// <summary>
            /// the draw order of the layer
            /// </summary>
            public byte DrawLayer;
            /// <summary>
            /// a special byte that tells the game what "behaviour" property the layer has
            /// </summary>
            public byte Behaviour;
            /// <summary>
            /// how fast the Layer moves while the player is moving
            /// </summary>
            public byte RelativeSpeed;
            /// <summary>
            /// how fast the layer moves while the player isn't moving
            /// </summary>
            public byte ConstantSpeed;

            /// <summary>
            /// indexes to HLine values
            /// </summary>
            public List<byte> LineIndexes = new List<byte>();

            public BackgroundLayer()
            {
                MapLayout = new ushort[1][];
                MapLayout[0] = new ushort[1];
            }
        }

        /// <summary>
        /// A list of Horizontal Line Scroll Values
        /// </summary>
        public List<ScrollInfo> HLines = new List<ScrollInfo>();
        /// <summary>
        /// A list of Vertical Line Scroll Values
        /// </summary>
        public List<ScrollInfo> VLines = new List<ScrollInfo>();
        /// <summary>
        /// A list of Background layers
        /// </summary>
        public List<BackgroundLayer> Layers = new List<BackgroundLayer>();

        public EngineType engineType;

        public Background()
        {
            Layers.Add(new BackgroundLayer());
        }

        public void ImportFrom(EngineType type, string filepath)
        {
            engineType = type;
            switch (engineType)
            {
                case EngineType.RSDKvB:
                    RSDKvB.BGLayout BGvB = new RSDKvB.BGLayout(filepath);
                    HLines.Clear();
                    VLines.Clear();
                    Layers.Clear();
                    for (int i = 0; i < BGvB.HLines.Count; i++)
                    {
                        HLines.Add(new ScrollInfo());
                        HLines[i].Behaviour = BGvB.HLines[i].Behaviour;
                        HLines[i].DrawLayer = BGvB.HLines[i].DrawLayer;
                        HLines[i].ConstantSpeed = BGvB.HLines[i].ConstantSpeed;
                        HLines[i].RelativeSpeed = BGvB.HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGvB.VLines.Count; i++)
                    {
                        VLines.Add(new ScrollInfo());
                        VLines[i].Behaviour = BGvB.VLines[i].Behaviour;
                        VLines[i].DrawLayer = BGvB.VLines[i].DrawLayer;
                        VLines[i].ConstantSpeed = BGvB.VLines[i].ConstantSpeed;
                        VLines[i].RelativeSpeed = BGvB.VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGvB.Layers.Count; i++)
                    {
                        Layers.Add(new BackgroundLayer());
                        Layers[i].Behaviour = BGvB.Layers[i].Behaviour;
                        Layers[i].Behaviour = BGvB.Layers[i].DrawLayer;
                        Layers[i].Behaviour = BGvB.Layers[i].ConstantSpeed;
                        Layers[i].Behaviour = BGvB.Layers[i].RelativeSpeed;
                        Layers[i].width = BGvB.Layers[i].width;
                        Layers[i].height = BGvB.Layers[i].height;
                        Layers[i].MapLayout = BGvB.Layers[i].MapLayout;
                        Layers[i].LineIndexes = BGvB.Layers[i].LineIndexes;
                    }
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.BGLayout BGv2 = new RSDKv2.BGLayout(filepath);
                    HLines.Clear();
                    VLines.Clear();
                    Layers.Clear();
                    for (int i = 0; i < BGv2.HLines.Count; i++)
                    {
                        HLines.Add(new ScrollInfo());
                        HLines[i].Behaviour = BGv2.HLines[i].Behaviour;
                        HLines[i].DrawLayer = BGv2.HLines[i].DrawLayer;
                        HLines[i].ConstantSpeed = BGv2.HLines[i].ConstantSpeed;
                        HLines[i].RelativeSpeed = BGv2.HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGv2.VLines.Count; i++)
                    {
                        VLines.Add(new ScrollInfo());
                        VLines[i].Behaviour = BGv2.VLines[i].Behaviour;
                        VLines[i].DrawLayer = BGv2.VLines[i].DrawLayer;
                        VLines[i].ConstantSpeed = BGv2.VLines[i].ConstantSpeed;
                        VLines[i].RelativeSpeed = BGv2.VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGv2.Layers.Count; i++)
                    {
                        Layers.Add(new BackgroundLayer());
                        Layers[i].Behaviour = BGv2.Layers[i].Behaviour;
                        Layers[i].Behaviour = BGv2.Layers[i].DrawLayer;
                        Layers[i].Behaviour = BGv2.Layers[i].ConstantSpeed;
                        Layers[i].Behaviour = BGv2.Layers[i].RelativeSpeed;
                        Layers[i].width = BGv2.Layers[i].width;
                        Layers[i].height = BGv2.Layers[i].height;
                        Layers[i].MapLayout = BGv2.Layers[i].MapLayout;
                        Layers[i].LineIndexes = BGv2.Layers[i].LineIndexes;
                    }
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.BGLayout BGv1 = new RSDKv1.BGLayout(filepath);
                    HLines.Clear();
                    VLines.Clear();
                    Layers.Clear();
                    for (int i = 0; i < BGv1.HLines.Count; i++)
                    {
                        HLines.Add(new ScrollInfo());
                        HLines[i].Behaviour = BGv1.HLines[i].Deform;
                        HLines[i].ConstantSpeed = BGv1.HLines[i].ConstantSpeed;
                        HLines[i].RelativeSpeed = BGv1.HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGv1.VLines.Count; i++)
                    {
                        VLines.Add(new ScrollInfo());
                        VLines[i].Behaviour = BGv1.VLines[i].Deform;
                        VLines[i].ConstantSpeed = BGv1.VLines[i].ConstantSpeed;
                        VLines[i].RelativeSpeed = BGv1.VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGv1.Layers.Count; i++)
                    {
                        Layers.Add(new BackgroundLayer());
                        Layers[i].Behaviour = BGv1.Layers[i].Deform;
                        Layers[i].Behaviour = BGv1.Layers[i].ConstantSpeed;
                        Layers[i].Behaviour = BGv1.Layers[i].RelativeSpeed;
                        Layers[i].width = BGv1.Layers[i].width;
                        Layers[i].height = BGv1.Layers[i].height;
                        Layers[i].MapLayout = BGv1.Layers[i].MapLayout;
                        Layers[i].LineIndexes = BGv1.Layers[i].LineIndexes;
                    }
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.BGLayout BGvRS = new RSDKvRS.BGLayout(filepath);
                    HLines.Clear();
                    VLines.Clear();
                    Layers.Clear();
                    for (int i = 0; i < BGvRS.HLines.Count; i++)
                    {
                        HLines.Add(new ScrollInfo());
                        HLines[i].Behaviour = BGvRS.HLines[i].Deform;
                        HLines[i].ConstantSpeed = BGvRS.HLines[i].ConstantSpeed;
                        HLines[i].RelativeSpeed = BGvRS.HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGvRS.VLines.Count; i++)
                    {
                        VLines.Add(new ScrollInfo());
                        VLines[i].Behaviour = BGvRS.VLines[i].Deform;
                        VLines[i].ConstantSpeed = BGvRS.VLines[i].ConstantSpeed;
                        VLines[i].RelativeSpeed = BGvRS.VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < BGvRS.Layers.Count; i++)
                    {
                        Layers.Add(new BackgroundLayer());
                        Layers[i].Behaviour = BGvRS.Layers[i].Deform;
                        Layers[i].Behaviour = BGvRS.Layers[i].ConstantSpeed;
                        Layers[i].Behaviour = BGvRS.Layers[i].RelativeSpeed;
                        Layers[i].width = BGvRS.Layers[i].width;
                        Layers[i].height = BGvRS.Layers[i].height;
                        Layers[i].MapLayout = BGvRS.Layers[i].MapLayout;
                        Layers[i].LineIndexes = BGvRS.Layers[i].LineIndexes;
                    }
                    break;
            }
        }

        public void ExportTo(EngineType type, string filepath)
        {
            switch (type)
            {
                case EngineType.RSDKvB:
                    RSDKvB.BGLayout BGvB = new RSDKvB.BGLayout();
                    BGvB.HLines.Clear();
                    BGvB.VLines.Clear();
                    BGvB.Layers.Clear();
                    for (int i = 0; i < HLines.Count; i++)
                    {
                        BGvB.HLines.Add(new RSDKvB.BGLayout.ScrollInfo());
                        BGvB.HLines[i].Behaviour = HLines[i].Behaviour;
                        BGvB.HLines[i].DrawLayer = HLines[i].DrawLayer;
                        BGvB.HLines[i].ConstantSpeed = HLines[i].ConstantSpeed;
                        BGvB.HLines[i].RelativeSpeed = HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < VLines.Count; i++)
                    {
                        BGvB.VLines.Add(new RSDKvB.BGLayout.ScrollInfo());
                        BGvB.VLines[i].Behaviour = VLines[i].Behaviour;
                        BGvB.VLines[i].DrawLayer = VLines[i].DrawLayer;
                        BGvB.VLines[i].ConstantSpeed = VLines[i].ConstantSpeed;
                        BGvB.VLines[i].RelativeSpeed = VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < Layers.Count; i++)
                    {
                        BGvB.Layers.Add(new RSDKvB.BGLayout.BGLayer());
                        BGvB.Layers[i].Behaviour = Layers[i].Behaviour;
                        BGvB.Layers[i].DrawLayer = Layers[i].DrawLayer;
                        BGvB.Layers[i].ConstantSpeed = Layers[i].ConstantSpeed;
                        BGvB.Layers[i].RelativeSpeed = Layers[i].RelativeSpeed;
                        BGvB.Layers[i].width = Layers[i].width;
                        BGvB.Layers[i].height = Layers[i].height;
                        BGvB.Layers[i].MapLayout = Layers[i].MapLayout;
                        BGvB.Layers[i].LineIndexes = Layers[i].LineIndexes;
                    }
                    BGvB.Write(filepath);
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.BGLayout BGv2 = new RSDKv2.BGLayout();
                    BGv2.HLines.Clear();
                    BGv2.VLines.Clear();
                    BGv2.Layers.Clear();
                    for (int i = 0; i < HLines.Count; i++)
                    {
                        BGv2.HLines.Add(new RSDKv2.BGLayout.ScrollInfo());
                        BGv2.HLines[i].Behaviour = HLines[i].Behaviour;
                        BGv2.HLines[i].DrawLayer = HLines[i].DrawLayer;
                        BGv2.HLines[i].ConstantSpeed = HLines[i].ConstantSpeed;
                        BGv2.HLines[i].RelativeSpeed = HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < VLines.Count; i++)
                    {
                        BGv2.VLines.Add(new RSDKv2.BGLayout.ScrollInfo());
                        BGv2.VLines[i].Behaviour = VLines[i].Behaviour;
                        BGv2.VLines[i].DrawLayer = VLines[i].DrawLayer;
                        BGv2.VLines[i].ConstantSpeed = VLines[i].ConstantSpeed;
                        BGv2.VLines[i].RelativeSpeed = VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < Layers.Count; i++)
                    {
                        BGv2.Layers.Add(new RSDKv2.BGLayout.BGLayer());
                        BGv2.Layers[i].Behaviour = Layers[i].Behaviour;
                        BGv2.Layers[i].DrawLayer = Layers[i].DrawLayer;
                        BGv2.Layers[i].ConstantSpeed = Layers[i].ConstantSpeed;
                        BGv2.Layers[i].RelativeSpeed = Layers[i].RelativeSpeed;

                        //Checks to make sure the data fits the format
                        if (Layers[i].width > 255) Layers[i].width = 255;
                        if (Layers[i].height > 255) Layers[i].height = 255;
                        ushort[][] newLayout = new ushort[Layers[i].height][];
                        for (int ii = 0; ii < Layers[i].height; ii++)
                        {
                            newLayout[ii] = new ushort[Layers[i].width];
                        }
                        for (int y = 0; y < Layers[i].height; y++)
                        {
                            for (int x = 0; x < Layers[i].height; x++)
                            {
                                newLayout[y][x] = Layers[i].MapLayout[x][y];
                            }
                        }

                        BGv2.Layers[i].width = (byte)Layers[i].width;
                        BGv2.Layers[i].height = (byte)Layers[i].height;
                        BGv2.Layers[i].MapLayout = newLayout;
                        BGv2.Layers[i].LineIndexes = Layers[i].LineIndexes;
                    }
                    BGv2.Write(filepath);
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.BGLayout BGv1 = new RSDKv1.BGLayout();
                    BGv1.HLines.Clear();
                    BGv1.VLines.Clear();
                    BGv1.Layers.Clear();
                    for (int i = 0; i < HLines.Count; i++)
                    {
                        BGv1.HLines.Add(new RSDKv1.BGLayout.ScrollInfo());
                        BGv1.HLines[i].Deform = HLines[i].Behaviour;
                        BGv1.HLines[i].ConstantSpeed = HLines[i].ConstantSpeed;
                        BGv1.HLines[i].RelativeSpeed = HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < VLines.Count; i++)
                    {
                        BGv1.VLines.Add(new RSDKv1.BGLayout.ScrollInfo());
                        BGv1.VLines[i].Deform = VLines[i].Behaviour;
                        BGv1.VLines[i].ConstantSpeed = VLines[i].ConstantSpeed;
                        BGv1.VLines[i].RelativeSpeed = VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < Layers.Count; i++)
                    {
                        BGv1.Layers.Add(new RSDKv1.BGLayout.BGLayer());
                        BGv1.Layers[i].Deform = Layers[i].Behaviour;
                        BGv1.Layers[i].ConstantSpeed = Layers[i].ConstantSpeed;
                        BGv1.Layers[i].RelativeSpeed = Layers[i].RelativeSpeed;

                        //Checks to make sure the data fits the format
                        if (Layers[i].width > 255) Layers[i].width = 255;
                        if (Layers[i].height > 255) Layers[i].height = 255;
                        ushort[][] newLayout = new ushort[Layers[i].height][];
                        for (int ii = 0; ii < Layers[i].height; ii++)
                        {
                            newLayout[ii] = new ushort[Layers[i].width];
                        }
                        for (int y = 0; y < Layers[i].height; y++)
                        {
                            for (int x = 0; x < Layers[i].height; x++)
                            {
                                newLayout[y][x] = Layers[i].MapLayout[x][y];
                            }
                        }

                        BGv1.Layers[i].width = (byte)Layers[i].width;
                        BGv1.Layers[i].height = (byte)Layers[i].height;
                        BGv1.Layers[i].MapLayout = newLayout;
                        BGv1.Layers[i].LineIndexes = Layers[i].LineIndexes;
                    }
                    BGv1.Write(filepath);
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.BGLayout BGvRS = new RSDKvRS.BGLayout();
                    BGvRS.HLines.Clear();
                    BGvRS.VLines.Clear();
                    BGvRS.Layers.Clear();
                    for (int i = 0; i < HLines.Count; i++)
                    {
                        BGvRS.HLines.Add(new RSDKvRS.BGLayout.ScrollInfo());
                        BGvRS.HLines[i].Deform = HLines[i].Behaviour;
                        BGvRS.HLines[i].ConstantSpeed = HLines[i].ConstantSpeed;
                        BGvRS.HLines[i].RelativeSpeed = HLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < VLines.Count; i++)
                    {
                        BGvRS.VLines.Add(new RSDKvRS.BGLayout.ScrollInfo());
                        BGvRS.VLines[i].Deform = VLines[i].Behaviour;
                        BGvRS.VLines[i].ConstantSpeed = VLines[i].ConstantSpeed;
                        BGvRS.VLines[i].RelativeSpeed = VLines[i].RelativeSpeed;
                    }
                    for (int i = 0; i < Layers.Count; i++)
                    {
                        BGvRS.Layers.Add(new RSDKvRS.BGLayout.BGLayer());
                        BGvRS.Layers[i].Deform = Layers[i].Behaviour;
                        BGvRS.Layers[i].ConstantSpeed = Layers[i].ConstantSpeed;
                        BGvRS.Layers[i].RelativeSpeed = Layers[i].RelativeSpeed;

                        //Checks to make sure the data fits the format
                        if (Layers[i].width > 255) Layers[i].width = 255;
                        if (Layers[i].height > 255) Layers[i].height = 255;
                        ushort[][] newLayout = new ushort[Layers[i].height][];
                        for (int ii = 0; ii < Layers[i].height; ii++)
                        {
                            newLayout[ii] = new ushort[Layers[i].width];
                        }
                        for (int y = 0; y < Layers[i].height; y++)
                        {
                            for (int x = 0; x < Layers[i].height; x++)
                            {
                                newLayout[y][x] = Layers[i].MapLayout[x][y];
                            }
                        }

                        BGvRS.Layers[i].width = (byte)Layers[i].width;
                        BGvRS.Layers[i].height = (byte)Layers[i].height;
                        BGvRS.Layers[i].MapLayout = newLayout;
                        BGvRS.Layers[i].LineIndexes = Layers[i].LineIndexes;
                    }
                    BGvRS.Write(filepath);
                    break;
            }
        }
    }

    public class MetaTiles
    {

        public class MetaTile
        {
            public class Tile
            {
                /// <summary>
                /// if tile is on the high or low layer
                /// </summary>
                public byte VisualPlane { get; set; }
                /// <summary>
                /// the flip value of the tile
                /// </summary>
                public byte Direction { get; set; }
                /// <summary>
                /// the Tile's index
                /// </summary>
                public ushort Tile16x16 { get; set; }
                /// <summary>
                /// the flags for Collision Path A
                /// </summary>
                public byte CollisionFlag0 { get; set; }
                /// <summary>
                /// the flags for Collision Path B
                /// </summary>
                public byte CollisionFlag1 { get; set; }
            }

            /// <summary>
            /// the list of 16x16Tiles in this chunk
            /// </summary>
            public Tile[][] Mappings;
            public MetaTile()
            {
                Mappings = new Tile[8][];
                for (int i = 0; i < 8; i++)
                {
                    Mappings[i] = new Tile[8];
                }

                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Mappings[y][x] = new Tile();
                    }
                }

            }

            public Bitmap Render(Image tiles)
            {
                Bitmap retval = new Bitmap(128, 128);
                using (Graphics rg = Graphics.FromImage(retval))
                {
                    for (int y = 0; y < 8; y++)
                    {
                        for (int x = 0; x < 8; x++)
                        {
                            Rectangle destRect = new Rectangle(x * 16, y * 16, 16, 16);
                            Rectangle srcRect = new Rectangle(0, Mappings[y][x].Tile16x16 * 16, 16, 16);
                            using (Bitmap tile = new Bitmap(16, 16))
                            {
                                using (Graphics tg = Graphics.FromImage(tile))
                                {
                                    tg.DrawImage(tiles, 0, 0, srcRect, GraphicsUnit.Pixel);
                                }
                                if (Mappings[y][x].Direction == 1)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipX);
                                }
                                else if (Mappings[y][x].Direction == 2)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipY);
                                }
                                else if (Mappings[y][x].Direction == 3)
                                {
                                    tile.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                                }
                                rg.DrawImage(tile, destRect);
                            }
                        }
                    }
                }
                return retval;
            }
        }

        /// <summary>
        /// the list of chunks in the file
        /// </summary>
        public MetaTile[] ChunkList = new MetaTile[512];

        public int MaxChunks
        {
            get
            {
                if (engineType == EngineType.RSDKvRS)
                {
                    return 256;
                }
                else
                {
                    return 512;
                }
            }
        }

        public EngineType engineType;

        public MetaTiles()
        {
            for (int i = 0; i < ChunkList.Length; i++)
            {
                ChunkList[i] = new MetaTile();
            }
        }

        public MetaTile Clone(int ChunkID)
        {
            MetaTile Copy = new MetaTile();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Copy.Mappings[y][x].VisualPlane = ChunkList[ChunkID].Mappings[y][x].VisualPlane;
                    Copy.Mappings[y][x].Direction = ChunkList[ChunkID].Mappings[y][x].Direction;
                    Copy.Mappings[y][x].Tile16x16 = ChunkList[ChunkID].Mappings[y][x].Tile16x16;
                    Copy.Mappings[y][x].CollisionFlag0 = ChunkList[ChunkID].Mappings[y][x].CollisionFlag0;
                    Copy.Mappings[y][x].CollisionFlag1 = ChunkList[ChunkID].Mappings[y][x].CollisionFlag1;
                }
            }
            return Copy;
        }

        public void ImportFrom(EngineType type, string filepath)
        {
            engineType = type;
            switch (engineType)
            {
                case EngineType.RSDKvB:
                    RSDKvB.Tiles128x128 ChunksvB = new RSDKvB.Tiles128x128(filepath);
                    for (int i = 0; i < 512; i++)
                    {
                        ChunkList[i] = new MetaTile();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                ChunkList[i].Mappings[y][x] = new MetaTile.Tile();
                                ChunkList[i].Mappings[y][x].CollisionFlag0 = ChunksvB.BlockList[i].Mapping[y][x].CollisionFlag0;
                                ChunkList[i].Mappings[y][x].CollisionFlag1 = ChunksvB.BlockList[i].Mapping[y][x].CollisionFlag1;
                                ChunkList[i].Mappings[y][x].Direction = ChunksvB.BlockList[i].Mapping[y][x].Direction;
                                ChunkList[i].Mappings[y][x].Tile16x16 = ChunksvB.BlockList[i].Mapping[y][x].Tile16x16;
                                ChunkList[i].Mappings[y][x].VisualPlane = ChunksvB.BlockList[i].Mapping[y][x].VisualPlane;
                            }
                        }
                    }
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.Tiles128x128 Chunksv2 = new RSDKv2.Tiles128x128(filepath);
                    for (int i = 0; i < 512; i++)
                    {
                        ChunkList[i] = new MetaTile();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                ChunkList[i].Mappings[y][x] = new MetaTile.Tile();
                                ChunkList[i].Mappings[y][x].CollisionFlag0 = Chunksv2.BlockList[i].Mapping[y][x].CollisionFlag0;
                                ChunkList[i].Mappings[y][x].CollisionFlag1 = Chunksv2.BlockList[i].Mapping[y][x].CollisionFlag1;
                                ChunkList[i].Mappings[y][x].Direction = Chunksv2.BlockList[i].Mapping[y][x].Direction;
                                ChunkList[i].Mappings[y][x].Tile16x16 = Chunksv2.BlockList[i].Mapping[y][x].Tile16x16;
                                ChunkList[i].Mappings[y][x].VisualPlane = Chunksv2.BlockList[i].Mapping[y][x].VisualPlane;
                            }
                        }
                    }
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.Tiles128x128 Chunksv1 = new RSDKv1.Tiles128x128(filepath);
                    for (int i = 0; i < 512; i++)
                    {
                        ChunkList[i] = new MetaTile();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                ChunkList[i].Mappings[y][x] = new MetaTile.Tile();
                                ChunkList[i].Mappings[y][x].CollisionFlag0 = Chunksv1.BlockList[i].Mapping[y][x].CollisionFlag0;
                                ChunkList[i].Mappings[y][x].CollisionFlag1 = Chunksv1.BlockList[i].Mapping[y][x].CollisionFlag1;
                                ChunkList[i].Mappings[y][x].Direction = Chunksv1.BlockList[i].Mapping[y][x].Direction;
                                ChunkList[i].Mappings[y][x].Tile16x16 = Chunksv1.BlockList[i].Mapping[y][x].Tile16x16;
                                ChunkList[i].Mappings[y][x].VisualPlane = Chunksv1.BlockList[i].Mapping[y][x].VisualPlane;
                            }
                        }
                    }
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.Tiles128x128 ChunksvRS = new RSDKvRS.Tiles128x128(filepath);
                    for (int i = 0; i < 256; i++)
                    {
                        ChunkList[i] = new MetaTile();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                ChunkList[i].Mappings[y][x] = new MetaTile.Tile();
                                ChunkList[i].Mappings[y][x].CollisionFlag0 = ChunksvRS.BlockList[i].Mapping[y][x].CollisionFlag0;
                                ChunkList[i].Mappings[y][x].CollisionFlag1 = ChunksvRS.BlockList[i].Mapping[y][x].CollisionFlag1;
                                ChunkList[i].Mappings[y][x].Direction = ChunksvRS.BlockList[i].Mapping[y][x].Direction;
                                ChunkList[i].Mappings[y][x].Tile16x16 = ChunksvRS.BlockList[i].Mapping[y][x].Tile16x16;
                                ChunkList[i].Mappings[y][x].VisualPlane = ChunksvRS.BlockList[i].Mapping[y][x].VisualPlane;
                            }
                        }
                    }
                    break;
            }
        }

        public void ExportTo(EngineType type, string filepath)
        {
            switch (type)
            {
                case EngineType.RSDKvB:
                    RSDKvB.Tiles128x128 ChunksvB = new RSDKvB.Tiles128x128();
                    for (int i = 0; i < 512; i++)
                    {
                        ChunksvB.BlockList[i] = new RSDKvB.Tiles128x128.Tile128();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                ChunksvB.BlockList[i].Mapping[y][x] = new RSDKvB.Tiles128x128.Tile128.Tile16();
                                ChunksvB.BlockList[i].Mapping[y][x].CollisionFlag0 = ChunkList[i].Mappings[y][x].CollisionFlag0;
                                ChunksvB.BlockList[i].Mapping[y][x].CollisionFlag1 = ChunkList[i].Mappings[y][x].CollisionFlag1;
                                ChunksvB.BlockList[i].Mapping[y][x].Direction = ChunkList[i].Mappings[y][x].Direction;
                                ChunksvB.BlockList[i].Mapping[y][x].Tile16x16 = ChunkList[i].Mappings[y][x].Tile16x16;
                                ChunksvB.BlockList[i].Mapping[y][x].VisualPlane = ChunkList[i].Mappings[y][x].VisualPlane;
                            }
                        }
                    }
                    ChunksvB.Write(filepath);
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.Tiles128x128 Chunksv2 = new RSDKv2.Tiles128x128();
                    for (int i = 0; i < 512; i++)
                    {
                        Chunksv2.BlockList[i] = new RSDKv2.Tiles128x128.Tile128();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                Chunksv2.BlockList[i].Mapping[y][x] = new RSDKv2.Tiles128x128.Tile128.Tile16();
                                Chunksv2.BlockList[i].Mapping[y][x].CollisionFlag0 = ChunkList[i].Mappings[y][x].CollisionFlag0;
                                Chunksv2.BlockList[i].Mapping[y][x].CollisionFlag1 = ChunkList[i].Mappings[y][x].CollisionFlag1;
                                Chunksv2.BlockList[i].Mapping[y][x].Direction = ChunkList[i].Mappings[y][x].Direction;
                                Chunksv2.BlockList[i].Mapping[y][x].Tile16x16 = ChunkList[i].Mappings[y][x].Tile16x16;
                                Chunksv2.BlockList[i].Mapping[y][x].VisualPlane = ChunkList[i].Mappings[y][x].VisualPlane;
                            }
                        }
                    }
                    Chunksv2.Write(filepath);
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.Tiles128x128 Chunksv1 = new RSDKv1.Tiles128x128();
                    for (int i = 0; i < 512; i++)
                    {
                        Chunksv1.BlockList[i] = new RSDKv1.Tiles128x128.Tile128();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                Chunksv1.BlockList[i].Mapping[y][x] = new RSDKv1.Tiles128x128.Tile128.Tile16();
                                Chunksv1.BlockList[i].Mapping[y][x].CollisionFlag0 = ChunkList[i].Mappings[y][x].CollisionFlag0;
                                Chunksv1.BlockList[i].Mapping[y][x].CollisionFlag1 = ChunkList[i].Mappings[y][x].CollisionFlag1;
                                Chunksv1.BlockList[i].Mapping[y][x].Direction = ChunkList[i].Mappings[y][x].Direction;
                                Chunksv1.BlockList[i].Mapping[y][x].Tile16x16 = ChunkList[i].Mappings[y][x].Tile16x16;
                                Chunksv1.BlockList[i].Mapping[y][x].VisualPlane = ChunkList[i].Mappings[y][x].VisualPlane;
                            }
                        }
                    }
                    Chunksv1.Write(filepath);
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.Tiles128x128 ChunksvRS = new RSDKvRS.Tiles128x128();
                    for (int i = 0; i < 256; i++)
                    {
                        ChunksvRS.BlockList[i] = new RSDKvRS.Tiles128x128.Tile128();
                        for (int y = 0; y < 8; y++)
                        {
                            for (int x = 0; x < 8; x++)
                            {
                                ChunksvRS.BlockList[i].Mapping[y][x] = new RSDKvRS.Tiles128x128.Tile128.Tile16();
                                ChunksvRS.BlockList[i].Mapping[y][x].CollisionFlag0 = ChunkList[i].Mappings[y][x].CollisionFlag0;
                                ChunksvRS.BlockList[i].Mapping[y][x].CollisionFlag1 = ChunkList[i].Mappings[y][x].CollisionFlag1;
                                ChunksvRS.BlockList[i].Mapping[y][x].Direction = ChunkList[i].Mappings[y][x].Direction;
                                ChunksvRS.BlockList[i].Mapping[y][x].Tile16x16 = ChunkList[i].Mappings[y][x].Tile16x16;
                                ChunksvRS.BlockList[i].Mapping[y][x].VisualPlane = ChunkList[i].Mappings[y][x].VisualPlane;
                            }
                        }
                    }
                    ChunksvRS.Write(filepath);
                    break;
            }
        }
    }

    public class ObjectData
    {
        /// <summary>
        /// the filepath to the script
        /// </summary>
        public string FilePath;
        /// <summary>
        /// the spritesheet ID for the object
        /// </summary>
        public byte SpriteSheetID = 0;

        public ObjectData()
        {

        }

        public ObjectData(string Name)
        {
            FilePath = Name;
        }

        public ObjectData(string name, int ID)
        {
            FilePath = name;
            SpriteSheetID = (byte)ID;
        }
    }

    public class Stageconfig
    {

        /// <summary>
        /// the stageconfig palette (index 96-128)
        /// </summary>
        public System.Drawing.Color[] StagePalette = new System.Drawing.Color[96];
        /// <summary>
        /// the list of Stage SoundFX paths
        /// </summary>
        public List<string> SoundFX = new List<string>();
        /// <summary>
        /// a list of names for each SFX file
        /// </summary>
        public List<string> SfxNames = new List<string>();
        /// <summary>
        /// a list of names for each script
        /// </summary>
        public List<string> ObjectsNames = new List<string>();
        /// <summary>
        /// the list of stage objects
        /// </summary>
        public List<ObjectData> ScriptPaths = new List<ObjectData>();
        /// <summary>
        /// whether or not to load the global objects in this stage
        /// </summary>
        public bool LoadGlobalScripts = false;
        /// <summary>
        /// the list of the stage music tracks
        /// </summary>
        public List<string> Music = new List<string>();

        public EngineType engineType;

        public Stageconfig()
        {

        }

        public void ImportFrom(EngineType type, string filepath)
        {
            engineType = type;
            switch (engineType)
            {
                case EngineType.RSDKvB:
                    RSDKvB.StageConfig stageconfigvB = new RSDKvB.StageConfig(filepath);
                    ObjectsNames = stageconfigvB.ObjectsNames;
                    SfxNames = stageconfigvB.SfxNames;
                    SoundFX = stageconfigvB.SoundFX;
                    LoadGlobalScripts = stageconfigvB.LoadGlobalScripts;
                    ScriptPaths.Clear();
                    for (int i = 0; i < stageconfigvB.ScriptPaths.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(stageconfigvB.ScriptPaths[i]));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            StagePalette[i] = Color.FromArgb(255, stageconfigvB.StagePalette.Colors[i][ii].R, stageconfigvB.StagePalette.Colors[i][ii].G, stageconfigvB.StagePalette.Colors[i][ii].B);
                        }
                    }
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.StageConfig stageconfigv2 = new RSDKv2.StageConfig(filepath);
                    ObjectsNames = stageconfigv2.ObjectsNames;
                    SoundFX = stageconfigv2.SoundFX;
                    LoadGlobalScripts = stageconfigv2.LoadGlobalScripts;
                    ScriptPaths.Clear();
                    for (int i = 0; i < stageconfigv2.ScriptPaths.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(stageconfigv2.ScriptPaths[i]));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            StagePalette[i] = Color.FromArgb(255, stageconfigv2.StagePalette.Colors[i][ii].R, stageconfigv2.StagePalette.Colors[i][ii].G, stageconfigv2.StagePalette.Colors[i][ii].B);
                        }
                    }
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.StageConfig stageconfigv1 = new RSDKv1.StageConfig(filepath);
                    SoundFX = stageconfigv1.SoundFX;
                    LoadGlobalScripts = stageconfigv1.LoadGlobalScripts;
                    ScriptPaths.Clear();
                    for (int i = 0; i < stageconfigv1.ScriptPaths.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(stageconfigv1.ScriptPaths[i]));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            StagePalette[i] = Color.FromArgb(255, stageconfigv1.StagePalette.Colors[i][ii].R, stageconfigv1.StagePalette.Colors[i][ii].G, stageconfigv1.StagePalette.Colors[i][ii].B);
                        }
                    }
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.Zoneconfig stageconfigvRS = new RSDKvRS.Zoneconfig(filepath);
                    SoundFX = stageconfigvRS.SoundFX;
                    Music = stageconfigvRS.Music;
                    ObjectsNames = stageconfigvRS.ObjectSpritesheets;
                    ScriptPaths.Clear();
                    for (int i = 0; i < stageconfigvRS.Objects.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(stageconfigvRS.Objects[i].FilePath, stageconfigvRS.Objects[i].SpriteSheetID));
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            StagePalette[i] = Color.FromArgb(255, stageconfigvRS.StagePalette.Colors[i][ii].R, stageconfigvRS.StagePalette.Colors[i][ii].G, stageconfigvRS.StagePalette.Colors[i][ii].B);
                        }
                    }
                    break;
            }
        }

        public void ExportTo(EngineType type, string filepath)
        {
            int colour = 0;
            switch (type)
            {
                case EngineType.RSDKvB:
                    RSDKvB.StageConfig stageconfigvB = new RSDKvB.StageConfig(filepath);
                    stageconfigvB.ObjectsNames = ObjectsNames;
                    stageconfigvB.SfxNames = SfxNames;
                    stageconfigvB.SoundFX = SoundFX;
                    stageconfigvB.LoadGlobalScripts = LoadGlobalScripts;
                    stageconfigvB.ScriptPaths.Clear();
                    colour = 0;
                    for (int i = 0; i < ScriptPaths.Count; i++)
                    {
                        stageconfigvB.ScriptPaths.Add(ScriptPaths[i].FilePath);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            stageconfigvB.StagePalette.Colors[i][ii].R = StagePalette[colour].R;
                            stageconfigvB.StagePalette.Colors[i][ii].G = StagePalette[colour].G;
                            stageconfigvB.StagePalette.Colors[i][ii].B = StagePalette[colour].B;
                            colour++;
                        }
                    }
                    stageconfigvB.Write(filepath);
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.StageConfig stageconfigv2 = new RSDKv2.StageConfig(filepath);
                    stageconfigv2.ObjectsNames = ObjectsNames;
                    stageconfigv2.SoundFX = SoundFX;
                    stageconfigv2.LoadGlobalScripts = LoadGlobalScripts;
                    stageconfigv2.ScriptPaths.Clear();
                    colour = 0;
                    for (int i = 0; i < ScriptPaths.Count; i++)
                    {
                        stageconfigv2.ScriptPaths.Add(ScriptPaths[i].FilePath);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            stageconfigv2.StagePalette.Colors[i][ii].R = StagePalette[colour].R;
                            stageconfigv2.StagePalette.Colors[i][ii].G = StagePalette[colour].G;
                            stageconfigv2.StagePalette.Colors[i][ii].B = StagePalette[colour].B;
                            colour++;
                        }
                    }
                    stageconfigv2.Write(filepath);
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.StageConfig stageconfigv1 = new RSDKv1.StageConfig(filepath);
                    stageconfigv1.SoundFX = SoundFX;
                    stageconfigv1.LoadGlobalScripts = LoadGlobalScripts;
                    stageconfigv1.ScriptPaths.Clear();
                    colour = 0;
                    for (int i = 0; i < ScriptPaths.Count; i++)
                    {
                        stageconfigv1.ScriptPaths.Add(ScriptPaths[i].FilePath);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            stageconfigv1.StagePalette.Colors[i][ii].R = StagePalette[colour].R;
                            stageconfigv1.StagePalette.Colors[i][ii].G = StagePalette[colour].G;
                            stageconfigv1.StagePalette.Colors[i][ii].B = StagePalette[colour].B;
                            colour++;
                        }
                    }
                    stageconfigv1.Write(filepath);
                    break;
                case EngineType.RSDKvRS:
                    RSDKvRS.Zoneconfig stageconfigvRS = new RSDKvRS.Zoneconfig(filepath);
                    stageconfigvRS.SoundFX = SoundFX;
                    stageconfigvRS.Objects.Clear();
                    stageconfigvRS.ObjectSpritesheets = ObjectsNames;
                    colour = 0;
                    for (int i = 0; i < ScriptPaths.Count; i++)
                    {
                        stageconfigvRS.Objects.Add(new RSDKvRS.Zoneconfig.ObjectData());
                        stageconfigvRS.Objects[i].FilePath = ScriptPaths[i].FilePath;
                        stageconfigvRS.Objects[i].SpriteSheetID = ScriptPaths[i].SpriteSheetID;
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            stageconfigvRS.StagePalette.Colors[i][ii].R = StagePalette[colour].R;
                            stageconfigvRS.StagePalette.Colors[i][ii].G = StagePalette[colour].G;
                            stageconfigvRS.StagePalette.Colors[i][ii].B = StagePalette[colour].B;
                            colour++;
                        }
                    }
                    stageconfigvRS.Write(filepath);
                    break;
            }
        }
    }

    public class Animation
    {
        public EngineType engineType;

        public Animation()
        {

        }

        public void ImportFrom(EngineType type, string filepath)
        {
            engineType = type;
            switch (engineType)
            {
                case EngineType.RSDKvB:
                    break;
                case EngineType.RSDKv2:
                    break;
                case EngineType.RSDKv1:
                    break;
                case EngineType.RSDKvRS:
                    break;
            }
        }

        public void ExportTo(EngineType type, string filepath)
        {
            switch (type)
            {
                case EngineType.RSDKvB:
                    break;
                case EngineType.RSDKv2:
                    break;
                case EngineType.RSDKv1:
                    break;
                case EngineType.RSDKvRS:
                    break;
            }
        }
    }

    public class Gameconfig
    {

        public class Category
        {
            /// <summary>
            /// the list of stages in this category
            /// </summary>
            public List<SceneInfo> Scenes = new List<SceneInfo>();

            public class SceneInfo
            {
                /// <summary>
                /// not entirely sure
                /// </summary>
                public byte Unknown;
                /// <summary>
                /// the folder of the scene
                /// </summary>
                public string SceneFolder = "Folder";
                /// <summary>
                /// the scene's identifier (E.G Act1 or Act2)
                /// </summary>
                public string ActID = "1";
                /// <summary>
                /// the scene name (shows up on the dev menu)
                /// </summary>
                public string Name = "Scene";

                public SceneInfo()
                {
                    SceneFolder = "Folder";
                    ActID = "1";
                    Name = "Stage";
                    Unknown = 0;
                }
            }

            public Category()
            {

            }
        }

        public class GlobalVariable
        {

            /// <summary>
            /// the name of the variable
            /// </summary>
            public string Name;
            /// <summary>
            /// the variable's value
            /// </summary>
            public int Value = 0;

            public GlobalVariable()
            {

            }

            public GlobalVariable(string name)
            {
                Name = name;
            }
        }

        public class PlayerData
        {
            /// <summary>
            /// The location of the player sprite mappings
            /// </summary>
            public string PlayerAnimLocation;
            /// <summary>
            /// the location of the player script
            /// </summary>
            public string PlayerScriptLocation;
            /// <summary>
            /// the name of the player
            /// </summary>
            public string PlayerName;

            public PlayerData()
            {
                PlayerAnimLocation = "";
                PlayerScriptLocation = "";
                PlayerName = "";
            }
        }

        /// <summary>
        /// the game name, appears on the window
        /// </summary>
        public string GameWindowText;
        /// <summary>
        /// the string the appears in the about window
        /// </summary>
        public string GameDescriptionText;
        /// <summary>
        /// i have no idea
        /// </summary>
        public string Unknown;

        /// <summary>
        /// a set of colours to be used as the masterpalette
        /// </summary>
        public System.Drawing.Color[] MasterPalette = new System.Drawing.Color[96];
        /// <summary>
        /// a unique name for each object in the script list
        /// </summary>
        public List<string> ObjectsNames = new List<string>();
        /// <summary>
        /// the list of filepaths for the global objects
        /// </summary>
        public List<ObjectData> ScriptPaths = new List<ObjectData>();
        /// <summary>
        /// the list of global SoundFX
        /// </summary>
        public List<string> SoundFX = new List<string>();
        /// <summary>
        /// a list of names for each SFX file
        /// </summary>
        public List<string> SfxNames = new List<string>();
        /// <summary>
        /// the list of global variable names and values
        /// </summary>
        public List<GlobalVariable> GlobalVariables = new List<GlobalVariable>();
        /// <summary>
        /// the list of playerdata needed for players
        /// </summary>
        public List<PlayerData> Players = new List<PlayerData>();
        /// <summary>
        /// the category list (stage list)
        /// </summary>
       public Category[] Categories = new Category[4];

        public EngineType engineType;

        public Gameconfig()
        {

        }

        public void ImportFrom(EngineType type, string filepath)
        {
            engineType = type;
            GlobalVariables.Clear();
            Players.Clear();
            ScriptPaths.Clear();
            switch (engineType)
            {
                case EngineType.RSDKvB:
                    RSDKvB.GameConfig GameconfigvB = new RSDKvB.GameConfig(filepath);
                    GameWindowText = GameconfigvB.GameWindowText;
                    GameDescriptionText = GameconfigvB.GameDescriptionText;
                    Unknown = "Data";
                    SoundFX = GameconfigvB.SoundFX;
                    SfxNames = GameconfigvB.SfxNames;
                    ObjectsNames = GameconfigvB.ObjectsNames;
                    for (int i = 0; i < GameconfigvB.Players.Count; i++)
                    {
                        Players.Add(new PlayerData());
                        Players[i].PlayerName = GameconfigvB.Players[i];
                    }
                    for (int i = 0; i < GameconfigvB.ScriptPaths.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(GameconfigvB.ScriptPaths[i]));
                    }
                    for (int i = 0; i < GameconfigvB.GlobalVariables.Count; i++)
                    {
                        GlobalVariables.Add(new GlobalVariable());
                        GlobalVariables[i].Name = GameconfigvB.GlobalVariables[i].Name;
                        GlobalVariables[i].Value = GameconfigvB.GlobalVariables[i].Value;
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        Categories[c] = new Category();
                        Categories[c].Scenes.Clear();
                        for (int s = 0; s < GameconfigvB.Categories[c].Scenes.Count; s++)
                        {
                            Categories[c].Scenes.Add(new Category.SceneInfo());
                            Categories[c].Scenes[s].ActID = GameconfigvB.Categories[c].Scenes[s].ActID;
                            Categories[c].Scenes[s].Name = GameconfigvB.Categories[c].Scenes[s].Name;
                            Categories[c].Scenes[s].SceneFolder = GameconfigvB.Categories[c].Scenes[s].SceneFolder;
                            Categories[c].Scenes[s].Unknown = GameconfigvB.Categories[c].Scenes[s].Unknown;
                        }
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            MasterPalette[i] = Color.FromArgb(255, GameconfigvB.MasterPalette.Colors[i][ii].R, GameconfigvB.MasterPalette.Colors[i][ii].G, GameconfigvB.MasterPalette.Colors[i][ii].B);
                        }
                    }
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.GameConfig Gameconfigv2 = new RSDKv2.GameConfig(filepath);
                    GameWindowText = Gameconfigv2.GameWindowText;
                    GameDescriptionText = Gameconfigv2.GameDescriptionText;
                    Unknown = Gameconfigv2.DataFileName;
                    SoundFX = Gameconfigv2.SoundFX;
                    ObjectsNames = Gameconfigv2.ObjectsNames;
                    for (int i = 0; i < Gameconfigv2.Players.Count; i++)
                    {
                        Players.Add(new PlayerData());
                        Players[i].PlayerName = Gameconfigv2.Players[i];
                    }
                    for (int i = 0; i < Gameconfigv2.ScriptPaths.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(Gameconfigv2.ScriptPaths[i]));
                    }
                    for (int i = 0; i < Gameconfigv2.GlobalVariables.Count; i++)
                    {
                        GlobalVariables.Add(new GlobalVariable());
                        GlobalVariables[i].Name = Gameconfigv2.GlobalVariables[i].Name;
                        GlobalVariables[i].Value = Gameconfigv2.GlobalVariables[i].Value;
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        Categories[c] = new Category();
                        Categories[c].Scenes.Clear();
                        for (int s = 0; s < Gameconfigv2.Categories[c].Scenes.Count; s++)
                        {
                            Categories[c].Scenes.Add(new Category.SceneInfo());
                            Categories[c].Scenes[s].ActID = Gameconfigv2.Categories[c].Scenes[s].ActID;
                            Categories[c].Scenes[s].Name = Gameconfigv2.Categories[c].Scenes[s].Name;
                            Categories[c].Scenes[s].SceneFolder = Gameconfigv2.Categories[c].Scenes[s].SceneFolder;
                            Categories[c].Scenes[s].Unknown = Gameconfigv2.Categories[c].Scenes[s].Unknown;
                        }
                    }
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.GameConfig Gameconfigv1 = new RSDKv1.GameConfig(filepath);
                    GameWindowText = Gameconfigv1.GameWindowText;
                    GameDescriptionText = Gameconfigv1.GameDescriptionText;
                    Unknown = Gameconfigv1.DataFileName;
                    SoundFX = Gameconfigv1.SoundFX;
                    for (int i = 0; i < Gameconfigv1.playerData.Count; i++)
                    {
                        Players.Add(new PlayerData());
                        Players[i].PlayerName = Gameconfigv1.playerData[i].PlayerName;
                        Players[i].PlayerAnimLocation = Gameconfigv1.playerData[i].PlayerAnimLocation;
                        Players[i].PlayerScriptLocation = Gameconfigv1.playerData[i].PlayerScriptLocation;
                    }
                    for (int i = 0; i < Gameconfigv1.ScriptPaths.Count; i++)
                    {
                        ScriptPaths.Add(new ObjectData(Gameconfigv1.ScriptPaths[i]));
                    }
                    for (int i = 0; i < Gameconfigv1.GlobalVariables.Count; i++)
                    {
                        GlobalVariables.Add(new GlobalVariable());
                        GlobalVariables[i].Name = Gameconfigv1.GlobalVariables[i].Name;
                        GlobalVariables[i].Value = Gameconfigv1.GlobalVariables[i].Value;
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        Categories[c] = new Category();
                        Categories[c].Scenes.Clear();
                        for (int s = 0; s < Gameconfigv1.Categories[c].Scenes.Count; s++)
                        {
                            Categories[c].Scenes.Add(new Category.SceneInfo());
                            Categories[c].Scenes[s].ActID = Gameconfigv1.Categories[c].Scenes[s].ActID;
                            Categories[c].Scenes[s].Name = Gameconfigv1.Categories[c].Scenes[s].Name;
                            Categories[c].Scenes[s].SceneFolder = Gameconfigv1.Categories[c].Scenes[s].SceneFolder;
                            Categories[c].Scenes[s].Unknown = Gameconfigv1.Categories[c].Scenes[s].Unknown;
                        }
                    }
                    break;
                case EngineType.RSDKvRS:
                    //RSDKvRS Doesn't have a gameconfig!
                    break;
            }
        }

        public void ExportTo(EngineType type, string filepath)
        {
            switch (type)
            {
                case EngineType.RSDKvB:
                    RSDKvB.GameConfig GameconfigvB = new RSDKvB.GameConfig();
                    GameconfigvB.GameWindowText = GameWindowText;
                    GameconfigvB.GameDescriptionText = GameDescriptionText;
                    GameconfigvB.SoundFX = SoundFX;
                    GameconfigvB.SfxNames = SfxNames;
                    GameconfigvB.ObjectsNames = ObjectsNames;
                    for (int i = 0; i < GameconfigvB.Players.Count; i++)
                    {
                        GameconfigvB.Players[i]= Players[i].PlayerName;
                    }
                    for (int i = 0; i < GameconfigvB.ScriptPaths.Count; i++)
                    {
                        GameconfigvB.ScriptPaths.Add(ScriptPaths[i].FilePath);
                    }
                    for (int i = 0; i < GameconfigvB.GlobalVariables.Count; i++)
                    {
                        GameconfigvB.GlobalVariables.Add(new RSDKvB.GameConfig.GlobalVariable());
                        GameconfigvB.GlobalVariables[i].Name = GameconfigvB.GlobalVariables[i].Name;
                        GameconfigvB.GlobalVariables[i].Value = GameconfigvB.GlobalVariables[i].Value;
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        GameconfigvB.Categories[c].Scenes.Clear();
                        for (int s = 0; s < GameconfigvB.Categories[c].Scenes.Count; s++)
                        {
                            GameconfigvB.Categories[c].Scenes.Add(new RSDKvB.GameConfig.Category.SceneInfo());
                            GameconfigvB.Categories[c].Scenes[s].ActID = GameconfigvB.Categories[c].Scenes[s].ActID;
                            GameconfigvB.Categories[c].Scenes[s].Name = GameconfigvB.Categories[c].Scenes[s].Name;
                            GameconfigvB.Categories[c].Scenes[s].SceneFolder = GameconfigvB.Categories[c].Scenes[s].SceneFolder;
                            GameconfigvB.Categories[c].Scenes[s].Unknown = GameconfigvB.Categories[c].Scenes[s].Unknown;
                        }
                    }
                    int colour = 0;
                    for (int i = 0; i < 2; i++)
                    {
                        for (int ii = 0; ii < 16; ii++)
                        {
                            GameconfigvB.MasterPalette.Colors[i][ii].R = MasterPalette[colour].R;
                            GameconfigvB.MasterPalette.Colors[i][ii].R = MasterPalette[colour].G;
                            GameconfigvB.MasterPalette.Colors[i][ii].R = MasterPalette[colour].B;
                            colour++;
                        }
                    }
                    GameconfigvB.Write(filepath);
                    break;
                case EngineType.RSDKv2:
                    RSDKv2.GameConfig Gameconfigv2 = new RSDKv2.GameConfig();
                    Gameconfigv2.GameWindowText = GameWindowText;
                    Gameconfigv2.GameDescriptionText = GameDescriptionText;
                    Gameconfigv2.SoundFX = SoundFX;
                    Gameconfigv2.DataFileName = Unknown;
                    Gameconfigv2.ObjectsNames = ObjectsNames;
                    for (int i = 0; i < Gameconfigv2.Players.Count; i++)
                    {
                        Gameconfigv2.Players[i] = Players[i].PlayerName;
                    }
                    for (int i = 0; i < Gameconfigv2.ScriptPaths.Count; i++)
                    {
                        Gameconfigv2.ScriptPaths.Add(ScriptPaths[i].FilePath);
                    }
                    for (int i = 0; i < Gameconfigv2.GlobalVariables.Count; i++)
                    {
                        Gameconfigv2.GlobalVariables.Add(new RSDKv2.GameConfig.GlobalVariable());
                        Gameconfigv2.GlobalVariables[i].Name = Gameconfigv2.GlobalVariables[i].Name;
                        Gameconfigv2.GlobalVariables[i].Value = Gameconfigv2.GlobalVariables[i].Value;
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        Gameconfigv2.Categories[c].Scenes.Clear();
                        for (int s = 0; s < Gameconfigv2.Categories[c].Scenes.Count; s++)
                        {
                            Gameconfigv2.Categories[c].Scenes.Add(new RSDKv2.GameConfig.Category.SceneInfo());
                            Gameconfigv2.Categories[c].Scenes[s].ActID = Gameconfigv2.Categories[c].Scenes[s].ActID;
                            Gameconfigv2.Categories[c].Scenes[s].Name = Gameconfigv2.Categories[c].Scenes[s].Name;
                            Gameconfigv2.Categories[c].Scenes[s].SceneFolder = Gameconfigv2.Categories[c].Scenes[s].SceneFolder;
                            Gameconfigv2.Categories[c].Scenes[s].Unknown = Gameconfigv2.Categories[c].Scenes[s].Unknown;
                        }
                    }
                    Gameconfigv2.Write(filepath);
                    break;
                case EngineType.RSDKv1:
                    RSDKv1.GameConfig Gameconfigv1 = new RSDKv1.GameConfig();
                    Gameconfigv1.GameWindowText = GameWindowText;
                    Gameconfigv1.GameDescriptionText = GameDescriptionText;
                    Gameconfigv1.SoundFX = SoundFX;
                    Gameconfigv1.DataFileName = Unknown;
                    for (int i = 0; i < Gameconfigv1.playerData.Count; i++)
                    {
                        Gameconfigv1.playerData[i].PlayerName = Players[i].PlayerName;
                        Gameconfigv1.playerData[i].PlayerAnimLocation = Players[i].PlayerAnimLocation;
                        Gameconfigv1.playerData[i].PlayerScriptLocation = Players[i].PlayerScriptLocation;
                    }
                    for (int i = 0; i < Gameconfigv1.ScriptPaths.Count; i++)
                    {
                        Gameconfigv1.ScriptPaths.Add(ScriptPaths[i].FilePath);
                    }
                    for (int i = 0; i < Gameconfigv1.GlobalVariables.Count; i++)
                    {
                        Gameconfigv1.GlobalVariables.Add(new RSDKv1.GameConfig.GlobalVariable());
                        Gameconfigv1.GlobalVariables[i].Name = Gameconfigv1.GlobalVariables[i].Name;
                        Gameconfigv1.GlobalVariables[i].Value = Gameconfigv1.GlobalVariables[i].Value;
                    }
                    for (int c = 0; c < 4; c++)
                    {
                        Gameconfigv1.Categories[c].Scenes.Clear();
                        for (int s = 0; s < Gameconfigv1.Categories[c].Scenes.Count; s++)
                        {
                            Gameconfigv1.Categories[c].Scenes.Add(new RSDKv1.GameConfig.Category.SceneInfo());
                            Gameconfigv1.Categories[c].Scenes[s].ActID = Gameconfigv1.Categories[c].Scenes[s].ActID;
                            Gameconfigv1.Categories[c].Scenes[s].Name = Gameconfigv1.Categories[c].Scenes[s].Name;
                            Gameconfigv1.Categories[c].Scenes[s].SceneFolder = Gameconfigv1.Categories[c].Scenes[s].SceneFolder;
                            Gameconfigv1.Categories[c].Scenes[s].Unknown = Gameconfigv1.Categories[c].Scenes[s].Unknown;
                        }
                    }
                    Gameconfigv1.Write(filepath);
                    break;
                case EngineType.RSDKvRS:
                    break;
            }
        }
    }

}
