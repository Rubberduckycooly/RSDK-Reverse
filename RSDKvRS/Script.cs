using System;
using System.Collections.Generic;
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
            public int XPos;
            public int YPos;
        }

        string[] VARIABLE_NAME = new string[]
{
"NullValue",
"Object.Type",
"Object.SubType",
"Object.XPos",
"Object.YPos",
"Object.ValueA",
"Object.ValueB",
"Object.ValueC",
"Object.ValueD",
"Object.ValueE",
"Object.ValueF",
"TempObject.Type",
"TempObject.SubType",
"TempObject.XPos",
"TempObject.YPos",
"TempObject.ValueA",
"TempObject.ValueB",
"TempObject.ValueC",
"TempObject.ValueD",
"TempObject.ValueE",
"TempObject.ValueF",
"Player.XPos",
"NullValue2",
"Player.YPos",
"NullValue3",
"Player.XVelocity",
"Player.YVelocity",
"NullValue4",
"NullValue5",
"NullValue6",
"NullValue7",
"Value.Unknown1",
"Value.Unknown2",
"Value.Unknown3",
"Rings",
"NullValue8",
"Value.Unknown4",
"Stage.Width",
"Value.Unknown21",
"Stage.Height",
"TempObjectPos",
"MainGameMode",
"TempValA",
"TempValB",
"CheckResult",
"Value.Unknown22",
"Object.Priority",
"Value.Unknown5",
"Value.Unknown6",
"Value.Unknown7",
"Value.Unknown8",
"Value.Unknown9",
"Value.Unknown10",
"Value.Unknown11",
"Value.Unknown12",
"Value.Unknown13",
"Value.Unknown14",
"Stage.State2",
"FERActivated",
"Player.CollisionLeft",
"Player.CollisionTop",
"Player.CollisionRight",
"Player.CollisionBottom",
"Value.Unknown15",
"Value.Unknown16",
"Value.Unknown17",
"Player.Moveset",
"Object.Pos",
"Value.Unknown20",


/*"Player.Movement",
"Player.MovementMomentumn",
"XBoundary1",
"XBoundary2",
"YBoundary1",
"YBoundary2",
"AngleTimer",
"Score",
"DeformationPosF1",
"DeformationPosF2",
"DeformationPosB1",
"DeformationPosB2",*/
};

        string[] opcodeList = new string[]
{
//NULL
    "NULL",//0x00
    "Equal", //0x01
    "Add", //0x02
    "Sub", //0x03
    "Inc", //0x04
    "Dec", //0x05
    "Mul", //0x06
    "Div", //0x07
    "ShR", //0x08
    "ShL", //0x09
    "Null0x0A", //0x0A
    "Goto",  //0x0B
    "Rand", //0x0C
    "EditorFunc", //0x0D
    "SetEditorIcon", //0x0E
    "ObjectFloorCollision", //0x0F
    "ObjectRoofCollision", //0x10
    "SpriteFrame", //0x11
    "DrawSprite", //0x12
    "CreateObject", //0x13
    "CheckPlayerCollisionTouch", //0x14
    "CheckPlayerCollisionBox", //0x15
    "CheckPlayerCollisionPlatform", //0x16
    "CheckPlayerCollisionTouchUnknown", //0x17
    "CheckPlayerCollisionTouchUnknown2", //0x18
    "CheckEqual", //0x19
    "CheckGreater", //0x1A
    "CheckLower", //0x1B
    "CheckNotEqual", //0x1C
    "Sin512", //0x1D
    "Cos512", //0x1E
    "Unknown26", //0x1F
    "Unknown27", //0x20
    "Unknown28", //0x21
    "Unknown29", //0x22
    "Unknown30", //0x23
    "Unknown31", //0x24
    "Unknown32", //0x25
    "RotatePalette", //0x26
    "PlaySFX", //0x27
    "StopSFX", //0x28
    "Unknown36", //0x29
    "Unknown37", //0x2A
    "Unknown38", //0x2B
    "Unknown39", //0x2C
    "SetMusicTrack", //0x2D
    "Unknown41", //0x2E
    "SetLayerValues", //0x2F
    "Unknown43", //0x30
    "Unknown44", //0x31
    "Null0x32", //0x32
    "Null0x33", //0x33
    "Unknown45", //0x34
    "Unknown46", //0x35
    "DrawRect", //0x36
    "Unknown48", //0x37
    "Unknown49", //0x38
    "Unknown50", //0x39
    "SetObjectValue", //0x3A
    "GetObjectValue", //0x3B
    "Unknown60", //0x3C
    "Unknown61", //0x3D
    "Unknown62", //0x3E
    "DrawScaledSprite", //0x3F
    "Unknown64", //0x40
    "PlayStageSFX", //0x41
    "StopStageSFX", //0x42
};

        /// <summary>
        /// how many paramaters each Function has
        /// </summary>
        readonly byte[] scriptOpcodeSizes = new byte[]
{
1, //0x00
2, //0x01
2, //0x02
2, //0x03
1, //0x04
1, //0x05
2, //0x06
2, //0x07
2, //0x08
2, //0x09
1, //0x0A
1, //0x0B
2, //0x0C
0, //0x0D
6, //0x0E - SetEditorIcon
3, //0x0F
3, //0x10
0, //0x11 - SpriteFrame
1, //0x12
4, //0x13
4, //0x14
4, //0x15
4, //0x16
4, //0x17
4, //0x18
2, //0x19
2, //0x1A
2, //0x1B
2, //0x1C
2, //0x1D
2, //0x1E
4, //0x1F
4, //0x20
4, //0x21
4, //0x22
1, //0x23
1, //0x24
3, //0x25
3, //0x26
2, //0x27
1, //0x28
6, //0x29
1, //0x2A
4, //0x2B
1, //0x2C
1, //0x2D
4, //0x2E
5, //0x2F
2, //0x30
1, //0x31
0, //0x32
0, //0x33
1, //0x34
7, //0x35
8, //0x36
4, //0x37
4, //0x38
3, //0x39
3, //0x3A
3, //0x3B
4, //0x3C
4, //0x3D
1, //0x3E
1, //0x3F
6, //0x40
2, //0x41
1, //0x42
};

        public class Sub
        {
            public int scriptDataPos;
            public int jumpTableDataPos;

            /// <summary>
            /// the scriptdata for the Sub
            /// </summary>
            public int[] scriptData = new int[0x10000];
            /// <summary>
            /// the endpoint for the Sub
            /// </summary>
            public int Endpoint = 0;
            /// <summary>
            /// the Jumptabledata for the Main Sub
            /// </summary>
            public int[] jumpTableData = new int[0x10000];
            /// <summary>
            /// Main Labels
            /// </summary>
            public int[] Labels = new int[0x10000];
        }

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
        /// Sub data
        /// </summary>
        public Sub[] Subs = new Sub[5];
        /// <summary>
        /// a list of the SpriteFrames
        /// </summary>
        public List<SpriteFrame> spriteFrames = new List<SpriteFrame>();
        public List<SpriteFrame> EditorFrames = new List<SpriteFrame>();

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

        public Script(string filename) : this(new Reader(filename))
        { }

        public Script(Reader reader, bool EditorMode = false)
        {
            scriptEng.operands = new int[10];
            scriptEng.tempValue = new int[8];
            scriptEng.arrayPosition = new int[3];

            for (int CurrentSub = 0; CurrentSub < SubCount; ++CurrentSub)
            {
                int Opcode = 0;
                int OpcodeSize = 0;

                scriptDataPos = 0;
                spriteframePtr = 0;

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

                Subs[CurrentSub] = new Sub();
                Subs[CurrentSub].Endpoint = Value1;
                Subs[CurrentSub].scriptData = new int[0xFFFu];

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

                    Opcode = FileBuffer;

                    if (k < 1)
                    {
                        Subs[CurrentSub].scriptData[scriptDataPos++] = FileBuffer;

                        switch (Opcode)
                        {
                            case 0:
                            case 4:
                            case 5:
                            case 0xA:
                            case 0xB:
                            case 0xD:
                            case 0x23:
                            case 0x24:
                            case 0x28:
                            case 0x2A:
                            case 0x2C:
                            case 0x2D:
                            case 0x31:
                            case 0x34:
                            case 0x3E:
                            case 0x3F:
                            case 0x42:
                                OpcodeSize = 1;
                                break;
                            case 1:
                            case 2:
                            case 3:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 0xC:
                            case 0x30:
                            case 0x41:
                                OpcodeSize = 2;
                                break;
                            case 0xE:
                                OpcodeSize = 6;
                                break;
                            case 0xF:
                            case 0x10:
                            case 0x39:
                            case 0x3A:
                            case 0x3B:
                                OpcodeSize = 3;
                                break;
                            case 0x11:
                                OpcodeSize = 6;
                                break;
                            case 0x12:
                                OpcodeSize = 1;
                                break;
                            case 0x13:
                            case 0x14:
                            case 0x15:
                            case 0x16:
                            case 0x17:
                            case 0x18:
                            case 0x2E:
                            case 0x37:
                            case 0x38:
                            case 0x3C:
                            case 0x3D:
                                OpcodeSize = 4;
                                break;
                            case 0x19:
                                OpcodeSize = 2;
                                break;
                            case 0x1A:
                                OpcodeSize = 2;
                                break;
                            case 0x1B:
                                OpcodeSize = 2;
                                break;
                            case 0x1C:
                                OpcodeSize = 2;
                                break;
                            case 0x1D:
                                OpcodeSize = 2;
                                break;
                            case 0x1E:
                                OpcodeSize = 2;
                                break;
                            case 0x1F:
                                OpcodeSize = 4;
                                break;
                            case 0x20:
                                OpcodeSize = 4;
                                break;
                            case 0x21:
                                OpcodeSize = 4;
                                break;
                            case 0x22:
                                OpcodeSize = 4;
                                break;
                            case 0x25:
                                OpcodeSize = 3;
                                break;
                            case 0x26:
                                OpcodeSize = 3;
                                break;
                            case 0x27:
                                OpcodeSize = 2;
                                break;
                            case 0x29:
                            case 0x40:
                                OpcodeSize = 6;
                                break;
                            case 0x2B:
                                OpcodeSize = 4;
                                break;
                            case 0x2F:
                                OpcodeSize = 5;
                                break;
                            case 0x35:
                                OpcodeSize = 7;
                                break;
                            case 0x36:
                                OpcodeSize = 8;
                                break;
                            default:
                                break;
                        }

                        //Error?
                        for (int i = 0; i < OpcodeSize; i++)
                        {
                            FileBuffer = reader.ReadByte();

                            Subs[CurrentSub].scriptData[scriptDataPos++] = FileBuffer;

                            if (FileBuffer != 0)
                            {
                                FileBuffer = reader.ReadByte();
                                Subs[CurrentSub].scriptData[scriptDataPos++] = FileBuffer;

                                Value1 = reader.ReadByte();

                                if (Value1 > 128) { Value1 = 128 - Value1; }

                                Subs[CurrentSub].scriptData[scriptDataPos++] = Value1;
                            }
                            else
                            {
                                FileBuffer = reader.ReadByte();
                                if (FileBuffer < 128)
                                {
                                    Value2 = FileBuffer << 8;
                                    Value2 |= reader.ReadByte();
                                }
                                else
                                {
                                    FileBuffer += -128;
                                    Value2 = FileBuffer << 8;
                                    Value2 |= reader.ReadByte();
                                    Value2 = -Value2;
                                }

                                switch (CurrentSub)
                                {
                                    default:
                                        Subs[CurrentSub].scriptData[scriptDataPos++] = Value2;
                                        break;
                                    case 3:
                                        if (Opcode == 0x11) //SetEditorIcon?
                                        {
                                            if (spriteFrames.Count <= spriteframePtr)
                                            {
                                                spriteFrames.Add(new SpriteFrame());
                                            }
                                            switch (i)
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
                                                    spriteFrames[spriteframePtr].XPos = Value2;
                                                    break;
                                                case 5:
                                                    spriteFrames[spriteframePtr++].YPos = Value2;
                                                    break;
                                                default:
                                                    break;
                                            }
                                        }
                                        break;
                                    case 4:
                                        if (Opcode == 0x0E) //SetEditorIcon?
                                        {
                                            if (EditorFrames.Count <= spriteframePtr)
                                            {
                                                EditorFrames.Add(new SpriteFrame());
                                            }
                                            switch (i)
                                            {
                                                case 0:
                                                    EditorFrames[spriteframePtr].Width = Value2;
                                                    break;
                                                case 1:
                                                    EditorFrames[spriteframePtr].Height = Value2;
                                                    break;
                                                case 2:
                                                    EditorFrames[spriteframePtr].PivotX = Value2;
                                                    break;
                                                case 3:
                                                    EditorFrames[spriteframePtr].PivotY = Value2;
                                                    break;
                                                case 4:
                                                    EditorFrames[spriteframePtr].XPos = Value2;
                                                    break;
                                                case 5:
                                                    EditorFrames[spriteframePtr++].YPos = Value2;
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

                OpcodeSize = FileBuffer;
                for (int j = 0; j < OpcodeSize; j++)
                {
                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();
                }

                FileBuffer = reader.ReadByte();
                FileBuffer = reader.ReadByte();

                OpcodeSize = FileBuffer;

                for (int j = 0; j < OpcodeSize; j++)
                {
                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    Value4 = reader.ReadByte() << 8;
                    Value4 |= reader.ReadByte();

                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();

                    Subs[CurrentSub].Labels[scriptDataPos++] = Value1;
                }
                k = 0;

                FileBuffer = reader.ReadByte();
                FileBuffer = reader.ReadByte();
                OpcodeSize = FileBuffer;

                Value8 = 0;

                for (int j = 0; j < 512; ++j) //Clear out space
                {
                    Subs[CurrentSub].jumpTableData[j] = 0;
                }

                jumpTableDataPos = 0;
                for (int j = 0; j < OpcodeSize; j++)
                {
                    Value0 = 0x8000;
                    Value9 = 0;

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    Value3 = reader.ReadByte() << 8;
                    Value4 = reader.ReadByte() + Value3;

                    Value7 = Value8;

                    Subs[CurrentSub].jumpTableData[jumpTableDataPos++] = 0;
                    Subs[CurrentSub].jumpTableData[jumpTableDataPos++] = 0;

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    Subs[CurrentSub].jumpTableData[jumpTableDataPos] = Value1;
                    //SetJumptableData(CurrentSub, Value1);

                    FileBuffer = reader.ReadByte();
                    FileBuffer = reader.ReadByte();

                    Value1 = reader.ReadByte() << 8;
                    Value1 |= reader.ReadByte();

                    if (Subs[CurrentSub].jumpTableData[jumpTableDataPos] == 0)
                    {
                        Subs[CurrentSub].jumpTableData[jumpTableDataPos++] = Value1;
                    }

                    Subs[CurrentSub].jumpTableData[jumpTableDataPos++] = Value1;

                    for (int v = 0; v < Value4; ++v)
                    {
                        /*FileBuffer = reader.ReadByte();
                        Value1 = FileBuffer << 8;
                        FileBuffer = reader.ReadByte();
                        Value1 += FileBuffer;*/

                        Value1 = reader.ReadByte() << 8;
                        Value1 |= reader.ReadByte();

                        FileBuffer = reader.ReadByte();
                        Value6 = FileBuffer << 8;
                        FileBuffer = reader.ReadByte();
                        Value6 += FileBuffer;

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

                        Subs[CurrentSub].jumpTableData[Value6 + jumpTableDataPos] = Value1;
                    }


                    Subs[CurrentSub].jumpTableData[jumpTableDataPos++] = Value0;

                    for (int m = Value0 - 1; m < Value9; m++)
                    {

                        Subs[CurrentSub].jumpTableData[jumpTableDataPos] = Subs[CurrentSub].jumpTableData[jumpTableDataPos + Value0];
                        if (Subs[CurrentSub].jumpTableData[jumpTableDataPos] != 0)
                        {
                            //jumpTableData_PlayerInteraction[jumpTableDataPos] = dword_AFEA9C[16129 * ScriptID + Value7];
                        }
                        jumpTableDataPos++;

                        /*if (CurrentSub != 0)
                        {
                            if (CurrentSub == 1)
                            {
                                jumpTableData_PlayerInteraction[jumpTableDataPos] = jumpTableData_PlayerInteraction[jumpTableDataPos + Value0];
                                if (jumpTableData_PlayerInteraction[jumpTableDataPos] != 0)
                                {
                                    //jumpTableData_PlayerInteraction[jumpTableDataPos] = dword_AFEA9C[16129 * ScriptID + Value7];
                                }
                            }
                            else if (CurrentSub == 2)
                            {
                                jumpTableData_Draw[jumpTableDataPos] = jumpTableData_Draw[jumpTableDataPos + Value0];
                                if (jumpTableData_Draw[jumpTableDataPos] != 0)
                                {
                                    //jumpTableData_Draw[jumpTableDataPos] = dword_AFF29C[16129 * ScriptID + Value7];
                                }
                            }
                            else if (CurrentSub == 3)
                            {
                                jumpTableData_Startup[jumpTableDataPos] = jumpTableData_Startup[jumpTableDataPos + Value0];
                                if (jumpTableData_Startup[jumpTableDataPos] != 0)
                                {
                                    //jumpTableData_Draw[jumpTableDataPos] = dword_AFF29C[16129 * ScriptID + Value7];
                                }
                            }
                            else if (CurrentSub == 4)
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
                            jumpTableData_Main[jumpTableDataPos] = jumpTableData_Main[jumpTableDataPos + Value0];
                            if (jumpTableData_Main[jumpTableDataPos] != 0)
                            {
                                //jumpTableData_Main[jumpTableDataPos] = dword_AFE29C[16129 * ScriptID + Value7];
                            }
                        }*/
                        
                    }

                    /*if (CurrentSub != 0)
                    {
                        if (CurrentSub == 1)
                        {
                            if (dword_AF4A98[jumpTableDataPos] <= 0)
                            {
                                dword_AF4AA4[jumpTableDataPos] = Value7;
                            }
                            else
                                dword_AF4AA8[jumpTableDataPos] = Value7;
                        }
                        else if (CurrentSub == 2)
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
                        else if (CurrentSub == 3)
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


            Console.Write("Main script, ");
            writer.WriteLine("//---------------------------Main Sub---------------------------//");
            writer.WriteLine("//-------Called once a frame, use this for most functions-------//");
            DecompileScript(writer, 0);

            Console.Write("PlayerInteraction script, ");
            writer.WriteLine("//---------------------------PlayerInteraction Sub---------------------------//");
            writer.WriteLine("//-------Called once a frame, use this for interacting with the player-------//");
            DecompileScript(writer, 1);

            writer.WriteLine("//----------------------Drawing Sub-------------------//");
            writer.WriteLine("//-------Called once a frame after the Main Sub-------//");
            Console.Write("Draw script, ");
            DecompileScript(writer, 2);

            writer.WriteLine("//--------------------Startup Sub---------------------//");
            writer.WriteLine("//-------Called once when the object is spawned-------//");
            Console.Write("Startup script, ");
            DecompileScript(writer, 3);

            writer.WriteLine("//--------------------RSDK Sub---------------------//");
            writer.WriteLine("//-------Used for editor functionality-------//");
            Console.WriteLine("RSDK script.");
            DecompileScript(writer, 4);

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
                    strFuncName = "PlayerInteraction";
                    break;
                case 2:
                    strFuncName = "Draw";
                    break;
                case 3:
                    strFuncName = "Startup";
                    break;
                case 4:
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
            spriteframePtr = 0;

            DecompileSub(writer, scriptSub);
            writer.Write(Environment.NewLine);
        }

        public void DecompileSub(StreamWriter writer, int scriptSub)
        {
            scriptCodePtr = 0;
            scriptEndPtr = 0;
            state.EndFlag = false;
            state.LoopBreakFlag = false;
            state.SwitchBreakFlag = false;
            writer.Write(Environment.NewLine);

            bool newline = true;

            while (!state.EndFlag)
            {
                scriptEndPtr++;
                //scriptCodePtr++;
                
                opcode = Subs[scriptSub].scriptData[scriptCodePtr];
                scriptCodePtr++;
                int paramsCount = scriptOpcodeSizes[opcode];

                string[] variableName = new string[10];

                for (int i = 0; i < variableName.Length; i++)
                {
                    variableName[i] = "UNKNOWN VARIABLE";
                }

                for (int i = 0; i < paramsCount; i++)
                {
                    int paramId = Subs[scriptSub].scriptData[scriptCodePtr];
                    scriptCodePtr++;

                    switch (paramId)
                    {
                        case 0:
                            variableName[i] = Subs[scriptSub].scriptData[scriptCodePtr].ToString();
                            break;
                        case 1:
                        default:
                            variableName[i] = VARIABLE_NAME[Subs[scriptSub].scriptData[scriptCodePtr]];
                            scriptCodePtr++;
                            int ArrayVal = Subs[scriptSub].scriptData[scriptCodePtr];
                            break;
                    }
                    scriptCodePtr++;
                }
                //scriptCodePtr++;

                //Check what opcodes terminates a statement
                switch (opcode)
                {
                    case 0x00: // end sub
                        break;
                    /*case 0x19: // else
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
                        break;*/
                    default:
                        for (int i = 0; i < state.deep; i++) writer.Write("\t");
                        break;
                }

                if (opcode >= opcodeList.Length)
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
                        variableName[i] = "Object.ValueA";
                    }
                }

                string operand = opcodeList[opcode];

                switch (opcode)
                {
                    case 0x00: newline = false; break;
                    case 0x01: writer.Write(variableName[0] + "=" + variableName[1] + ";"); break;
                    case 0x02: writer.Write(variableName[0] + "+=" + variableName[1] + ";"); break;
                    case 0x03: writer.Write(variableName[0] + "-=" + variableName[1] + ";"); break;
                    case 0x04: writer.Write(variableName[0] + "++;"); break;
                    case 0x05: writer.Write(variableName[0] + "--;"); break;
                    case 0x06: writer.Write(variableName[0] + "*=" + variableName[1] + ";"); break;
                    case 0x07: writer.Write(variableName[0] + "/=" + variableName[1] + ";"); break;
                    case 0x08: writer.Write(variableName[0] + ">>=" + variableName[1] + ";"); break;
                    case 0x09: writer.Write(variableName[0] + "<<=" + variableName[1] + ";"); break;
                    case 0x0B: writer.Write("goto " + variableName[0] + ":"); break;
                    case 0x0D: //lol idk
                    case 0x0E:
                    case 0x11:
                        if (scriptSub == 4)
                        {
                            writer.Write("SetEditorIcon(" + EditorFrames[spriteframePtr].PivotX + "," + EditorFrames[spriteframePtr].PivotY + "," + EditorFrames[spriteframePtr].Width + "," + EditorFrames[spriteframePtr].Height + "," + EditorFrames[spriteframePtr].XPos + "," + EditorFrames[spriteframePtr].YPos + ");");
                        }
                        else
                        {
                            writer.Write("SpriteFrame(" + spriteFrames[spriteframePtr].PivotX + "," + spriteFrames[spriteframePtr].PivotY + "," + spriteFrames[spriteframePtr].Width + "," + spriteFrames[spriteframePtr].Height + "," + spriteFrames[spriteframePtr].XPos + "," + spriteFrames[spriteframePtr].YPos + ");");
                        }
                        spriteframePtr++;
                        break;
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
                        writer.Write(");");
                        break;
                }

                if (opcode != 0)
                    writer.Write(Environment.NewLine);

                if (scriptEndPtr >= Subs[scriptSub].Endpoint)
                {
                    state.EndFlag = true;
                    writer.WriteLine("endsub");
                }

            }

            if (newline && opcode != 0)
            {
                writer.Write(Environment.NewLine);
            }
            else
            {
                newline = true;
            }
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
