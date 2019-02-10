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

        public class SpriteFrame
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
//NULL
    "Equal",
    "Add",
    "Sub",
    "Inc",
    "Dec",
    "Mul",
    "Div",
    "ShR",
    "ShL",
    "Unknown1",
    "Rand",
                        "Unknown2",
                        "Unknown3",
                        "DrawSprite",
                        "Unknown4",
                        "Unknown5",
                        "Unknown6",
                        "SetEditorIcon",
                        "Unknown8",
                        "Unknown9",
                        "CheckEqual",
                        "CheckGreater",
                        "CheckLower",
                        "CheckNotEqual",
                        "Unknown10",
                        "Unknown11",
                        "Unknown12",
                        "Unknown13",
                        "Unknown14",
                        "Unknown15",
                        "Unknown16",
                        "Unknown17",
                        "Unknown18",
                        "Unknown19",
                        "Unknown20",
                        "Unknown21",
                        "Unknown22",
                        "Unknown23",
                        "Unknown24",
                        "Unknown25",
                        "Unknown26",
                        "Unknown27",
                        "Unknown28",
                        "Unknown29",
                        "Unknown30",
                        "Unknown31",
                        "Unknown32",
                        "Unknown33",
                        "Unknown34",
                        "Unknown35",
                        "SetObjValue",
                        "GetObjValue",
                        "Unknown36",
                        "Unknown37",
                        "Unknown38",
                        "DrawSpriteFX",
                        "Unknown40",
                        "Unknown41",
                        "Unknown42",
};

        /// <summary>
        /// how many paramaters each Function has
        /// </summary>
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

        /// <summary>
        /// How Many Subs per Script
        /// </summary>
        public byte SubCount
        {
            get
            {
                return 5;
            }
        }

        int scriptDataPos;
        int jumpTableDataPos;

        /// <summary>
        /// the scriptdata for the Main Sub
        /// </summary>
        int[] scriptData_Main = new int[0x40000];
        int Endpoint_Main = 0;
        /// <summary>
        /// the Jumptabledata for the Main Sub
        /// </summary>
        int[] jumpTableData_Main = new int[0x40000];
        /// <summary>
        /// No idea but it's for main
        /// </summary>
        int[] unknownArray1 = new int[0x40000];

        /// <summary>
        /// the ScriptData for the Draw sub
        /// </summary>
        int[] scriptData_Draw = new int[0x40000];
        int Endpoint_Draw = 0;
        /// <summary>
        /// the JumpTable data for the Draw sub
        /// </summary>
        int[] jumpTableData_Draw = new int[0x40000];
        /// <summary>
        /// No idea but it's for draw
        /// </summary>
        int[] unknownArray2 = new int[0x40000];

        /// <summary>
        /// the scriptdata for the Startup Sub
        /// </summary>
        int[] scriptData_Startup = new int[0x40000];
        int Endpoint_Startup = 0;
        /// <summary>
        /// the jumptableData for the Startup Sub
        /// </summary>
        int[] jumpTableData_Startup = new int[0x40000];
        /// <summary>
        /// no idea but it's for Startup
        /// </summary>
        int[] unknownArray3 = new int[0x40000];

        /// <summary>
        /// the scriptData for the Editor sub
        /// </summary>
        int[] scriptData_RSDK = new int[0x40000];
        int Endpoint_RSDK = 0;
        /// <summary>
        /// the JumptableData for the Editor sub
        /// </summary>
        int[] jumpTableData_RSDK = new int[0x40000];

        /// <summary>
        /// a list of the SpriteFrames
        /// </summary>
        public List<SpriteFrame> spriteFrames = new List<SpriteFrame>();

        /// <summary>
        /// index in the scriptCode
        /// </summary>
        int scriptCodePtr = 0;
        int spriteframePtr = 0;
        int jumpTablePtr = 0;
        int scriptEndPtr = 0;
        int opcode = 0;

        ScriptEngine scriptEng;
        StateScriptEngine state = new StateScriptEngine();

        public Script()
        { }

        public Script(string filename): this(new Reader(filename))
        { }

        public Script(Reader reader, bool EditorMode = false)
        {
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];
            scriptEng.arrayPosition = new int[3];

            for (int i = 0; i < SubCount; i++)
            {
                uint Opcode = 0;
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

                    if (EditorMode)
                    {
                        while(i < 3)
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
                                            i++;
                                            FileBuffer = reader.ReadByte();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    Opcode = (uint)FileBuffer;

                    if (k < 1)
                    {
                        SetScriptData(i, FileBuffer);

                        switch (Opcode)
                        {
                            case 0u:
                            case 4u:
                            case 5u:
                            case 0xAu:
                            case 0xBu:
                            case 0xDu:
                            case 0x23u:
                            case 0x24u:
                            case 0x28u:
                            case 0x2Au:
                            case 0x2Cu:
                            case 0x2Du:
                            case 0x31u:
                            case 0x34u:
                            case 0x3Eu:
                            case 0x3Fu:
                            case 0x42u:
                                OpcodeSize = 1;
                                break;
                            case 1u:
                            case 2u:
                            case 3u:
                            case 6u:
                            case 7u:
                            case 8u:
                            case 9u:
                            case 0xCu:
                            case 0x30u:
                            case 0x41u:
                                OpcodeSize = 2;
                                break;
                            case 0xEu:
                                OpcodeSize = 6;
                                break;
                            case 0xFu:
                            case 0x10u:
                            case 0x39u:
                            case 0x3Au:
                            case 0x3Bu:
                                OpcodeSize = 3;
                                break;
                            case 0x11u:
                                OpcodeSize = 6;
                                break;
                            case 0x12u:
                                OpcodeSize = 1;
                                break;
                            case 0x13u:
                            case 0x14u:
                            case 0x15u:
                            case 0x16u:
                            case 0x17u:
                            case 0x18u:
                            case 0x2Eu:
                            case 0x37u:
                            case 0x38u:
                            case 0x3Cu:
                            case 0x3Du:
                                OpcodeSize = 4;
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
                            case 0x25u:
                                OpcodeSize = 3;
                                break;
                            case 0x26u:
                                OpcodeSize = 3;
                                break;
                            case 0x27u:
                                OpcodeSize = 2;
                                break;
                            case 0x29u:
                            case 0x40u:
                                OpcodeSize = 6;
                                break;
                            case 0x2Bu:
                                OpcodeSize = 4;
                                break;
                            case 0x2Fu:
                                OpcodeSize = 5;
                                break;
                            case 0x35u:
                                OpcodeSize = 7;
                                break;
                            case 0x36u:
                                OpcodeSize = 8;
                                break;
                            default:
                                break;
                        }
                        
                        //Error?
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

                                if (Value1 > 128) { Value1 = 128 - Value1; }

                                SetScriptData(i, Value1);
                            }
                            else
                            {
                                FileBuffer = reader.ReadByte();
                                if (FileBuffer < 128)
                                {
                                    Value2 = reader.ReadByte() << 8;
                                    Value2 |= reader.ReadByte();
                                }
                                else
                                {
                                    FileBuffer = FileBuffer + -128;
                                    Value2 = reader.ReadByte() << 8;
                                    Value2 |= reader.ReadByte();
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
                                        if (Opcode == 17) //SetEditorIcon?
                                        {
                                            if (spriteFrames.Count <= spriteframePtr)
                                            {
                                                spriteFrames.Add(new SpriteFrame());
                                            }
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

                Value8 = 0;

                for (int j = 0; j < 512; ++j) //Clear out space
                {
                    SetJumptableData(i, 0);
                }

                for (int j = 0; j < OpcodeSize; ++j)
                {
                    Value0 = 0x8000;
                    Value9 = 0;

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    Value3 = reader.ReadByte() << 8;
                    Value4 = reader.ReadByte() + Value3;

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

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    SetJumptableData(i, Value1);

                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();

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
                DecompileScript(writer, 2);

                Console.Write("Main script, ");
                writer.WriteLine("//---------------------------Main Sub---------------------------//");
                writer.WriteLine("//-------Called once a frame, use this for most functions-------//");
                DecompileScript(writer, 0);

                writer.WriteLine("//----------------------Drawing Sub-------------------//");
                writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
                Console.Write("Draw script, ");
                DecompileScript(writer, 1);

                writer.WriteLine("//--------------------RSDK Sub---------------------//");
                writer.WriteLine("//-------Used for editor functionality-------//");
                Console.WriteLine("RSDK script.");
                DecompileScript(writer, 3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            writer.Write(Environment.NewLine);
            writer.Close();
        }

        public void DecompileScript(StreamWriter writer, int scriptSub)
        {
            string strFuncName = "";
            switch (scriptSub)
            {
                case 0:
                    strFuncName = "Main";
                    break;
                case 1:
                    strFuncName = "Draw";
                    break;
                case 2:
                    strFuncName = "Startup";
                    break;
                case 3:
                    strFuncName = "RSDK";
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

            bool newline = true;

            while (!state.EndFlag)
            {
                scriptEndPtr++;

                opcode = GetScriptData(scriptSub);

                int paramsCount = 0;

                string[] variableName = new string[10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNKNOWN VARIABLE";
                }

                switch (opcode)
                {
                    case 0:

                        paramsCount = 0;
                        break;
                    case 1:

                        paramsCount = 2;
                        break;
                    case 2:

                        paramsCount = 2;
                        break;
                    case 3:

                        paramsCount = 2;
                        break;
                    case 4:

                        paramsCount = 2;
                        break;
                    case 5:

                        paramsCount = 2;
                        break;
                    case 6:

                        paramsCount = 2;
                        break;
                    case 7:

                        paramsCount = 2;
                        break;
                    case 8:

                        paramsCount = 2;
                        break;
                    case 9:

                        paramsCount = 2;
                        break;
                    case 0xB:

                        paramsCount = 1;
                        if (scriptSub != 0)
                        {
                            if (scriptSub == 1)
                            {
                                scriptCodePtr = scriptData_Main[scriptCodePtr];
                                scriptCodePtr = scriptData_Main[scriptCodePtr + unknownArray1[scriptCodePtr]];
                                scriptEndPtr = 0;
                                scriptEndPtr = unknownArray1[scriptCodePtr];
                            }
                            else if (scriptSub == 2)
                            {
                                scriptCodePtr = scriptData_Draw[scriptCodePtr];
                                scriptCodePtr = scriptData_Draw[scriptCodePtr + unknownArray2[scriptCodePtr]];
                                scriptEndPtr = 0;
                                scriptEndPtr = unknownArray2[scriptCodePtr];
                            }
                        }
                        else
                        {
                            scriptCodePtr = scriptData_Startup[scriptCodePtr];
                            scriptCodePtr = scriptData_Startup[scriptCodePtr + unknownArray3[scriptCodePtr]];
                            scriptEndPtr = 0;
                            scriptEndPtr = unknownArray3[scriptCodePtr];
                        }
                        scriptCodePtr++;
                        break;
                    case 0xC:

                        paramsCount = 2;
                        break;
                    case 0xF:

                        paramsCount = 3;
                        break;
                    case 0x10:

                        paramsCount = 3;
                        break;
                    case 0x11: //SetEditorIcon

                        paramsCount = 1;
                        break;
                    case 0x12:

                        paramsCount = 1;
                        break;
                    case 0x13:

                        paramsCount = 4;
                        break;
                    case 0x14:

                        paramsCount = 4;
                        break;
                    case 0x15:

                        paramsCount = 4;
                        break;
                    case 0x16:

                        paramsCount = 4;
                        break;
                    case 0x17:

                        paramsCount = 4;
                        break;
                    case 0x18:

                        paramsCount = 4;
                        break;
                    case 0x19:

                        paramsCount = 2; //CheckEqual
                        break;
                    case 0x1A:

                        paramsCount = 2; //CheckGreater
                        break;
                    case 0x1B:

                        paramsCount = 2; //CheckLower
                        break;
                    case 0x1C:

                        paramsCount = 2; //CheckNotEqual
                        break;
                    case 0x1D:

                        paramsCount = 2;
                        break;
                    case 0x1E:

                        paramsCount = 2;
                        break;
                    case 0x1F:

                        paramsCount = 4;
                        break;
                    case 0x20:

                        paramsCount = 4;
                        break;
                    case 0x21:

                        paramsCount = 4;
                        break;
                    case 0x22:

                        paramsCount = 4;
                        break;
                    case 0x23:

                        paramsCount = 1;
                        break;
                    case 0x24:

                        paramsCount = 1;
                        break;
                    case 0x25:

                        paramsCount = 3;
                        break;
                    case 0x26:

                        paramsCount = 3;
                        break;
                    case 0x27:

                        paramsCount = 2;
                        break;
                    case 0x28:

                        paramsCount = 1;
                        break;
                    case 0x29:

                        paramsCount = 6; //
                        break;
                    case 0x2A:

                        paramsCount = 1;
                        break;
                    case 0x2B:

                        paramsCount = 4;
                        break;
                    case 0x2C:

                        paramsCount = 1;
                        break;
                    case 0x2D:

                        paramsCount = 1;
                        break;
                    case 0x2E:

                        paramsCount = 4;
                        break;
                    case 0x2F:

                        paramsCount = 5;
                        break;
                    case 0x30:

                        paramsCount = 2;
                        //do shit here
                        break;
                    case 0x31:

                        paramsCount = 1;
                        break;
                    case 0x34:

                        paramsCount = 1;
                        break;
                    case 0x35:

                        paramsCount = 7;
                        break;
                    case 0x36:

                        paramsCount = 8;
                        break;
                    case 0x37:

                        paramsCount = 4;
                        break;
                    case 0x38:

                        paramsCount = 4;
                        break;
                    case 0x39:

                        paramsCount = 3;
                        break;
                    case 0x3A:

                        paramsCount = 3;
                        break;
                    case 0x3B:

                        paramsCount = 3;
                        break;
                    case 0x3C:

                        paramsCount = 4;
                        break;
                    case 0x3D:

                        paramsCount = 4;
                        break;
                    case 0x3E:

                        paramsCount = 1;
                        break;
                    case 0x3F: //DrawScaledSprite

                        paramsCount = 1;
                        break;
                    case 0x40:

                        paramsCount = 6;
                        break;
                    case 0x41:

                        paramsCount = 2;
                        break;
                    case 0x42:

                        paramsCount = 1;
                        break;
                    default:
                        break;
                }

                if (opcode != 17)
                {
                    int opcde = opcode;

                    for (int i = 0; i < paramsCount; i++)
                    {
                        variableName[i] = GetVariable(scriptSub, opcde);
                        //opcde = GetScriptData(scriptSub);
                    }
                }
                else
                {
                    variableName[0] = GetScriptData(scriptSub).ToString();
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
                    case 0x00: newline = false; break;
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


                if (scriptEndPtr >= Endpoint_Startup)
                {
                    state.EndFlag = true;
                }

                writer.Write(Environment.NewLine);

            }
            if (newline)
                writer.Write(Environment.NewLine);
            else
                newline = true;
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
                data = scriptData_Startup[scriptCodePtr++];
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
                    return "Object.Xpos";
                case 0x4:
                    return "Object.Ypos";
                case 0x5:
                    return "Object.ValueA";
                case 0x6:
                    return "Object.ValueB";
                case 0x7:
                    return "Object.ValueC";
                case 0x8:
                    return "Object.ValueD";
                case 0x9:
                    return "Object.ValueE";
                case 0xA:
                    return "Object.ValueF";
                case 0xB:
                    return "TempObject.Type";
                case 0xC:
                    return "TempObject.SubType";
                case 0xD:
                    return "TempObject.Xpos";
                case 0xE:
                    return "TempObject.Ypos";
                case 0xF:
                    return "TempObject.ValueA";
                case 0x10:
                    return "TempObject.ValueB";
                case 0x11:
                    return "TempObject.ValueC";
                case 0x12:
                    return "TempObject.ValueD";
                case 0x13:
                    return "TempObject.ValueE";
                case 0x14:
                    return "TempObject.ValueF";
                case 0x15:
                    return "UnknownValue";
                case 0x17:
                    return "UnknownValue";
                case 0x19:
                    return "Player.XPos";
                case 0x1A:
                    return "Player.YPos";
                case 0x1E:
                    return "UnknownValue";
                case 0x20:
                    return "UnknownValue";
                case 0x21:
                    return "UnknownValue";
                case 0x22:
                    return "UnknownValue";
                case 0x24:
                    return "UnknownValue";
                case 0x25:
                    return "UnknownValue";
                case 0x26:
                    return "UnknownValue";
                case 0x27:
                    return "UnknownValue";
                case 0x28:
                    return "TempObjectPos";
                case 0x29:
                    return "UnknownValue";
                case 0x2A:
                    return "MusicSFXCount";
                case 0x2B:
                    return "UnknownValue";
                case 0x2C:
                    return "CheckResult";
                case 0x2D:
                    return "UnknownValue";
                case 0x2E:
                    return "ObjectPos";
                case 0x2F:
                    return "UnknownValue";
                case 0x30:
                    return "UnknownValue";
                case 0x31:
                    return "UnknownValue";
                case 0x32:
                    return "UnknownValue";
                case 0x33:
                    return "UnknownValue";
                case 0x34:
                    return "UnknownValue";
                case 0x35:
                    return "UnknownValue";
                case 0x36:
                    return "UnknownValue";
                case 0x37:
                    return "UnknownValue";
                case 0x38:
                    return "UnknownValue";
                case 0x39:
                    return "UnknownValue";
                case 0x3A:
                    return "Player.FullEngineRotation";
                case 0x3B:
                    return "UnknownValue";
                case 0x3C:
                    return "UnknownValue";
                case 0x3D:
                    return "UnknownValue";
                case 0x3E:
                    return "UnknownValue";
                case 0x3F:
                    return "UnknownValue";
                case 0x40:
                    return "UnknownValue";
                case 0x41:
                    return "UnknownValue";
                case 0x42:
                    return "Player.ID";
                case 0x43:
                    return "Object.Priority";
                case 0x44:
                    return "UnknownValue";
                default:
                    return GetScriptData(SubID).ToString();
            }
        }

        public int EditorDecompile()
        {
            state = new StateScriptEngine();
            scriptCodePtr = 0;
            state.EndFlag = false;
            state.LoopBreakFlag = false;
            state.SwitchBreakFlag = false;

            while (!state.EndFlag)
            {
                scriptEndPtr++;

                opcode = GetScriptData(3);

                int paramsCount = 0;

                string[] variableName = new string[10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNKNOWN VARIABLE";
                }

                switch (opcode)
                {
                    case 0:

                        paramsCount = 0;
                        break;
                    case 1:

                        paramsCount = 2;
                        break;
                    case 2:

                        paramsCount = 2;
                        break;
                    case 3:

                        paramsCount = 2;
                        break;
                    case 4:

                        paramsCount = 2;
                        break;
                    case 5:

                        paramsCount = 2;
                        break;
                    case 6:

                        paramsCount = 2;
                        break;
                    case 7:

                        paramsCount = 2;
                        break;
                    case 8:

                        paramsCount = 2;
                        break;
                    case 9:

                        paramsCount = 2;
                        break;
                    case 0xB:

                        paramsCount = 1;
                        scriptCodePtr++;
                        break;
                    case 0xC:

                        paramsCount = 2;
                        break;
                    case 0xF:

                        paramsCount = 3;
                        break;
                    case 0x10:

                        paramsCount = 3;
                        break;
                    case 0x11: //SetEditorIcon

                        paramsCount = 1;
                        break;
                    case 0x12:

                        paramsCount = 1;
                        break;
                    case 0x13:

                        paramsCount = 4;
                        break;
                    case 0x14:

                        paramsCount = 4;
                        break;
                    case 0x15:

                        paramsCount = 4;
                        break;
                    case 0x16:

                        paramsCount = 4;
                        break;
                    case 0x17:

                        paramsCount = 4;
                        break;
                    case 0x18:

                        paramsCount = 4;
                        break;
                    case 0x19:

                        paramsCount = 2; //CheckEqual
                        break;
                    case 0x1A:

                        paramsCount = 2; //CheckGreater
                        break;
                    case 0x1B:

                        paramsCount = 2; //CheckLower
                        break;
                    case 0x1C:

                        paramsCount = 2; //CheckNotEqual
                        break;
                    case 0x1D:

                        paramsCount = 2;
                        break;
                    case 0x1E:

                        paramsCount = 2;
                        break;
                    case 0x1F:

                        paramsCount = 4;
                        break;
                    case 0x20:

                        paramsCount = 4;
                        break;
                    case 0x21:

                        paramsCount = 4;
                        break;
                    case 0x22:

                        paramsCount = 4;
                        break;
                    case 0x23:

                        paramsCount = 1;
                        break;
                    case 0x24:

                        paramsCount = 1;
                        break;
                    case 0x25:

                        paramsCount = 3;
                        break;
                    case 0x26:

                        paramsCount = 3;
                        break;
                    case 0x27:

                        paramsCount = 2;
                        break;
                    case 0x28:

                        paramsCount = 1;
                        break;
                    case 0x29:

                        paramsCount = 6; //
                        break;
                    case 0x2A:

                        paramsCount = 1;
                        break;
                    case 0x2B:

                        paramsCount = 4;
                        break;
                    case 0x2C:

                        paramsCount = 1;
                        break;
                    case 0x2D:

                        paramsCount = 1;
                        break;
                    case 0x2E:

                        paramsCount = 4;
                        break;
                    case 0x2F:

                        paramsCount = 5;
                        break;
                    case 0x30:

                        paramsCount = 2;
                        //do shit here
                        break;
                    case 0x31:

                        paramsCount = 1;
                        break;
                    case 0x34:

                        paramsCount = 1;
                        break;
                    case 0x35:

                        paramsCount = 7;
                        break;
                    case 0x36:

                        paramsCount = 8;
                        break;
                    case 0x37:

                        paramsCount = 4;
                        break;
                    case 0x38:

                        paramsCount = 4;
                        break;
                    case 0x39:

                        paramsCount = 3;
                        break;
                    case 0x3A:

                        paramsCount = 3;
                        break;
                    case 0x3B:

                        paramsCount = 3;
                        break;
                    case 0x3C:

                        paramsCount = 4;
                        break;
                    case 0x3D:

                        paramsCount = 4;
                        break;
                    case 0x3E:

                        paramsCount = 1;
                        break;
                    case 0x3F: //DrawScaledSprite

                        paramsCount = 1;
                        break;
                    case 0x40:

                        paramsCount = 6;
                        break;
                    case 0x41:

                        paramsCount = 2;
                        break;
                    case 0x42:

                        paramsCount = 1;
                        break;
                    default:
                        break;
                }

                if (opcode != 17)
                {
                    int opcde = opcode;

                    for (int i = 0; i < paramsCount; i++)
                    {
                        variableName[i] = GetVariable(3, opcde);
                        //opcde = GetScriptData(scriptSub);
                    }
                }
                else
                {
                    variableName[0] = GetScriptData(3).ToString();
                }

                string operand = opcodeList[opcode];

                if (opcode == 17)
                {
                    return Int32.Parse(variableName[0]);
                }
            }

            return 0;
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
