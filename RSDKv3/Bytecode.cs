using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace RSDKv3
{
    public class Bytecode
    {
        public class FunctionScript
        {
            public FunctionScript() { }

            public int scriptCodePos = 0x3FFFF;
            public int jumpTablePos = 0x3FFFF;
        };

        public class ScriptInfo
        {
            public ScriptInfo() { }

            public int scriptCodePos_main = 0x3FFFF;
            public int jumpTablePos_main = 0x3FFFF;

            public int scriptCodePos_playerInteraction = 0x3FFFF;
            public int jumpTablePos_playerInteraction = 0x3FFFF;

            public int scriptCodePos_draw = 0x3FFFF;
            public int jumpTablePos_draw = 0x3FFFF;

            public int scriptCodePos_startup = 0x3FFFF;
            public int jumpTablePos_startup = 0x3FFFF;
        };
        private class DataInfo
        {
            public List<int> data = new List<int>();
            public bool readInt = false;

            public DataInfo() { }
        }

        public int[] scriptCode = new int[0x40000];
        public int scriptCodeLength = 0;
        public int[] jumpTable = new int[0x4000];
        public int jumpTableLength = 0;

        List<ScriptInfo> scripts = new List<ScriptInfo>();
        List<FunctionScript> functionList = new List<FunctionScript>();

        public Bytecode(Reader reader) { Read(reader); }

        public void Read(Reader reader)
        {
            int scriptCodePos = 0;
            int jumpTablePos = 0;

            // Bytecode
            for (int scriptCodeSize = reader.ReadInt32(); scriptCodeSize > 0;)
            {
                int buffer = reader.ReadByte();
                int blockSize = buffer & 0x7F;

                if ((buffer & 0x80) == 0)
                {
                    for (int i = 0; i < blockSize; i++)
                        scriptCode[scriptCodePos++] = reader.ReadByte();
                    scriptCodeSize -= blockSize;
                }
                else
                {
                    for (int i = 0; i < blockSize; i++)
                        scriptCode[scriptCodePos++] = reader.ReadInt32();
                    scriptCodeSize -= blockSize;
                }
            }

            for (int jumpTableSize = reader.ReadInt32(); jumpTableSize > 0;)
            {
                int buffer = reader.ReadByte();
                int blockSize = buffer & 0x7F;

                if ((buffer & 0x80) == 0)
                {
                    for (int i = 0; i < blockSize; i++)
                        jumpTable[jumpTablePos++] = reader.ReadByte();
                    jumpTableSize -= blockSize;
                }
                else
                {
                    for (int i = 0; i < blockSize; i++)
                        jumpTable[jumpTablePos++] = reader.ReadInt32();
                    jumpTableSize -= blockSize;
                }
            }

            // Scripts
            int scriptCount = reader.ReadInt16();
            for (int i = 0; i < scriptCount; i++)
            {
                ScriptInfo info = new ScriptInfo();
                info.scriptCodePos_main = reader.ReadInt32();
                info.scriptCodePos_playerInteraction = reader.ReadInt32();
                info.scriptCodePos_draw = reader.ReadInt32();
                info.scriptCodePos_startup = reader.ReadInt32();
                scripts.Add(info);
            }

            foreach (ScriptInfo info in scripts)
            {
                info.jumpTablePos_main = reader.ReadInt32();
                info.jumpTablePos_playerInteraction = reader.ReadInt32();
                info.jumpTablePos_draw = reader.ReadInt32();
                info.jumpTablePos_startup = reader.ReadInt32();
            }

            // Functions 
            int functionCount = reader.ReadInt16();
            for (int i = 0; i < functionCount; i++)
            {
                FunctionScript info = new FunctionScript();
                info.scriptCodePos = reader.ReadInt32();
                functionList.Add(info);
            }
            foreach (FunctionScript info in functionList)
                info.jumpTablePos = reader.ReadInt32();

            scriptCodeLength = scriptCodePos;
            jumpTableLength = jumpTablePos;
        }

        public void Write(Writer writer)
        {
            List<DataInfo> dataInfo = new List<DataInfo>();

            // Script Code
            dataInfo.Clear();
            for (int dataPos = 0; dataPos < scriptCodeLength;)
            {
                DataInfo info = new DataInfo();
                info.data.Clear();
                info.readInt = scriptCode[dataPos] < 0 || scriptCode[dataPos] >= 0x100;
                if (!info.readInt)
                {
                    for (int i = 0; (scriptCode[dataPos] >= 0 && scriptCode[dataPos] < 0x100) && i < 0x7F; ++i)
                    {
                        info.data.Add(scriptCode[dataPos++]);
                        if (dataPos >= scriptCodeLength)
                            break;
                    }
                    dataInfo.Add(info);
                }
                else
                {
                    for (int i = 0; (scriptCode[dataPos] < 0 || scriptCode[dataPos] >= 0x100) && i < 0x7F; ++i)
                    {
                        info.data.Add(scriptCode[dataPos++]);
                        if (dataPos >= scriptCodeLength)
                            break;
                    }
                    dataInfo.Add(info);
                }
            }

            int count = 0;
            for (int i = 0; i < dataInfo.Count; ++i) count += dataInfo[i].data.Count;
            writer.Write(count);

            for (int i = 0; i < dataInfo.Count; ++i)
            {
                DataInfo info = dataInfo[i];
                writer.Write((byte)(info.data.Count | ((byte)(info.readInt ? 1 : 0) << 7)));
                for (int d = 0; d < info.data.Count; ++d)
                {
                    if (info.readInt)
                        writer.Write(info.data[d]);
                    else
                        writer.Write((byte)info.data[d]);
                }
            }

            // Jump Table
            dataInfo.Clear();
            for (int dataPos = 0; dataPos < jumpTableLength;)
            {
                DataInfo info = new DataInfo();

                info.data.Clear();
                info.readInt = jumpTable[dataPos] < 0 || jumpTable[dataPos] >= 0x100;
                if (!info.readInt)
                {
                    for (int i = 0; (jumpTable[dataPos] >= 0 && jumpTable[dataPos] < 0x100) && i < 0x7F;
                         ++i)
                    {
                        info.data.Add(jumpTable[dataPos++]);
                        if (dataPos >= jumpTableLength)
                            break;
                    }
                    dataInfo.Add(info);
                }
                else
                {
                    for (int i = 0; (jumpTable[dataPos] < 0 || jumpTable[dataPos] >= 0x100) && i < 0x7F;
                         ++i)
                    {
                        info.data.Add(jumpTable[dataPos++]);
                        if (dataPos >= jumpTableLength)
                            break;
                    }
                    dataInfo.Add(info);
                }
            }

            count = 0;
            for (int i = 0; i < dataInfo.Count; ++i) count += dataInfo[i].data.Count;
            writer.Write(count);

            for (int i = 0; i < dataInfo.Count; ++i)
            {
                DataInfo info = dataInfo[i];
                writer.Write((byte)(info.data.Count | ((byte)(info.readInt ? 1 : 0) << 7)));
                for (int d = 0; d < info.data.Count; ++d)
                {
                    if (info.readInt)
                        writer.Write(info.data[d]);
                    else
                        writer.Write((byte)info.data[d]);
                }
            }

            // Scripts
            writer.Write((ushort)scripts.Count);

            foreach (ScriptInfo script in scripts)
            {
                writer.Write(script.scriptCodePos_main);
                writer.Write(script.scriptCodePos_playerInteraction);
                writer.Write(script.scriptCodePos_draw);
                writer.Write(script.scriptCodePos_startup);
            }

            foreach (ScriptInfo script in scripts)
            {
                writer.Write(script.jumpTablePos_main);
                writer.Write(script.jumpTablePos_playerInteraction);
                writer.Write(script.jumpTablePos_draw);
                writer.Write(script.jumpTablePos_startup);
            }

            // Functions
            writer.Write((ushort)functionList.Count);

            foreach (FunctionScript func in functionList)
                writer.Write(func.scriptCodePos);

            foreach (FunctionScript func in functionList)
                writer.Write(func.jumpTablePos);

            writer.Close();
        }
    }
}
