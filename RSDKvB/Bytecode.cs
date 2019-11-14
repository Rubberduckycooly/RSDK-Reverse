using System;
using System.IO;
using System.Text;

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
            public AnimationFileList animationFile;
        };

        public class AnimationSystem
        {
            public static AnimationFileList GetDefaultAnimationRef()
            {
                AnimationFileList animationFile = new AnimationFileList();
                animationFile.fileName[0] = '\0';
                animationFile.numAnimations = 0;
                animationFile.aniListOffset = 0;
                animationFile.cbListOffset = 0;
                return animationFile;
            }
        };

        public class AnimationFileList
        {
            public char[] fileName = new char[32];
            public int numAnimations;
            public int aniListOffset;
            public int cbListOffset;
        };

        public struct StateScriptEngine
        {
            public int scriptCodePtr;
            public int scriptCodeOffset;
            public int jumpTablePtr;
            public int jumpTableOffset;
            public int scriptSub;
            public int deep;
            public int SwitchDeep;
            public bool isSwitchEnd;
            public bool error;
            public bool SwitchCheck;
            public bool EndFlag;
            public bool LoopBreakFlag;
            public bool SwitchBreakFlag;

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
                "ArrayPos7",
                "TempObjectPos",
                "ArrayPos9",
            };
        };

        string[] VARIABLE_NAME = new string[]
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
"UnknownVar0",
"UnknownVar1",
"UnknownVar2",
"UnknownVar3",
"UnknownVar4",
"UnknownVar5",
"Global",
"ObjectScriptData",
"Object.EntityNo",
"Object.Type",
"Object.Unknown",
"Object.PropertyValue",
"Object.XPos",
"Object.YPos",
"Object.iXPos",
"Object.iYPos",
    "Object.XVelocity",//"Object.State",
    "Object.YVelocity",//"Object.Scale",
    "Object.Speed", //"Object.Priority",
    "Object.State",//"Object.DrawOrder",
    "Object.Rotation",//"Object.Direction",
    "Object.Scale",//"Object.InkEffect(OG)",
    "Object.Priority",//"Object.Alpha",
    "Object.DrawOrder",//"Object.Frame",
    "Object.Direction",//"Object.Animation",
    "Object.InkEffect",//"Object.PrevAnimation",
    "Object.Alpha",//"Object.AnimationSpeed",
    "Object.Frame",//"Object.AnimationTimer",
    "Object.Animation",//"Object.Value0",					
	"Object.PrevAnimation",//"Object.Value1(OG)",
    "Object.AnimationSpeed",//"Object.Value2",					
	"Object.AnimationTimer",//"Object.Value3(OG)",
    "Object.Angle",//"Object.Value4",
    "Object.Value5(OG)",
    "Object.Value6(OG)",
    "Player.CollisionMode",//"Object.Value7",
    "Player.CollisionPlane",//"Object.OutOfBounds(OG)",
    "Player.ControlMode",//"Player.State",
    "Player.ControlLock(OG)",
    "Player.ControlMode",//"Player.ControlLock",
    "Player.Visible",//"Player.CollisionMode",
    "Player.CollisionPlane(OG)",
    "Player.Up",//"Player.XPos",
    "Player.Gravity",//"Player.YPos",
    "Player.Left",//"Player.iXPos",						
    "Player.Down", //"Player.iYPos",
    "Player.ScreenXPos(OG)",
    "Player.ScreenYPos(OG)",
    "Object.Priority",//"Player.Speed",						
	"Player.XVelocity(OG)",
    "Player.TrackScroll",//"Player.YVelocity",					
	"Player.Gravity(OG)",
    "Player.Angle(OG)",
    "Player.CollisionLeft",//"Player.Skidding",
    "Player.Pushing(OG)",
    "Player.TrackScroll(OG)",
    "Player.Up(OG)",
    "Player.Down(OG)",
    "Player.Left(OG)",
    "Player.Right(OG)",
    "Player.JumpPress(OG)",
    "Player.JumpHold(OG)",
    "Object.Value0",//"Player.FollowPlayer1(OG)",
    "Object.Timer",//"Player.LookPos",
    "Object.Value1",//"Player.Water",
    "Object.Value2",//"Player.TopSpeed",
    "Object.Value3",//"Player.Acceleration(OG)",
    "Object.Value4",//"Player.Deceleration",
    "Object.Value5",//"Player.AirAcceleration(OG)",
    "Object.Value6",//"Player.AirDeceleration(OG)",
    "Object.Value7",//"Player.GravityStrength(OG)",
    "Object.Value8", //"Player.JumpStrength(OG)",
    "Object.Value9",//"Player.JumpCap(OG)",
    "Object.Value10",//"Player.RollingAcceleration(OG)",
    "Object.Frame",//"Player.RollingDeceleration",
    "Player.Value1", //"Player.EntityNo",
    "Player.Skidding",//"Player.CollisionLeft",				
	"Player.CollisionTop(OG)",
    "Player.CollisionRight(OG)",
    "Player.Right",//"Player.CollisionBottom",
    "Player.DrawOrder",//"Player.Flailing",
    "Player.Timer(OG)",
    "Player.TopSpeed",//"Player.TileCollisions",
    "Player.Acceleration",//"Player.ObjectInteraction",
    "Player.Deceleration",//"Player.Visible",
    "Player.AirAcceleration",//"Player.Rotation",
    "Player.Scale(OG)",
    "Player.GravityStrength",//"Player.Priority",
    "Player.DrawOrder(OG)",
    "Player.Direction(OG)",
    "Player.JumpCap",//"Player.InkEffect",
    "Player.Alpha(OG)",
    "Player.Value9",//"Player.Frame",
    "Player.Direction",//"Player.Animation",
    "Player.PrevAnimation(OG)",
    "Player.Value12",//"Player.AnimationSpeed",
    "Player.AnimationTimer(OG)",
    "Player.Value0(OG)",
    "Player.PrevAnimation",//"Player.Value1",
    "Player.Value2(OG)",
    "Player.AnimationTimer",//"Player.Value3",
    "Player.Value4(OG)",
    "Player.Value5(OG)",
    "Player.Value6(OG)",
    "Player.Value7(OG)",
    "Player.Value8(OG)",
    "Player.Value9(OG)",
    "Player.Value10(OG)",
    "Player.Value11(OG)",
    "Player.DrawOrder",	//"Player.Value12",
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
"foreachGreater",
"foreachLower",
"loop(foreach)",
"switch",
"break",
"endswitch",
"Rand",
"Sin256",
"Cos256",
"SinChange",
"CosChange",
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
"SetDebugIcon",
"LoadPalette",
"RotatePalette",
"SetScreenFade",
"SetActivePalette",
"SetPaletteFade",
"SetPaletteEntryPacked",
"GetPaletteEntryPacked",
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
"GetStageName",
"Absolute",
"EngineCallback",
"CallEngineFunction", //Set Achievement
"CallEngineFunction", // Probably SetLeaderBoards
"SetObjectBorder",
"GetObjectAttribute",
"SetObjectAttribute",
"CopyObjectAttribute",
};

        readonly byte[] scriptOpcodeSizes = new byte[]
{
0, 2, 2, 2, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2,
2, 3, 3, 3, 3, 3, 3, 0, 0, 3, 3, 3, 3, 3, 3, 0, 3, 3,
0, 2, 0, 0, 2, 2, 2, 2, 2, 3, 4, 7, 1, 1, 1, 3, 3, 4,
7, 7, 3, 6, 7, 5, 4, 4, 3, 6, 3, 3, 5, 1, 4, 4, 1, 4,
3, 4, 0, 8, 5, 7, 4, 0, 0, 0, 0, 3, 1, 0, 0, 0, 4, 2,
1, 3, 4, 4, 1, 0, 1, 2, 4, 4, 2, 2, 2, 4, 1, 3, 1, 0,
6, 4, 4, 4, 3, 3, 1, 2, 3, 3, 4, 4, 2, 2, 0, 0, 2, 5,
2, 3, 3, 1, 1, 1, 3, 5, 1, 3, 3, 3, 3, 0,
};

        string[] BoolAliases = new string[]
{
            "true",
            "false"
};

        string[] FXAliases = new string[]
{
"FX_SCALE",
"FX_ROTATE",
"FX_ROTOZOOM",
"FX_INK",
"FX_FLIP",
"FX_UNKNOWN",
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
"C_BOX", //???
//"C_BOX2",
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
"FACING_LEFT",
"FACING_RIGHT",
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

        #region Misc
        public string[] functionNames = new string[0x200];
        public string[] typeNames = new string[0x100];
        public string[] sourceNames = new string[0x100];
        public string[] globalVariableNames = new string[0x100];
        public int[] globalVariables = new int[0x100];

        public int numGlobalSfx;
        public string[] sfxNames = new string[0x100];

        int m_stageVarsIndex;
        int functionCount = 0;

        public bool UseHex = false;

        #endregion

        #region Raw Bytecode Data
        int scriptDataPos;
        int jumpTableDataPos;
        public int[] scriptData = new int[0x40000];
        public int[] jumpTableData = new int[0x4000];

        int[] ScriptDataBytes = new int[0x4000];

        ScriptEngine scriptEng = new ScriptEngine();
        StateScriptEngine state = new StateScriptEngine();
        ObjectScript[] objectScriptList = new ObjectScript[0x100];
        FunctionScript[] functionScriptList = new FunctionScript[0x200];
        #endregion

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

        public Bytecode(Reader reader, int ScriptCount = 0, bool Gameconfig = true)
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

            m_stageVarsIndex = ScriptCount;

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

            //Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        public Bytecode(Reader reader, int ScriptCount = 0)
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

            m_stageVarsIndex = ScriptCount;

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

            ScriptDataBytes = new int[objectScriptList[1].mainScript];
            for (int i = 0; i < objectScriptList[1].mainScript; i++)
            {
                ScriptDataBytes[i] = scriptData[i];
            }

            //Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

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

            m_stageVarsIndex = ScriptCount;

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

            ScriptDataBytes = new int[objectScriptList[1].mainScript];
            for (int i = 0; i < objectScriptList[1].mainScript; i++)
            {
                ScriptDataBytes[i] = scriptData[i];
            }

            //Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        string _SetArrayValue(string strIn, string index)
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

        public void Decompile(string DestPath = "")
        {
            HexadecimalEncoding.UseHex = UseHex;
            for (int i = m_stageVarsIndex; i < sourceNames.Length; i++)
            {
                string path = "";
                if (DestPath != "")
                {
                    if (!Directory.Exists(DestPath + "/Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), "")))
                    {
                        DirectoryInfo d = new DirectoryInfo(DestPath + "/Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), ""));
                        d.Create();
                    }
                    Console.WriteLine(DestPath + "/Scripts/" + sourceNames[i]);

                    path = DestPath + "/Scripts/" + sourceNames[i];
                }
                else
                {
                    if (!Directory.Exists("Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), "")))
                    {
                        DirectoryInfo d = new DirectoryInfo("Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), ""));
                        d.Create();
                    }
                    Console.WriteLine("Scripts/" + sourceNames[i]);
                    path = "Scripts/" + sourceNames[i];
                }

                StreamWriter writer = new StreamWriter(path);

                Console.WriteLine("Decompiling: " + typeNames[i]);

                writer.WriteLine("//---------------------Sonic 1/2 " + typeNames[i] + " Script----------------------------//");
                writer.WriteLine("//--------Scripted by Christian Whitehead 'The Taxman' & Simon Thomley 'Stealth'--------//");
                writer.WriteLine("//-----------------Unpacked By Rubberduckycooly's Script Unpacker-----------------------//");

                writer.Write(Environment.NewLine);

                writer.WriteLine("//-------Aliases-------//");

                //for (int j = 0; j < typeNames.Length; j++)
                //writer.Write("#define " + typeNames[j] + " " + j + Environment.NewLine);

                writer.Write("#alias " + i + ": TYPE_" + typeNames[i].ToUpper().Replace(" ", "_") + Environment.NewLine);

                ObjectScript objectScript = objectScriptList[i];

                writer.Write(Environment.NewLine);
                writer.Write(Environment.NewLine);

                //try
                //{
                //if (objectScript.mainScript > 0 && objectScript.mainJumpTable > 0 && i != 0)
                //{
                Console.Write("Main script, ");
                writer.WriteLine("//---------------------------Main Sub---------------------------//");
                writer.WriteLine("//-------Called once a frame, use this for most functions-------//");
                DecompileScript(writer, objectScript.mainScript, objectScript.mainJumpTable, 0, false);
                //}

                //if (objectScript.drawScript > 0 && objectScript.drawJumpTable > 0 && i != 0)
                //{
                writer.WriteLine("//----------------------Drawing Sub-------------------//");
                writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
                Console.Write("Draw script, ");
                DecompileScript(writer, objectScript.drawScript, objectScript.drawJumpTable, 1, false);
                //}

                //if (objectScript.startupScript > 0 && objectScript.startupJumpTable > 0 && i != 0)
                //{
                writer.WriteLine("//--------------------Startup Sub---------------------//");
                writer.WriteLine("//-------Called once when the object is spawned-------//");
                Console.Write("Startup script, ");
                DecompileScript(writer, objectScript.startupScript, objectScript.startupJumpTable, 2, false);
                //}

                if (i == 1)//&& typeNames[i] == "PlayerObject")
                {
                    for (int ii = 0; ii < functionCount; ii++)
                    {
                        //if (functionScriptList[ii].mainScript > 0 && functionScriptList[ii].mainJumpTable > 0 && i != 0)
                        //{
                        writer.WriteLine("//--------------------Function Sub---------------------//");
                        writer.WriteLine("//-------it do shit-------//");
                        Console.WriteLine("Function script " + ii + ".");
                        DecompileScript(writer, functionScriptList[ii].mainScript, functionScriptList[ii].mainJumpTable, ii, true);
                        //}
                    }
                }

                //}
                //catch (Exception ex)
                //{
                //    writer.Write(Environment.NewLine);
                //    writer.Close();
                //    Console.WriteLine(ex.Message);
                //}

                writer.WriteLine("//--------------------RSDK Sub---------------------//");
                writer.WriteLine("//-----------Used for editor functionality---------//");
                Console.WriteLine("RSDK script.");
                writer.WriteLine("sub RSDK");
                writer.WriteLine();
                writer.WriteLine("//I put a 'dummy' sprite here so it shows up in retroED/RSDK! :)");
                writer.WriteLine("LoadSpriteSheet(" + "\"Global/Display.gif\"" + ")");
                writer.WriteLine("SetEditorIcon(Icon0,SingleIcon,0,0,32,32,1,143)");
                writer.WriteLine();
                writer.WriteLine("endsub");

                writer.Write(Environment.NewLine);
                writer.Close();
            }

            if (!Directory.Exists(DestPath + "/Scripts/" + sourceNames[3].Replace(Path.GetFileName(sourceNames[3]), "")))
            {
                DirectoryInfo d = new DirectoryInfo(DestPath + "/Scripts/" + sourceNames[3].Replace(Path.GetFileName(sourceNames[3]), ""));
                d.Create();
            }

            /*if (ScriptDataBytes.Length > 0)
            {
                Writer writer2 = new Writer(DestPath + "/Scripts/" + sourceNames[3].Replace(Path.GetFileName(sourceNames[3]), "") + "//ScriptData.bin");

                for (int i = 0; i < ScriptDataBytes.Length; i++)
                {
                    writer2.Write(ScriptDataBytes[i]);
                }
                writer2.Close();
            }*/

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

            if (scriptCodePtr == 0 && jumpTablePtr == 0)
            {
                //return;
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
            state.isSwitchEnd = false;
            state.error = false;

            try
            {
                DecompileSub(writer, isFunction);
            }
            catch (Exception ex)
            {
                Console.Write(isFunction ? "ERROR IN FUNCTION " + scriptSub : "ERROR IN SUB " + strFuncName);
                Console.WriteLine("! Error: " + ex.Message);
            }
            writer.Write(Environment.NewLine);
        }

        public void DecompileSub(StreamWriter writer, bool isfunction)
        {
            int objectLoop = 0;
            string index1 = "0";
            state.EndFlag = false;
            state.LoopBreakFlag = false;
            state.SwitchBreakFlag = false;
            writer.Write(Environment.NewLine);

            while (!state.EndFlag)
            {
                int num2 = 0;
                int opcode = scriptData[state.scriptCodePtr++];
                int paramsCount = scriptOpcodeSizes[opcode];

                //state.isSwitchEnd = false;

                string[] variableName = new string[10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNKNOWN VARIABLE";
                }

                for (int i = 0; i < paramsCount; i++)
                {
                    int paramId = scriptData[state.scriptCodePtr++];
                    switch (paramId)
                    {
                        case 1: // Read value from RSDK
                            switch (scriptData[state.scriptCodePtr++])
                            {
                                case 0:
                                    index1 = objectLoop.ToString();
                                    int tmp2 = scriptData[state.scriptCodePtr];
                                    variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    break;
                                case 1: // ARRAY
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                    { index1 = scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]]; }
                                    else
                                        index1 = scriptData[state.scriptCodePtr++].ToString();
                                    num2 += 2;

                                    variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], index1.ToString());
                                    break;
                                case 2:
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                        index1 = (objectLoop - scriptData[state.scriptCodePtr++]).ToString();
                                    else
                                        index1 = (objectLoop - scriptData[state.scriptCodePtr++]).ToString();
                                    num2 += 2;
                                    variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    break;
                                case 3:
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                        index1 = (objectLoop - scriptData[state.scriptCodePtr++]).ToString();
                                    else
                                        index1 = (objectLoop - scriptData[state.scriptCodePtr++]).ToString();
                                    num2 += 2;
                                    variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    break;
                            }
                            num2 += 3;
                            break;
                        case 2: // Read constant value from bytecode
                            scriptEng.operands[i] = scriptData[state.scriptCodePtr++];
                            variableName[i] = "";
                            variableName[i] = variableName[i] + scriptEng.operands[i]; //it's an int!!
                            num2 += 2;
                            break;
                        case 3: // Read string
                            string tmp = "";
                            num2++;
                            int strLen = scriptData[state.scriptCodePtr];
                            for (int j = 0; j < strLen;)
                            {
                                state.scriptCodePtr++;
                                num2++;
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
                                num2 += 2;
                                break;
                            }
                            state.scriptCodePtr++;
                            num2++;
                            break;
                    }
                }

                string operand = opcodeList[opcode];

                if (operand == "End" || operand == "EndFunction")
                {
                    if (isfunction) writer.Write("endfunction");
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
                            state.SwitchBreakFlag = true;
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            state.deep--;
                            // do a peek if the next statement is an endswitch
                            if (scriptData[state.scriptCodePtr] == 0x24)
                            {
                                //state.isSwitchEnd = true;
                            }
                            break;
                        case 0x27: // end switch
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            //state.deep--;
                            state.isSwitchEnd = true;
                            break;
                        default:
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                    }

                    if (opcode >= 138)
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

                    // Special Aliases for some functions
                    switch (operand)
                    {
                        case "DrawSpriteFX":
                            int o = 0;
                            Int32.TryParse((variableName[1]), out o);
                            //o--;
                            if (o < 0) o = 0;
                            variableName[1] = FXAliases[o];
                            break;
                        case "DrawSpriteScreenFX":
                            int oo = 0;
                            Int32.TryParse((variableName[1]), out oo);
                            // oo--;
                            if (oo < 0) oo = 0;
                            variableName[1] = FXAliases[oo];
                            break;
                        case "PlayerObjectCollision":
                            int ooo = 0;
                            Int32.TryParse(variableName[0], out ooo);
                            if (ooo < CollisionAliases.Length) variableName[0] = CollisionAliases[ooo];
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
                        switch (variableName[0])
                        {
                            case "Engine.PlatformID":
                                variableName[1] = StagesAliases[Int32.Parse(variableName[1])];
                                break;
                            case "Stage.ActiveList":
                                variableName[1] = StagesAliases[Int32.Parse(variableName[1])];
                                break;
                            case "Stage.Stage":
                                variableName[1] = StageStateAliases[Int32.Parse(variableName[1])];
                                break;
                        }

                        switch (variableName[1])
                        {
                            case "Engine.PlatformID":
                                variableName[2] = StagesAliases[Int32.Parse(variableName[2])];
                                break;
                            case "Stage.ActiveList":
                                variableName[2] = StagesAliases[Int32.Parse(variableName[2])];
                                break;
                            case "Stage.Stage":
                                variableName[2] = StageStateAliases[Int32.Parse(variableName[2])];
                                break;
                        }

                        //I'll do it later
                        /*switch (variableName[2])
                        {
                            case "0":
                                variableName[2] = BoolAliases[0];
                                break;
                            case "1":
                                variableName[2] = BoolAliases[1];
                                break;
                        }*/
                    }

                    switch (opcode)
                    {
                        case 0x00:
                            writer.Write("endsub");
                            state.EndFlag = true;
                            state.deep = 0;
                            break;
                        case 0x01: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x02: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "+=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x03: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "-=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x04: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "++"); break;
                        case 0x05: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "--"); break;
                        case 0x06: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "*=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x07: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "/=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x08: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + ">>=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x09: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "<<=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x0A: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "&=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x0B: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "|=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x0C: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "^=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x0D: writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "%=" + HexadecimalEncoding.ToHexString(variableName[1])); break;
                        case 0x13:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "==" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x14:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + ">" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x15:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + ">=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x16:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "<" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x17:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "<=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x18:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "!=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x19:
                            writer.Write("else");
                            break;
                        case 0x1A:
                            writer.Write("endif");
                            break;
                        case 0x1B:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "==" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x1C:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + ">" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x1D:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + ">=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x1E:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "<" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x1F:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "<=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x20:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "!=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //DecompileSub(writer);
                            break;
                        case 0x21:
                            writer.Write("loop");
                            break;
                        case 0x22:
                            writer.Write("for " + HexadecimalEncoding.ToHexString(variableName[2]) + ">" + HexadecimalEncoding.ToHexString(variableName[1]));
                            state.deep += 1;
                            break;
                        case 0x23:
                            writer.Write("for " + HexadecimalEncoding.ToHexString(variableName[2]) + "<" + HexadecimalEncoding.ToHexString(variableName[1]));
                            state.deep += 1;
                            break;
                        case 0x24:
                            writer.Write("loop (foreach)");
                            break;
                        case 0x25:
                            writer.Write("switch " + HexadecimalEncoding.ToHexString(variableName[1]) + Environment.NewLine);
                            state.SwitchCheck = false;
                            state.SwitchDeep++;

                            for (int i = 0; !state.isSwitchEnd;)
                            {
                                //LABEL_1:
                                if (!state.SwitchCheck)
                                {
                                    for (int j = 0; j < state.deep; j++) { writer.Write("\t"); }
                                    writer.Write("case " + i);
                                    state.SwitchCheck = true;
                                    state.deep += 1;
                                    i++;
                                }
                                DecompileSub(writer, isfunction);
                                /*if (state.SwitchDeep >= 1 && state.isSwitchEnd)
                                {
                                    state.isSwitchEnd = false;
                                    //goto LABEL_1;
                                    return;
                                }*/
                            }
                            state.isSwitchEnd = false;
                            break;
                        case 0x26:
                            writer.Write("break");
                            if (scriptData[state.scriptCodePtr] == 0x27)
                            {
                                state.scriptCodePtr++;
                                writer.Write(Environment.NewLine);
                                for (int i = 0; i < state.deep; i++) writer.Write("\t");
                                writer.Write("endswitch");
                                state.SwitchDeep--;
                                state.SwitchCheck = false;
                                state.isSwitchEnd = true;
                            }
                            state.SwitchCheck = false;
                            break;
                        case 0x27:
                            writer.Write("endswitch");
                            state.SwitchDeep--;
                            state.SwitchCheck = false;
                            state.isSwitchEnd = true;
                            return;
                        default:
                            writer.Write(operand + "(");
                            switch (paramsCount)
                            {
                                case 1:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]));
                                    break;
                                case 2:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]));
                                    break;
                                case 3:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]));
                                    break;
                                case 4:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]) + "," + HexadecimalEncoding.ToHexString(variableName[3]));
                                    break;
                                case 5:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]) + "," + HexadecimalEncoding.ToHexString(variableName[3]) + "," + HexadecimalEncoding.ToHexString(variableName[4]));
                                    break;
                                case 6:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]) + "," + HexadecimalEncoding.ToHexString(variableName[3]) + "," + HexadecimalEncoding.ToHexString(variableName[4]) + "," + HexadecimalEncoding.ToHexString(variableName[5]));
                                    break;
                                case 7:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]) + "," + HexadecimalEncoding.ToHexString(variableName[3]) + "," + HexadecimalEncoding.ToHexString(variableName[4]) + "," + HexadecimalEncoding.ToHexString(variableName[5]) + "," + HexadecimalEncoding.ToHexString(variableName[6]));
                                    break;
                                case 8:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]) + "," + HexadecimalEncoding.ToHexString(variableName[3]) + "," + HexadecimalEncoding.ToHexString(variableName[4]) + "," + HexadecimalEncoding.ToHexString(variableName[5]) + "," + HexadecimalEncoding.ToHexString(variableName[6]) + "," + HexadecimalEncoding.ToHexString(variableName[7]));
                                    break;
                                case 9:
                                    writer.Write(HexadecimalEncoding.ToHexString(variableName[0]) + "," + HexadecimalEncoding.ToHexString(variableName[1]) + "," + HexadecimalEncoding.ToHexString(variableName[2]) + "," + HexadecimalEncoding.ToHexString(variableName[3]) + "," + HexadecimalEncoding.ToHexString(variableName[4]) + "," + HexadecimalEncoding.ToHexString(variableName[5]) + "," + HexadecimalEncoding.ToHexString(variableName[6]) + "," + HexadecimalEncoding.ToHexString(variableName[7]) + "," + HexadecimalEncoding.ToHexString(variableName[8]));
                                    break;
                            }
                            writer.Write(")");
                            break;
                    }

                    //if (!state.isSwitchEnd && !state.EndFlag)
                    writer.Write(Environment.NewLine);

                    if (state.SwitchBreakFlag)
                    {
                        state.SwitchBreakFlag = false;
                        return;
                    }

                    if (state.LoopBreakFlag)
                    {
                        state.LoopBreakFlag = false;
                        //return;
                    }
                }
            }
        }

        public void Write(Writer writer)
        {

        }

    }

    public static class stringshit
    {

        public static bool isNumber(string str)
        {
            return int.TryParse(str, out int value);
        }

    }

    public class HexadecimalEncoding
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

            try
            {
                int value;
                bool isNumeric = int.TryParse(str, out value);

                if (!isNumeric) return str;

                if (value < 1023 && value > -1023)
                {
                    return str;
                }

                sb.Append("0x");
                sb.Append(value.ToString("X"));

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return str;
            }
        }

        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
    }

}
