using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace RSDKv5
{
    public class Model
    {
        public class TexturePosition
        {
            public float X = 0;
            public float Y = 0;

            public TexturePosition()
            {

            }

            public TexturePosition(Reader reader)
            {
                X = reader.ReadSingle();
                Y = reader.ReadSingle();
            }

            public void Write(Writer writer)
            {
                writer.Write(X);
                writer.Write(Y);
            }
        }

        public class Colour
        {
            /// <summary>
            /// Colour Red Value
            /// </summary>
            public byte r;
            /// <summary>
            /// Colour Green Value
            /// </summary>
            public byte g;
            /// <summary>
            /// Colour Blue Value
            /// </summary>
            public byte b;
            /// <summary>
            /// Colour Alpha Value
            /// </summary>
            public byte a;

            public Colour()
            {
                b = 255;
                g = 0;
                r = 255;
                a = 255;
            }

            public Colour(Reader reader)
            {
                b = reader.ReadByte();
                g = reader.ReadByte();
                r = reader.ReadByte();
                a = reader.ReadByte();
            }

            public void Write(Writer writer)
            {
                writer.Write(b);
                writer.Write(g);
                writer.Write(r);
                writer.Write(a);
            }
        }

        public class Frame
        {
            public class Vertex
            {
                /// <summary>
                /// an extra point to be used if it's a quad
                /// </summary>
                public float w = 0;

                public float x = 0;
                public float y = 0;
                public float z = 0;

                /// <summary>
                /// is it a quad
                /// </summary>
                public bool isQuad = false;

                /// <summary>
                /// normal data
                /// </summary>
                public Normal normal = new Normal();

                public class Normal
                {
                    public float x = 0;
                    public float y = 0;
                    public float z = 0;

                    public Normal()
                    {

                    }

                    public Normal(Reader reader)
                    {
                        x = reader.ReadSingle();
                        y = reader.ReadSingle();
                        z = reader.ReadSingle();
                    }

                    public void Write(Writer writer)
                    {
                        writer.Write(x);
                        writer.Write(y);
                        writer.Write(z);
                    }
                }

                public Vertex()
                {

                }

                public Vertex(Reader reader, bool useNormals)
                {
                    x = reader.ReadSingle();
                    y = reader.ReadSingle();
                    z = reader.ReadSingle();

                    if (isQuad)
                    {
                        w = reader.ReadSingle();
                    }

                    if (useNormals)
                    {
                        normal = new Normal(reader);
                    }
                }

                public void Write(Writer writer, bool useNormals)
                {
                    writer.Write(x);
                    writer.Write(y);
                    writer.Write(z);

                    if (isQuad)
                    {
                        writer.Write(w);
                    }

                    if (useNormals)
                    {
                        normal.Write(writer);
                    }

                }

            }

            /// <summary>
            /// a list of verticies in this frame
            /// </summary>
            public List<Vertex> Vertices = new List<Vertex>();

            public Frame()
            {

            }

            public Frame(Reader reader, int VertexCount, bool useNormals = false)
            {
                for (int i = 0; i < VertexCount; i++)
                {
                    Vertices.Add(new Vertex(reader, useNormals));
                }
            }

            public void Write(Writer writer, bool useNormals = false)
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vertices[i].Write(writer, useNormals);
                }
            }

            public void AddVertex()
            {
                Vertices.Add(new Vertex());
            }

            public void DeleteVertex(int index)
            {
                Vertices.RemoveAt(index);
            }
        }

        /// <summary>
        /// the file's signtature
        /// </summary>
        public static readonly byte[] MAGIC = new byte[] { (byte)'M', (byte)'D', (byte)'L', (byte)'\0' };

        /// <summary>
        /// does it have normals?
        /// </summary>
        public bool HasNormals = true;
        /// <summary>
        /// does it have [Unknown]?
        /// </summary>
        public bool HasUnknown = false;
        /// <summary>
        /// is it using colours?
        /// </summary>
        public bool HasColours = true;
        /// <summary>
        /// is it using quads (4) or tris (3)
        /// </summary>
        public byte FaceVerticiesCount = 3; //Tri (3) or Quad (4)?
        /// <summary>
        /// how many verticies in the model?
        /// </summary>
        public ushort VertexCount = 0;
        /// <summary>
        /// how many frames in the model?
        /// </summary>
        public ushort FramesCount = 0;
        /// <summary>
        /// how many faces in the model?
        /// </summary>
        public short FaceCount = 0;

        /// <summary>
        /// a list of all the Texture positions
        /// </summary>
        public List<TexturePosition> TexturePositions = new List<TexturePosition>();
        /// <summary>
        /// a list of all the colours for each face
        /// </summary>
        public List<Colour> Colours = new List<Colour>();
        /// <summary>
        /// a list of frames, used to animate the model
        /// </summary>
        public List<Frame> Frames = new List<Frame>();
        /// <summary>
        /// a list of all the faces
        /// </summary>
        public List<short> Faces = new List<short>();

        public Model()
        {
            AddFrame();
        }

        public Model(string filename) : this(new Reader(filename))
        {

        }

        public Model(Stream stream) : this(new Reader(stream))
        {

        }

        public static bool GetBit(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static int SetBit(int pos, bool Set, int Value)
        {
            if (Set)
            {
                Value |= 1 << pos;
            }
            if (!Set)
            {
                Value &= ~(1 << pos);
            }
            return Value;
        }

        public Model(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid config file header magic");

            byte flags = reader.ReadByte();
            //HasNormals = (flags & 0x00000001) != 0;
            //HasUnknown = (flags & 0x00000010) != 0;
            //HasColours = (flags & 0x00000100) != 0;

            HasNormals = GetBit(flags, 0);
            HasUnknown = GetBit(flags, 1);
            HasColours = GetBit(flags, 2);

            Console.WriteLine("MDL READ: FLAGS:" + Pad(flags));

            string Pad(byte b)
            {
                return Convert.ToString(b, 2).PadLeft(8, '0');
            }


            FaceVerticiesCount = reader.ReadByte();
            if (FaceVerticiesCount != 3 && FaceVerticiesCount != 4)
                throw new Exception("Detected Vertex Type wasn't Tris or Quads! RSDKv5 doesn't support other N-gons!");

            VertexCount = reader.ReadUInt16();
            FramesCount = reader.ReadUInt16();

            Console.WriteLine("Frame Count: " + FramesCount + Environment.NewLine + "Vertex Count: " + VertexCount);

            if (HasUnknown)
                for (int i = 0; i < VertexCount; ++i)
                    TexturePositions.Add(new TexturePosition(reader));

            if (HasColours)
                for (int i = 0; i < VertexCount; ++i)
                    Colours.Add(new Colour(reader));

            FaceCount = reader.ReadInt16();
            for (int i = 0; i < FaceCount; ++i)
                Faces.Add(reader.ReadInt16());

            for (int i = 0; i < FramesCount; ++i)
                Frames.Add(new Frame(reader, VertexCount, HasNormals));

            Console.WriteLine("MDL READ: FileSize: {0}, Position: 0x{1:X8}, DataLeft: {2}", reader.BaseStream.Length, reader.BaseStream.Position, reader.BaseStream.Length - reader.BaseStream.Position);
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);
            byte flags = 0;
            //flags |= (byte)(HasColours ? 0x00000001 : 0);
            //flags |= (byte)(HasUnknown ? 0x00000010 : 0);
            //flags |= (byte)(HasNormals ? 0x00000100 : 0);
            flags = (byte)SetBit(0, HasNormals, flags);
            flags = (byte)SetBit(1, HasUnknown, flags);
            flags = (byte)SetBit(2, HasColours, flags);
            writer.Write(flags);

            Console.WriteLine("MDL WRITE: FLAGS:" + Pad(flags));

            string Pad(byte b)
            {
                return Convert.ToString(b, 2).PadLeft(8, '0');
            }

            Console.WriteLine("MDL WRITE: Flags: {0}, HasColours: {1}, HasUnknown: {2}, HasNormals: {3}", flags, HasColours, HasUnknown, HasNormals);

            FramesCount = (ushort)Frames.Count;
            VertexCount = (ushort)Frames[0].Vertices.Count;

            writer.Write(FaceVerticiesCount);
            writer.Write(VertexCount);
            writer.Write(FramesCount);

            if (HasUnknown)
                for (int i = 0; i < VertexCount; ++i)
                    TexturePositions[i].Write(writer);

            if (HasColours)
                for (int i = 0; i < VertexCount; ++i)
                    Colours[i].Write(writer);

            writer.Write(FaceCount);
            for (int i = 0; i < FaceCount; ++i)
                writer.Write(Faces[i]);

            for (int i = 0; i < FramesCount; ++i)
                Frames[i].Write(writer, HasNormals);
            writer.Close();
        }

        public ushort ToRGB555(byte red, byte green, byte blue)
        {
            return (ushort)(((red & 0b11111000) << 7) | ((green & 0b11111000) << 2) | ((blue & 0b11111000) >> 3));
        }

        public void WriteAsOBJ(string filename, string mtlfilename)
        {
            for (int frame = 0; frame < Frames.Count; ++frame)
            {

                string tmp = Path.GetExtension(filename);
                string tmp2 = filename.Replace(tmp, "");
                string streamName = tmp2 + " Frame " + frame + tmp;

                StringBuilder builder = new StringBuilder();
                // Shows I been here lol
                builder.AppendLine("# Kimi no sei kimi no sei kimi no sei de watashi Oh Okubyou de kakkou tsukanai kimi no sei da yo");

                // Link Material
                builder.AppendLine("mtllib " + mtlfilename + ".mtl");
                // Object
                builder.AppendLine("o ManiaModel");
                for (int v = 0; v < Frames[frame].Vertices.Count; ++v)
                    builder.AppendLine(string.Format("v {0} {1} {2}", Frames[frame].Vertices[v].x, Frames[frame].Vertices[v].y, Frames[frame].Vertices[v].z));

                builder.AppendLine("vn 0.0000 1.0000 0.0000");
                builder.AppendLine("usemtl None");
                builder.AppendLine("s off");

                int[] verts = new int[FaceVerticiesCount];
                for (int v = 0; v < FaceCount; v += FaceVerticiesCount)
                {
                    for (int ii = 0; ii < FaceVerticiesCount; ++ii)
                        verts[ii] = Faces[v + ii];
                    builder.AppendLine($"usemtl ManiaModel.Colour.{Faces[v]:###}");
                    builder.AppendLine(string.Format("f {0} {1} {2}", verts[0] + 1, verts[1] + 1, verts[2] + 1));
                    if (FaceVerticiesCount == 4)
                        builder.AppendLine(string.Format("f {0} {1} {2}", verts[0] + 1, verts[2] + 1, verts[3] + 1));
                }

                File.WriteAllText(streamName, builder.ToString());
            }
        }

        public void WriteMTL(Writer writer)
        {
            StringBuilder builder = new StringBuilder();
            // Shows I been here lol
            builder.AppendLine("# Kimi no sei kimi no sei kimi no sei de watashi Oh Okubyou de kakkou tsukanai kimi no sei da yo");

            for (int i = 0; i < Colours.Count; ++i)
            {
                builder.AppendLine($"newmtl ManiaModel.Colour.{i:###}");
                builder.AppendLine($"kd {(Colours[i].r / 255f)} {(Colours[i].g / 255f)} {(Colours[i].b / 255f)}");
                builder.AppendLine("");
            }
            writer.Write(Encoding.ASCII.GetBytes(builder.ToString()));
            writer.Close();

        }


        public void WriteAsSTLBinary(Writer writer)
        {

            Debugger.Launch();
            byte[] junk = Encoding.ASCII.GetBytes("Kimi no sei kimi no sei kimi no sei de watashi Oh Okubyou de kakkou tsukanai kimi no sei da yo");
            writer.Write(junk, 0, 0x50);
            //for (int i = 0; i < FramesCount; i++)
            //{
            //
            //}

            writer.Write((int)(FaceCount / FaceVerticiesCount));
            // Triangle

            var vertices = new Frame.Vertex[FaceVerticiesCount];
            for (int v = 0; v < FaceCount; v += FaceVerticiesCount)
            {
                for (int ii = 0; ii < FaceVerticiesCount; ++ii)
                    vertices[ii] = Frames[0].Vertices[Faces[v + ii]];

                // Normal
                //writer.Write(0f);
                //writer.Write(0f);
                //writer.Write(1f);
                writer.Write(vertices[0].normal.x);
                writer.Write(vertices[0].normal.y);
                writer.Write(vertices[0].normal.z);

                // Vector 1
                writer.Write(vertices[0].x);
                writer.Write(vertices[0].z);
                writer.Write(vertices[0].y);
                // Vector 2
                writer.Write(vertices[1].x);
                writer.Write(vertices[1].z);
                writer.Write(vertices[1].y);
                // Vector 3
                writer.Write(vertices[2].x);
                writer.Write(vertices[2].z);
                writer.Write(vertices[2].y);

                // Attribute
                if (HasColours)
                {
                    int colour = Faces[v];
                    ushort attb = (ushort)(ToRGB555(Colours[colour].r, Colours[colour].g, Colours[colour].b));
                    writer.Write(attb);
                }
                else
                    writer.Write((short)0);

                if (FaceVerticiesCount == 4)
                {
                    // Normal
                    //writer.Write(0f);
                    //writer.Write(0f);
                    //writer.Write(1f);
                    writer.Write(vertices[0].normal.x);
                    writer.Write(vertices[0].normal.y);
                    writer.Write(vertices[0].normal.z);

                    // Vector 1
                    writer.Write(vertices[0].x);
                    writer.Write(vertices[0].z);
                    writer.Write(vertices[0].y);
                    // Vector 2
                    writer.Write(vertices[2].x);
                    writer.Write(vertices[2].z);
                    writer.Write(vertices[2].y);
                    // Vector 3
                    writer.Write(vertices[3].x);
                    writer.Write(vertices[3].z);
                    writer.Write(vertices[3].y);

                    // Attribute
                    if (HasColours)
                    {
                        int colour = Faces[v];
                        ushort attb = (ushort)(ToRGB555(Colours[colour].r, Colours[colour].g, Colours[colour].b));
                        writer.Write(attb);
                    }
                    else
                        writer.Write((short)0);
                }
            }

            writer.Close();
        }

        public void WriteAsSTL(string filename)
        {
            for (int i = 0; i < FramesCount; i++)
            {
                string tmp = Path.GetExtension(filename);
                string tmp2 = filename.Replace(tmp, "");
                string streamName = tmp2 + " Frame " + i + tmp;
                using (var writer = new StreamWriter(File.Create(streamName)))
                {
                    writer.WriteLine("solid obj");
                    var vertices = new Frame.Vertex[FaceVerticiesCount];
                    for (int v = 0; v < FaceCount; v += FaceVerticiesCount)
                    {
                        for (int ii = 0; ii < FaceVerticiesCount; ++ii)
                            vertices[ii] = Frames[i].Vertices[Faces[v + ii]];

                        writer.WriteLine(" facet normal 0.000000 0.000000 1.000000");
                        writer.WriteLine("  outer loop");
                        writer.WriteLine(WriteVertex(vertices[0]));
                        writer.WriteLine(WriteVertex(vertices[1]));
                        writer.WriteLine(WriteVertex(vertices[2]));
                        writer.WriteLine("  endloop");
                        writer.WriteLine(" endfacet");
                        if (FaceVerticiesCount == 4)
                        {
                            writer.WriteLine(" facet normal 0.000000 0.000000 1.000000");
                            writer.WriteLine("  outer loop");
                            writer.WriteLine(WriteVertex(vertices[0]));
                            writer.WriteLine(WriteVertex(vertices[2]));
                            writer.WriteLine(WriteVertex(vertices[3]));
                            writer.WriteLine("  endloop");
                            writer.WriteLine(" endfacet");
                        }
                    }
                    writer.WriteLine("endsolid");
                }
            }

            string WriteVertex(Frame.Vertex vertex)
            {
                return string.Format("   vertex {0} {1:} {2:}", vertex.x, vertex.z, vertex.y);
            }
        }

        public void AddFrame()
        {
            Frame F = new Frame();
            F.AddVertex();
            VertexCount++;
            Frames.Add(F);
            FramesCount++;
        }

        public void DeleteFrame(int index)
        {
            int VertexCnt = Frames[index].Vertices.Count - 1;
            Frames.RemoveAt(index);
            FramesCount--;
            VertexCount -= (ushort)VertexCnt;
        }
    }
}
