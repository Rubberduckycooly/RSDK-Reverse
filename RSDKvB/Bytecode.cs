using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace RSDKvB
{
    public class Bytecode
    {

        public struct FunctionScript
        {
            public int mainScript;
            public int mainJumpTable;
        };

        public struct ObjectScript
        {
            public int numFrames;
            public byte surfaceNum;
            public int mainScript;
            public int drawScript;
            public int startupScript;
            public int mainJumpTable;
            public int drawJumpTable;
            public int startupJumpTable;
            public int frameListOffset;
            //public AnimationFileList animationFile;
        };

        public class SwitchJumpPtr
        {
            public List<int> Cases = new List<int>();
            public int Jump;
        }

        public struct SwitchState
        {
            public int Active;
            public int JumpTableOffset;
            public int LowCase;
            public int HighCase;
            public int Case;
            public int DefaultJmp;
            public int EndJmp;
            public int ScriptCodePtr;
            public List<SwitchJumpPtr> JumpPtr;
        }

        public class StateScriptEngine
        {
            public int scriptCodePtr;
            public int jumpTablePtr;
            public int scriptCodeOffset;
            public int jumpTableOffset;
            public int scriptSub;
            public int deep;
            public int SwitchDeep;
            public bool error;
            public bool SwitchCheck;
            public bool DefaultFlag;
            public bool EndFlag;
            public bool LoopBreakFlag;
            public SwitchState[] switchState = new SwitchState[0x100];

            public StateScriptEngine()
            {
                for (int i = 0; i < 0x100; i++)
                {
                    switchState[i] = new SwitchState();
                    switchState[i].JumpPtr = new List<SwitchJumpPtr>();
                }
            }

            public StateScriptEngine IncDeep()
            {
                this.deep += 1;
                return this;
            }
        };

        class ScriptEngine
        {
            public int[] operands;
            public int[] tempValue;
            public string[] arrayPosition = new string[]
            {
                "ArrayPos0",
                "ArrayPos1",
                "ArrayPos2",
                "ArrayPos3",
                "ArrayPos4",
                "ArrayPos5",
                "PlayerObjectPos",
                "PlayerObjectCount",
                "TempObjectPos",
            };
        };

        string[] VariableNames = new string[]
{
"TempValue0",
"TempValue1",
"TempValue2",
"TempValue3",
"TempValue4",
"TempValue5",
"TempValue6",
"TempValue7",
"CheckResult",
"ArrayPos0",
"ArrayPos1",
"ArrayPos2",
"ArrayPos3",
"ArrayPos4",
"ArrayPos5",
"PlayerObjectPos",
"PlayerObjectCount",
"Global",
"ScriptData",
"Object.EntityNo",
"Object.TypeGroup",
"Object.Type",
"Object.PropertyValue",
"Object.XPos",
"Object.YPos",
"Object.iXPos",
"Object.iYPos",
    "Object.XVelocity",
    "Object.YVelocity",
    "Object.Speed",
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
    "Object.Angle",
    "Object.ValueF0",
    "Object.LookPos",
    "Object.CollisionMode",
    "Object.CollisionPlane",
    "Object.ControlMode",
    "Object.ControlLock",
    "Object.Pushing",
    "Object.Visible",
    "Object.TileCollisions",
    "Object.ObjectInteractions",
    "Object.Gravity",
    "Object.Up",
    "Object.Down",
    "Object.Left",
    "Object.Right",
    "Object.JumpPress",
    "Object.JumpHold",
    "Object.TrackScroll",
    "Object.Flailing0",
    "Object.Flailing1",
    "Object.Flailing2",
    "Object.Flailing3",
    "Object.Flailing4",
    "Object.CollisionLeft",
    "Object.CollisionTop",
    "Object.CollisionRight",
    "Object.CollisionBottom",
    "Object.OutOfBounds",
    "Object.SpriteSheet",
    "Object.Value0",
    "Object.Value1",
    "Object.Value2",
    "Object.Value3",
    "Object.Value4",
    "Object.Value5",
    "Object.Value6",
    "Object.Value7",
    "Object.Value8",
    "Object.Value9",
    "Object.Value10",
    "Object.Value11",
    "Object.Value12",
    "Object.Value13",
    "Object.Value14",
    "Object.Value50",
    "Object.Value54",
    "Object.Value58",
    "Object.Value5C",
    "Object.Value60",
    "Object.Value64",
    "Object.Value68",
    "Object.Value6C",
    "Object.Value70",
    "Object.Value74",
    "Object.Value78",
    "Object.Value7C",
    "Object.Value80",
    "Object.Value84",
    "Object.Value88",
    "Object.Value8C",
    "Object.Value90",
    "Object.Value94",
    "Object.Value98",
    "Object.Value9C",
    "Object.ValueA0",
    "Object.ValueA4",
    "Object.ValueA8",
    "Object.ValueAC",
    "Object.ValueB0",
    "Object.ValueB4",
    "Object.ValueB8",
    "Object.ValueBC",
    "Object.ValueC0",
    "Object.ValueC4",
    "Object.ValueC8",
    "Object.ValueCC",
    "Object.ValueD0",
"Stage.State",
"Stage.ActiveList",
"Stage.ListPos",
"Stage.TimeEnabled",
"Stage.MilliSeconds",
"Stage.Seconds",
"Stage.Minutes",
"Stage.ActNo",
"Stage.PauseEnabled",
"Stage.ListSize",
"Stage.NewXBoundary1",
"Stage.NewXBoundary2",
"Stage.NewYBoundary1",
"Stage.NewYBoundary2",
"Stage.XBoundary1",
"Stage.XBoundary2",
"Stage.YBoundary1",
"Stage.YBoundary2",
"Stage.DeformationData0",
"Stage.DeformationData1",
"Stage.DeformationData2",
"Stage.DeformationData3",
"Stage.WaterLevel",
"Stage.ActiveLayer",
"Stage.MidPoint",
"Stage.PlayerListPos",
"Stage.DebugMode",
"Stage.ObjectEntityPos",
"Screen.CameraEnabled",
"Screen.CameraTarget",
"Screen.CameraStyle",
"Screen.CameraXPos",
"Screen.CameraYPos",
"Screen.DrawListSize",
"Screen.CenterX",
"Screen.CenterY",
"Screen.XSize",
"Screen.YSize",
"Screen.XOffset",
"Screen.YOffset",
"Screen.ShakeX",
"Screen.ShakeY",
"Screen.AdjustCameraY",
"TouchScreen.Down",
"TouchScreen.XPos",
"TouchScreen.YPos",
"Music.Volume",
"Music.CurrentTrack",
"Unknown",
"KeyDown.Up",
"KeyDown.Down",
"KeyDown.Left",
"KeyDown.Right",
"KeyDown.ButtonA",
"KeyDown.ButtonB",
"KeyDown.ButtonC",
"KeyDown.ButtonX",
"KeyDown.ButtonY",
"KeyDown.ButtonZ",
"KeyDown.Start",
"KeyDown.Select",
"KeyDown.AnyStart",
"KeyPress.Up",
"KeyPress.Down",
"KeyPress.Left",
"KeyPress.Right",
"KeyPress.ButtonA",
"KeyPress.ButtonB",
"KeyPress.ButtonC",
"KeyPress.ButtonX",
"KeyPress.ButtonY",
"KeyPress.ButtonZ",
"KeyPress.Start",
"KeyPress.Select",
"KeyPress.AnyStart",
"Menu1.Selection",
"Menu2.Selection",
"TileLayer.Type",
"TileLayer.XSize",
"TileLayer.YSize",
"TileLayer.Angle",
"TileLayer.XPos",
"TileLayer.YPos",
"TileLayer.ZPos",
"TileLayer.ParallaxFactor",
"TileLayer.ScrollSpeed",
"TileLayer.ScrollPos",
"TileLayer.DeformationOffset",
"TileLayer.DeformationOffsetW",
"TileLayer.Unknown1",
"TileLayer.Unknown2",
"HParallax.ParallaxFactor",
"HParallax.ScrollSpeed",
"HParallax.ScrollPos",
"VParallax.ParallaxFactor",
"VParallax.ScrollSpeed",
"VParallax.ScrollPos",
"3DScene.NoVertices",
"3DScene.NoFaces",
"3DScene.ProjectionX",
"3DScene.ProjectionY",
"3DScene.FogColor",
"3DScene.FogStrength",
"VertexBuffer.x",
"VertexBuffer.y",
"VertexBuffer.z",
"VertexBuffer.u",
"VertexBuffer.v",
"FaceBuffer.a",
"FaceBuffer.b",
"FaceBuffer.c",
"FaceBuffer.d",
"FaceBuffer.Flag",
"FaceBuffer.Color",
"SaveRAM",
"Engine.State",
"Engine.Language",
"Engine.OnlineActive",
"Engine.SFXVolume",
"Engine.BGMVolume",
"Engine.TrialMode",
"Engine.PlatformID", //Read-Only
};

        string[] FunctionNames = new string[]
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
"CheckGreater",
"CheckLower",
"CheckNotEqual",
"IfEqual",
"IfGreater",
"IfGreaterOrEqual",
"IfLower",
"IfLowerOrEqual",
"IfNotEqual",
"else",
"endif",
"WEqual",
"WGreater",
"WGreaterOrEqual",
"WLower",
"WLowerOrEqual",
"WNotEqual",
"loop",
"foreach", //TypeGroup
"foreach", //TypeName
"loop",
"switch",
"break",
"endswitch",
"Rand",
"Sin",
"Cos",
"Sin256",
"Cos256",
"ATan2",
"Interpolate",
"InterpolateXY",
"LoadSpriteSheet",
"RemoveSpriteSheet",
"DrawSprite",
"DrawSpriteXY",
"DrawSpriteScreenXY",
"DrawTintRect",
"DrawNumbers",
"DrawActName",
"DrawMenu",
"SpriteFrame",
"SetSpriteFrame",
"LoadPalette",
"RotatePalette",
"SetScreenFade",
"SetActivePalette",
"SetPaletteFade",
"SetPaletteEntry",
"GetPaletteEntry",
"CopyPalette",
"ClearScreen",
"DrawSpriteFX",
"DrawSpriteScreenFX",
"LoadAnimation",
"SetupMenu",
"AddMenuEntry",
"EditMenuEntry",
"LoadStage",
"DrawRect",
"ResetObjectEntity",
"PlayerObjectCollision",
"CreateTempObject",
"PlayerTileCollision",
"ProcessPlayerControl",
"DrawObjectAnimation",
"DrawPlayerAnimation",
"SetMusicTrack",
"PlayMusic",
"StopMusic",
"PauseMusic",
"ResumeMusic",
"SwapMusicTrack",
"PlaySfx",
"StopSfx",
"SetSfxAttributes",
"ObjectTileCollision",
"ObjectTileGrip",
"Not",
"Draw3DScene",
"SetIdentityMatrix",
"MatrixMultiply",
"MatrixTranslateXYZ",
"MatrixScaleXYZ",
"MatrixRotateX",
"MatrixRotateY",
"MatrixRotateZ",
"MatrixRotateXYZ",
"MatrixInverse",
"TransformVertices",
"CallFunction",
"EndFunction",
"SetLayerDeformation",
"CheckTouchRect",
"GetTileLayerEntry",
"SetTileLayerEntry",
"GetBit",
"SetBit",
"ClearDrawList",
"AddDrawListEntityRef",
"GetDrawListEntityRef",
"SetDrawListEntityRef",
"Get16x16TileInfo",
"Set16x16TileInfo",
"Copy16x16Tile",
"GetAnimationByName",
"ReadSaveRAM",
"WriteSaveRAM",
"LoadTextFile",
"DrawText",
"GetVersionNumber",
"GetScriptData",
"SetScriptData",
"CheckZoneFolder",
"Absolute",
"EngineCallback",
"CallEngineFunction", //SetAchievement
"CallEngineFunction", // SetLeaderboards
"SetObjectBorders", //Operand0 = Offset
"GetObjectValue",
"SetObjectValue",
"CopyObject",
};

        readonly byte[] scriptOpcodeSizes = new byte[]
{
0, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2,
2, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 3, 3, 3, 0, 3, 3,
0, 2, 0, 0, 2, 2, 2, 2, 2, 3, 4, 7, 1, 1, 1, 3, 3, 4,
7, 7, 3, 6, 7, 5, 4, 4, 3, 6, 3, 3, 5, 1, 4, 4, 1, 4,
3, 4, 0, 8, 5, 0xB, 4, 0, 0, 0, 0, 3, 1, 0, 0, 0, 4, 2,
1, 3, 4, 4, 1, 0, 1, 2, 4, 4, 2, 2, 2, 4, 1, 3, 1, 0,
6, 4, 4, 4, 3, 3, 1, 2, 3, 3, 4, 4, 2, 2, 0, 0, 2, 5,
2, 3, 3, 1, 1, 1, 3, 5, 1, 3, 3, 3, 3, 0,
};

        #region Default Aliases

        string[] BoolAliases = new string[]
        {
            "false",
            "true",
        };

        string[] FXAliases = new string[]
{
"FX_SCALE",
"FX_ROTATE",
"FX_ROTOZOOM",
"FX_INK",
"FX_TINT",
"FX_FLIP",
};

        string[] StagesAliases = new string[]
{
"PRESENTATION_STAGE",
"REGULAR_STAGE",
"BONUS_STAGE",
"SPECIAL_STAGE",
};

        string[] MenuAliases = new string[]
{
"MENU_1",
"MENU_2",
};

        string[] CollisionAliases = new string[]
{
"C_TOUCH",
"C_BOX",
"C_BOX2",
"C_PLATFORM",
};

        string[] MatAliases = new string[]
{
"MAT_WORLD",
"MAT_VIEW",
"MAT_TEMP",
};

        string[] DirectionAliases = new string[]
{
"FACING_RIGHT",
"FACING_LEFT",
};

        string[] StageStateAliases = new string[]
{
"STAGE_PAUSED",
"STAGE_RUNNING",
"RESET_GAME",
};

        string[] PlatformAliases = new string[]
        {
        "RETRO_WIN",
        "RETRO_OSX",
        "RETRO_X_360",
        "RETRO_PS3",
        "RETRO_IOS",
        "RETRO_ANDROID",
        "RETRO_WP7",
        };
        #endregion

        #region custom aliases

        string[] InkAliases = new string[]
{
"INK_NONE",
"INK_BLEND",
"INK_ALPHA",
"INK_ADD",
"INK_SUB",
};

        string[] CModeAliases = new string[]
{
"CMODE_FLOOR",
"CMODE_LWALL",
"CMODE_CEILING",
"CMODE_RWALL",
};

        string[] CPathAliases = new string[]
{
"PATH_A",
"PATH_B",
};

        string[] GravityAliases = new string[]
{
"GRAVITY_GROUND",
"GRAVITY_AIR",
};


        string[] FaceFlagAliases = new string[]
{
"FACE_TEXTURED_3D",
"FACE_TEXTURED_2D",
"FACE_COLOURED_3D",
"FACE_COLOURED_2D",
"FACE_FADED",
"FACE_TEXTURED_C",
"FACE_TEXTURED_D",
"FACE_SPRITE_3D",
};

        string[] PriorityAliases = new string[]
{
"PRIORITY_ACTIVE_BOUNDS",
"PRIORITY_ACTIVE",
"PRIORITY_ACTIVE_PAUSED",
"PRIORITY_XBOUNDS",
"PRIORITY_XBOUNDS_DESTROY",
"PRIORITY_INACTIVE",
"PRIORITY_BOUNDS_SMALL",
"PRIORITY_XBOUNDS_SMALL",
};

        #endregion

        #region Misc
        public string[] functionNames = new string[0x200];
        public string[] typeNames = new string[0x100];
        public string[] sourceNames = new string[0x100];
        public string[] globalVariableNames = new string[0x100];
        public int[] globalVariables = new int[0x100];

        public int numGlobalSfx;
        public string[] sfxNames = new string[0x100];

        int GlobalScriptCount;
        public int functionCount = 0;
        public int GlobalfunctionCount = 0;

        public bool UseHex = false;

        #endregion

        #region Raw Bytecode Data
        int scriptDataPos;
        int jumpTableDataPos;
        public int[] scriptData = new int[0x40000];
        public int scriptDataLength = 0;
        public int[] jumpTableData = new int[0x4000];

        int[] ScriptDataBytes = new int[0x4000];

        ScriptEngine scriptEng = new ScriptEngine();
        StateScriptEngine state = new StateScriptEngine();
        ObjectScript[] objectScriptList = new ObjectScript[0x100];
        FunctionScript[] functionScriptList = new FunctionScript[0x200];
        #endregion

        string filename = "";

        int Read32(Reader reader)
        {
            return reader.ReadByte() + (reader.ReadByte() << 8) + (reader.ReadByte() << 16) + (reader.ReadByte() << 24);
        }
        int Read16(Reader reader)
        {
            return reader.ReadByte() + (reader.ReadByte() << 8);
        }
        int Read8(Reader reader)
        {
            return reader.ReadByte();
        }

        public Bytecode(Reader reader, int ScriptCount = 0)
        {
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];

            filename = reader.GetFilename();

            for (int opcount = Read32(reader); opcount > 0;)
            {
                int data = Read8(reader);
                int blocksCount = data & 0x7F;

                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read8(reader); }
                    opcount -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read32(reader); }
                    opcount -= blocksCount;
                }
            }

            for (int opcount = Read32(reader); opcount > 0;)
            {
                int data = Read8(reader);
                int blocksCount = data & 0x7F;
                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                        jumpTableData[jumpTableDataPos++] = Read8(reader);
                    opcount -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                        jumpTableData[jumpTableDataPos++] = Read32(reader);
                    opcount -= blocksCount;
                }
            }

            GlobalScriptCount = ScriptCount;

            int count = Read16(reader);

            for (int i = 0; i < count; i++)
            {
                objectScriptList[i + ScriptCount].mainScript = Read32(reader);
                objectScriptList[i + ScriptCount].drawScript = Read32(reader);
                objectScriptList[i + ScriptCount].startupScript = Read32(reader);
            }
            for (int i = 0; i < count; i++)
            {
                objectScriptList[i + ScriptCount].mainJumpTable = Read32(reader);
                objectScriptList[i + ScriptCount].drawJumpTable = Read32(reader);
                objectScriptList[i + ScriptCount].startupJumpTable = Read32(reader);
            }

            functionCount = Read16(reader);

            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainScript = Read32(reader);
            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainJumpTable = Read32(reader);

            scriptDataLength = scriptDataPos;
        }

        public void LoadStageBytecodeData(Reader reader, int ScriptCount = 0)
        {
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];

            for (int opcount = Read32(reader); opcount > 0;)
            {
                int data = Read8(reader);
                int blocksCount = data & 0x7F;

                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read8(reader); }
                    opcount -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read32(reader); }
                    opcount -= blocksCount;
                }
            }

            for (int opcount = Read32(reader); opcount > 0;)
            {
                int data = Read8(reader);
                int blocksCount = data & 0x7F;
                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                        jumpTableData[jumpTableDataPos++] = Read8(reader);
                    opcount -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                        jumpTableData[jumpTableDataPos++] = Read32(reader);
                    opcount -= blocksCount;
                }
            }

            GlobalScriptCount = ScriptCount;

            int count = Read16(reader);

            for (int i = 0; i < count; i++)
            {
                objectScriptList[i + ScriptCount].mainScript = Read32(reader);
                objectScriptList[i + ScriptCount].drawScript = Read32(reader);
                objectScriptList[i + ScriptCount].startupScript = Read32(reader);
            }
            for (int i = 0; i < count; i++)
            {
                objectScriptList[i + ScriptCount].mainJumpTable = Read32(reader);
                objectScriptList[i + ScriptCount].drawJumpTable = Read32(reader);
                objectScriptList[i + ScriptCount].startupJumpTable = Read32(reader);
            }

            functionCount = Read16(reader);

            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainScript = Read32(reader);
            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainJumpTable = Read32(reader);

            scriptDataLength = scriptDataPos;
        }

        string SetArrayValue(string strIn, string index)
        {
            string strOut = strIn;
            int point = -1;

            if (strIn == "Global")
            {
                try
                {
                    strOut = globalVariableNames[Int32.Parse(index)];
                }
                catch (Exception ex)
                {

                }
                if (strOut == "") return strIn;
                return strOut;
            }
            else
            {
                for (int i = 0; i < strIn.Length; i++)
                {
                    if (strIn[i] == '.')
                    {
                        point = i;
                        break;
                    }
                }
                if (point >= 0)
                {
                    string[] split = strIn.Split('.');
                    strOut = split[0] + "[" + index + "]." + split[1];
                    return strOut;
                }
                else
                {
                    strOut = strOut + "[" + index + "]";
                    return strOut;
                }
            }
        }

        bool Print = false;

        public void Decompile(string DestPath = "")
        {
            HexString.UseHex = UseHex;
            for (int i = GlobalScriptCount; i < sourceNames.Length; i++)
            {
                string path = "";
                if (!Directory.Exists(DestPath + "/Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), "")))
                {
                    DirectoryInfo d = new DirectoryInfo(DestPath + "/Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), ""));
                    d.Create();
                }
                //Console.WriteLine(DestPath + "/Scripts/" + sourceNames[i]);

                path = DestPath + "/Scripts/" + sourceNames[i];

                StreamWriter writer = new StreamWriter(path);

                Console.WriteLine("Decompiling: " + sourceNames[i]);
                if (Print) { Console.WriteLine("Decompiling: " + typeNames[i]); }

                writer.WriteLine("//---------------------Sonic 1/2 " + typeNames[i] + " Script----------------------------//");
                writer.WriteLine("//--------Scripted by Christian Whitehead 'The Taxman' & Simon Thomley 'Stealth'--------//");
                writer.WriteLine("//-----------------Unpacked By Rubberduckycooly's Script Unpacker-----------------------//");

                writer.Write(Environment.NewLine);

                writer.WriteLine("//-------Aliases-------//");

                writer.Write("#alias " + i + ": TYPE_" + typeNames[i].ToUpper().Replace(" ", "") + Environment.NewLine);

                ObjectScript objectScript = objectScriptList[i];

                writer.Write(Environment.NewLine);
                writer.Write(Environment.NewLine);

                if (!Directory.Exists(DestPath + "/Objects/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), "")))
                {
                    DirectoryInfo d = new DirectoryInfo(DestPath + "/Objects/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), ""));
                    d.Create();
                }

                if (Print) { Console.Write("Main Sub, "); }
                writer.WriteLine("//---------------------------Main Sub---------------------------//");
                writer.WriteLine("//-------Called once a frame, use this for most functions-------//");
                DecompileScript(writer, objectScript.mainScript, objectScript.mainJumpTable, 0, false);

                if (Print) { Console.Write("Draw Sub, "); }
                writer.WriteLine("//----------------------Drawing Sub-------------------//");
                writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
                DecompileScript(writer, objectScript.drawScript, objectScript.drawJumpTable, 1, false);

                if (Print) { Console.Write("Startup Sub, "); }
                writer.WriteLine("//--------------------Startup Sub---------------------//");
                writer.WriteLine("//-------Called once when the object is spawned-------//");
                DecompileScript(writer, objectScript.startupScript, objectScript.startupJumpTable, 2, false);

                if ((GlobalScriptCount - i) == 0)
                {
                    for (int ii = GlobalfunctionCount; ii < functionCount; ii++)
                    {
                        if (Print) { Console.Write("Function Sub " + ii + ", "); }
                        writer.WriteLine();
                        writer.WriteLine("//-----------------------------Function Sub-----------------------------//");
                        writer.WriteLine("//-------for many misc tasks that can be offloaded from main subs-------//");
                        DecompileScript(writer, functionScriptList[ii].mainScript, functionScriptList[ii].mainJumpTable, ii, true);
                    }
                }

                if (Print) { Console.WriteLine("RSDK Sub."); }
                writer.WriteLine("//--------------------RSDK Sub---------------------//");
                writer.WriteLine("//-----------Used for editor functionality---------//");
                writer.WriteLine("sub RSDK");
                writer.WriteLine();
                writer.WriteLine("\tLoadSpriteSheet(" + "\"Global/Display.gif\"" + ")");
                writer.WriteLine("\tSetEditorIcon(Icon0,SingleIcon,-16,-16,32,32,1,143)");
                writer.WriteLine();
                writer.WriteLine("endsub");

                writer.Write(Environment.NewLine);
                writer.Close();
            }

            if (!Directory.Exists(DestPath + "/Objects/" + Path.GetFileNameWithoutExtension(filename) + "_Global.bin"))
            {
                DirectoryInfo d = new DirectoryInfo(DestPath + "/Objects/");
                d.Create();
            }

        }

        public void DecompileScript(StreamWriter writer, int scriptCodePtr, int jumpTablePtr, int scriptSub, bool isFunction)
        {
            string strFuncName = "";
            switch (scriptSub)
            {
                case 0:
                    strFuncName = "ObjectMain";
                    break;
                case 1:
                    strFuncName = "ObjectDraw";
                    break;
                case 2:
                    strFuncName = "ObjectStartup";
                    break;
                case 3:
                    strFuncName = "RSDK";
                    break;
            }

            if (!isFunction)
            {
                writer.Write("sub" + strFuncName + Environment.NewLine);
            }
            else
            {
                writer.Write("function " + scriptSub + Environment.NewLine);
            }

            state = new StateScriptEngine();
            state.scriptCodePtr = state.scriptCodeOffset = scriptCodePtr;
            state.jumpTablePtr = state.jumpTableOffset = jumpTablePtr;
            state.scriptSub = scriptSub;
            state.deep = 1;
            state.error = false;

            DecompileSub(writer, isFunction);
            writer.Write(Environment.NewLine);
        }

        public void DecompileSub(StreamWriter writer, bool isFunction)
        {
            state.EndFlag = false;
            state.LoopBreakFlag = false;
            writer.Write(Environment.NewLine);

            while (!state.EndFlag)
            {
                int opcode = scriptData[state.scriptCodePtr++];
                int paramsCount = scriptOpcodeSizes[opcode];

                string[] variableName = new string[0x10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNKNOWN VARIABLE";
                }

                for (int i = 0; i < paramsCount; i++)
                {
                    int paramId = scriptData[state.scriptCodePtr++];
                    switch (paramId)
                    {
                        case 0: //Unused
                            break;
                        case 1: // Read value from RSDK
                            switch (scriptData[state.scriptCodePtr++])
                            {
                                case 0: //Read Const Variable
                                    variableName[i] = VariableNames[scriptData[state.scriptCodePtr++]];
                                    break;
                                case 1: // ARRAY
                                    if (scriptData[state.scriptCodePtr++] == 1) // Variable
                                    {
                                        string value = scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                        variableName[i] = SetArrayValue(VariableNames[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    else //Value
                                    {
                                        string value = scriptData[state.scriptCodePtr++].ToString();
                                        variableName[i] = SetArrayValue(VariableNames[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    break;
                                case 2:
                                    //ObjectLoop +
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                    {
                                        string value = "+" + scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                        variableName[i] = SetArrayValue(VariableNames[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    else
                                    {
                                        string value = "+" + scriptData[state.scriptCodePtr++].ToString();
                                        variableName[i] = SetArrayValue(VariableNames[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    break;
                                case 3:
                                    //ObjectLoop -
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                    {
                                        string value = "-" + scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                        variableName[i] = SetArrayValue(VariableNames[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    else
                                    {
                                        string value = "-" + scriptData[state.scriptCodePtr++].ToString();
                                        variableName[i] = SetArrayValue(VariableNames[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    break;
                            }
                            break;
                        case 2: // Read constant value from bytecode
                            variableName[i] = scriptData[state.scriptCodePtr++].ToString();
                            break;
                        case 3: // Read string
                            string tmp = "";
                            int strLen = scriptData[state.scriptCodePtr];
                            for (int j = 0; j < strLen;)
                            {
                                state.scriptCodePtr++;
                                if (j < strLen)
                                {
                                    int val = scriptData[state.scriptCodePtr] >> 24;
                                    if (val < 0) val = 0;
                                    tmp = tmp + Convert.ToChar(val);
                                }
                                j++;
                                if (j < strLen)
                                {
                                    int val = (scriptData[state.scriptCodePtr] & 0x00FFFFFF) >> 16;
                                    if (val < 0) val = 0;
                                    tmp = tmp + Convert.ToChar(val);
                                }
                                j++;
                                if (j < strLen)
                                {
                                    int val = (scriptData[state.scriptCodePtr] & 0x0000FFFF) >> 8;
                                    if (val < 0) val = 0;
                                    tmp = tmp + Convert.ToChar(val);
                                }
                                j++;
                                if (j < strLen)
                                {
                                    int val = scriptData[state.scriptCodePtr] & 0x000000FF;
                                    if (val < 0) val = 0;
                                    tmp = tmp + Convert.ToChar(val);
                                }
                                j++;
                            }
                            variableName[i] = '"' + tmp + '"';
                            if ((strLen & 3) == 0)
                            {
                                state.scriptCodePtr += 2;
                                break;
                            }
                            state.scriptCodePtr++;
                            break;
                    }
                }

                string operand = FunctionNames[opcode];

                if (operand == "End" || operand == "EndFunction")
                {
                    if (isFunction) writer.Write("endfunction");
                    else writer.Write("endsub");
                    state.EndFlag = true;
                    state.deep = 0;
                }

                if (!state.EndFlag)
                {
                    // Check what opcodes terminates a statement
                    switch (opcode)
                    {
                        case 0x00: // end sub
                            break;
                        case 0x19: // else
                            for (int i = 0; i < state.deep - 1; i++) writer.Write("\t");
                            break;
                        case 0x1A: // end if
                            state.deep--;
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                        case 0x21: // loop
                            state.LoopBreakFlag = true;
                            state.deep--;
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                        case 0x24: // foreach loop
                            state.deep--;
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                        case 0x26: // break
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            state.deep--;
                            break;
                        case 0x27: // end switch
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                        default:
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                    }

                    if (opcode >= FunctionNames.Length)
                    {
                        writer.Write("ERROR AT: " + state.scriptCodePtr + " : " + opcode);
                        Console.WriteLine("OPCODE ABOVE THE MAX OPCODES");
                        state.error = true;
                        return;
                    }

                    for (int i = 0; i < variableName.Length; i++)
                    {
                        if (variableName[i] == "" || variableName[i] == null)
                        {
                            variableName[i] = "Object.Value0";
                        }
                    }

                    #region Script Aliases
                    if (variableName[0].Contains("Object") && variableName[0].Contains(".Type"))
                    {
                        try
                        {
                            variableName[1] = "TypeName[" + typeNames[Convert.ToInt32(variableName[1])].Replace(" ", "").Replace("TouchControls", "DebugMode") + "]";
                        }
                        catch { }
                    }

                    if (variableName[0].Contains("animalType"))
                    {
                        try
                        {
                            variableName[1] = "TypeName[" + typeNames[Convert.ToInt32(variableName[1])].Replace(" ", "").Replace("TouchControls", "DebugMode") + "]";
                        }
                        catch { }
                    }

                    if (variableName[1].Contains("Object") && variableName[1].Contains(".Type"))
                    {
                        try
                        {
                            variableName[2] = "TypeName[" + typeNames[Convert.ToInt32(variableName[2])].Replace(" ", "").Replace("TouchControls", "DebugMode") + "]";
                        }
                        catch { }
                    }

                    // Special Aliases for some functions
                    int AliasID = 0;
                    switch (operand)
                    {
                        case "DrawSpriteFX":
                            Int32.TryParse((variableName[1]), out AliasID);
                            if (AliasID < 0) AliasID = 0;
                            variableName[1] = FXAliases[AliasID];
                            break;
                        case "DrawSpriteScreenFX":
                            Int32.TryParse((variableName[1]), out AliasID);
                            if (AliasID < 0) AliasID = 0;
                            variableName[1] = FXAliases[AliasID];
                            break;
                        case "PlayerObjectCollision":
                            Int32.TryParse(variableName[0], out AliasID);
                            if (AliasID < CollisionAliases.Length) variableName[0] = CollisionAliases[AliasID];
                            break;
                        case "CreateTempObject":
                            try
                            {
                                variableName[0] = "TypeName[" + typeNames[Convert.ToInt32(variableName[0])].Replace(" ", "").Replace("TouchControls", "DebugMode") + "]";
                            }
                            catch { }
                            break;
                        case "ResetObjectEntity":
                            try
                            {
                                variableName[1] = "TypeName[" + typeNames[Convert.ToInt32(variableName[1])].Replace(" ", "").Replace("TouchControls", "DebugMode") + "]";
                            }
                            catch { }
                            break;
                        case "PlaySfx":
                            try
                            {
                                variableName[0] = "SFXName[" + sfxNames[Convert.ToInt32(variableName[0])].Replace(" ", "") + "]";
                            }
                            catch { }
                            break;
                        case "StopSfx":
                            try
                            {
                                variableName[0] = "SFXName[" + sfxNames[Convert.ToInt32(variableName[0])].Replace(" ", "") + "]";
                            }
                            catch { }
                            break;
                        case "SetSfxAttributes":
                            try
                            {
                                variableName[0] = "SFXName[" + sfxNames[Convert.ToInt32(variableName[0])].Replace(" ", "") + "]";
                            }
                            catch { }
                            break;
                        case "ObjectTileCollision":
                            if (UseHex)
                            {
                                variableName[0] = CModeAliases[Int32.Parse(variableName[0])];
                            }
                            break;
                        case "ObjectTileGrip":
                            if (UseHex)
                            {
                                variableName[0] = CModeAliases[Int32.Parse(variableName[0])];
                            }
                            break;
                    }

                    if (operand.Contains("Matrix"))
                    {
                        int alias = 0;
                        Int32.TryParse(variableName[0], out alias);
                        if (alias < MatAliases.Length) variableName[0] = MatAliases[alias];
                    }

                    if (opcode < 0x21)
                    {
                        try
                        {
                            switch (variableName[0])
                            {
                                case "Engine.PlatformID":
                                    variableName[1] = PlatformAliases[Int32.Parse(variableName[1])];
                                    break;
                                case "Stage.ActiveList":
                                    variableName[1] = StagesAliases[Int32.Parse(variableName[1])];
                                    break;
                                case "Stage.State":
                                    variableName[1] = StageStateAliases[Int32.Parse(variableName[1])];
                                    break;
                            }
                            if (variableName[0].Contains(".Direction"))
                            {
                                variableName[1] = DirectionAliases[Int32.Parse(variableName[1])];
                            }

                            if (variableName[0].Contains(".InkEffect"))
                            {
                                if (UseHex)
                                {
                                    variableName[1] = InkAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            if (variableName[0].Contains(".Flag"))
                            {
                                if (UseHex)
                                {
                                    variableName[1] = FaceFlagAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            if (variableName[0].Contains(".Priority"))
                            {
                                if (UseHex)
                                {
                                    variableName[1] = PriorityAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            if (variableName[0].Contains(".Gravity"))
                            {
                                if (UseHex)
                                {
                                    variableName[1] = GravityAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            if (variableName[0].Contains(".CollisionMode"))
                            {
                                if (UseHex)
                                {
                                    variableName[1] = CModeAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            if (variableName[0].Contains(".CollisionPlane"))
                            {
                                if (UseHex)
                                {
                                    variableName[1] = CPathAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            switch (variableName[1])
                            {
                                case "Engine.PlatformID":
                                    variableName[2] = PlatformAliases[Int32.Parse(variableName[2])];
                                    break;
                                case "Stage.ActiveList":
                                    variableName[2] = StagesAliases[Int32.Parse(variableName[2])];
                                    break;
                                case "Stage.State":
                                    variableName[2] = StageStateAliases[Int32.Parse(variableName[2])];
                                    break;
                            }

                            if (variableName[1].Contains(".Direction"))
                            {
                                variableName[2] = DirectionAliases[Int32.Parse(variableName[2])];
                            }

                            if (variableName[1].Contains(".Flag"))
                            {
                                if (UseHex)
                                {
                                    variableName[2] = FaceFlagAliases[Int32.Parse(variableName[2])];
                                }
                            }

                            if (variableName[1].Contains(".Priority"))
                            {
                                if (UseHex)
                                {
                                    variableName[2] = PriorityAliases[Int32.Parse(variableName[2])];
                                }
                            }

                            if (variableName[1].Contains(".Gravity"))
                            {
                                if (UseHex)
                                {
                                    variableName[2] = GravityAliases[Int32.Parse(variableName[2])];
                                }
                            }

                            if (variableName[1].Contains(".InkEffect"))
                            {
                                if (UseHex)
                                {
                                    variableName[2] = InkAliases[Int32.Parse(variableName[2])];
                                }
                            }

                            if (variableName[1].Contains(".CollisionMode"))
                            {
                                if (UseHex)
                                {
                                    variableName[2] = CModeAliases[Int32.Parse(variableName[1])];
                                }
                            }

                            if (variableName[1].Contains(".CollisionPlane"))
                            {
                                if (UseHex)
                                {
                                    variableName[2] = CPathAliases[Int32.Parse(variableName[1])];
                                }
                            }
                        }
                        catch { }
                    }

                    #endregion

                    switch (opcode)
                    {
                        case 0x00:
                            if (isFunction) writer.Write("endfunction");
                            else writer.Write("endsub");
                            state.EndFlag = true;
                            state.deep = 0;
                            break;
                        case 0x01: writer.Write(HexString.ToHexString(variableName[0]) + "=" + HexString.ToHexString(variableName[1])); break;
                        case 0x02: writer.Write(HexString.ToHexString(variableName[0]) + "+=" + HexString.ToHexString(variableName[1])); break;
                        case 0x03: writer.Write(HexString.ToHexString(variableName[0]) + "-=" + HexString.ToHexString(variableName[1])); break;
                        case 0x04: writer.Write(HexString.ToHexString(variableName[0]) + "++"); break;
                        case 0x05: writer.Write(HexString.ToHexString(variableName[0]) + "--"); break;
                        case 0x06: writer.Write(HexString.ToHexString(variableName[0]) + "*=" + HexString.ToHexString(variableName[1])); break;
                        case 0x07: writer.Write(HexString.ToHexString(variableName[0]) + "/=" + HexString.ToHexString(variableName[1])); break;
                        case 0x08: writer.Write(HexString.ToHexString(variableName[0]) + ">>=" + HexString.ToHexString(variableName[1])); break;
                        case 0x09: writer.Write(HexString.ToHexString(variableName[0]) + "<<=" + HexString.ToHexString(variableName[1])); break;
                        case 0x0A: writer.Write(HexString.ToHexString(variableName[0]) + "&=" + HexString.ToHexString(variableName[1])); break;
                        case 0x0B: writer.Write(HexString.ToHexString(variableName[0]) + "|=" + HexString.ToHexString(variableName[1])); break;
                        case 0x0C: writer.Write(HexString.ToHexString(variableName[0]) + "^=" + HexString.ToHexString(variableName[1])); break;
                        case 0x0D: writer.Write(HexString.ToHexString(variableName[0]) + "%=" + HexString.ToHexString(variableName[1])); break;
                        case 0x13:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("if " + HexString.ToHexString(variableName[1]) + "==" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x14:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("if " + HexString.ToHexString(variableName[1]) + ">" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x15:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("if " + HexString.ToHexString(variableName[1]) + ">=" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x16:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("if " + HexString.ToHexString(variableName[1]) + "<" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x17:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("if " + HexString.ToHexString(variableName[1]) + "<=" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x18:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("if " + HexString.ToHexString(variableName[1]) + "!=" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x19:
                            writer.Write("else");
                            break;
                        case 0x1A:
                            writer.Write("endif");
                            break;
                        case 0x1B:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("while " + HexString.ToHexString(variableName[1]) + "==" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x1C:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("while " + HexString.ToHexString(variableName[1]) + ">" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x1D:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("while " + HexString.ToHexString(variableName[1]) + ">=" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x1E:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("while " + HexString.ToHexString(variableName[1]) + "<" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x1F:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("while " + HexString.ToHexString(variableName[1]) + "<=" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x20:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            writer.Write("while " + HexString.ToHexString(variableName[1]) + "!=" + HexString.ToHexString(variableName[2]));
                            state.deep += 1;
                            break;
                        case 0x21:
                            writer.Write("loop");
                            break;
                        case 0x22:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            string ForTypeA = "TypeGroup[" + variableName[1] + "]";
                            writer.Write("foreach " + ForTypeA + "," + variableName[2]);
                            state.deep += 1;
                            break;
                        case 0x23:
                            //JumpTableOffset = Int32.Parse(variableName[0]);
                            string ForTypeB = variableName[1];
                            try
                            {
                                ForTypeB = "TypeName[" + typeNames[Convert.ToInt32(variableName[1])].Replace(" ", "") + "]";
                            }
                            catch { }
                            writer.Write("foreach " + ForTypeB + "," + variableName[2]);
                            state.deep += 1;
                            break;
                        case 0x24:
                            writer.Write("loop");
                            break;
                        case 0x25:
                            writer.WriteLine("switch " + HexString.ToHexString(variableName[1]));
                            state.SwitchDeep++;

                            //Store Read Switch Data
                            state.switchState[state.SwitchDeep].Active = 1;
                            state.switchState[state.SwitchDeep].JumpTableOffset = Int32.Parse(variableName[0]);
                            state.switchState[state.SwitchDeep].LowCase = jumpTableData[state.jumpTableOffset + state.switchState[state.SwitchDeep].JumpTableOffset]; //Lowest Case No
                            state.switchState[state.SwitchDeep].HighCase = jumpTableData[state.jumpTableOffset + state.switchState[state.SwitchDeep].JumpTableOffset + 1]; //Highest Case No
                            state.switchState[state.SwitchDeep].DefaultJmp = jumpTableData[state.jumpTableOffset + state.switchState[state.SwitchDeep].JumpTableOffset + 2]; //Default Ptr
                            state.switchState[state.SwitchDeep].EndJmp = jumpTableData[state.jumpTableOffset + state.switchState[state.SwitchDeep].JumpTableOffset + 3]; //EndSwitch Ptr
                            state.switchState[state.SwitchDeep].Case = 0; //EndSwitch Ptr
                            state.switchState[state.SwitchDeep].ScriptCodePtr = state.scriptCodePtr;

                            state.switchState[state.SwitchDeep].JumpPtr = new List<SwitchJumpPtr>();
                            int CaseID = 0;

                            //Load Data for Switches
                            for (state.switchState[state.SwitchDeep].Case = state.switchState[state.SwitchDeep].LowCase; state.switchState[state.SwitchDeep].Case <= state.switchState[state.SwitchDeep].HighCase + 1; state.switchState[state.SwitchDeep].Case++)
                            {
                                if (state.switchState[state.SwitchDeep].Case == state.switchState[state.SwitchDeep].HighCase + 1)
                                {
                                    if (state.switchState[state.SwitchDeep].EndJmp - state.switchState[state.SwitchDeep].DefaultJmp > 1)
                                    {
                                        int JumpPtr = state.scriptCodeOffset + state.switchState[state.SwitchDeep].DefaultJmp;

                                        int Match = -1;

                                        for (int i = 0; i < state.switchState[state.SwitchDeep].JumpPtr.Count; i++)
                                        {
                                            if (JumpPtr == state.switchState[state.SwitchDeep].JumpPtr[i].Jump)
                                            {
                                                Match = i;
                                                break;
                                            }
                                        }

                                        if (Match > -1) //fall through
                                        {
                                            state.switchState[state.SwitchDeep].JumpPtr[Match].Cases.Add(state.switchState[state.SwitchDeep].Case);
                                        }
                                        else //new case
                                        {
                                            state.switchState[state.SwitchDeep].JumpPtr.Add(new SwitchJumpPtr());
                                            state.switchState[state.SwitchDeep].JumpPtr[CaseID].Cases.Add(state.switchState[state.SwitchDeep].Case);

                                            state.switchState[state.SwitchDeep].JumpPtr[CaseID].Jump = JumpPtr;
                                            CaseID++;
                                        }
                                    }
                                }
                                else
                                {
                                    int JumpTblPtr = state.jumpTableOffset + state.switchState[state.SwitchDeep].JumpTableOffset + (state.switchState[state.SwitchDeep].Case - state.switchState[state.SwitchDeep].LowCase) + 4;
                                    int JumpPtr = state.scriptCodeOffset + jumpTableData[JumpTblPtr];

                                    int Match = -1;

                                    for (int i = 0; i < state.switchState[state.SwitchDeep].JumpPtr.Count; i++)
                                    {
                                        if (JumpPtr == state.switchState[state.SwitchDeep].JumpPtr[i].Jump)
                                        {
                                            Match = i;
                                            break;
                                        }
                                    }

                                    if (Match > -1) //fall through
                                    {
                                        state.switchState[state.SwitchDeep].JumpPtr[Match].Cases.Add(state.switchState[state.SwitchDeep].Case);
                                    }
                                    else //new case
                                    {
                                        state.switchState[state.SwitchDeep].JumpPtr.Add(new SwitchJumpPtr());
                                        state.switchState[state.SwitchDeep].JumpPtr[CaseID].Cases.Add(state.switchState[state.SwitchDeep].Case);

                                        state.switchState[state.SwitchDeep].JumpPtr[CaseID].Jump = JumpPtr;
                                        CaseID++;
                                    }
                                }
                            }

                            //Sort Switches by JumpPtr
                            state.switchState[state.SwitchDeep].JumpPtr.Sort((x, y) => x.Jump.CompareTo(y.Jump));

                            //Use loaded data in a sorted way to jump where needed
                            for (state.switchState[state.SwitchDeep].Case = 0; state.switchState[state.SwitchDeep].Case < state.switchState[state.SwitchDeep].JumpPtr.Count; state.switchState[state.SwitchDeep].Case++)
                            {

                                if (state.scriptCodeOffset + state.switchState[state.SwitchDeep].DefaultJmp == state.switchState[state.SwitchDeep].JumpPtr[state.switchState[state.SwitchDeep].Case].Jump)
                                {
                                    for (int j = 0; j < state.deep; j++) { writer.Write("\t"); }
                                    writer.Write("default");
                                }
                                else
                                {
                                    for (int i = 0; i < state.switchState[state.SwitchDeep].JumpPtr[state.switchState[state.SwitchDeep].Case].Cases.Count; i++)
                                    {
                                        for (int j = 0; j < state.deep; j++) { writer.Write("\t"); }
                                        if (i + 1 < state.switchState[state.SwitchDeep].JumpPtr[state.switchState[state.SwitchDeep].Case].Cases.Count)
                                        {
                                            writer.WriteLine("case " + state.switchState[state.SwitchDeep].JumpPtr[state.switchState[state.SwitchDeep].Case].Cases[i]);
                                        }
                                        else
                                        {
                                            writer.Write("case " + state.switchState[state.SwitchDeep].JumpPtr[state.switchState[state.SwitchDeep].Case].Cases[i]);
                                        }
                                    }
                                }

                                state.scriptCodePtr = state.switchState[state.SwitchDeep].JumpPtr[state.switchState[state.SwitchDeep].Case].Jump;

                                state.deep += 1;

                                DecompileSub(writer, isFunction);

                            }

                            continue;
                        case 0x26:
                            writer.Write("break");
                            if (state.switchState[state.SwitchDeep].Active != 0)
                            {
                                writer.WriteLine();
                                return;
                            }
                            break;
                        case 0x27:
                            writer.Write("endswitch");
                            state.switchState[state.SwitchDeep].Active = 0;
                            state.SwitchDeep--;
                            break;
                        default:
                            writer.Write(operand + "(");
                            for (int i = 0; i < paramsCount; i++)
                            {
                                writer.Write(HexString.ToHexString(variableName[i]));
                                if (i + 1 < paramsCount)
                                {
                                    writer.Write(",");
                                }
                            }
                            writer.Write(")");
                            break;
                    }

                    writer.WriteLine();
                }
            }
        }

        public void Write(Writer writer)
        {

        }

    }

    public class HexString
    {
        public static bool UseHex = false;
        public static string ToHexString(string str)
        {
            //lazy
            if (!UseHex)
            {
                return str;
            }
            var sb = new StringBuilder();

            bool negative = str.Contains("-");

            if (negative)
            {
                str = str.Replace("-", "");
            }

            try
            {
                int value;
                bool isNumeric = int.TryParse(str, out value);

                if (!isNumeric)
                {
                    return str;
                }

                if (value < 0xFF && value > -0xFF)
                {
                    if (negative)
                    {
                        str = "-" + str;
                    }
                    return str;
                }

                sb.Append("0x");
                sb.Append(value.ToString("X"));

                str = sb.ToString();

                if (negative)
                {
                    str = "-" + str;
                }

                return str;
            }
            catch (Exception ex)
            {
                return str;
            }
        }
    }

}
