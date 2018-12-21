using System;
using System.IO;

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
                this.deep+= 1;
                return this;
            }
        };

        struct ScriptEngine
        {
            public int[] operands;
            public int[] tempValue;
            public int[] arrayPosition;
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
	"Engine.Haptics",					// 0xE4 ???
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

        #region Misc
        public string[] functionNames = new string[0x200];
        public string[] typeNames = new string[0x100];
        public string[] sourceNames = new string[0x100];
        public string[] globalVariableNames = new string[0x100];
        public int[] globalVariables = new int[0x100];

        public int numGlobalSfx;
        public string[] sfxNames = new string[0x100];

        int m_stageVarsIndex;

        #endregion

        #region Raw Bytecode Data
        int scriptDataPos;
        int jumpTableDataPos;
        int[] scriptData = new int[0x40000];
        int[] jumpTableData = new int[0x4000];
        int[] jumpTableStack = new int[0x400];
        int[] functionStack = new int[0x400];

        ScriptEngine scriptEng;
        StateScriptEngine state = new StateScriptEngine();
        ObjectScript[] objectScriptList =new ObjectScript[0x100];
        FunctionScript[] functionScriptList = new FunctionScript[0x200];
        #endregion

        int Read32(Reader reader)
        {
            return reader.ReadInt32();
        }
        int Read16(Reader reader)
        {
            return reader.ReadInt16();
        }
        int Read8(Reader reader)
        {
            return reader.ReadByte();
        }

        public Bytecode(Reader reader, int ScriptCount = 0)
        {
            //ClearScriptData();
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];
            scriptEng.arrayPosition = new int[3];

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
                objectScriptList[m_stageVarsIndex + i].mainScript = Read32(reader);
                objectScriptList[m_stageVarsIndex + i].playerScript = Read32(reader);
                objectScriptList[m_stageVarsIndex + i].drawScript = Read32(reader);
                objectScriptList[m_stageVarsIndex + i].startupScript = Read32(reader);
            }
            for (int i = 0; i < count; i++)
            {
                objectScriptList[m_stageVarsIndex + i].mainJumpTable = Read32(reader);
                objectScriptList[m_stageVarsIndex + i].playerJumpTable = Read32(reader);
                objectScriptList[m_stageVarsIndex + i].drawJumpTable = Read32(reader);
                objectScriptList[m_stageVarsIndex + i].startupJumpTable = Read32(reader);
            }

            count = Read16(reader);
            for (int i = 0; i < count; i++)
                functionScriptList[i].mainScript = Read32(reader);
            for (int i = 0; i < count; i++)
                functionScriptList[i].mainJumpTable = Read32(reader);

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
                typeNames[i] = "";
            }

            //SetObjectTypeName("BlankObject", 0);
        }

        string _SetArrayValue(string strIn, int index)
        {
            string strOut = strIn;
            int point = -1;

            if (strIn == "Global")
            {
                strOut = globalVariableNames[index];
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

                writer.WriteLine("//------------Sonic CD "+ typeNames[i] + " Script-------------//");
                writer.WriteLine("//--------Scripted by Christian Whitehead 'The Taxman'--------//");
                writer.WriteLine("//-------Unpacked By Rubberduckycooly's Script Unpacker-------//");

                writer.Write(Environment.NewLine);

                writer.WriteLine("//-------Object Definitions-------//");

                for (int j = 0; j < typeNames.Length; j++)
                    writer.Write("#define " + typeNames[j] + " " + j + Environment.NewLine);

                ObjectScript objectScript = objectScriptList[i];


                FunctionScript functionScript = functionScriptList[i];

                writer.Write(Environment.NewLine);
                writer.Write(Environment.NewLine);

                try
                {
                    Console.Write("Main script, ");
                    writer.WriteLine("//---------------------------Main Sub---------------------------//");
                    writer.WriteLine("//-------Called once a frame, use this for most functions-------//");
                    DecompileScript(writer, objectScript.mainScript, objectScript.mainJumpTable, 0);

                    writer.WriteLine("//-------------------------Player Interaction Sub---------------------------//");
                    writer.WriteLine("//-------This sub is called when the object interacts with the player-------//");
                    Console.Write("Player script, ");
                    DecompileScript(writer, objectScript.playerScript, objectScript.playerJumpTable, 1);

                    writer.WriteLine("//----------------------Drawing Sub-------------------//");
                    writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
                    Console.Write("Draw script, ");
                    DecompileScript(writer, objectScript.drawScript, objectScript.drawJumpTable, 2);

                    writer.WriteLine("//--------------------Startup Sub---------------------//");
                    writer.WriteLine("//-------Called once when the object is spawned-------//");
                    Console.WriteLine("Startup script.");
                    DecompileScript(writer, objectScript.startupScript, objectScript.startupJumpTable, 3);


                    DecompileScript(writer, functionScript.mainScript, functionScript.mainJumpTable, 4);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                writer.Write(Environment.NewLine);
                writer.Close();
            }
        }

        public void DecompileScript(StreamWriter writer, int scriptCodePtr, int jumpTablePtr, int scriptSub)
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
                default:
                    strFuncName = "Function";
                    break;
            }

            writer.Write("sub" + strFuncName + Environment.NewLine);

            state = new StateScriptEngine();
            state.scriptCodePtr = scriptCodePtr;
            state.jumpTablePtr = jumpTablePtr;
            state.scriptSub = scriptSub;
            state.deep = 1;
            state.isSwitchEnd = false;
            state.error = false;

            DecompileSub(writer);
            writer.Write(Environment.NewLine);
        }

        public void DecompileSub(StreamWriter writer)
        {
            int objectLoop = 0;
            int index1 = 0;
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
                                    index1 = objectLoop;
                                    variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    break;
                                case 1: // ARRAY
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                        index1 = scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                    else
                                        index1 = scriptData[state.scriptCodePtr++];
                                    num2 += 2;
                                    variableName[i] = _SetArrayValue(VARIABLE_NAME[scriptData[state.scriptCodePtr++]], index1);
                                    break;
                                case 2:
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                        index1 = objectLoop + scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                    else
                                        index1 = objectLoop + scriptData[state.scriptCodePtr++];
                                    num2 += 2;
                                    variableName[i] = VARIABLE_NAME[scriptData[state.scriptCodePtr++]];
                                    break;
                                case 3:
                                    if (scriptData[state.scriptCodePtr++] == 1)
                                        index1 = objectLoop - scriptEng.arrayPosition[scriptData[state.scriptCodePtr++]];
                                    else
                                        index1 = objectLoop - scriptData[state.scriptCodePtr++];
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
                                if (j < strLen) tmp = tmp + Convert.ToChar((scriptData[state.scriptCodePtr] >> 24));
                                j++;
                                if (j < strLen) tmp = tmp + Convert.ToChar(((scriptData[state.scriptCodePtr] & 0x00FFFFFF) >> 16));
                                j++;
                                if (j < strLen) tmp = tmp + Convert.ToChar(((scriptData[state.scriptCodePtr] & 0x0000FFFF) >> 8));
                                j++;
                                if (j < strLen) tmp = tmp + Convert.ToChar((scriptData[state.scriptCodePtr] & 0x000000FF));
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

                // Check what opcodes terminates a statement
                switch (opcode)
                {
                    case 0x00: // end sub
                        break;
                    case 0x19: // else
                        for (int i = 0; i < state.deep-1; i++) writer.Write("\t");
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

                // Use specific operands in some situation
                switch (opcode)
                {
                    case 0x55: // PlaySfx
                    case 0x56: // StopSfx
                        break;
                }


                if (opcode >= 134)
                {
                    writer.Write("ERROR AT: " + state.scriptCodePtr + " : " + opcode);
                    Console.WriteLine("OPCODE ABOVE THE MAX OPCODES");
                    state.error = true;
                    return;
                }

                string operand = opcodeList[opcode];


                for (int i = 0; i < variableName.Length; i++)
                {
                    if (variableName[i] == "" || variableName[i] == null)
                    {
                        variableName[i] = "Object.Value0";
                    }
                }

                switch (opcode)
                {
                    case 0x00:
                        writer.Write("endsub");
                        state.EndFlag = true;
                        state.deep = 0;
                        break;
                    case 0x01: writer.Write(variableName[0] + "=" + variableName[1]); break;
                    case 0x02: writer.Write(variableName[0] + "+=" + variableName[1]); break;
                    case 0x03: writer.Write(variableName[0] + "-=" + variableName[1]); break;
                    case 0x04: writer.Write(variableName[0] + "++"); break;
                    case 0x05: writer.Write(variableName[0] + "--"); break;
                    case 0x06: writer.Write(variableName[0] + "*=" + variableName[1]); break;
                    case 0x07: writer.Write(variableName[0] + "/=" + variableName[1]); break;
                    case 0x08: writer.Write(variableName[0] + ">>=" + variableName[1]); break;
                    case 0x09: writer.Write(variableName[0] + "<<=" + variableName[1]); break;
                    case 0x0A: writer.Write(variableName[0] + "&=" + variableName[1]); break;
                    case 0x0B: writer.Write(variableName[0] + "|=" + variableName[1]); break;
                    case 0x0C: writer.Write(variableName[0] + "^=" + variableName[1]); break;
                    case 0x0D: writer.Write(variableName[0] + "%=" + variableName[1]); break;
                    case 0x0E: writer.Write(variableName[0] + "-=" + variableName[0]); break;
                    case 0x0F:
                        writer.Write("if " + variableName[0] + "!=" + variableName[1]);
                        state.deep += 1;
                        break;
                    case 0x10:
                        writer.Write("if " + variableName[0] + "<=" + variableName[1]);
                        state.deep += 1;
                        break;
                    case 0x11:
                        writer.Write("if " + variableName[0] + ">=" + variableName[1]);
                        state.deep += 1;
                        break;
                    case 0x12:
                        writer.Write("if " + variableName[0] + "==" + variableName[1]);
                        state.deep += 1;
                        break;
                    case 0x13:
                        writer.Write("if " + variableName[1] + "==" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x14:
                        writer.Write("if " + variableName[1] + ">" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x15:
                        writer.Write("if " + variableName[1] + ">=" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x16:
                        writer.Write("if " + variableName[1] + "<" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x17:
                        writer.Write("if " + variableName[1] + "<=" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x18:
                        writer.Write("if " + variableName[1] + "!=" + variableName[2]);
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
                        writer.Write("while " + variableName[1] + "==" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x1C:
                        writer.Write("while " + variableName[1] + ">" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x1D:
                        writer.Write("while " + variableName[1] + ">=" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x1E:
                        writer.Write("while " + variableName[1] + "<" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x1F:
                        writer.Write("while " + variableName[1] + "<=" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x20:
                        writer.Write("while " + variableName[1] + "!=" + variableName[2]);
                        state.deep += 1;
                        //DecompileSub(writer);
                        break;
                    case 0x21:
                        writer.Write("loop");
                        break;
                    case 0x22:
                        writer.Write("switch " + variableName[1] + Environment.NewLine);
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
                            DecompileSub(writer);
                            /*if (state.SwitchDeep >= 1 && state.isSwitchEnd)
                            {
                                state.isSwitchEnd = false;
                                //goto LABEL_1;
                                return;
                            }*/
                        }
                        state.isSwitchEnd = false;
                        break;
                    case 0x23:
                        writer.Write("break");
                        if (scriptData[state.scriptCodePtr] == 0x24)
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
                    case 0x24:
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
                                writer.Write(variableName[0]);
                                break;
                            case 2:
                                writer.Write(variableName[0] + "," + variableName[1]);
                                break;
                            case 3:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2]);
                                break;
                            case 4:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2] + "," + variableName[3]);
                                break;
                            case 5:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2] + "," + variableName[3] + "," + variableName[4]);
                                break;
                            case 6:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2] + "," + variableName[3] + "," + variableName[4] + "," + variableName[5]);
                                break;
                            case 7:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2] + "," + variableName[3] + "," + variableName[4] + "," + variableName[5] + "," + variableName[6]);
                                break;
                            case 8:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2] + "," + variableName[3] + "," + variableName[4] + "," + variableName[5] + "," + variableName[6] + "," + variableName[7]);
                                break;
                            case 9:
                                writer.Write(variableName[0] + "," + variableName[1] + "," + variableName[2] + "," + variableName[3] + "," + variableName[4] + "," + variableName[5] + "," + variableName[6] + "," + variableName[7] + "," + variableName[8]);
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

        public void Write(Writer writer)
        {

        }

    }
}
