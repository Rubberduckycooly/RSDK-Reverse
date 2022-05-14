using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RSDKv1
{
    public class Script
    {
        public class FunctionInfo
        {
            public string name = "Function";
            public int paramCount = 0;

            public FunctionInfo() { }
            public FunctionInfo(string name, int paramCount)
            {
                this.name = name;
                this.paramCount = paramCount;
            }
        }

        static string[] variableList = new string[]
        {
            "[NULL]",                   // 0x00
            "Object.Type",              // 0x01
            "Object.PropertyValue",     // 0x02
            "Object.XPos",              // 0x03
            "Object.YPos",              // 0x04
            "Object.ValueA",            // 0x05
            "Object.ValueB",            // 0x06
            "Object.ValueC",            // 0x07
            "Object.ValueD",            // 0x08
            "Object.ValueE",            // 0x09
            "Object.ValueF",            // 0x0A
            "TempObject.Type",          // 0x0B
            "TempObject.PropertyValue", // 0x0C
            "TempObject.XPos",          // 0x0D
            "TempObject.YPos",          // 0x0E
            "TempObject.ValueA",        // 0x0F
            "TempObject.ValueB",        // 0x10
            "TempObject.ValueC",        // 0x11
            "TempObject.ValueD",        // 0x12
            "TempObject.ValueE",        // 0x13
            "TempObject.ValueF",        // 0x14
            "Player.Direction",         // 0x15
            "Blank16",                  // 0x16
            "Player.State",             // 0x17
            "Blank18",                  // 0x18
            "Player.XPos",              // 0x19 //Sets "TempValC"
            "Player.YPos",              // 0x1A //Sets "TempValC"
            "Blank1B",                  // 0x1B
            "Blank1C",                  // 0x1C
            "Blank1D",                  // 0x1D
            "Player.Down",              // 0x1E
            "Blank1F",                  // 0x1F
            "Player.Animation",         // 0x20
            "Player.Frame",             // 0x21
            "Player.Rings",             // 0x22
            "Blank23",                  // 0x23
            "XBoundary1",               // 0x24 //Sets "newXBoundary1"
            "XBoundary2",               // 0x25 //Sets "newXBoundary2"
            "YBoundary1",               // 0x26 //Sets "newYBoundary1"
            "YBoundary2",               // 0x27 //Sets "newYBoundary2"
            "TempObjectPos",            // 0x28
            "MainGameMode",             // 0x29
            "TempValA",                 // 0x2A
            "TempValB",                 // 0x2B
            "CheckResult",              // 0x2C
            "AngleTimer",               // 0x2D
            "Object.Priority",          // 0x2E
            "DeformationPosB1",         // 0x2F
            "DeformationPosB2",         // 0x30
            "DeformationPosF1",         // 0x31
            "DeformationPosF2",         // 0x32
            "WaterLevel",               // 0x33
            "BGType",                   // 0x34
            "BGCustomXPos",             // 0x35
            "BGCustomYPos",             // 0x36
            "RingAni",                  // 0x37
            "Stage.State2",             // 0x38
            "MainGameMode2",            // 0x39
            "PlayerListSize",           // 0x3A
            "Player.CollisionLeft",     // 0x3B
            "Player.CollisionTop",      // 0x3C
            "Player.CollisionRight",    // 0x3D
            "Player.CollisionBottom",   // 0x3E
            "Player.Unknown3F",         // 0x3F
            "Player.XVelocity",         // 0x40
            "Player.YVelocity",         // 0x41
            "Player.MovesetType",       // 0x42
            "Object.EntityNo",          // 0x43
            "EarthquakeY",              // 0x44
        };

        static FunctionInfo[] opcodeList = new FunctionInfo[]
        {
            new FunctionInfo("EndSub",              1), // 0x00
            new FunctionInfo("Equal",               2), // 0x01 (LHS, RHS)
            new FunctionInfo("Add",                 2), // 0x02 (LHS, RHS)
            new FunctionInfo("Sub",                 2), // 0x03 (LHS, RHS)
            new FunctionInfo("Inc",                 1), // 0x04 (LHS, RHS)
            new FunctionInfo("Dec",                 1), // 0x05 (LHS, RHS)
            new FunctionInfo("Mul",                 2), // 0x06 (LHS, RHS)
            new FunctionInfo("Div",                 2), // 0x07 (LHS, RHS)
            new FunctionInfo("ShR",                 2), // 0x08 (LHS, RHS)
            new FunctionInfo("ShL",                 2), // 0x09 (LHS, RHS)
            new FunctionInfo("[Null_0xA]",          3), // 0x0A // possibly label?
            new FunctionInfo("goto",                1), // 0x0B (LabelID)
            new FunctionInfo("Rand",                2), // 0x0C (store, max)
            new FunctionInfo("SetEditorIcon",       1), // 0x0D (Frame)
            new FunctionInfo("SetEditorFrame",      6), // 0x0E (Width, Height, PivotX, PivotY, SprX, SprY)
            new FunctionInfo("ObjectFloorCollision",3), // 0x0F (objID, XOffset, YOffset)
            new FunctionInfo("ObjectFloorGrip",     3), // 0x10 (objID, XOffset, YOffset)
            new FunctionInfo("SpriteFrame",         6), // 0x11 (Width, Height, PivotX, PivotY, SprX, SprY)
            new FunctionInfo("DrawSprite",          1), // 0x12 (Frame)
            new FunctionInfo("CreateTempObject",    4), // 0x13 (Type, PropertyVal, XPos, YPos)
            new FunctionInfo("TouchCollision",      4), // 0x14 (left, top, right, bottom)
            new FunctionInfo("BoxCollision",        4), // 0x15 (left, top, right, bottom)
            new FunctionInfo("PlatformCollision",   4), // 0x16 (left, top, right, bottom)
            new FunctionInfo("HurtCollision",       4), // 0x17 (left, top, right, bottom) (hits if vulnerable)
            new FunctionInfo("HurtCollisionForce",  4), // 0x18 (left, top, right, bottom) (always hits)
            new FunctionInfo("CheckEqual",          2), // 0x19 (A, B)
            new FunctionInfo("CheckGreater",        2), // 0x1A (A, B)
            new FunctionInfo("CheckLower",          2), // 0x1B (A, B)
            new FunctionInfo("CheckNotEqual",       2), // 0x1C (A, B)
            new FunctionInfo("Sin",                 2), // 0x1D (store, angle)
            new FunctionInfo("Cos",                 2), // 0x1E (store, angle)
            new FunctionInfo("SinX",                4), // 0x1F (store, origin, angle, shift)
            new FunctionInfo("CosX",                4), // 0x20 (store, origin, angle, shift)
            new FunctionInfo("SinY",                4), // 0x21 (store, origin, angle, shift)
            new FunctionInfo("CosY",                4), // 0x22 (store, origin, angle, shift)
            new FunctionInfo("MovePlayerX",         1), // 0x23 (Offset)
            new FunctionInfo("MovePlayerY",         1), // 0x24 (Offset)
            new FunctionInfo("CheckPlayerDiagonal", 3), // 0x25 (X, Y, FlipX)
            new FunctionInfo("RotatePalette",       3), // 0x26 (start, end, rotateRight)
            new FunctionInfo("PlaySfx",             2), // 0x27 (SFX, Loop)
            new FunctionInfo("StopSfx",             1), // 0x28 (SFX)
            new FunctionInfo("SetBGDeformation",    6), // 0x29 (deformID, waveLength, waveWidth, waveType, waveStart, waveEnd)
            new FunctionInfo("ClearBGDeformation",  1), // 0x2A (deformID)
            new FunctionInfo("HandleCorkscrewStart",4), // 0x2B (corkscrewX, corkscrewY, corkscrewWidth, corkscrewHeight)
            new FunctionInfo("HandleCorkscrewEnd",  1), // 0x2C (Unused)
            new FunctionInfo("PlayMusic",           1), // 0x2D (TrackID)
            new FunctionInfo("SetWaterColour",      4), // 0x2E (R, G, B, A)
            new FunctionInfo("SetBackground",       5), // 0x2F (activeLayer0, activeLayer1, activeLayer2, activeLayer3, layerMidPoint)
            new FunctionInfo("switch",              2), // 0x30 (switchVal, jmp)
            new FunctionInfo("break",               1), // 0x31 (jmp)
            new FunctionInfo("[Null_0x32]",         0), // 0x32 // possibly "case"
            new FunctionInfo("[Null_0x33]",         0), // 0x33 // possibly "default"
            new FunctionInfo("end switch",          1), // 0x34 ()
            new FunctionInfo("Interpolate",         7), // 0x35 (X1, Y1, X2, Y2, outX, outY, percent)
            new FunctionInfo("CopyGfx",             8), // 0x36 (width, height, sprX1, sprY1, sprY2, sprX2, sheet1, sheet2)
            new FunctionInfo("BossCollision",       4), // 0x37 (left, top, right, bottom)
            new FunctionInfo("BlockCollision",      4), // 0x38 (left, top, right, bottom)
            new FunctionInfo("HandleBonusMovement", 3), // 0x39 (Angle, outX, outY)
            new FunctionInfo("GetObjectValue",      3), // 0x3A (object, type, value)
            new FunctionInfo("SetObjectValue",      3), // 0x3B (object, type, store)
            new FunctionInfo("BumperCollision",     4), // 0x3C (left, top, right, bottom)
            new FunctionInfo("SetFade",             4), // 0x3D (R, G, B, A)
            new FunctionInfo("HandleMode7Movement", 1), // 0x3E (Unused)
            new FunctionInfo("DrawSpriteMode7",     1), // 0x3F (Frame)
            new FunctionInfo("Mode7Collision",      6), // 0x40 (A, B, C, D, E, F)
            new FunctionInfo("PlayStageSfx",        2), // 0x41 (SFX, loop)
            new FunctionInfo("StopStageSfx",        1), // 0x42 (SFX)
        };

        public class ParamInfo
        {
            public string name
            {
                get
                {
                    if (isVariable)
                    {
                        if (value >= 0 && value < variableList.Length)
                            return variableList[value];
                        else
                            return $"Unknown Variable {value}";
                    }
                    else
                        return $"Constant Value";
                }
            }

            /// <summary>
            /// the value of the parameter, a constant value if not a variable, else its the variableID
            /// </summary>
            public int value = 0;
            /// <summary>
            /// the variable array index. Only valid on variables, and only a select few
            /// </summary>
            public sbyte arrayIndex = 0;
            /// <summary>
            /// the flags that determines if the parameter is an integer constant or a variable
            /// </summary>
            public bool isVariable = false;

            public ParamInfo() { }
        }

        public class OpcodeInfo
        {
            /// <summary>
            /// the size (in bytes) this function will use as bytecode
            /// </summary>
            public int size
            {
                get
                {
                    int s = 1;
                    foreach (ParamInfo param in parameters)
                        s += param.isVariable ? 3 : 2;
                    return s;
                }
            }

            /// <summary>
            /// the function ID
            /// </summary>
            public byte opcode = 0xFF;
            /// <summary>
            /// the parameters to pass to this function
            /// </summary>
            public List<ParamInfo> parameters = new List<ParamInfo>();

            public OpcodeInfo() { }
        }

        public class LabelInfo
        {
            /// <summary>
            /// where to set the scriptCode pos to when this label is jumped to
            /// </summary>
            public int scriptCodePos = 0;
            /// <summary>
            /// the label number, E.G: LABEL 6: would have an id of 6 
            /// </summary>
            public int id = 0;
            /// <summary>
            /// the line this label is on, relative to the start of the sub (with empty lines removed)
            /// </summary>
            public int lineID = 0;

            public LabelInfo() { }
        }

        public class SwitchCaseInfo
        {
            /// <summary>
            /// where to set the scriptCode pos to when this case is jumped to
            /// </summary>
            public int scriptCodePos = 0;
            /// <summary>
            /// the case number, E.G: case 6: would have a caseNum of 6 
            /// </summary>
            public int caseNum = 0;
            /// <summary>
            /// the line this case is on, relative to the start of the sub (with empty lines removed)
            /// </summary>
            public int lineID = 0;

            public SwitchCaseInfo() { }
        }

        public class SwitchInfo
        {
            /// <summary>
            /// the scriptCode pos of the switch function opcode
            /// </summary>
            public int scriptCodePos = 0;
            /// <summary>
            /// the smallest caseNum that appears in the switch statement
            /// </summary>
            public int lowestCase = 0;
            /// <summary>
            /// the largest caseNum that appears in the switch statement
            /// </summary>
            public int highestCase = 0;
            /// <summary>
            /// where to set the scriptCode pos to if the default case is jumped to
            /// </summary>
            public int defaultScriptCodePos = 0;
            /// <summary>
            /// where to set the scriptCode pos to after a case statement has finished (should be just before the "end switch" call)
            /// </summary>
            public int endScriptCodePos = 0;

            /// <summary>
            /// the line the default case is on, relative to the start of the sub (with empty lines removed), will be 0 if the default case doesn't exist
            /// </summary>
            public int defaultCaseLineID = 0;

            public List<SwitchCaseInfo> cases = new List<SwitchCaseInfo>();

            public SwitchInfo() { }
        }

        public class ScriptSub
        {
            /// <summary>
            /// a list of each function call in the sub
            /// </summary>
            public List<OpcodeInfo> scriptCode = new List<OpcodeInfo>();
            /// <summary>
            /// a list of each switch statement in the sub
            /// </summary>
            public List<SwitchInfo> jumpTable = new List<SwitchInfo>();
            /// <summary>
            /// a list of each label in the sub
            /// </summary>
            public List<LabelInfo> labels = new List<LabelInfo>();
            /// <summary>
            /// a list of the lineIDs where a blank line should be inserted into
            /// </summary>
            public List<int> blankLines = new List<int>();

            /// <summary>
            /// the size of the scriptCode as bytecode (in bytes)
            /// </summary>
            public int scriptCodeLength
            {
                get
                {
                    return scriptCode.Take(scriptCode.Count).Sum(x => x.size);
                }
            }

            public ScriptSub() { }
        }

        public ScriptSub[] subs = new ScriptSub[5];

        public Script()
        {
            for (int i = 0; i < 5; ++i)
                subs[i] = new ScriptSub();
        }

        public Script(string filename) : this(new Reader(filename)) { }

        public Script(Reader reader) : this()
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            foreach (ScriptSub sub in subs)
            {
                sub.scriptCode.Clear();
                sub.blankLines.Clear();
                sub.labels.Clear();
                sub.jumpTable.Clear();

                bool finishedScriptCode = false;

                int scriptCodeLength = reader.ReadByte() << 8;
                scriptCodeLength |= reader.ReadByte();

                sub.scriptCode.Clear();
                while (!finishedScriptCode)
                {
                    byte scriptOpcode = reader.ReadByte();
                    if (scriptOpcode == 0xFF)
                    {
                        scriptOpcode = reader.ReadByte();
                        if (scriptOpcode == 0xFF)
                        {
                            scriptOpcode = reader.ReadByte();
                            if (scriptOpcode == 0xFF)
                            {
                                scriptOpcode = reader.ReadByte();
                                if (scriptOpcode == 0xFF)
                                    finishedScriptCode = true;
                            }
                        }
                    }

                    if (!finishedScriptCode)
                    {
                        OpcodeInfo opInfo = new OpcodeInfo();

                        opInfo.opcode = scriptOpcode;

                        uint paramCount = (uint)opcodeList[scriptOpcode].paramCount;
                        opInfo.parameters.Clear();

                        for (int p = 0; p < paramCount; p++)
                        {
                            ParamInfo param = new ParamInfo();

                            int paramType = reader.ReadByte(); // if 0 then int constant, else variable

                            if (paramType != 0)
                            {
                                param.isVariable = true;

                                param.value = reader.ReadByte();

                                int arrayIndex = reader.ReadByte();
                                if (arrayIndex > 0x80) arrayIndex = 0x80 - arrayIndex;

                                param.arrayIndex = (sbyte)arrayIndex;
                            }
                            else
                            {
                                param.isVariable = false;
                                param.arrayIndex = -1;

                                int byte1 = reader.ReadByte();
                                int constVal = 0;
                                if (byte1 < 0x80) // unsigned uint16
                                {
                                    constVal = byte1 << 8;
                                    constVal |= reader.ReadByte();
                                }
                                else // signed uint16
                                {
                                    constVal = (byte1 - 0x80) << 8;
                                    constVal |= reader.ReadByte();
                                    constVal = -constVal;
                                }

                                param.value = constVal;
                            }
                            opInfo.parameters.Add(param);
                        }

                        sub.scriptCode.Add(opInfo);
                    }
                }

                int blankLineUnused = reader.ReadByte();
                int blankLineCount = reader.ReadByte();
                for (int u = 0; u < blankLineCount; u++)
                {
                    int blankLineID = reader.ReadByte() << 8;
                    blankLineID |= reader.ReadByte();
                    sub.blankLines.Add(blankLineID);
                }

                int labelUnused = reader.ReadByte();
                int labelCount = reader.ReadByte();
                for (int l = 0; l < labelCount; l++)
                {
                    LabelInfo label = new LabelInfo();

                    label.scriptCodePos = reader.ReadByte() << 8;
                    label.scriptCodePos |= reader.ReadByte();

                    label.id = reader.ReadByte() << 8;
                    label.id |= reader.ReadByte();

                    label.lineID = reader.ReadByte() << 8;
                    label.lineID |= reader.ReadByte();

                    sub.labels.Add(label);
                }

                int jumpTableUnused = reader.ReadByte();
                int jumpTableCount = reader.ReadByte();
                for (int s = 0; s < jumpTableCount; ++s)
                {
                    SwitchInfo info = new SwitchInfo();

                    info.scriptCodePos = reader.ReadByte() << 8;
                    info.scriptCodePos |= reader.ReadByte();

                    int caseCount = reader.ReadByte() << 8;
                    caseCount |= reader.ReadByte();

                    info.defaultScriptCodePos = reader.ReadByte() << 8;
                    info.defaultScriptCodePos |= reader.ReadByte();

                    info.defaultCaseLineID = reader.ReadByte() << 8;
                    info.defaultCaseLineID |= reader.ReadByte();

                    info.endScriptCodePos = reader.ReadByte() << 8;
                    info.endScriptCodePos |= reader.ReadByte();

                    // if (info.defaultScriptCodePos == 0)
                    //     info.defaultScriptCodePos = info.endScriptCodePos;

                    int lowestCase = 0x8000;
                    int highestCase = 0;
                    info.cases.Clear();
                    for (int c = 0; c < caseCount; c++)
                    {
                        SwitchCaseInfo caseInfo = new SwitchCaseInfo();

                        caseInfo.scriptCodePos = reader.ReadByte() << 8;
                        caseInfo.scriptCodePos |= reader.ReadByte();

                        caseInfo.caseNum = reader.ReadByte() << 8;
                        caseInfo.caseNum |= reader.ReadByte();

                        if (caseInfo.caseNum < lowestCase)
                            lowestCase = caseInfo.caseNum;

                        if (caseInfo.caseNum > highestCase)
                            highestCase = caseInfo.caseNum;

                        caseInfo.lineID = reader.ReadByte() << 8;
                        caseInfo.lineID |= reader.ReadByte();

                        info.cases.Add(caseInfo);
                    }


                    info.lowestCase = lowestCase;
                    info.highestCase = highestCase;

                    // Take any duds and make em default cases
                    // for (int m = lowestCase; m <= highestCase; ++m)
                    // {
                    //     int jump = sub.scriptCode[caseTablePos + m];
                    //     if (jump == 0)
                    //         sub.scriptCode[caseTablePos + m] = sub.jumpTable[startSwitchTablePos + 2];
                    // }

                    // wow this is weird
                    // it manually goes and sets up the offsets instead of doing it at compile-time like later RSDK versions
                    int pos = 0;
                    foreach (OpcodeInfo opcodeInfo in sub.scriptCode)
                    {
                        if (pos == info.scriptCodePos)
                        {
                            opcodeInfo.parameters[1].value = pos;
                            break;
                        }
                        pos += opcodeInfo.size;
                    }

                    sub.jumpTable.Add(info);
                }
            }

            reader.Close();
        }
        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                Write(writer);
        }

        public void Write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                Write(writer);
        }

        public void Write(Writer writer)
        {
            foreach (ScriptSub sub in subs)
            {
                // Script Code
                writer.Write((byte)(sub.scriptCodeLength >> 8));
                writer.Write((byte)(sub.scriptCodeLength & 0xFF));

                foreach (OpcodeInfo opInfo in sub.scriptCode)
                {
                    writer.Write(opInfo.opcode);

                    int paramID = 0;
                    foreach (ParamInfo param in opInfo.parameters)
                    {
                        writer.Write((byte)(param.isVariable ? 1 : 0));
                        if (param.isVariable)
                        {
                            writer.Write((byte)param.value);

                            if (param.arrayIndex < 0)
                            {
                                byte value = (byte)-param.value;
                                writer.Write((byte)(value + 0x80));
                            }
                            else
                            {
                                writer.Write((byte)param.arrayIndex);
                            }
                        }
                        else
                        {
                            // Gross switch hack, but its set using jumpTable, not here
                            if (opInfo.opcode == 0x30 && paramID == 1)
                            {
                                writer.Write((byte)(0 >> 8));
                                writer.Write((byte)(0 & 0xFF));
                            }
                            else
                            {
                                if (param.value < 0)
                                {
                                    int value = -param.value;
                                    sbyte byte1 = (sbyte)(value >> 8);
                                    writer.Write((byte)(byte1 + 0x80));
                                    writer.Write((byte)(value & 0xFF));
                                }
                                else
                                {
                                    writer.Write((byte)(param.value >> 8));
                                    writer.Write((byte)(param.value & 0xFF));
                                }
                            }
                        }

                        ++paramID;
                    }
                }
                writer.Write(0xFFFFFFFF);

                // Unknown
                writer.Write((byte)(0 >> 8));
                writer.Write((byte)(sub.blankLines.Count & 0xFF));
                foreach (int lineID in sub.blankLines)
                {
                    // a line to put a blank space on
                    writer.Write((byte)(lineID >> 8));
                    writer.Write((byte)(lineID & 0xFF));
                }

                // Labels
                writer.Write((byte)(0 >> 8));
                writer.Write((byte)(sub.labels.Count & 0xFF));
                foreach (LabelInfo label in sub.labels)
                {
                    // Where the label should jump to
                    writer.Write((byte)(label.scriptCodePos >> 8));
                    writer.Write((byte)(label.scriptCodePos & 0xFF));

                    // the id of the label
                    writer.Write((byte)(label.id >> 8));
                    writer.Write((byte)(label.id & 0xFF));

                    // the line ID of the label
                    writer.Write((byte)(label.lineID >> 8));
                    writer.Write((byte)(label.lineID & 0xFF));
                }

                // JumpTable (switch cases)
                writer.Write((byte)(0 >> 8));
                writer.Write((byte)(sub.jumpTable.Count & 0xFF));
                foreach (SwitchInfo switchStatement in sub.jumpTable)
                {
                    // the script code pos of the corresponding switch function call
                    writer.Write((byte)(switchStatement.scriptCodePos >> 8));
                    writer.Write((byte)(switchStatement.scriptCodePos & 0xFF));

                    // How many cases the switch statement has
                    writer.Write((byte)(switchStatement.cases.Count >> 8));
                    writer.Write((byte)(switchStatement.cases.Count & 0xFF));

                    // the scriptCodePos to jump to on default cases
                    writer.Write((byte)(switchStatement.defaultScriptCodePos >> 8));
                    writer.Write((byte)(switchStatement.defaultScriptCodePos & 0xFF));

                    // the line ID of the default case
                    writer.Write((byte)(switchStatement.defaultCaseLineID >> 8));
                    writer.Write((byte)(switchStatement.defaultCaseLineID & 0xFF));

                    // the scriptCodePos to jump to on end switch
                    writer.Write((byte)(switchStatement.endScriptCodePos >> 8));
                    writer.Write((byte)(switchStatement.endScriptCodePos & 0xFF));

                    foreach (SwitchCaseInfo caseInfo in switchStatement.cases)
                    {
                        // Where to set the scriptCodePos to
                        writer.Write((byte)(caseInfo.scriptCodePos >> 8));
                        writer.Write((byte)(caseInfo.scriptCodePos & 0xFF));

                        // Case Number
                        writer.Write((byte)(caseInfo.caseNum >> 8));
                        writer.Write((byte)(caseInfo.caseNum & 0xFF));

                        // the line ID of the switch case
                        writer.Write((byte)(caseInfo.lineID >> 8));
                        writer.Write((byte)(caseInfo.lineID & 0xFF));
                    }
                }
            }

            writer.Close();
        }

    }
}
