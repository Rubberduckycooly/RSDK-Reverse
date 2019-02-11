using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RSDKvB
{
    public class Model
    {
        public static readonly byte[] MAGIC = new byte[] { (byte)'R', (byte)'3', (byte)'D', (byte)'\0' };

        public class Vertex
        {
            /// <summary>
            /// Vertex X
            /// </summary>
            public float x;
            /// <summary>
            /// Vertex Y
            /// </summary>
            public float y;
            /// <summary>
            /// Vertex Z
            /// </summary>
            public float z;

            public Normal normal = new Normal();

            public class Normal
            {
                /// <summary>
                /// Normal X
                /// </summary>
                public float x;
                /// <summary>
                /// Normal Y
                /// </summary>
                public float y;
                /// <summary>
                /// Normal Z
                /// </summary>
                public float z;

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

            public Vertex(Reader reader)
            {
                x = reader.ReadSingle();
                y = reader.ReadSingle();
                z = reader.ReadSingle();

                normal = new Normal(reader);
            }

            public void Write(Writer writer)
            {
                writer.Write(x);
                writer.Write(y);
                writer.Write(z);

                normal.Write(writer);
            }

        }

        public class Face
        {
            /// <summary>
            /// Face X
            /// </summary>
            public short X = 0;
            /// <summary>
            /// Face Y
            /// </summary>
            public short Y = 0;
            /// <summary>
            /// Face Z
            /// </summary>
            public short Z = 0;

            public Face()
            {

            }

            public Face(Reader reader)
            {
                X = reader.ReadInt16();
                Y = reader.ReadInt16();
                Z = reader.ReadInt16();
            }

            public void Write(Writer writer)
            {
                writer.Write(X);
                writer.Write(Y);
                writer.Write(Z);
            }
        }

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
        /// <summary>
        /// a list of all the Texture positions
        /// </summary>
        public List<TexturePosition> TexturePositions = new List<TexturePosition>();
        /// <summary>
        /// a list of all the faces
        /// </summary>
        public List<Face> Faces = new List<Face>();
        /// <summary>
        /// a list of all the verticies
        /// </summary>
        public List<Vertex> Vertices = new List<Vertex>();

        /// <summary>
        /// if it's using Quads or Tris (it's always using tris)
        /// </summary>
        public int FaceVerticiesCount
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// how many faces there are
        /// </summary>
        public short FaceCount;
        /// <summary>
        /// how many texture positions there are
        /// </summary>
        public ushort TexturePosCount;
        /// <summary>
        /// how many verticies there are
        /// </summary>
        public ushort VertexCount;

        /// <summary>
        /// if it's a wierd file that does weird shit
        /// </summary>
        public bool WeirdOne = false;

        public Model()
        {

        }

        public Model(string filename) : this(new Reader(filename))
        {

        }

        public Model(Stream stream) : this(new Reader(stream))
        {

        }

        public Model(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid config file header magic");

            TexturePosCount = reader.ReadUInt16();

            for (int i = 0; i < TexturePosCount; i++)
            {
                TexturePositions.Add(new TexturePosition(reader));
            }

            FaceCount = reader.ReadInt16();
            for (int i = 0; i < FaceCount; ++i)
            {
                //Faces.Add(reader.ReadInt16());
                Faces.Add(new Face(reader));
            }

            VertexCount = reader.ReadUInt16();

            //FIX THIS
            //if (VertexCount == 1)
            //{
            int newcnt = (int)((reader.BaseStream.Length - reader.BaseStream.Position) / 4) / 6;

            for (int i = 0; i < newcnt; i++)
            {
                Vertices.Add(new Vertex(reader));
            }
            WeirdOne = true;
            /*}
            else
            {
                for (int i = 0; i < VertexCount; i++)
                {
                    Vertices.Add(new Vertex(reader));
                }
            }*/

            Console.WriteLine("File Size: " + reader.BaseStream.Length + " Reader Pos: " + reader.BaseStream.Position + " Data Left: " + (reader.BaseStream.Length - reader.BaseStream.Position));

        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);

            writer.Write((ushort)TexturePositions.Count);

            for (int i = 0; i < TexturePositions.Count; i++)
            {
                TexturePositions[i].Write(writer);
            }

            writer.Write((ushort)Faces.Count);
            for (int i = 0; i < Faces.Count; ++i)
            {
                Faces[i].Write(writer);
            }
            if (WeirdOne)
            {
                writer.Write(1);
            }
            else
            {
                writer.Write((ushort)Vertices.Count);
            }

            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i].Write(writer);
            }
        }

        public ushort ToRGB555(byte red, byte green, byte blue)
        {
            return (ushort)(((red & 0b11111000) << 7) | ((green & 0b11111000) << 2) | ((blue & 0b11111000) >> 3));
        }

        public void WriteAsOBJ(string filename, string mtlfilename)
        {
            string streamName = filename;

            StringBuilder builder = new StringBuilder();
            // Shows I been here lol
            builder.AppendLine("# Kimi no sei kimi no sei kimi no sei de watashi Oh Okubyou de kakkou tsukanai kimi no sei da yo");

            // Link Material
            builder.AppendLine("mtllib " + mtlfilename + ".mtl");
            // Object
            builder.AppendLine("o RetrovBModel");
            for (int v = 0; v < Vertices.Count; ++v)
                builder.AppendLine(string.Format("v {0} {1} {2}", Vertices[v].x, Vertices[v].y, Vertices[v].z));

            builder.AppendLine("vn 0.0000 1.0000 0.0000");
            builder.AppendLine("usemtl None");
            builder.AppendLine("s off");

            for (int v = 0; v < FaceCount; v++)
            {
                builder.AppendLine($"usemtl ManiaModel.Colour.{Faces[v]:###}");
                builder.AppendLine(string.Format("f {0} {1} {2}", Faces[v].X + 1, Faces[v].Y + 1, Faces[v].Z + 1));
            }

            File.WriteAllText(streamName, builder.ToString());
        }

        public void WriteMTL(Writer writer)
        {
            StringBuilder builder = new StringBuilder();
            // Shows I been here lol - SS16
            builder.AppendLine("# Kimi no sei kimi no sei kimi no sei de watashi Oh Okubyou de kakkou tsukanai kimi no sei da yo");

            writer.Write(Encoding.ASCII.GetBytes(builder.ToString()));
            writer.Close();

        }


        public void WriteAsSTLBinary(Writer writer)
        {

            byte[] junk = Encoding.ASCII.GetBytes("Kimi no sei kimi no sei kimi no sei de watashi Oh Okubyou de kakkou tsukanai kimi no sei da yo");
            writer.Write(junk, 0, 0x50);

            writer.Write((int)(FaceCount / FaceVerticiesCount));
            // Triangle

            var vertices = new Vertex[FaceVerticiesCount];
            for (int v = 0; v < FaceCount; v++)
            {
                vertices[0] = Vertices[Faces[v].X];
                vertices[1] = Vertices[Faces[v].Y];
                vertices[2] = Vertices[Faces[v].Z];

                // Normal
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
                writer.Write((short)0);
            }

            writer.Close();
        }

        public void WriteAsSTL(string filename)
        {
            string streamName = filename;
            using (var writer = new StreamWriter(File.Create(streamName)))
            {
                writer.WriteLine("solid obj");
                var vertices = new Vertex[FaceVerticiesCount];
                for (int v = 0; v < FaceCount; v++)
                {
                    vertices[0] = Vertices[Faces[v].X];
                    vertices[1] = Vertices[Faces[v].Y];
                    vertices[2] = Vertices[Faces[v].Z];

                    writer.WriteLine(" facet normal 0.000000 0.000000 1.000000");
                    writer.WriteLine("  outer loop");
                    writer.WriteLine(WriteVertex(vertices[0]));
                    writer.WriteLine(WriteVertex(vertices[1]));
                    writer.WriteLine(WriteVertex(vertices[2]));
                    writer.WriteLine("  endloop");
                    writer.WriteLine(" endfacet");
                }
                writer.WriteLine("endsolid");
            }

            string WriteVertex(Vertex vertex)
            {
                return string.Format("   vertex {0} {1:} {2:}", vertex.x, vertex.z, vertex.y);
            }
        }

    }
}
