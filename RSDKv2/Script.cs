using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDK_Script_Parser
{
    public class Script
    {
        string[] VARIABLE_NAME = new string[]
{
    "TempValue0",						// 0x00
	"TempValue1",
    "TempValue2",
    "TempValue3",
    "TempValue4",
    "TempValue5",
    "TempValue6",
    "TempValue7",
    "CheckResult",						// 0x08
	"ArrayPos0",						// 0x09
	"ArrayPos1",
    "Global",							// 0x0B
	"Object.EntityNo",
    "Object.Type",
    "Object.PropertyValue",				// 0x0E
	"Object.XPos",
    "Object.YPos",						// 0x10
	"Object.iXPos",
    "Object.iYPos",
    "Object.State",
    "Object.Rotation",
    "Object.Scale",
    "Object.Priority",
    "Object.DrawOrder",
    "Object.Direction",
    "Object.InkEffect",
    "Object.Alpha",
    "Object.Frame",
    "Object.Animation",
    "Object.PrevAnimation",
    "Object.AnimationSpeed",
    "Object.AnimationTimer",
    "Object.Value0",					// 0x20
	"Object.Value1",
    "Object.Value2",					// 0x22
	"Object.Value3",
    "Object.Value4",
    "Object.Value5",
    "Object.Value6",
    "Object.Value7",
    "Object.OutOfBounds",
    "Player.State",
    "Player.ControlMode",
    "Player.ControlLock",
    "Player.CollisionMode",
    "Player.CollisionPlane",
    "Player.XPos",						// 0x2E
	"Player.YPos",						// 0x2F
	"Player.iXPos",						// 0x30
	"Player.iYPos",						// 0x31
	"Player.ScreenXPos",				// 0x32
	"Player.ScreenYPos",				// 0x33
	"Player.Speed",						// 0x34
	"Player.XVelocity",					// 0x35
	"Player.YVelocity",					// 0x36
	"Player.Gravity",					// 0x37
	"Player.Angle",
    "Player.Skidding",
    "Player.Pushing",
    "Player.TrackScroll",
    "Player.Up",						// 0x3C
	"Player.Down",
    "Player.Left",
    "Player.Right",
    "Player.JumpPress",					// 0x40
	"Player.JumpHold",
    "Player.FollowPlayer1",
    "Player.LookPos",
    "Player.Water",
    "Player.TopSpeed",
    "Player.Acceleration",				// 0x46
	"Player.Deceleration",
    "Player.AirAcceleration",
    "Player.AirDeceleration",
    "Player.GravityStrength",
    "Player.JumpStrength",
    "Player.JumpCap",
    "Player.RollingAcceleration",
    "Player.RollingDeceleration",
    "Player.EntityNo",
    "Player.CollisionLeft",				// 0x50
	"Player.CollisionTop",
    "Player.CollisionRight",
    "Player.CollisionBottom",
    "Player.Flailing",
    "Player.Timer",
    "Player.TileCollisions",
    "Player.ObjectInteraction",
    "Player.Visible",
    "Player.Rotation",
    "Player.Scale",
    "Player.Priority",
    "Player.DrawOrder",					// 0x5C
	"Player.Direction",
    "Player.InkEffect",
    "Player.Alpha",
    "Player.Frame",						// 0x60
	"Player.Animation",
    "Player.PrevAnimation",
    "Player.AnimationSpeed",
    "Player.AnimationTimer",
    "Player.Value0",
    "Player.Value1",
    "Player.Value2",
    "Player.Value3",
    "Player.Value4",
    "Player.Value5",
    "Player.Value6",
    "Player.Value7",
    "Player.Value8",
    "Player.Value9",
    "Player.Value10",
    "Player.Value11",
    "Player.Value12",
    "Player.Value13",
    "Player.Value14",
    "Player.Value15",
    "Player.OutOfBounds",
    "Stage.State",
    "Stage.ActiveList",
    "Stage.ListPos",					// 0x78
	"Stage.TimeEnabled",
    "Stage.MilliSeconds",				// 0x7A
	"Stage.Seconds",
    "Stage.Minutes",					// 0x7C
	"Stage.ActNo",
    "Stage.PauseEnabled",				// 0x7E
	"Stage.ListSize",
    "Stage.NewXBoundary1",				// 0x80
	"Stage.NewXBoundary2",
    "Stage.NewYBoundary1",				// 0x82
	"Stage.NewYBoundary2",
    "Stage.XBoundary1",					// 0x84
	"Stage.XBoundary2",
    "Stage.YBoundary1",					// 0x86
	"Stage.YBoundary2",
    "Stage.DeformationData0",			// 0x88
	"Stage.DeformationData1",
    "Stage.DeformationData2",
    "Stage.DeformationData3",
    "Stage.WaterLevel",					// 0x8C
	"Stage.ActiveLayer",
    "Stage.MidPoint",					// 0x8E
	"Stage.PlayerListPos",				// 0x8F
	"Stage.ActivePlayer",				// 0x90
	"Screen.CameraEnabled",
    "Screen.CameraTarget",
    "Screen.CameraStyle",				// 0x93
	"Screen.DrawListSize",
    "Screen.CenterX",
    "Screen.CenterY",					// 0x96
	"Screen.XSize",
    "Screen.YSize",						// 0x98
	"Screen.XOffset",
    "Screen.YOffset",
    "Screen.ShakeX",					// 0x9B
	"Screen.ShakeY",					// 0x9C
	"Screen.AdjustCameraY",				// 0x9D
	"TouchScreen.Down",
    "TouchScreen.XPos",
    "TouchScreen.YPos",					// 0xA0
	"Music.Volume",						// 0xA1
	"Music.CurrentTrack",				// 0xA2
	"KeyDown.Up",
    "KeyDown.Down",
    "KeyDown.Left",
    "KeyDown.Right",
    "KeyDown.ButtonA",
    "KeyDown.ButtonB",					// 0xA8
	"KeyDown.ButtonC",
    "KeyDown.Start",
    "KeyPress.Up",
    "KeyPress.Down",
    "KeyPress.Left",
    "KeyPress.Right",
    "KeyPress.ButtonA",
    "KeyPress.ButtonB",					// 0xB0
	"KeyPress.ButtonC",
    "KeyPress.Start",
    "Menu1.Selection",
    "Menu2.Selection",
    "TileLayer.XSize",
    "TileLayer.YSize",
    "TileLayer.Type",
    "TileLayer.Angle",					// 0xB8
	"TileLayer.XPos",
    "TileLayer.YPos",
    "TileLayer.ZPos",
    "TileLayer.ParallaxFactor",
    "TileLayer.ScrollSpeed",
    "TileLayer.ScrollPos",
    "TileLayer.DeformationOffset",
    "TileLayer.DeformationOffsetW",		// 0xC0
	"HParallax.ParallaxFactor",
    "HParallax.ScrollSpeed",
    "HParallax.ScrollPos",
    "VParallax.ParallaxFactor",
    "VParallax.ScrollSpeed",
    "VParallax.ScrollPos",
    "3DScene.NoVertices",
    "3DScene.NoFaces",					// 0xC8
	"VertexBuffer.x",
    "VertexBuffer.y",
    "VertexBuffer.z",
    "VertexBuffer.u",
    "VertexBuffer.v",
    "FaceBuffer.a",
    "FaceBuffer.b",
    "FaceBuffer.c",						// 0xD0
	"FaceBuffer.d",
    "FaceBuffer.Flag",
    "FaceBuffer.Color",
    "3DScene.ProjectionX",
    "3DScene.ProjectionY",				// 0xD5
	"Engine.State",						// 0xD6
	"Stage.DebugMode",					// 0xD7
	"Engine.Message",					// 0xD8
	"SaveRAM",							// 0xD9
	"Engine.Language",					// 0xDA
	"Object.SpriteSheet",				// 0xDB
	"Engine.OnlineActive",				// 0xDC
	"Engine.FrameSkipTimer",			// 0xDD
	"Engine.FrameSkipSetting",			// 0xDE
	"Engine.SFXVolume",					// 0xDF
	"Engine.BGMVolume",					// 0xE0
	"Engine.PlatformID",				// 0xE1
	"Engine.TrialMode",					// 0xE2
	"KeyPress.AnyStart",				// 0xE3
	"Engine.Haptics",                   // 0xE4 ???
};

        string[] opcodeList = new string[]
{
    "End",
    "Equal",
    "Add",
    "Sub",
    "Inc",
    "Dec",
    "Mul",
    "Div",
    "ShR",
    "ShL",
    "And",
    "Or",
    "Xor",
    "Mod",
    "FlipSign",
    "CheckEqual",
    "CheckGreater",					// 0x10
	"CheckLower",
    "CheckNotEqual",
    "IfEqual",						// 0x13 19
	"IfGreater",
    "IfGreaterOrEqual",
    "IfLower",
    "IfLowerOrEqual",
    "IfNotEqual",					// 0x18
	"else",							// 0x19 25
	"endif",						// 0x1A
	"WEqual",
    "WGreater",
    "WGreaterOrEqual",
    "WLower",
    "WLowerOrEqual",
    "WNotEqual",					// 0x20
	"loop",							// 0x21
	"switch",						// 0x22
	"break",						// 0x23
	"endswitch",
    "Rand",							// 0x25
	"Sin",
    "Cos",
    "Sin256",
    "Cos256",
    "SinChange",					// 0x2A
	"CosChange",
    "ATan2",
    "Interpolate",
    "InterpolateXY",
    "LoadSpriteSheet",				// 0x2F
	"RemoveSpriteSheet",			// 0x30
	"DrawSprite",
    "DrawSpriteXY",
    "DrawSpriteScreenXY",
    "DrawTintRect",
    "DrawNumbers",
    "DrawActName",
    "DrawMenu",
    "SpriteFrame",
    "SetDebugIcon",
    "LoadPalette",
    "RotatePalette",
    "SetScreenFade",
    "SetActivePalette",
    "SetPaletteFade",
    "CopyPalette",
    "ClearScreen",
    "DrawSpriteFX",
    "DrawSpriteScreenFX",
    "LoadAnimation",				// 0x43
	"SetupMenu",
    "AddMenuEntry",
    "EditMenuEntry",
    "LoadStage",
    "DrawRect",
    "ResetObjectEntity",
    "PlayerObjectCollision",
    "CreateTempObject",
    "BindPlayerToObject",
    "PlayerTileCollision",
    "ProcessPlayerControl",
    "ProcessAnimation",
    "DrawObjectAnimation",
    "DrawPlayerAnimation",
    "SetMusicTrack",				// 0x52
	"PlayMusic",
    "StopMusic",
    "PlaySfx",						// 0x55 85
	"StopSfx",						// 0x56 86
	"SetSfxAttributes",
    "ObjectTileCollision",
    "ObjectTileGrip",
    "LoadVideo",
    "NextVideoFrame",				// 0x5B
	"PlayStageSfx",
    "StopStageSfx",
    "Not",
    "Draw3DScene",
    "SetIdentityMatrix",			// 0x60
	"MatrixMultiply",
    "MatrixTranslateXYZ",
    "MatrixScaleXYZ",				// 0x63
	"MatrixRotateX",
    "MatrixRotateY",
    "MatrixRotateZ",
    "MatrixRotateXYZ",
    "TransformVertices",
    "CallFunction",					// 0x69 105
	"EndFunction",					// 0x6A 106
	"SetLayerDeformation",
    "CheckTouchRect",
    "GetTileLayerEntry",
    "SetTileLayerEntry",
    "GetBit",
    "SetBit",
    "PauseMusic",
    "ResumeMusic",
    "ClearDrawList",
    "AddDrawListEntityRef",
    "GetDrawListEntityRef",
    "SetDrawListEntityRef",
    "Get16x16TileInfo",
    "Copy16x16Tile",
    "Set16x16TileInfo",
    "GetAnimationByName",
    "ReadSaveRAM",
    "WriteSaveRAM",
    "LoadTextFont",
    "LoadTextFile",
    "DrawText",
    "GetTextInfo",
    "GetVersionNumber",
    "SetAchievement",
    "SetLeaderboard",
    "LoadOnlineMenu",
    "EngineCallback"
};

        readonly byte[] scriptOpcodeSizes = new byte[]
{
    0, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2,
    2, 2, 2, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 3, 3,
    3, 0, 2, 0, 0, 2, 2, 2, 2, 2, 5, 5, 3, 4, 7, 1,
    1, 1, 3, 3, 4, 7, 7, 3, 6, 6, 5, 3, 4, 3, 7, 2,
    1, 4, 4, 1, 4, 3, 4, 0, 8, 5, 5, 4, 2, 0, 0, 0,
    0, 0, 3, 1, 0, 2, 1, 3, 4, 4, 1, 0, 2, 1, 1, 0,
    1, 2, 4, 4, 2, 2, 2, 4, 3, 1, 0, 6, 4, 4, 4, 3,
    3, 0, 0, 1, 2, 3, 3, 4, 2, 4, 2, 0, 0, 1, 3, 7,
    5, 2, 2, 2, 1, 1, 4
};

        public enum SubType
        {
            NULL,
            Main,
            PlayerInteraction,
            Draw,
            Startup,
            RSDK,
            Function,
        }

        public class Sub
        {
            public string Name = "SubFunction";
            public class Function
            {
                public string Name;
                public int ParamCount;
                public List<string> Paramaters = new List<string>();
            }

            public List<Function> Functions = new List<Function>();

            public List<string> Comments = new List<string>();
        }

        public List<Sub> Subs = new List<Sub>();

        public List<string> Lines = new List<string>();

        public Script()
        {

        }

        public Script(StreamReader reader)
        {
            try
            {
                SubType Sub = SubType.NULL; //set it as null
                while (!reader.EndOfStream)
                {
                    string Line; //hold our line data
                    string FuncName = ""; //hold our func name

                    Line = reader.ReadLine();
                    Line = Line.Replace('\t'.ToString(), ""); //tell the tabs to fuck right off

                    Script.Sub sub = new Sub();

                    if (Line.Contains("sub")) // ok so it's a sub/endsub
                    {
                        //make sure it's a sub
                        if (Line.Contains("RSDK"))
                        {
                            Sub = SubType.RSDK;
                            sub.Name = "SubRSDK";
                        }
                        if (Line.Contains("ObjectMain"))
                        {
                            Sub = SubType.Main;
                            sub.Name = "SubObjectMain";
                        }
                        if (Line.Contains("ObjectDraw"))
                        {
                            Sub = SubType.Draw;
                            sub.Name = "SubObjectDraw";
                        }
                        if (Line.Contains("ObjectPlayerInteraction"))
                        {
                            Sub = SubType.PlayerInteraction;
                            sub.Name = "SubObjectPlayerInteraction";
                        }
                        if (Line.Contains("ObjectStartup"))
                        {
                            Sub = SubType.Startup;
                            sub.Name = "SubObjectStartup";
                        }
                        if (Line.Contains("end")) //if not, then set the NULL sub (so nothing happens)
                        {
                            Sub = SubType.NULL;
                        }
                    }

                    if (Line.Contains("//"))
                    {
                        //Split a line into two parts
                        //the func
                        //and the comment

                        string comment = Line.Substring(Line.IndexOf("//")); //get the comment data
                        sub.Comments.Add(comment); //Add our comment to the list
                        Line = helper.GetUntilOrEmpty(Line, "//"); //get the non-comment part
                    }

                    if (Line != "") Lines.Add(Line); //if it's a line with data then add it

                    switch (Sub)
                    {
                        case SubType.RSDK:
                            if (!Line.Contains("sub") && !Line.Contains("RSDK"))
                            {
                                Sub.Function Func = new Sub.Function(); //set our new Function Data

                                for (int i = 0; i < Line.Length; i++) //read the string, char by char
                                {
                                    if (Line[i] == '(') //if we get to the first parenthesis, break
                                    {
                                        break;
                                    }
                                    else //else, keep reading the func name
                                    {
                                        FuncName = FuncName + Line[i];
                                    }
                                }

                                int a = Line.IndexOf("("); //Get Parameter start

                                int b = Line.IndexOf(")"); //Get Parameter end

                                string Param = ""; //Parameter Buffer

                                for (int i = a + 1; i < b; i++) //read the parameters
                                {
                                    if (Line[i] == ',') //check if parameter end
                                    {
                                        if (Param.Contains("\"")) //get rid of this shit
                                        {
                                            Param.Replace("\"", "");
                                        }
                                        Func.Paramaters.Add(Param);
                                        Param = "";
                                    }
                                    else //else, read the param into a string
                                    {
                                        Param = Param + Line[i];
                                    }
                                }

                                if (Param.Contains("\"")) //fuck off with this last bit of shit
                                {
                                    Param.Replace("\"", "");
                                }

                                Func.Paramaters.Add(Param); //Add the Last Parameter

                                Console.WriteLine(FuncName); //Write the func name

                                Func.Name = FuncName; //set our func name

                                sub.Functions.Add(Func); //add our func
                            }
                            break;
                        case SubType.Main:
                            break;
                        case SubType.Draw:
                            break;
                        case SubType.PlayerInteraction:
                            break;
                        case SubType.Startup:
                            break;
                        case SubType.Function:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Sub.Function> GetFunctionByName(string name, string SubName)
        {
            List<Sub.Function> Funcs = new List<Sub.Function>();

            Sub s = new Sub();

            bool found = false;

            for (int i = 0; i < Subs.Count; i++)
            {
                if (Subs[i].Name == SubName)
                {
                    s = Subs[i];
                    found = true;
                }
            }

            if (!found) return null;

            for (int i = 0; i < s.Functions.Count; i++)
            {
                if (s.Functions[i].Name == name)
                {
                    Funcs.Add(s.Functions[i]);
                }
            }
            return Funcs;
        }
    }

    static class helper
    {
        public static string GetUntilOrEmpty(this string text, string stopAt = "-")
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }
    }

}
