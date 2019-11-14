using System;
using System.IO;
using System.Text;

namespace RSDKv2
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
            public int playerScript;
            public int drawScript;
            public int startupScript;
            public int mainJumpTable;
            public int playerJumpTable;
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
            public int jumpTablePtr;
            public int scriptCodeOffset;
            public int jumpTableOffset;
            public int scriptSub;
            public int deep;
            public int SwitchDeep;
            public bool isSwitchEnd;
            public bool error;
            public bool SwitchCheck;
            public bool DefaultFlag;
            public bool EndFlag;
            public bool LoopBreakFlag;
            public bool SwitchBreakFlag;
            public int[] SwitchValues;
            public int SwitchCase;

            public StateScriptEngine IncDeep()
            {
                this.deep += 1;
                return this;
            }
        };

        class ScriptEngine
        {
            public int[] operands;
            public string[] tempValue = new string[]
            {
                "TempValue0",
                "TempValue1",
                "TempValue2",
                "TempValue3",
                "TempValue4",
                "TempValue5",
                "TempValue6",
                "TempValue7",
            };
            public string[] arrayPosition = new string[]
            {
                "ArrayPos0",
                "ArrayPos1",
                "TempObjectPos",
            };
        };

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
	"Engine.HapticsEnabled",
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
"switch",
"break",
"endswitch",
"Rand",
"Sin",
"Cos",
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
"SetEditorIcon",
"LoadPalette",
"RotatePalette",
"SetScreenFade",
"SetActivePalette",
"SetPaletteFade",
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
"BindPlayerToObject",
"PlayerTileCollision",
"ProcessPlayerControl",
"ProcessAnimation",
"DrawObjectAnimation",
"DrawPlayerAnimation",
"SetMusicTrack",
"PlayMusic",
"StopMusic",
"PlaySfx",
"StopSfx",
"SetSfxAttributes",
"ObjectTileCollision",
"ObjectTileGrip",
"LoadVideo",
"NextVideoFrame",
"PlayStageSfx",
"StopStageSfx",
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
"TransformVertices",
"CallFunction",
"EndFunction",
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
"EngineCallback",
"QueueHapticEffect"
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

        string[] BoolAliases = new string[]
        {
            "false",
            "true",
        }; //ADDED

        string[] FXAliases = new string[]
{
"FX_SCALE",
"FX_ROTATE",
"FX_ROTOZOOM",
"FX_INK",
"FX_TINT",
"FX_FLIP",
}; //ADDED

        string[] StagesAliases = new string[]
{
"PRESENTATION_STAGE",
"REGULAR_STAGE",
"BONUS_STAGE",
"SPECIAL_STAGE",
}; //ADDED

        string[] MenuAliases = new string[]
{
"MENU_1",
"MENU_2",
}; // ADDED

        string[] CollisionAliases = new string[]
{
"C_TOUCH",
"C_BOX",
"C_BOX", //???
//"C_BOX2",
"C_PLATFORM",
}; //ADDED

        string[] MatAliases = new string[]
{
"MAT_WORLD",
"MAT_VIEW",
"MAT_TEMP",
}; //ADDED

        string[] DirectionAliases = new string[]
{
"FACING_LEFT",
"FACING_RIGHT",
}; //NOT YET ADDED

        string[] StageStateAliases = new string[]
{
"STAGE_PAUSED",
"STAGE_RUNNING",
"RESET_GAME",
}; //ADDED

        string[] PlatformAliases = new string[]
        {
        "RETRO_WIN",
        "RETRO_OSX",
        "RETRO_X_360",
        "RETRO_PS3",
        "RETRO_IOS",
        "RETRO_ANDROID",
        "RETRO_WP7",
        }; //ADDED

        #region Misc
        public string[] functionNames = new string[0x200];
        public string[] typeNames = new string[0x100];
        public string[] sourceNames = new string[0x100];
        public string[] globalVariableNames = new string[0x100];
        public int[] globalVariables = new int[0x100];

        int m_stageVarsIndex;

        public bool UseHex = false;
        #endregion

        #region Raw Bytecode Data
        int scriptDataPos;
        int jumpTableDataPos;
        public int[] scriptData = new int[0x40000];
        public int[] jumpTableData = new int[0x40000];

        ScriptEngine scriptEng = new ScriptEngine();
        StateScriptEngine state = new StateScriptEngine();
        ObjectScript[] objectScriptList = new ObjectScript[0x100];
        FunctionScript[] functionScriptList = new FunctionScript[0x100];
        int functionCount = 0;
        #endregion

        int Read32(Reader reader)
        {
            return reader.ReadInt32();
            //return reader.ReadByte() + (reader.ReadByte() << 8) + (reader.ReadByte() << 16) + (reader.ReadByte() << 24);
        }
        short Read16(Reader reader)
        {
            return reader.ReadInt16();
            //return (short)(reader.ReadByte() + (reader.ReadByte() << 8));
        }
        byte Read8(Reader reader)
        {
            return reader.ReadByte();
        }

        public Bytecode(Reader reader, int ScriptCount = 0, bool MobileVer = false)
        {
            ClearScriptData();
            scriptEng.operands = new int[10];

            if (MobileVer)
            {
                LoadMobileBytecode(reader, ScriptCount);
                return;
            }

            for (int opcount = Read32(reader); opcount > 0;)
            {
                byte data = Read8(reader);
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
                byte data = Read8(reader);
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

            int count = Read16(reader); //File count

            for (int i = 0; i < count; i++)
            {
                objectScriptList[ScriptCount + i].mainScript = Read32(reader);
                objectScriptList[ScriptCount + i].playerScript = Read32(reader);
                objectScriptList[ScriptCount + i].drawScript = Read32(reader);
                objectScriptList[ScriptCount + i].startupScript = Read32(reader);
            }
            for (int i = 0; i < count; i++)
            {
                objectScriptList[ScriptCount + i].mainJumpTable = Read32(reader);
                objectScriptList[ScriptCount + i].playerJumpTable = Read32(reader);
                objectScriptList[ScriptCount + i].drawJumpTable = Read32(reader);
                objectScriptList[ScriptCount + i].startupJumpTable = Read32(reader);
            }

            functionCount = Read16(reader);

            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainScript = Read32(reader);
            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainJumpTable = Read32(reader);



            Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        public void LoadMobileBytecode(Reader reader, int ScriptCount = 0)
        {
            scriptEng.operands = new int[10];

            for (int opcount1 = Read32(reader); opcount1 > 0;)
            {
                byte data = Read8(reader);
                int blocksCount = data & 0x7F;

                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read8(reader); }
                    opcount1 -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read32(reader); }
                    opcount1 -= blocksCount;
                }
            }

            int opcount = Read32(reader);
            int count;

            if (opcount <= 0)
            {
                count = Read16(reader); //File count

                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainScript = Read32(reader);
                    objectScriptList[ScriptCount + i].playerScript = Read32(reader);
                    objectScriptList[ScriptCount + i].drawScript = Read32(reader);
                    objectScriptList[ScriptCount + i].startupScript = Read32(reader);
                }
                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].playerJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].drawJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].startupJumpTable = Read32(reader);
                }

                count = Read16(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainScript = Read32(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainJumpTable = Read32(reader);

            }


            for (; opcount > 0;)
            {
                byte data = Read8(reader);
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

            if (opcount > 0)
            {
                count = Read16(reader); //File count

                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainScript = Read32(reader);
                    objectScriptList[ScriptCount + i].playerScript = Read32(reader);
                    objectScriptList[ScriptCount + i].drawScript = Read32(reader);
                    objectScriptList[ScriptCount + i].startupScript = Read32(reader);
                }
                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].playerJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].drawJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].startupJumpTable = Read32(reader);
                }

                count = Read16(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainScript = Read32(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainJumpTable = Read32(reader);
            }

            m_stageVarsIndex = ScriptCount;

            Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        public void LoadStageBytecodeData(Reader reader, int ScriptCount = 0, bool MobileVer = false)
        {
            //ClearScriptData();
            scriptEng.operands = new int[10];

            if (MobileVer)
            {
                LoadStageBytecodeDataMobile(reader, ScriptCount);
                return;
            }

            for (int opcount = Read32(reader); opcount > 0;)
            {
                byte data = Read8(reader);
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
                byte data = Read8(reader);
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

            int count = Read16(reader); //File count

            for (int i = 0; i < count; i++)
            {
                objectScriptList[ScriptCount + i].mainScript = Read32(reader);
                objectScriptList[ScriptCount + i].playerScript = Read32(reader);
                objectScriptList[ScriptCount + i].drawScript = Read32(reader);
                objectScriptList[ScriptCount + i].startupScript = Read32(reader);
            }
            for (int i = 0; i < count; i++)
            {
                objectScriptList[ScriptCount + i].mainJumpTable = Read32(reader);
                objectScriptList[ScriptCount + i].playerJumpTable = Read32(reader);
                objectScriptList[ScriptCount + i].drawJumpTable = Read32(reader);
                objectScriptList[ScriptCount + i].startupJumpTable = Read32(reader);
            }

            functionCount = Read16(reader);

            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainScript = Read32(reader);
            for (int i = 0; i < functionCount; i++)
                functionScriptList[i].mainJumpTable = Read32(reader);

            Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        public void LoadStageBytecodeDataMobile(Reader reader, int ScriptCount = 0)
        {
            scriptEng.operands = new int[10];

            for (int opcount1 = Read32(reader); opcount1 > 0;)
            {
                byte data = Read8(reader);
                int blocksCount = data & 0x7F;

                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read8(reader); }
                    opcount1 -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = Read32(reader); }
                    opcount1 -= blocksCount;
                }
            }

            int opcount = Read32(reader);
            int count;

            if (opcount <= 0)
            {
                count = Read16(reader); //File count

                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainScript = Read32(reader);
                    objectScriptList[ScriptCount + i].playerScript = Read32(reader);
                    objectScriptList[ScriptCount + i].drawScript = Read32(reader);
                    objectScriptList[ScriptCount + i].startupScript = Read32(reader);
                }
                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].playerJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].drawJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].startupJumpTable = Read32(reader);
                }

                count = Read16(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainScript = Read32(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainJumpTable = Read32(reader);

            }


            for (; opcount > 0;)
            {
                byte data = Read8(reader);
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

            if (opcount > 0)
            {
                count = Read16(reader); //File count

                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainScript = Read32(reader);
                    objectScriptList[ScriptCount + i].playerScript = Read32(reader);
                    objectScriptList[ScriptCount + i].drawScript = Read32(reader);
                    objectScriptList[ScriptCount + i].startupScript = Read32(reader);
                }
                for (int i = 0; i < count; i++)
                {
                    objectScriptList[ScriptCount + i].mainJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].playerJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].drawJumpTable = Read32(reader);
                    objectScriptList[ScriptCount + i].startupJumpTable = Read32(reader);
                }

                count = Read16(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainScript = Read32(reader);
                for (int i = 0; i < count; i++)
                    functionScriptList[i].mainJumpTable = Read32(reader);
            }

            m_stageVarsIndex = ScriptCount;

            Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        public void ClearScriptData()
        {
            for (int i = 0; i < 0x40000; i++)
                scriptData[i] = 0;

            for (int i = 0; i < 0x4000; i++)
                jumpTableData[i] = 0;

            scriptDataPos = 0;
            jumpTableDataPos = 0;

            for (int i = 0; i < 256; i++)
            {
                objectScriptList[i].mainScript = 0x3FFFF;
                objectScriptList[i].mainJumpTable = 0x3FFF;
                objectScriptList[i].playerScript = 0x3FFFF;
                objectScriptList[i].playerJumpTable = 0x3FFF;
                objectScriptList[i].drawScript = 0x3FFFF;
                objectScriptList[i].drawJumpTable = 0x3FFF;
                objectScriptList[i].startupScript = 0x3FFFF;
                objectScriptList[i].startupJumpTable = 0x3FFF;
                objectScriptList[i].frameListOffset = 0;
                objectScriptList[i].numFrames = 0;
                objectScriptList[i].surfaceNum = 0;
                objectScriptList[i].animationFile = AnimationSystem.GetDefaultAnimationRef();
                functionScriptList[i].mainScript = 0x3FFFF;
                functionScriptList[i].mainJumpTable = 0x3FFF;
            }
        }

        string _SetArrayValue(string strIn, string index)
        {
            string strOut = strIn;
            int point = -1;

            if (strIn == "Global")
            {
                strOut = globalVariableNames[Int32.Parse(index)];
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

                writer.WriteLine("//------------Sonic CD " + typeNames[i] + " Script-------------//");
                writer.WriteLine("//--------Scripted by Christian Whitehead 'The Taxman'--------//");
                writer.WriteLine("//-------Unpacked By Rubberduckycooly's Script Unpacker-------//");

                writer.Write(Environment.NewLine);

                writer.WriteLine("//-------Aliases-------//");

                writer.Write("#alias " + i + ": TYPE_" + typeNames[i].ToUpper().Replace(" ", "_") + Environment.NewLine);

                if (typeNames[i] == "Monitor")
                {
                    Console.WriteLine();
                }

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

                //if (objectScript.playerScript > 0 && objectScript.playerJumpTable > 0 && i != 0)
                //{
                writer.WriteLine("//-------------------------Player Interaction Sub---------------------------//");
                writer.WriteLine("//-------This sub is called when the object interacts with the player-------//");
                Console.Write("Player script, ");
                DecompileScript(writer, objectScript.playerScript, objectScript.playerJumpTable, 1, false);
                //}

                //if (objectScript.drawScript > 0 && objectScript.drawJumpTable > 0 && i != 0)
                //{
                writer.WriteLine("//----------------------Drawing Sub-------------------//");
                writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
                Console.Write("Draw script, ");
                DecompileScript(writer, objectScript.drawScript, objectScript.drawJumpTable, 2, false);
                //}

                //if (objectScript.startupScript > 0 && objectScript.startupJumpTable > 0 && i != 0)
                //{
                writer.WriteLine("//--------------------Startup Sub---------------------//");
                writer.WriteLine("//-------Called once when the object is spawned-------//");
                Console.Write("Startup script, ");
                DecompileScript(writer, objectScript.startupScript, objectScript.startupJumpTable, 3, false);
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
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}

                writer.Write(Environment.NewLine);
                writer.Close();
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
                    strFuncName = "ObjectPlayerInteraction";
                    break;
                case 2:
                    strFuncName = "ObjectDraw";
                    break;
                case 3:
                    strFuncName = "ObjectStartup";
                    break;
                case 4:
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

            DecompileSub(writer, isFunction);
            writer.Write(Environment.NewLine);
        }

        public void DecompileSub(StreamWriter writer, bool isFunction)
        {
            //int objectLoop = 1056;
            //string index1 = "0";
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
                                case 0: //Read Const Variable
                                    int tmp2 = scriptData[state.scriptCodePtr];
                                    variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    break;
                                case 1: // ARRAY
                                    if (scriptData[state.scriptCodePtr++] == 1) // Variable
                                    {
                                        string value = scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                        variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    else //Value
                                    {
                                        string value = scriptData[state.scriptCodePtr++].ToString();
                                        variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    num2 += 2;
                                    break;
                                case 2:
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                    {
                                        string value = scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                        variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    else
                                    {
                                        string value = scriptData[state.scriptCodePtr++].ToString();
                                        variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    num2 += 2;
                                    //variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    //variableName[i] = _SetArrayValue(variableName[i], objectLoop);
                                    break;
                                case 3:
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                    {
                                        string value = scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                        variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    else
                                    {
                                        string value = scriptData[state.scriptCodePtr++].ToString();
                                        variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], value);
                                    }
                                    num2 += 2;
                                    //variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    //variableName[i] = _SetArrayValue(variableName[i], objectLoop);
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
                        case 0x23: // break
                            state.SwitchBreakFlag = true;
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            state.deep--;
                            // do a peek if the next statement is an endswitch
                            if (scriptData[state.scriptCodePtr] == 0x24)
                            {
                                //state.isSwitchEnd = true;
                            }
                            break;
                        case 0x24: // end switch
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            //state.deep--;
                            state.isSwitchEnd = true;
                            break;
                        default:
                            for (int i = 0; i < state.deep; i++) writer.Write("\t");
                            break;
                    }


                    if (opcode >= 134)
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

                    if (variableName[0].Contains("Type"))
                    {
                        //variableName[1] = "TypeName[" + typeNames[Convert.ToInt32(variableName[1])] + "]";
                        //variableName[1] = "TypeName[" + Convert.ToInt32(variableName[1]) + "]";
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
                            if (isFunction) writer.Write("endfunction");
                            else writer.Write("endsub");
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
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x14:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + ">" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x15:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + ">=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x16:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "<" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x17:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "<=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x18:
                            writer.Write("if " + HexadecimalEncoding.ToHexString(variableName[1]) + "!=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x19:
                            writer.Write("else");
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x1A:
                            writer.Write("endif");
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x1B:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "==" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x1C:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + ">" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x1D:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + ">=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x1E:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "<" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x1F:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "<=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x20:
                            writer.Write("while " + HexadecimalEncoding.ToHexString(variableName[1]) + "!=" + HexadecimalEncoding.ToHexString(variableName[2]));
                            state.deep += 1;
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x21:
                            writer.Write("loop");
                            //state.jumpTablePtr += 4;
                            break;
                        case 0x22:
                            writer.WriteLine("switch " + HexadecimalEncoding.ToHexString(variableName[1]) + Environment.NewLine);
                            state.SwitchCheck = false;
                            state.SwitchDeep++;

                            for (int i = 0; !state.isSwitchEnd;)
                            {
                                //LABEL_1:
                                if (!state.SwitchCheck)
                                {
                                    for (int j = 0; j < state.deep; j++) { writer.Write("\t"); }
                                    int scrd = scriptData[state.scriptCodePtr];
                                    int jmpd = jumpTableData[state.jumpTablePtr];
                                    int dta = scriptData[(4 * Int32.Parse(variableName[0]))];
                                    int cse = scriptData[jumpTableData[state.jumpTablePtr]];

                                    //Our Case Value
                                    int CurCase = jumpTableData[state.jumpTablePtr + Int32.Parse(variableName[0])];

                                    int CurCase2 = jumpTableData[state.jumpTablePtr + Int32.Parse(variableName[0]) + 1];
                                    int Epic = jumpTableData[state.jumpTablePtr + Int32.Parse(variableName[0]) + 2];

                                    int bruh = scriptData[state.scriptCodePtr + 4 * Int32.Parse(variableName[0])];
                                    int bruh2 = scriptData[state.scriptCodePtr + 4 * Int32.Parse(variableName[0]) + 1];
                                    int bruh3 = scriptData[state.scriptCodePtr + 4 * Int32.Parse(variableName[0]) + 2];

                                    //writer.Write("case " + i);
                                    if (jmpd != 13)
                                    {
                                        writer.Write("case " + CurCase);
                                    }
                                    else
                                    {
                                        writer.Write("default");
                                    }
                                    state.SwitchCheck = true;
                                    state.deep += 1;
                                    i++;
                                }
                                DecompileSub(writer, isFunction);
                            }
                            state.isSwitchEnd = false;
                            state.jumpTablePtr = state.jumpTableOffset;
                            continue;
                        case 0x23:
                            writer.Write("break");
                            state.jumpTablePtr++;
                            if (scriptData[state.scriptCodePtr] == 0x24)
                            {
                                state.scriptCodePtr++;
                                //state.jumpTablePtr += 4;
                                writer.Write(Environment.NewLine);
                                for (int i = 0; i < state.deep; i++) writer.Write("\t");
                                writer.Write("endswitch");
                                state.SwitchDeep--;
                                state.SwitchCheck = false;
                                state.isSwitchEnd = true;
                            }
                            state.SwitchCheck = false;
                            break;
                        case 0x24:
                            writer.Write("endswitch");
                            state.SwitchDeep--;
                            //state.jumpTablePtr += 4;
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

        public void Write(Writer writer)
        {

            //int opcount = Read32(reader)
            int opcount = 0;
            writer.Write(opcount);

            for (; opcount > 0;)
            {
                //int data = Read8(reader);
                int data = 0;
                writer.Write((byte)data);
                int blocksCount = data & 0x7F;

                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    {
                        writer.Write((byte)scriptData[scriptDataPos++]);
                    }
                    opcount -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    {
                        writer.Write(scriptData[scriptDataPos++]);
                    }
                    opcount -= blocksCount;
                }
            }

            //int opcount = Read32(reader)
            int opcount2 = 0;
            writer.Write(opcount2);

            for (; opcount2 > 0;)
            {
                //int data = Read8(reader);
                int data = 0;
                writer.Write((byte)data);

                int blocksCount = data & 0x7F;
                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    {
                        writer.Write((byte)jumpTableData[jumpTableDataPos++]);
                    }
                    opcount2 -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    {
                        writer.Write(jumpTableData[jumpTableDataPos++]);
                    }
                    opcount2 -= blocksCount;
                }
            }

            //int count = Read16(reader);
            int count = 0;
            writer.Write((ushort)count);

            for (int i = 0; i < count; i++)
            {
                writer.Write(objectScriptList[m_stageVarsIndex + i].mainScript);
                writer.Write(objectScriptList[m_stageVarsIndex + i].playerScript);
                writer.Write(objectScriptList[m_stageVarsIndex + i].drawScript);
                writer.Write(objectScriptList[m_stageVarsIndex + i].startupScript);
            }
            for (int i = 0; i < count; i++)
            {
                writer.Write(objectScriptList[m_stageVarsIndex + i].mainJumpTable);
                writer.Write(objectScriptList[m_stageVarsIndex + i].playerJumpTable);
                writer.Write(objectScriptList[m_stageVarsIndex + i].drawJumpTable);
                writer.Write(objectScriptList[m_stageVarsIndex + i].startupJumpTable);
            }

            //count = Read16(reader);
            int count2 = 0;
            writer.Write((ushort)count);

            for (int i = 0; i < count2; i++)
            {
                writer.Write(functionScriptList[i].mainScript);
            }
            for (int i = 0; i < count2; i++)
            {
                writer.Write(functionScriptList[i].mainJumpTable);
            }
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
