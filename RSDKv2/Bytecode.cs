using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            public bool isSwitchEnd;
            public bool error;

            public StateScriptEngine IncDeep()
            {
                deep++;
                return this;
            }
        };

        struct ScriptEngine
        {
            public int[] operands;
            public int[] tempValue;
            public int[] arrayPosition;
            public int checkResult;
            public int sRegister;
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

        #region Misc
        public string[] functionNames = new string[0x200];
        public string[] typeNames = new string[0x100];
        public string[] sourceNames = new string[0x100];
        public string[] globalVariableNames = new string[0x100];
        public int[] globalVariables = new int[0x100];

        public int numGlobalSfx;
        public string[] sfxNames = new string[0x100];

        int m_stageVarsIndex;
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

        #endregion

        #region Raw Bytecode Data
        int scriptDataPos;
        int jumpTableDataPos;
        int[] scriptData = new int[0x40000];
        int[] jumpTableData = new int[0x4000];
        int[] jumpTableStack = new int[0x400];
        int[] functionStack = new int[0x400];

        ScriptEngine scriptEng;
        ObjectScript[] objectScriptList =new ObjectScript[0x100];
        FunctionScript[] functionScriptList = new FunctionScript[0x200];
        #endregion

        public Bytecode(Reader reader, int scriptNum = 0)
        {
            //ClearScriptData();
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];
            scriptEng.arrayPosition = new int[3];
            int cnt = reader.ReadInt32();
            while (cnt > 0)
            {
                byte data = reader.ReadByte();
                int blocksCount = data & 0x7F;

                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = reader.ReadByte(); }
                    cnt -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                    { scriptData[scriptDataPos++] = reader.ReadInt32(); }
                    cnt -= blocksCount;
                }
            }

            for (int opcount = reader.ReadInt32(); opcount > 0;)
            {
                byte data = reader.ReadByte();
                int blocksCount = data & 0x7F;
                if ((data & 0x80) == 0)
                {
                    for (int i = 0; i < blocksCount; i++)
                        jumpTableData[jumpTableDataPos++] = reader.ReadByte();
                    opcount -= blocksCount;
                }
                else
                {
                    for (int i = 0; i < blocksCount; i++)
                        jumpTableData[jumpTableDataPos++] = reader.ReadInt32();
                    opcount -= blocksCount;
                }
            }

            m_stageVarsIndex = scriptNum;

            int count = reader.ReadInt16();
            for (int i = 0; i < count; i++)
            {
                objectScriptList[scriptNum + i].mainScript = reader.ReadInt32();
                objectScriptList[scriptNum + i].playerScript = reader.ReadInt32();
                objectScriptList[scriptNum + i].drawScript = reader.ReadInt32();
                objectScriptList[scriptNum + i].startupScript = reader.ReadInt32();
            }
            for (int i = 0; i < count; i++)
            {
                objectScriptList[scriptNum + i].mainJumpTable = reader.ReadInt32();
                objectScriptList[scriptNum + i].playerJumpTable = reader.ReadInt32();
                objectScriptList[scriptNum + i].drawJumpTable = reader.ReadInt32();
                objectScriptList[scriptNum + i].startupJumpTable = reader.ReadInt32();
            }

            count = reader.ReadInt16();
            for (int i = 0; i < count; i++)
                functionScriptList[i].mainScript = reader.ReadInt32();
            for (int i = 0; i < count; i++)
                functionScriptList[i].mainJumpTable = reader.ReadInt32();
            Console.WriteLine(reader.BaseStream.Position + " " + reader.BaseStream.Length);

        }

        public void LoadStageFile(string stageconfig, string script)
        {
            StageConfig sc = new StageConfig(stageconfig);

            int scriptNum = sc.ObjectsNames.Count;

            for (int i = 1; i < sc.SourceTxtLocations.Count; i++)
            {
                SetObjectTypeName(sc.SourceTxtLocations[i], i + scriptNum);
                SetObjectTypeName(sc.ObjectsNames[i], i + scriptNum);
            }

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

            SetObjectTypeName("BlankObject", 0);
        }

        public void SetObjectTypeName(string typeName, int scriptNum)
        {
            int count = 0;
            int length = typeName.Length;
            for (int i = 0; i < length; i++)
            {
                if (typeName[i] != '\0')
                {
                    if (typeName[i] != ' ')
                    { typeNames[scriptNum] = typeNames[scriptNum] + typeName[i]; }
                }
                else
                    break;
            }
            if (count >= typeNames.Length)
		return;
            typeNames[scriptNum] = "";
        }

        public void SetObjectSourceName(string typeName, int scriptNum)
        {
            int count = 0;
            int length = typeName.Length;
            for (int i = 0; i < length; i++)
            {
                if (typeName[i] != '\0')
                {
                    if (typeName[i] != ' ')
                    { sourceNames[scriptNum] = sourceNames[scriptNum] + typeName[i]; }
                }
                else
                    break;
            }
            if (count >= sourceNames.Length)
                return;
            sourceNames[scriptNum] = "";
        }

        /*void _SetArrayValue(byte strOut, string strIn, int index)
        {
            int point = -1;
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
                //memcpy(strOut, strIn, point);
                //sConsole.WriteLine(strOut + point, "[%i]%s", index, strIn + point);
            }
            else
            {
                //sConsole.WriteLine(strOut, "%s[%i]", strIn, index);
            }
        }*/

        void _SetArrayValue(ref string strOut, string strIn, int index)
        {
            int point = -1;
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
                //memcpy(strOut, strIn, point);
                strOut = strIn;
                //Console.WriteLine(strOut);
            }
            else
            {
                //Console.WriteLine(strOut);
            }
        }

        public void DecompileStatement(StreamWriter writer, StateScriptEngine state)
        {
            int objectLoop = 0;
            int index1 = 0;
            bool flag = false;
            writer.Write(Environment.NewLine);
            
            while(!flag)
            {
                int num2 = 0;
                int opcode = scriptData[state.scriptCodePtr++];
                int paramsCount = scriptOpcodeSizes[opcode];

                string[] variableName = new string[10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNASSIGNED VARIABLE";
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
                                    _SetArrayValue(ref variableName[i], VARIABLE_NAME[scriptData[state.scriptCodePtr++]], index1);
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
                            for (int s = 0; s < scriptEng.operands.Length; s++)
                            {
                                variableName[i] = variableName[i] + Convert.ToChar(scriptEng.operands[i]);
                            }
                            num2 += 2;
                            break;
                        case 3: // Read string
                            string tmp = "";
                            num2++;
                            int strLen = scriptData[state.scriptCodePtr];
                            for (int j = 0; j < strLen; j += 4)
                            {
                                state.scriptCodePtr++;
                                num2++;
                                tmp = tmp + Convert.ToChar((scriptData[state.scriptCodePtr] >> 24));
                                tmp = tmp + Convert.ToChar(((scriptData[state.scriptCodePtr] & 0x00FFFFFF) >> 16));
                                tmp = tmp + Convert.ToChar(((scriptData[state.scriptCodePtr] & 0x0000FFFF) >> 8));
                                tmp = tmp + Convert.ToChar((scriptData[state.scriptCodePtr] & 0x000000FF));
                            }
                            tmp = tmp + '\0';
                            variableName[i] = "\\" + tmp + "\\";
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
                case 0x00: // endsub
                case 0x1A: // endif
                case 0x21: // loop
                    state.deep--;
                    for (int i = 0; i < state.deep; i++) writer.Write("\t");
                    flag = true;
                    break;
                case 0x23: // break
                    flag = true;
                    for (int i = 0; i < state.deep; i++) writer.Write("\t");
                    state.deep--;
                    // do a peek if the next statement is an endswitch
                    if (scriptData[state.scriptCodePtr] == 0x24)
                    {
                        state.isSwitchEnd = true;
                    }
                    break;
                case 0x19: // else
                    for (int i = 0; i < state.deep - 1; i++) writer.Write( "\t");
                    break;
                default:
                    for (int i = 0; i < state.deep; i++) writer.Write( "\t");
                    break;
            }

            // Use specific operands in some situation
            switch (opcode)
            {
                case 0x55: // PlaySfx
                case 0x56: // StopSfx
                           //sprintf_s(variableName[0], "\"%s\"", sfxNames[scriptEng.operands[0]]);
                    break;
            }


            if (opcode >= 134)
            {
                writer.Write( "ERROR AT: " + state.scriptCodePtr + " : " + opcode);
                state.error = true;
                return;
            }
            string operand = opcodeList[opcode];

            switch (opcode)
            {
                case 0x00: writer.Write("end sub"); flag = true; break;
                case 0x01: writer.Write(variableName[0] + "=" + variableName[1]); break;
                case 0x02: writer.Write(variableName[0] + "+=" + variableName[1]); break;
                case 0x03: writer.Write(variableName[0] + "-=" + variableName[1]); break;
                case 0x04: writer.Write(variableName[0] + "++"); break;
                case 0x05: writer.Write(variableName[0] + "--" ); break;
                case 0x06: writer.Write(variableName[0] + "*=" + variableName[1]); break;
                case 0x07: writer.Write(variableName[0] + "/=" + variableName[1]); break;
                case 0x08: writer.Write(variableName[0] + ">>=" + variableName[1]); break;
                case 0x09: writer.Write(variableName[0] + "<<=" + variableName[1]); break;
                case 0x0A: writer.Write(variableName[0] + "&=" + variableName[1]); break;
                case 0x0B: writer.Write(variableName[0] + "|=" + variableName[1]); break;
                case 0x0C: writer.Write(variableName[0] + "^=" + variableName[1]); break;
                case 0x0D: writer.Write(variableName[0] + "%%=" + variableName[1]); break;
                case 0x0E: writer.Write(variableName[0] + "=-" + variableName[0]); break;
                case 0x0F: writer.Write(variableName[0] + "!=" + variableName[1]); break;
                case 0x10: writer.Write(variableName[0] + "<=" + variableName[1]); break;
                case 0x11: writer.Write(variableName[0] + ">=" + variableName[1]); break;
                case 0x12: writer.Write(variableName[0] + "==" + variableName[1]); break;
                case 0x13:
                    writer.Write("if " + variableName[1] + "==" +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x14:
                    writer.Write("if " + variableName[1] + ">"  +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x15:
                    writer.Write("if "+  variableName[1] + ">=" +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x16:
                    writer.Write("if "+  variableName[1] + "<" +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x17:
                    writer.Write("if " + variableName[1] + "<=" +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x18:
                    writer.Write("if " + variableName[1] + "!=" + variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x19: writer.Write("else"); break;
                case 0x1A: writer.Write("endif"); break;
                case 0x1B:
                    writer.Write( "while " + variableName[1] + "=="  +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x1C:
                    writer.Write("while " + variableName[1] + ">"  + variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x1D:
                    writer.Write("while " + variableName[1] + ">="  +  variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x1E:
                    writer.Write("while " +  variableName[1] + "<" + variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x1F:
                    writer.Write("while " + variableName[1] + "<="  + variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x20:
                    writer.Write("while " + variableName[1] + "!=" + variableName[2]);
                    DecompileStatement(writer,  state.IncDeep());
                    break;
                case 0x21: writer.Write("loop"); break;
                case 0x22:
                    writer.Write("switch " + variableName[1] + Environment.NewLine);
                    for (int i = 0; !state.isSwitchEnd; i++)
                    {
                        for (int j = 0; j < state.deep; j++) writer.Write("\t");
                        writer.Write("case " + i);
                        DecompileStatement(writer,  state.IncDeep());
                    }
                    state.isSwitchEnd = false;
                    break;
                case 0x23: writer.Write("break"); break;
                case 0x24: writer.Write("endswitch"); break;
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
                if (!state.isSwitchEnd)
                    writer.Write(Environment.NewLine);

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
                    strFuncName = "ObjectPlayer";
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

            writer.Write("sub " + strFuncName + Environment.NewLine);

            StateScriptEngine state;
            state.scriptCodePtr = scriptCodePtr;
            state.jumpTablePtr = jumpTablePtr;
            state.scriptSub = scriptSub;
            state.deep = 1;
            state.isSwitchEnd = false;
            state.error = false;

            DecompileStatement(writer, state);
            writer.Write(Environment.NewLine);
        }

        public void Decompile()
        {
            for (int i = 1; i < sourceNames.Length; i++)
            {
                if (!Directory.Exists("Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]),"")))
                {
                    DirectoryInfo d = new DirectoryInfo("Scripts/" + sourceNames[i].Replace(Path.GetFileName(sourceNames[i]), ""));
                    d.Create();
                }
                Console.WriteLine("Scripts/" + sourceNames[i]);

                StreamWriter writer = new StreamWriter("Scripts/" + sourceNames[i]);

                if (i < m_stageVarsIndex)
                {
                    for (int j = 0; j < m_stageVarsIndex; j++)
                        writer.Write("#define " + typeNames[j] + " " + j + Environment.NewLine);
                }
                else
                {
                    for (int j = 0; i > typeNames.Length; j++)
                        writer.Write("#define " + typeNames[j] + " " + j + Environment.NewLine);
                }

                ObjectScript objectScript = objectScriptList[i];

                writer.Write(Environment.NewLine);
                writer.Write(Environment.NewLine);

                Console.WriteLine("Main script, ");
                DecompileScript(writer, objectScript.mainScript, objectScript.mainJumpTable, 0);
                Console.WriteLine("Player script, ");
                DecompileScript(writer, objectScript.playerScript, objectScript.playerJumpTable, 1);
                Console.WriteLine("Draw script, ");
                DecompileScript(writer, objectScript.drawScript, objectScript.drawJumpTable, 2);
                Console.WriteLine("Startup script.\n");
                DecompileScript(writer, objectScript.startupScript, objectScript.startupJumpTable, 3);

                writer.Write(Environment.NewLine);
                writer.Close();
            }
        }

        public void Write(Writer writer)
        {

        }

    }
}
