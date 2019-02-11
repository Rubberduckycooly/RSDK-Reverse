using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvRS
{
    public class ScriptOLD
    {

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
                this.deep += 1;
                return this;
            }
        };

        struct ScriptEngine
        {
            public int[] operands;
            public int[] tempValue;
            public int[] arrayPosition;
        };

        struct SpriteFrame
        {
            public int PivotX;
            public int PivotY;
            public int Width;
            public int Height;
            public int Xpos;
            public int Ypos;
        }

        string[] VARIABLE_NAME = new string[]
{
"Object.Type",
"Object.SubType",
"Object.Priority",
"Object.Xpos",
"Object.Ypos",
"Object.ValueA",
"Object.ValueB",
"Object.ValueC",
"Object.ValueD",
"Object.ValueE",
"Object.ValueF",
"ObjectPos",
"TempObject.Type",
"TempObject.SubType",
"TempObject.Priority",
"TempObject.Xpos",
"TempObject.Ypos",
"TempObject.ValueA",
"TempObject.ValueB",
"TempObject.ValueC",
"TempObject.ValueD",
"TempObject.ValueE",
"TempObject.ValueF",
"TempObjectPos",
"Player.Xpos",
"Player.Ypos",
"Player.XSpeed",
"Player.YSpeed",
"Player.Movement",
"Player.MovementMomentumn",
"XBoundary1",
"XBoundary2",
"YBoundary1",
"YBoundary2",
"MainGameMode",
"TempValA",
"TempValB",
"AngleTimer",
"CheckResult",
"Score",
"Rings",
"DeformationPosF1",
"DeformationPosF2",
"DeformationPosB1",
"DeformationPosB2",
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
    "Blank?",
    "Blank?",
    "Rand",
    "Unknown",
    "Unknown",
    "CheckEqual",
    //"CheckEqual",
    "CreateObject",					// 0x10						// 0x25
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
1,
2,
2,
2,
1,
1,
2,
2,
2,
2,
0,
1,
2,
0,  
0,
3,
3,
0,
1,
4,
4,
4,
4,
4,
4,
2,
2,
2,
2,
2,
2,
4,
4,
4,
4,
1,
1,
3,
3,
2,
1,
6,
1,
4,
1,
1,
4,
5,
2,
1,
0,
0,
1,
7,
8,
4,
4,
3,
3,
3,
4,
4,
1,
1,
6,
2,
1,
0,
0,
0,
0,
0,
0,
0,
0,
};

        public byte SubCount
        {
            get
            {
                return 5;
            }
        }

        int scriptDataPos;
        int jumpTableDataPos;

        int[] scriptData_Main = new int[0x40000];
        int Endpoint_Main = 0;
        int[] jumpTableData_Main = new int[0x40000];
        int[] unknownArray1 = new int[0x40000];

        int[] scriptData_Draw = new int[0x40000];
        int Endpoint_Draw = 0;
        int[] jumpTableData_Draw = new int[0x40000];
        int[] unknownArray2 = new int[0x40000];

        int[] scriptData_Startup = new int[0x40000];
        int Endpoint_Startup = 0;
        int[] jumpTableData_Startup = new int[0x40000];
        int[] unknownArray3 = new int[0x40000];

        int[] scriptData_RSDK = new int[0x40000];
        int Endpoint_RSDK = 0;
        int[] jumpTableData_RSDK = new int[0x40000];

        SpriteFrame[] spriteFrames = new SpriteFrame[0x100];

        int scriptCodePtr = 0;
        int spriteframePtr = 0;
        int jumpTablePtr = 0;
        int scriptEndPtr = 0;
        int opcode = 0;

        ScriptEngine scriptEng;
        StateScriptEngine state = new StateScriptEngine();

        public ScriptOLD()
        { }

        public ScriptOLD(string filename): this(new Reader(filename))
        { }

        public ScriptOLD(Reader reader)
        {
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];
            scriptEng.arrayPosition = new int[3];

            for (int i = 0; i < SubCount; i++)
            {
                uint Opcode = 0;
                int Opcode2 = 0;
                uint OpcodeSize = 0;
                uint OpcodeSizea = 0;
                uint OpcodeSizeb = 0;

                scriptDataPos = 0;

                int Value0 = 0;
                int Value1 = 0;
                int Value2 = 0;
                int Value3 = 0;
                int Value4 = 0;
                int Value5 = 0;
                int Value6 = 0;
                int Value7 = 0;
                int Value8 = 0;
                int Value9 = 0;

                int FileBuffer = 0;
                int k = 0;

                Value1 = reader.ReadByte() << 8;
                Value1 |= reader.ReadByte();

                if (i != 0)
                {
                    if (i == 1)
                    {
                        Endpoint_Main = Value1;
                        scriptData_Main = new int[0xFFFu];
                    }
                    else if (i == 2)
                    {
                        Endpoint_Draw = Value1;
                        scriptData_Main = new int[0xFFFu];
                    }
                    else if (i == 3)
                    {
                        Endpoint_RSDK = Value1;
                        scriptData_RSDK = new int[0xFFFu];
                    }
                }
                else
                {
                    Endpoint_Startup = Value1;
                    scriptData_Main = new int[0xFFFu];
                }

                while (k == 0)
                {
                    FileBuffer = reader.ReadByte();
                    if (FileBuffer == 255)
                    {
                        FileBuffer = reader.ReadByte();
                        if (FileBuffer == 255)
                        {
                            FileBuffer = reader.ReadByte();
                            if (FileBuffer == 255)
                            {
                                FileBuffer = reader.ReadByte();
                                if (FileBuffer == 255)
                                {
                                    k = 1;
                                }
                            }
                        }
                    }

                    if (k < 1)
                    {
                        Opcode2 = FileBuffer;

                        SetScriptData(i, FileBuffer);

                        switch (Opcode)
                        {
                            case 0u:
                                break;
                            case 1u:
                                break;
                            case 2u:
                                break;
                            case 3u:
                                break;
                            case 4u:
                                break;
                            case 5u:
                                break;
                            case 6u:
                                break;
                            case 7u:
                                break;
                            case 8u:
                                break;
                            case 9u:
                                break;
                            case 0xAu:
                                break;
                            case 0xBu:
                                break;
                            case 0xDu:
                                break;
                            case 0xCu:
                                break;
                            case 0xEu:
                                OpcodeSize = 6;
                                break;
                            case 0xFu:
                                break;
                            case 0x10u:
                                break;
                            case 0x11u:
                                OpcodeSize = 6;
                                break;
                            case 0x12u:
                                OpcodeSize = 1;
                                break;
                            case 0x13u:
                                break;
                            case 0x14u:
                                break;
                            case 0x15u:
                                break;
                            case 0x16u:
                                break;
                            case 0x17u:
                                break;
                            case 0x18u:
                                break;
                            case 0x19u:
                                OpcodeSize = 2;
                                break;
                            case 0x1Au:
                                OpcodeSize = 2;
                                break;
                            case 0x1Bu:
                                OpcodeSize = 2;
                                break;
                            case 0x1Cu:
                                OpcodeSize = 2;
                                break;
                            case 0x1Du:
                                OpcodeSize = 2;
                                break;
                            case 0x1Eu:
                                OpcodeSize = 2;
                                break;
                            case 0x1Fu:
                                OpcodeSize = 4;
                                break;
                            case 0x20u:
                                OpcodeSize = 4;
                                break;
                            case 0x21u:
                                OpcodeSize = 4;
                                break;
                            case 0x22u:
                                OpcodeSize = 4;
                                break;
                            case 0x23u:
                                break;
                            case 0x24u:
                                break;
                            case 0x25u:
                                OpcodeSize = 3;
                                break;
                            case 0x26u:
                                OpcodeSize = 3;
                                break;
                            case 0x27u:
                                OpcodeSize = 2;
                                break;
                            case 0x28u:
                                break;
                            case 0x29u:
                                break;
                            case 0x2Au:
                                break;
                            case 0x2Bu:
                                OpcodeSize = 4;
                                break;
                            case 0x2Cu:
                                break;
                            case 0x2Du:
                                break;
                            case 0x2Eu:
                                break;
                            case 0x2Fu:
                                OpcodeSize = 5;
                                break;
                            case 0x31u:
                                break;
                            case 0x34u:
                                break;
                            case 0x30u:
                                break;
                            case 0x35u:
                                OpcodeSize = 7;
                                break;
                            case 0x36u:
                                OpcodeSize = 8;
                                break;
                            case 0x37u:
                                break;
                            case 0x38u:
                                break;
                            case 0x39u:
                                break;
                            case 0x3Au:
                                break;
                            case 0x3Bu:
                                OpcodeSize = 3;
                                break;
                            case 0x3Cu:
                                break;
                            case 0x3Du:
                                OpcodeSize = 4;
                                break;
                            case 0x3Eu:
                                break;
                            case 0x3Fu:
                                break;
                            case 0x40u:
                                OpcodeSize = 6;
                                break;
                            case 0x41u:
                                OpcodeSize = 2;
                                break;
                            case 0x42u:
                                OpcodeSize = 1;
                                break;
                            default:
                                break;
                        }

                        for (int j = 0; j < OpcodeSize; j++)
                        {
                            FileBuffer = reader.ReadByte();

                            SetScriptData(i, FileBuffer);

                            int temp = FileBuffer;

                            if (temp != 0)
                            {
                                FileBuffer = reader.ReadByte();
                                SetScriptData(i, FileBuffer);

                                FileBuffer = reader.ReadByte();
                                Value1 = FileBuffer;

                                if (Value1 > 128) { Value1 -= 128; }

                                SetScriptData(i, Value1);
                            }
                            else
                            {
                                FileBuffer = reader.ReadByte();
                                if (FileBuffer < 128)
                                {
                                    Value2 = FileBuffer << 8;
                                    FileBuffer = reader.ReadByte();
                                    Value2 += FileBuffer;
                                }
                                else
                                {
                                    FileBuffer -= 128;
                                    Value2 = FileBuffer << 8;
                                    FileBuffer = reader.ReadByte();
                                    Value2 += FileBuffer;
                                    Value2 = -Value2;
                                }
                                switch (i)
                                {
                                    case 0:
                                        scriptData_Startup[scriptDataPos++] = Value2;
                                        break;
                                    case 1:
                                        scriptData_Main[scriptDataPos++] = Value2;
                                        break;
                                    case 2:
                                        scriptData_Draw[scriptDataPos++] = Value2;
                                        break;
                                    case 3:
                                        if (Opcode2 == 17)
                                        {
                                            switch(j)
                                            {
                                                case 0:
                                                    spriteFrames[spriteframePtr].Width = Value2;
                                                    break;
                                                case 1:
                                                    spriteFrames[spriteframePtr].Height = Value2;
                                                    break;
                                                case 2:
                                                    spriteFrames[spriteframePtr].PivotX = Value2;
                                                    break;
                                                case 3:
                                                    spriteFrames[spriteframePtr].PivotY = Value2;
                                                    break;
                                                case 4:
                                                    spriteFrames[spriteframePtr].Xpos = Value2;
                                                    break;
                                                case 5:
                                                    spriteFrames[spriteframePtr++].Ypos = Value2;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        break;
                                }
                            }

                        }
                    }
                }

                FileBuffer = reader.ReadByte();
                FileBuffer = reader.ReadByte();

                OpcodeSizea = (uint)FileBuffer;
                for (int j = 0; j < OpcodeSizea; j++)
                {
                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();
                }

                FileBuffer = reader.ReadByte();
                FileBuffer = reader.ReadByte();

                OpcodeSizeb = (uint)FileBuffer;

                for (int j = 0; j < OpcodeSizeb; j++)
                {
                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    Value4 = reader.ReadByte() << 8;
                    Value4 |= reader.ReadByte();

                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();

                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            unknownArray1[scriptDataPos++] = Value1;
                        }
                        else if (i == 2)
                        {
                            unknownArray2[scriptDataPos++] = Value1;
                        }
                        else if (i == 3)
                        {
                            
                        }
                    }
                    else
                    {
                        unknownArray3[scriptDataPos++] = Value1;
                    }
                }
                k = 0;

                FileBuffer = reader.ReadByte();
                FileBuffer = reader.ReadByte();
                OpcodeSize = (uint)FileBuffer;

                for (int j = 0; j < 512; ++j) //Clear out space
                {
                    SetJumptableData(i, 0);
                }

                Value8 = 0;

                for (int j = 0; j < OpcodeSize; ++j)
                {
                    Value0 = 0x8000;
                    Value9 = 0;

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    Value4 = reader.ReadByte() << 8;
                    Value4 |= reader.ReadByte();

                    Value7 = Value8;
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            jumpTableData_Main[jumpTableDataPos] = 0;
                            jumpTableData_Main[jumpTableDataPos++] = 0;
                        }
                        else if (i == 2)
                        {
                            jumpTableData_Draw[jumpTableDataPos] = 0;
                            jumpTableData_Draw[jumpTableDataPos++] = 0;
                        }
                        else if (i == 3)
                        {
                            jumpTableData_RSDK[jumpTableDataPos] = 0;
                            jumpTableData_RSDK[jumpTableDataPos++] = 0;
                        }
                    }
                    else
                    {
                        jumpTableData_Startup[jumpTableDataPos] = 0;
                        jumpTableData_Startup[jumpTableDataPos++] = 0;
                    }

                    /*FileBuffer = reader.ReadByte();
                    Value1 = FileBuffer << 8;
                    FileBuffer = reader.ReadByte();
                    Value1 += FileBuffer;*/

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    SetJumptableData(i, Value1);

                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();
                    /*FileBuffer = reader.ReadByte();
                    Value1 = FileBuffer << 8;
                    FileBuffer = reader.ReadByte();
                    Value1 += FileBuffer;*/

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            if (jumpTableData_Main[jumpTableDataPos] == 0)
                                jumpTableData_Main[jumpTableDataPos++] = Value1;
                        }
                        else if (i == 2 && jumpTableData_Draw[jumpTableDataPos] == 0)
                        {
                            jumpTableData_Draw[jumpTableDataPos++] = Value1;
                        }
                        else if (i == 3 && jumpTableData_RSDK[jumpTableDataPos] == 0)
                        {
                            jumpTableData_RSDK[jumpTableDataPos++] = Value1;
                        }
                    }
                    else if (jumpTableData_Startup[jumpTableDataPos] == 0)
                    {
                        jumpTableData_Startup[jumpTableDataPos++] = Value1;
                    }

                    SetJumptableData(i, Value1);

                    for (int v = 0; v < Value4; v++)
                    {
                        /*FileBuffer = reader.ReadByte();
                        Value1 = FileBuffer << 8;
                        FileBuffer = reader.ReadByte();
                        Value1 += FileBuffer;*/

                        Value1 = reader.ReadByte() << 8;
                        Value1 |= reader.ReadByte();

                        FileBuffer = reader.ReadByte();
                        Value5 = FileBuffer << 8;
                        FileBuffer = reader.ReadByte();
                        Value6 += FileBuffer + Value5;

                        if (Value6 < Value0)
                        {
                            Value0 = Value6;
                        }
                        if (Value6 > Value9)
                        {
                            Value9 = Value6;
                        }

                        FileBuffer = reader.ReadByte();
                        FileBuffer = reader.ReadByte();

                        SetJumptableData(i, Value1);
                    }
                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            jumpTableData_Main[jumpTableDataPos] = Value0;
                            //dword_AFEA98[jumpTableDataPos] = Value9;
                        }
                        else if (i == 2)
                        {
                            jumpTableData_Draw[jumpTableDataPos] = Value0;
                            //dword_AFF298[jumpTableDataPos] = Value9;
                        }
                        else if (i == 3)
                        {
                            jumpTableData_RSDK[jumpTableDataPos] = Value0;
                            //dword_AFF298[jumpTableDataPos] = Value9;
                        }
                    }
                    else
                    {
                        jumpTableData_Startup[jumpTableDataPos] = Value0;
                        //dword_AFE298[jumpTableDataPos] = Value9;
                    }
                    jumpTableDataPos++;

                    for (int m = Value0 - 1; m < Value9; ++m)
                    {
                        if (i != 0)
                        {
                            if (i == 1)
                            {
                                jumpTableData_Main[jumpTableDataPos] = jumpTableData_Main[jumpTableDataPos + Value0];
                                if (jumpTableData_Main[jumpTableDataPos] != 0)
                                {
                                    //jumpTableData_Main[jumpTableDataPos] = dword_AFEA9C[16129 * ScriptID + Value7];
                                }
                            }
                            else if (i == 2)
                            {
                                jumpTableData_Draw[jumpTableDataPos] = jumpTableData_Draw[jumpTableDataPos + Value0];
                                if (jumpTableData_Draw[jumpTableDataPos] != 0)
                                {
                                    //jumpTableData_Draw[jumpTableDataPos] = dword_AFF29C[16129 * ScriptID + Value7];
                                }
                            }
                            else if (i == 4)
                            {
                                jumpTableData_RSDK[jumpTableDataPos] = jumpTableData_RSDK[jumpTableDataPos + Value0];
                                if (jumpTableData_RSDK[jumpTableDataPos] != 0)
                                {
                                    //jumpTableData_Draw[jumpTableDataPos] = dword_AFF29C[16129 * ScriptID + Value7];
                                }
                            }
                        }
                        else
                        {
                            //jumpTableData_Startup[jumpTableDataPos] = jumpTableData_Startup[jumpTableDataPos + Value0];
                            if (jumpTableData_Startup[jumpTableDataPos] != 0)
                            {
                                //jumpTableData_Startup[jumpTableDataPos] = dword_AFE29C[16129 * ScriptID + Value7];
                            }
                        }
                        jumpTableDataPos++;
                    }

                    /*if (i != 0)
                    {
                        if (i == 1)
                        {
                            if (dword_AF4A98[jumpTableDataPos] <= 0)
                            {
                                dword_AF4AA4[jumpTableDataPos] = Value7;
                            }
                            else
                                dword_AF4AA8[jumpTableDataPos] = Value7;
                        }
                        else if (i == 2)
                        {
                            if (dword_AF8A98[jumpTableDataPos] <= 0)
                            {
                                dword_AF8AA4[jumpTableDataPos] = Value7;
                            }
                            else
                            {
                                dword_AF8AA8[jumpTableDataPos] = Value7;
                            }
                        }
                        else if (i == 3)
                        {
                            if (dword_AF8A98[jumpTableDataPos] <= 0)
                            {
                                dword_AF8AA4[jumpTableDataPos] = Value7;
                            }
                            else
                            {
                                dword_AF8AA8[jumpTableDataPos] = Value7;
                            }
                        }
                    }
                    else if (dword_AF0A98[jumpTableDataPos] <= 0)
                    {
                        dword_AF0AA4[jumpTableDataPos] = Value7;
                    }
                    else
                    {
                        dword_AF0AA8[jumpTableDataPos] = Value7;
                    }*/

                }
            }
            Console.WriteLine("File Length: " + reader.BaseStream.Length + " Pos: " + reader.BaseStream.Position + " Data Left: " + (reader.BaseStream.Length - reader.BaseStream.Position));
            reader.Close();
        }

        private void SetScriptData(int i, int Data)
        {
            if (i != 0)
            {
                if (i == 1)
                {
                    scriptData_Main[scriptDataPos++] = Data;
                }
                else if (i == 2)
                {
                    scriptData_Draw[scriptDataPos++] = Data;
                }
                else if (i == 3)
                {
                    scriptData_RSDK[scriptDataPos++] = Data;
                }
            }
            else
            {
                scriptData_Startup[scriptDataPos++] = Data;
            }
        }
        private void SetJumptableData(int i, int Data)
        {
            if (i != 0)
            {
                if (i == 1)
                {
                    jumpTableData_Main[jumpTableDataPos++] = Data;
                }
                else if (i == 2)
                {
                    jumpTableData_Draw[jumpTableDataPos++] = Data;
                }
                else if (i == 3)
                {
                    jumpTableData_RSDK[scriptDataPos++] = Data;
                }
            }
            else
            {
                jumpTableData_Startup[jumpTableDataPos++] = Data;
            }
        }

        public void Decompile(string DestPath)
        {
            string name = Path.GetFileNameWithoutExtension(DestPath);

            StreamWriter writer = new StreamWriter(DestPath);

            Console.WriteLine("Decompiling: " + name);

            writer.WriteLine("//------------Retro-Sonic " + name + " Script-------------//");
            writer.WriteLine("//--------Scripted by Christian Whitehead 'The Taxman'--------//");
            writer.WriteLine("//-------Unpacked By Rubberduckycooly's Script Unpacker-------//");

            writer.Write(Environment.NewLine);

            writer.Write(Environment.NewLine);
            writer.Write(Environment.NewLine);

            try
            {
                writer.WriteLine("//--------------------Startup Sub---------------------//");
                writer.WriteLine("//-------Called once when the object is spawned-------//");
                Console.Write("Startup script, ");
                DecompileScript(writer, 3);

                Console.Write("Main script, ");
                writer.WriteLine("//---------------------------Main Sub---------------------------//");
                writer.WriteLine("//-------Called once a frame, use this for most functions-------//");
                DecompileScript(writer, 0);

                /*writer.WriteLine("//-------------------------Player Interaction Sub---------------------------//");
                writer.WriteLine("//-------This sub is called when the object interacts with the player-------//");
                Console.Write("Player script, ");
                DecompileScript(writer, 1);*/

                writer.WriteLine("//----------------------Drawing Sub-------------------//");
                writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
                Console.Write("Draw script, ");
                DecompileScript(writer, 2);

                writer.WriteLine("//--------------------RSDK Sub---------------------//");
                writer.WriteLine("//-------Used for editor functionality-------//");
                Console.WriteLine("RSDK script.");
                DecompileScript(writer, 4);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            writer.Write(Environment.NewLine);
            writer.Close();
            {

            }
        }

        public void DecompileScript(StreamWriter writer, int scriptSub)
        {
            string strFuncName = "";
            switch (scriptSub)
            {
                case 0:
                    strFuncName = "Main_Routine";
                    break;
                case 1:
                    strFuncName = "PlayerInteraction_Routine";
                    break;
                case 2:
                    strFuncName = "Draw_Routine";
                    break;
                case 3:
                    strFuncName = "Startup_Routine";
                    break;
                case 4:
                    strFuncName = "RSDK_Routine";
                    break;
                default:
                    strFuncName = "Function";
                    break;
            }

            writer.Write("sub" + strFuncName + Environment.NewLine);

            state = new StateScriptEngine();
            state.scriptSub = scriptSub;
            state.deep = 1;
            state.isSwitchEnd = false;
            state.error = false;

            DecompileSub(writer, scriptSub);
            writer.Write(Environment.NewLine);
        }

        public void DecompileSub(StreamWriter writer, int scriptSub)
        {
            scriptCodePtr = 0;
            state.EndFlag = false;
            state.LoopBreakFlag = false;
            state.SwitchBreakFlag = false;
            writer.Write(Environment.NewLine);

            opcode = GetScriptData(scriptSub);

            while (!state.EndFlag)
            {
                scriptEndPtr++;

                switch (opcode)
                {
                    case 0:
                        opcode += 4;
                        GetVariables(0,writer);
                        break;
                    case 1:
                        opcode += 4;
                        DoArithmatic(writer,opcode);
                        break;
                    case 2:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 3:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 4:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 5:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 6:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 7:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 8:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 9:
                        opcode += 4;
                        DoArithmatic(writer, opcode);
                        break;
                    case 0xB:
                        opcode += 4;
                        writer.Write("Unknown1(");
                        GetVariables(1, writer);
                        if (scriptSub != 0)
                        {
                            if (scriptSub == 1)
                            {
                                opcode = scriptData_Main[scriptCodePtr];
                                opcode = scriptData_Main[scriptCodePtr + unknownArray1[scriptCodePtr]];
                                scriptEndPtr = 0;
                                scriptEndPtr = unknownArray1[scriptCodePtr];
                            }
                            else if (scriptSub == 2)
                            {
                                opcode = scriptData_Draw[scriptCodePtr];
                                opcode = scriptData_Draw[scriptCodePtr + unknownArray2[scriptCodePtr]];
                                scriptEndPtr = 0;
                                scriptEndPtr = unknownArray2[scriptCodePtr];
                            }
                        }
                        else
                        {
                            opcode = scriptData_Startup[scriptCodePtr];
                            opcode = scriptData_Startup[scriptCodePtr + unknownArray3[scriptCodePtr]];
                            scriptEndPtr = 0;
                            scriptEndPtr = unknownArray3[scriptCodePtr];
                        }
                        scriptCodePtr++;
                        break;
                    case 0xC:
                        opcode += 4;
                        writer.Write("Rand(");
                        GetVariables(2, writer);
                        break;
                    case 0xF:
                        opcode += 4;
                        writer.Write("Unknown2(");
                        GetVariables(3, writer);
                        break;
                    case 0x10:
                        opcode += 4;
                        writer.Write("Unknown3(");
                        GetVariables(3, writer);
                        break;
                    case 0x12:
                        opcode += 4;
                        writer.Write("DrawSprite(");
                        GetVariables(1, writer);
                        break;
                    case 0x13:
                        opcode += 4;
                        writer.Write("Unknown4(");
                        GetVariables(4, writer);
                        break;
                    case 0x14:
                        opcode += 4;
                        writer.Write("Unknown5(");
                        GetVariables(4, writer);
                        break;
                    case 0x15:
                        opcode += 4;
                        writer.Write("Unknown6(");
                        GetVariables(4, writer);
                        break;
                    case 0x16:
                        opcode += 4;
                        writer.Write("Unknown7(");
                        GetVariables(4, writer);
                        break;
                    case 0x17:
                        opcode += 4;
                        writer.Write("Unknown8(");
                        GetVariables(4, writer);
                        break;
                    case 0x18:
                        opcode += 4;
                        writer.Write("Unknown9(");
                        GetVariables(4, writer);
                        break;
                    case 0x19:
                        opcode += 4;
                        writer.Write("CheckEqual(");
                        GetVariables(2, writer); //CheckEqual
                        break;
                    case 0x1A:
                        opcode += 4;
                        writer.Write("CheckGreater(");
                        GetVariables(2, writer); //CheckGreater
                        break;
                    case 0x1B:
                        opcode += 4;
                        writer.Write("CheckLower(");
                        GetVariables(2, writer); //CheckLower
                        break;
                    case 0x1C:
                        opcode += 4;
                        writer.Write("CheckNotEqual(");
                        GetVariables(2, writer); //CheckNotEqual
                        break;
                    case 0x1D:
                        opcode += 4;
                        writer.Write("Unknown10(");
                        GetVariables(2, writer);
                        break;
                    case 0x1E:
                        opcode += 4;
                        writer.Write("Unknown11(");
                        GetVariables(2, writer);
                        break;
                    case 0x1F:
                        opcode += 4;
                        writer.Write("Unknown12(");
                        GetVariables(4, writer);
                        break;
                    case 0x20:
                        opcode += 4;
                        writer.Write("Unknown13(");
                        GetVariables(4, writer);
                        break;
                    case 0x21:
                        opcode += 4;
                        writer.Write("Unknown14(");
                        GetVariables(4, writer);
                        break;
                    case 0x22:
                        opcode += 4;
                        writer.Write("Unknown15(");
                        GetVariables(4, writer);
                        break;
                    case 0x23:
                        opcode += 4;
                        writer.Write("Unknown16(");
                        GetVariables(1, writer);
                        break;
                    case 0x24:
                        opcode += 4;
                        writer.Write("Unknown17(");
                        GetVariables(1, writer);
                        break;
                    case 0x25:
                        opcode += 4;
                        writer.Write("Unknown18(");
                        GetVariables(3, writer);
                        break;
                    case 0x26:
                        opcode += 4;
                        writer.Write("Unknown19(");
                        GetVariables(3, writer);
                        break;
                    case 0x27:
                        opcode += 4;
                        writer.Write("Unknown20(");
                        GetVariables(2, writer);
                        break;
                    case 0x28:
                        opcode += 4;
                        writer.Write("Unknown21(");
                        GetVariables(1, writer);
                        break;
                    case 0x29:
                        opcode += 4;
                        writer.Write("Unknown22(");
                        GetVariables(6, writer); //
                        break;
                    case 0x2A:
                        opcode += 4;
                        writer.Write("Unknown23(");
                        GetVariables(1, writer);
                        break;
                    case 0x2B:
                        opcode += 4;
                        writer.Write("Unknown24(");
                        GetVariables(4, writer);
                        break;
                    case 0x2C:
                        opcode += 4;
                        writer.Write("Unknown25(");
                        GetVariables(1, writer);
                        break;
                    case 0x2D:
                        opcode += 4;
                        writer.Write("Unknown26(");
                        GetVariables(1, writer);
                        break;
                    case 0x2E:
                        opcode += 4;
                        writer.Write("Unknown26(");
                        GetVariables(4, writer);
                        break;
                    case 0x2F:
                        opcode += 4;
                        writer.Write("Unknown27(");
                        GetVariables(5, writer);
                        break;
                    case 0x30:
                        opcode += 4;
                        writer.Write("Unknown28(");
                        GetVariables(2, writer);
                        //do shit here
                        break;
                    case 0x31:
                        opcode += 4;
                        writer.Write("Unknown29(");
                        GetVariables(1, writer);
                        /*if (SubID)
                        {
                            if (SubID == 1)
                            {
                                opcode = (int)&ScriptData1[16129 * ScriptID];
                                opcode = (int)&ScriptData1[16129 * ScriptID + dword_AF0264[v37]];
                                ScriptCodePtr = 0;
                                ScriptCodePtr = dword_AF0264[v37];
                            }
                            else if (SubID == 2)
                            {
                                opcode = (int)&ScriptData2[16129 * ScriptID];
                                opcode = (int)&ScriptData2[16129 * ScriptID + dword_AF0264[v37]];
                                ScriptCodePtr = 0;
                                ScriptCodePtr = dword_AF0264[v37];
                            }
                        }
                        else
                        {
                            opcode = (int)&ScriptData3[16129 * ScriptID];
                            opcode = (int)&ScriptData3[16129 * ScriptID + dword_AF0264[v37]];
                            ScriptCodePtr = 0;
                            ScriptCodePtr = dword_AF0264[v37];
                        }*/
                        break;
                    case 0x34:
                        opcode += 4;
                        writer.Write("Unknown30(");
                        GetVariables(1, writer);
                        break;
                    case 0x35:
                        opcode += 4;
                        writer.Write("Unknown31(");
                        GetVariables(7, writer);
                        break;
                    case 0x36:
                        opcode += 4;
                        writer.Write("Unknown32(");
                        GetVariables(8, writer);
                        break;
                    case 0x37:
                        opcode += 4;
                        writer.Write("Unknown33(");
                        GetVariables(4, writer);
                        break;
                    case 0x38:
                        opcode += 4;
                        writer.Write("Unknown34(");
                        GetVariables(4, writer);
                        break;
                    case 0x39:
                        opcode += 4;
                        writer.Write("Unknown35(");
                        GetVariables(3, writer);
                        break;
                    case 0x3A:
                        opcode += 4;
                        writer.Write("SetObjValue(");
                        GetVariables(3, writer);
                        //set object value
                        break;
                    case 0x3B:
                        opcode += 4;
                        writer.Write("GetObjValue(");
                        GetVariables(3, writer);
                        //get object value
                        break;
                    case 0x3C:
                        opcode += 4;
                        writer.Write("Unknown36(");
                        GetVariables(4, writer);
                        break;
                    case 0x3D:
                        opcode += 4;
                        writer.Write("Unknown37(");
                        GetVariables(4, writer);
                        break;
                    case 0x3E:
                        opcode += 4;
                        writer.Write("Unknown38(");
                        GetVariables(1, writer);
                        break;
                    case 0x3F: //DrawScaledSprite
                        opcode += 4;
                        writer.Write("Unknown39(");
                        GetVariables(1, writer);
                        break;
                    case 0x40:
                        opcode += 4;
                        writer.Write("Unknown40(");
                        GetVariables(6, writer); //
                        break;
                    case 0x41:
                        opcode += 4;
                        writer.Write("Unknown41(");
                        GetVariables(2, writer);
                        break;
                    case 0x42:
                        opcode += 4;
                        writer.Write("Unknown42(");
                        GetVariables(1, writer);
                        break;
                    default:
                        break;
                }

                if (scriptEndPtr >= Endpoint_Startup)
                {
                    state.EndFlag = true;
                }

            }
            //if (!state.isSwitchEnd && !state.EndFlag)
            writer.Write(Environment.NewLine);
        }

        private void DoArithmatic(StreamWriter writer, int Type)
        {

            bool Single = false;

            switch (opcode)
            {
                case 1:
                    opcode += 4;
                    writer.Write("Object.Type");
                    break;
                case 2:
                    opcode += 4;
                    writer.Write("Object.PropertyValue");
                    break;
                case 3:
                    opcode += 4;
                    writer.Write("Object.XPos");
                    break;
                case 4:
                    opcode += 4;
                    writer.Write("Object.YPos");
                    break;
                case 5:
                    opcode += 4;
                    writer.Write("Object.ValueA");
                    break;
                case 6:
                    opcode += 4;
                    writer.Write("Object.ValueB");
                    break;
                case 7:
                    opcode += 4;
                    writer.Write("Object.ValueC");
                    break;
                case 8:
                    opcode += 4;
                    writer.Write("Object.ValueD");
                    break;
                case 9:
                    opcode += 4;
                    writer.Write("Object.ValueE");
                    break;
                case 0xA:
                    opcode += 4;
                    writer.Write("Object.ValueF");
                    break;
                case 0xB:
                    opcode += 4;
                    writer.Write("TempObject.Type");
                    break;
                case 0xC:
                    opcode += 4;
                    writer.Write("TempObject.PropertyValue");
                    break;
                case 0xD:
                    opcode += 4;
                    writer.Write("TempObject.XPos");
                    break;
                case 0xE:
                    opcode += 4;
                    writer.Write("TempObject.YPos");
                    break;
                case 0xF:
                    opcode += 4;
                    writer.Write("TempObject.ValueA");
                    break;
                case 0x10:
                    opcode += 4;
                    writer.Write("TempObject.ValueB");
                    break;
                case 0x11:
                    opcode += 4;
                    writer.Write("TempObject.ValueC");
                    break;
                case 0x12:
                    opcode += 4;
                    writer.Write("TempObject.ValueD");
                    break;
                case 0x13:
                    opcode += 4;
                    writer.Write("TempObject.ValueE");
                    break;
                case 0x14:
                    opcode += 4;
                    writer.Write("TempObject.ValueF");
                    break;
                case 0x15:
                    opcode += 4;
                    break;
                case 0x17:
                    opcode += 4;
                    break;
                case 0x19:
                    opcode += 4;
                    writer.Write("Player.XPos");
                    break;
                case 0x1A:
                    opcode += 4;
                    writer.Write("Player.YPos");
                    break;
                case 0x1E:
                    opcode += 4;
                    break;
                case 0x20:
                    opcode += 4;
                    break;
                case 0x21:
                    opcode += 4;
                    break;
                case 0x22:
                    opcode += 4;
                    break;
                case 0x24:
                    opcode += 4;
                    break;
                case 0x25:
                    opcode += 4;
                    break;
                case 0x26:
                    opcode += 4;
                    break;
                case 0x27:
                    opcode += 4;
                    break;
                case 0x28:
                    opcode += 4;
                    writer.Write("TempObjectPos");
                    break;
                case 0x29:
                    opcode += 4;
                    break;
                case 0x2A:
                    opcode += 4;
                    writer.Write("MusicSFXCount");
                    break;
                case 0x2B:
                    opcode += 4;
                    break;
                case 0x2C:
                    opcode += 4;
                    writer.Write("CheckResult");
                    break;
                case 0x2D:
                    opcode += 4;
                    break;
                case 0x2E:
                    opcode += 4;
                    writer.Write("ObjectPos");
                    break;
                case 0x2F:
                    opcode += 4;
                    break;
                case 0x30:
                    opcode += 4;
                    break;
                case 0x31:
                    opcode += 4;
                    break;
                case 0x32:
                    opcode += 4;
                    break;
                case 0x33:
                    opcode += 4;
                    break;
                case 0x34:
                    opcode += 4;
                    break;
                case 0x35:
                    opcode += 4;
                    break;
                case 0x36:
                    opcode += 4;
                    break;
                case 0x37:
                    opcode += 4;
                    break;
                case 0x38:
                    opcode += 4;
                    break;
                case 0x39:
                    opcode += 4;
                    break;
                case 0x3A:
                    opcode += 4;
                    writer.Write("Player.FullEngineRotation");
                    break;
                case 0x3B:
                    opcode += 4;
                    break;
                case 0x3C:
                    opcode += 4;
                    break;
                case 0x3D:
                    opcode += 4;
                    break;
                case 0x3E:
                    opcode += 4;
                    break;
                case 0x3F:
                    opcode += 4;
                    break;
                case 0x40:
                    opcode += 4;
                    break;
                case 0x41:
                    opcode += 4;
                    break;
                case 0x42:
                    opcode += 4;
                    writer.Write("Player.ID");
                    break;
                case 0x43:
                    opcode += 4;
                    writer.Write("Object.Priority");
                    break;
                case 0x44:
                    opcode += 4;
                    break;
                default:
                    break;
            }

            switch (Type)
            {
                case 1:
                    writer.Write("=");
                    break;
                case 2:                    
                    writer.Write("+=");
                    break;
                case 3:                   
                    writer.Write("-=");
                    break;
                case 4:                   
                    writer.Write("++");
                    Single = true;
                    break;
                case 5:                   
                    writer.Write("--");
                    Single = true;
                    break;
                case 6:
                    writer.Write("*=");
                    break;
                case 7:
                    writer.Write("/=");
                    break;
                case 8:
                    writer.Write(">>=");
                    break;
            }

            if (!Single)
            {
                switch (opcode)
                {
                    case 1:
                        writer.Write("Object.Type");
                        break;
                    case 2:
                        writer.Write("Object.PropertyValue");
                        break;
                    case 3:
                        writer.Write("Object.XPos");
                        break;
                    case 4:
                        writer.Write("Object.YPos");
                        break;
                    case 5:
                        writer.Write("Object.ValueA");
                        break;
                    case 6:
                        writer.Write("Object.ValueB");
                        break;
                    case 7:
                        writer.Write("Object.ValueC");
                        break;
                    case 8:
                        writer.Write("Object.ValueD");
                        break;
                    case 9:
                        writer.Write("Object.ValueE");
                        break;
                    case 0xA:
                        writer.Write("Object.ValueF");
                        break;
                    case 0xB:
                        writer.Write("TempObject.Type");
                        break;
                    case 0xC:
                        writer.Write("TempObject.PropertyValue");
                        break;
                    case 0xD:
                        writer.Write("TempObject.XPos");
                        break;
                    case 0xE:
                        writer.Write("TempObject.YPos");
                        break;
                    case 0xF:
                        writer.Write("TempObject.ValueA");
                        break;
                    case 0x10:
                        writer.Write("TempObject.ValueB");
                        break;
                    case 0x11:
                        writer.Write("TempObject.ValueC");
                        break;
                    case 0x12:
                        writer.Write("TempObject.ValueD");
                        break;
                    case 0x13:
                        writer.Write("TempObject.ValueE");
                        break;
                    case 0x14:
                        writer.Write("TempObject.ValueF");
                        break;
                    case 0x15:
                        writer.Write("UnknownValue0x15");
                        break;
                    case 0x17:
                        writer.Write("UnknownValue0x17");
                        break;
                    case 0x19:
                        writer.Write("Player.XPos");
                        break;
                    case 0x1A:
                        writer.Write("Player.YPos");
                        break;
                    case 0x1E:
                        writer.Write("UnknownValue0x1E");
                        break;
                    case 0x20:
                        writer.Write("UnknownValue0x20");
                        break;
                    case 0x21:
                        writer.Write("UnknownValue0x21");
                        break;
                    case 0x22:
                        writer.Write("UnknownValue0x22");
                        break;
                    case 0x24:
                        writer.Write("UnknownValue0x24");
                        break;
                    case 0x25:
                        writer.Write("UnknownValue0x25");
                        break;
                    case 0x26:
                        writer.Write("UnknownValue0x26");
                        break;
                    case 0x27:
                        writer.Write("UnknownValue0x27");
                        break;
                    case 0x28:
                        writer.Write("TempObjectPos");
                        break;
                    case 0x29:
                        writer.Write("UnknownValue0x29");
                        break;
                    case 0x2A:
                        writer.Write("MusicSFXCount");
                        break;
                    case 0x2B:
                        writer.Write("UnknownValue0x2B");
                        break;
                    case 0x2C:
                        writer.Write("CheckResult");
                        break;
                    case 0x2D:
                        writer.Write("UnknownValue0x2D");
                        break;
                    case 0x2E:
                        writer.Write("ObjectPos");
                        break;
                    case 0x2F:
                        writer.Write("UnknownValue0x2F");
                        break;
                    case 0x30:
                        writer.Write("UnknownValue0x30");
                        break;
                    case 0x31:
                        writer.Write("UnknownValue0x31");
                        break;
                    case 0x32:
                        writer.Write("UnknownValue0x32");
                        break;
                    case 0x33:
                        writer.Write("UnknownValue0x33");
                        break;
                    case 0x34:
                        writer.Write("UnknownValue0x34");
                        break;
                    case 0x35:
                        writer.Write("UnknownValue0x35");
                        break;
                    case 0x36:
                        writer.Write("UnknownValue0x36");
                        break;
                    case 0x37:
                        writer.Write("UnknownValue0x37");
                        break;
                    case 0x38:
                        writer.Write("UnknownValue0x38");
                        break;
                    case 0x39:
                        writer.Write("UnknownValue0x39");
                        break;
                    case 0x3A:
                        writer.Write("Player.FullEngineRotation");
                        break;
                    case 0x3B:
                        writer.Write("UnknownValue0x3B");
                        break;
                    case 0x3C:
                        writer.Write("UnknownValue0x3C");
                        break;
                    case 0x3D:
                        writer.Write("UnknownValue0x3D");
                        break;
                    case 0x3E:
                        writer.Write("UnknownValue0x3E");
                        break;
                    case 0x3F:
                        writer.Write("UnknownValue0x3F");
                        break;
                    case 0x40:
                        writer.Write("UnknownValue0x40");
                        break;
                    case 0x41:
                        writer.Write("UnknownValue0x41");
                        break;
                    case 0x42:
                        writer.Write("Player.ID");
                        break;
                    case 0x43:
                        writer.Write("Object.Priority");
                        break;
                    case 0x44:
                        writer.Write("UnknownValue0x44");
                        break;
                    default:
                        break;
                }
            }

            int vars = 2;
            if (Single) vars = 1;

            //SetVariables(vars);
            writer.WriteLine();
        }

        private int GetVariables(int ParameterCount, StreamWriter writer)
        {
            int v1;
            int result;
            int i;
            byte v4 = 0;
            byte v5 = 0;

            v4 = 0;
            for (i = 0; i < ParameterCount; ++i)
            {
                if (opcode != 0)
                {
                    opcode += 4;
                    v5 = (byte)(v4 + 2);
                    switch (opcode)
                    {
                        case 1:
                            opcode += 4;
                            writer.Write("Object.Type");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 2:
                            opcode += 4;
                            writer.Write("Object.PropertyValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 3:
                            opcode += 4;
                            writer.Write("Object.XPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 4:
                            opcode += 4;
                            writer.Write("Object.YPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 5:
                            opcode += 4;
                            writer.Write("Object.ValueA");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 6:
                            opcode += 4;
                            writer.Write("Object.ValueB");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 7:
                            opcode += 4;
                            writer.Write("Object.ValueC");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 8:
                            opcode += 4;
                            writer.Write("Object.ValueD");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 9:
                            opcode += 4;
                            writer.Write("Object.ValueE");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0xA:
                            opcode += 4;
                            writer.Write("Object.ValueF");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0xB:
                            opcode += 4;
                            writer.Write("TempObject.Type");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0xC:
                            opcode += 4;
                            writer.Write("TempObject.PropertyValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0xD:
                            opcode += 4;
                            writer.Write("TempObject.XPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0xE:
                            opcode += 4;
                            writer.Write("TempObject.YPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0xF:
                            opcode += 4;
                            writer.Write("TempObject.ValueA");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x10:
                            opcode += 4;
                            writer.Write("TempObject.ValueB");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x11:
                            opcode += 4;
                            writer.Write("TempObject.ValueC");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x12:
                            opcode += 4;
                            writer.Write("TempObject.ValueD");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x13:
                            opcode += 4;
                            writer.Write("TempObject.ValueE");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x14:
                            opcode += 4;
                            writer.Write("TempObject.ValueF");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x15:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x17:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x19:
                            opcode += 4;
                            writer.Write("Player.XPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x1A:
                            opcode += 4;
                            writer.Write("Player.YPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x1E:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x20:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x21:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x22:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x24:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x25:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x26:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x27:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x28:
                            opcode += 4;
                            writer.Write("TempObjectPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x29:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x2A:
                            opcode += 4;
                            writer.Write("MusicSFXCount");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x2B:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x2C:
                            opcode += 4;
                            writer.Write("CheckResult");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x2D:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x2E:
                            opcode += 4;
                            writer.Write("ObjectPos");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x2F:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x30:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x31:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x32:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x33:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x34:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x35:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x36:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x37:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x38:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x39:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x3A:
                            opcode += 4;
                            writer.Write("Player.FullEngineRotation");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x3B:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x3C:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x3D:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x3E:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            if (i + 1 < ParameterCount)
                            {
                                writer.Write(",");
                            }
                            break;
                        case 0x3F:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            break;
                        case 0x40:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            break;
                        case 0x41:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            break;
                        case 0x42:
                            opcode += 4;
                            writer.Write("Player.ID");
                            break;
                        case 0x43:
                            opcode += 4;
                            writer.Write("Object.Priority");
                            break;
                        case 0x44:
                            opcode += 4;
                            writer.Write("UnknownValue");
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    opcode += 4;
                    v5 = (byte)(v4 + 1);
                }
                opcode += 4;
                v4 = (byte)(v5 + 1);
            }
            if (ParameterCount > 0) writer.Write(")");
            writer.Write(Environment.NewLine);
            v1 = 4 * v4;
            result = opcode - v1;
            opcode -= v1;
            //SetVariables(ParameterCount);
            return result;
        }

        private int SetVariables(int ParameterCount)
        {
            int i;

            for (i = 0; i < ParameterCount; ++i)
            {
                if (opcode != 0)
                {
                    opcode += 4;
                    scriptEndPtr += 3;
                    switch (opcode)
                    {
                        case 1:
                            opcode += 4;
                            break;
                        case 2:
                            opcode += 4;
                            break;
                        case 3:
                            opcode += 4;
                            break;
                        case 4:
                            opcode += 4;
                            break;
                        case 5:
                            opcode += 4;
                            break;
                        case 6:
                            opcode += 4;
                            break;
                        case 7:
                            opcode += 4;
                            break;
                        case 8:
                            opcode += 4;
                            break;
                        case 9:
                            opcode += 4;
                            break;
                        case 0xA:
                            opcode += 4;
                            break;
                        case 0xB:
                            opcode += 4;
                            break;
                        case 0xC:
                            opcode += 4;
                            break;
                        case 0xD:
                            opcode += 4;
                            break;
                        case 0xE:
                            opcode += 4;
                            break;
                        case 0xF:
                            opcode += 4;
                            break;
                        case 0x10:
                            opcode += 4;
                            break;
                        case 0x11:
                            opcode += 4;
                            break;
                        case 0x12:
                            opcode += 4;
                            break;
                        case 0x13:
                            opcode += 4;
                            break;
                        case 0x14:
                            opcode += 4;
                            break;
                        case 0x15:
                            opcode += 4;
                            break;
                        case 0x17:
                            opcode += 4;
                            break;
                        case 0x19:
                            opcode += 4;
                            break;
                        case 0x1A:
                            opcode += 4;
                            break;
                        case 0x1E:
                            opcode += 4;
                            break;
                        case 0x20:
                            opcode += 4;
                            break;
                        case 0x21:
                            opcode += 4;
                            break;
                        case 0x22:
                            opcode += 4;
                            break;
                        case 0x24:
                            opcode += 4;
                            break;
                        case 0x25:
                            opcode += 4;
                            break;
                        case 0x26:
                            opcode += 4;
                            break;
                        case 0x27:
                            opcode += 4;
                            break;
                        case 0x28:
                            opcode += 4;
                            break;
                        case 0x29:
                            opcode += 4;
                            break;
                        case 0x2A:
                            opcode += 4;
                            break;
                        case 0x2B:
                            opcode += 4;
                            break;
                        case 0x2C:
                            opcode += 4;
                            break;
                        case 0x2D:
                            opcode += 4;
                            break;
                        case 0x2E:
                            opcode += 4;
                            break;
                        case 0x2F:
                            opcode += 4;
                            break;
                        case 0x30:
                            opcode += 4;
                            break;
                        case 0x31:
                            opcode += 4;
                            break;
                        case 0x32:
                            opcode += 4;
                            break;
                        case 0x33:
                            opcode += 4;
                            break;
                        case 0x34:
                            opcode += 4;
                            break;
                        case 0x35:
                            opcode += 4;
                            break;
                        case 0x36:
                            opcode += 4;
                            break;
                        case 0x37:
                            opcode += 4;
                            break;
                        case 0x38:
                            opcode += 4;
                            break;
                        case 0x39:
                            opcode += 4;
                            break;
                        case 0x3A:
                            opcode += 4;
                            break;
                        case 0x3B:
                            opcode += 4;
                            break;
                        case 0x3C:
                            opcode += 4;
                            break;
                        case 0x3D:
                            opcode += 4;
                            break;
                        case 0x3E:
                            opcode += 4;
                            break;
                        case 0x3F:
                            opcode += 4;
                            break;
                        case 0x40:
                            opcode += 4;
                            break;
                        case 0x41:
                            opcode += 4;
                            break;
                        case 0x42:
                            opcode += 4;
                            break;
                        case 0x43:
                            opcode += 4;
                            break;
                        case 0x44:
                            opcode += 4;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    opcode += 4;
                    scriptEndPtr += 2;
                }
                opcode += 4;
            }
            return 0;
        }

        int GetScriptData(int SubID)
        {
            int data = 0;

            if (SubID != 0)
            {
                if (SubID == 1)
                {
                    data = scriptData_Main[scriptCodePtr++];
                }
                else if (SubID == 2)
                {
                    data = scriptData_Draw[scriptCodePtr++];
                }
                else if (SubID == 3)
                {
                    data = scriptData_RSDK[scriptCodePtr++];
                }
            }
            else
            {
                data = scriptData_Startup[scriptCodePtr += 4];
            }

            return data;
        }

        string GetVariable(int SubID, int opcode)
        {
            switch(opcode)
            {
                case 0x1:
                    return "Object.Type";
                case 0x2:
                    return "Object.SubType";
                case 0x3:
                    return "Object.Priority";
                case 0x4:
                    return "Object.Xpos";
                case 0x5:
                    return "Object.Ypos";
                case 0x6:
                    return "Object.ValueA";
                case 0x7:
                    return "Object.ValueB";
                case 0x8:
                    return "Object.ValueC";
                case 0x9:
                    return "Object.ValueD";
                case 0xA:
                    return "Object.ValueE";
                case 0xB:
                    return "Object.ValueF";
                case 0xC:
                    return "TempObject.Type";
                case 0xD:
                    return "TempObject.SubType";
                case 0xE:
                    return "TempObject.Priority";
                case 0xF:
                    return "TempObject.Xpos";
                case 0x10:
                    return "TempObject.Ypos";
                case 0x11:
                    return "TempObject.ValueA";
                case 0x12:
                    return "TempObject.ValueB";
                case 0x13:
                    return "TempObject.ValueC";
                case 0x14:
                    return "TempObject.ValueD";
                case 0x15:
                    return "TempObject.ValueE";
                case 0x16:
                    return "TempObject.ValueF";
                default:
                    return GetScriptData(SubID).ToString();
            }
            return "0";
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        public void Write(Writer writer)
        {

            writer.Close();
        }

    }
}
