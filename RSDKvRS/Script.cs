using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvRS
{
    public class Script
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
        int[] scriptData_Draw = new int[0x40000];
        int[] scriptData_Startup = new int[0x40000];
        int[] scriptData_RSDK = new int[0x40000];

        int[] jumpTableData_Main = new int[0x40000];
        int[] jumpTableData_Draw = new int[0x40000];
        int[] jumpTableData_Startup = new int[0x40000];
        int[] jumpTableData_RSDK = new int[0x40000];

        int scriptCodePtr = 0;
        int jumpTablePtr = 0;

        ScriptEngine scriptEng;
        StateScriptEngine state = new StateScriptEngine();

        public Script()
        { }

        public Script(string filename): this(new Reader(filename))
        { }

        public Script(Reader reader)
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

                FileBuffer = reader.ReadByte();
                Value1 = FileBuffer << 8;
                FileBuffer = reader.ReadByte();
                Value1 += FileBuffer;

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
                                                    break;
                                                case 1:
                                                    break;
                                                case 2:
                                                    break;
                                                case 3:
                                                    break;
                                                case 4:
                                                    break;
                                                case 5:
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
                    /*FileBuffer = reader.ReadByte();
                    Value1 = FileBuffer << 8;
                    FileBuffer = reader.ReadByte();
                    Value1 += FileBuffer;*/

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    /*FileBuffer = reader.ReadByte();
                    Value3 = FileBuffer << 8;
                    FileBuffer = reader.ReadByte();
                    Value4 += FileBuffer + Value3;*/

                    Value4 = reader.ReadByte() << 8;
                    Value4 |= reader.ReadByte();

                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();

                    if (i != 0)
                    {
                        if (i == 1)
                        {
                            //dword_AFD294[16129 * ScriptID + v20] = Value1;
                        }
                        else if (i == 2)
                        {
                            //dword_AFDA94[16129 * ScriptID + v20] = Value1;
                        }
                        else if (i == 3)
                        {
                            
                        }
                    }
                    else
                    {
                        //dword_AFCA94[16129 * ScriptID + v20] = Value1;
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

                    /*FileBuffer = reader.ReadByte();
                    Value1 = FileBuffer << 8;
                    FileBuffer = reader.ReadByte();
                    Value1 += FileBuffer;*/

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    /*FileBuffer = reader.ReadByte();
                    Value3 = FileBuffer << 8;
                    FileBuffer = reader.ReadByte();
                    Value4 += FileBuffer + Value3;*/

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
            int index1 = 0;
            int objectLoop = 0;
            scriptCodePtr = 0;
            state.EndFlag = false;
            state.LoopBreakFlag = false;
            state.SwitchBreakFlag = false;
            writer.Write(Environment.NewLine);

            while (!state.EndFlag)
            {
                int opcode = GetScriptData(scriptSub);
                int paramsCount = scriptOpcodeSizes[opcode];

                //state.isSwitchEnd = false;

                string[] variableName = new string[10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNKNOWN VARIABLE";
                }

                int Val = opcode;

                for (int i = 0; i < paramsCount; i++)
                {
                    variableName[i] = GetVariable(scriptSub, Val+=4);
                    //scriptCodePtr++;
                }


                if (opcode >= 134)
                {
                    writer.Write("ERROR AT: " + scriptCodePtr + " : " + opcode);
                    Console.WriteLine("OPCODE ABOVE THE MAX OPCODES");
                    state.error = true;
                    return;
                }

                string operand = opcodeList[opcode];


                for (int i = 0; i < variableName.Length; i++)
                {
                    if (variableName[i] == "" || variableName[i] == null)
                    {
                        variableName[i] = "Object.ValueA";
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
