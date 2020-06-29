using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace RSDKvB
{
    public class Model
    {
        static double[] MyCos = new double[256];
        static double[] MySin = new double[256];

        public static readonly byte[] MAGIC = new byte[] { (byte)'R', (byte)'3', (byte)'D', (byte)'\0' };

        public class Vector3f
        {
            public float x;
            public float y;
            public float z;

            public Vector3f()
            {

            }

            public Vector3f(float X, float Y, float Z)
            {
                x = X;
                y = Y;
                z = Z;
            }

            public Vector3f Multiply(float x, float y, float z)
            {
                this.x *= x;
                this.y *= y;
                this.z *= z;
                return new Vector3f(this.x, this.y, this.z);
            }
            public Vector3f Multiply(Vector3f v)
            {
                return Multiply(v.x, v.y, v.z);
            }

            public float DotProduct(float x2, float y2, float z2)
            {
                return x * x2 + y * y2 + z * z2;
            }
            public float DotProduct(Vector3f v)
            {
                return x * v.x + y * v.y + z * v.z;
            }

            public Vector3f Normalize()
            {
                float len = (float)Math.Sqrt(x * x + y * y + z * z);
                x /= len;
                y /= len;
                z /= len;
                return new Vector3f(z, y, z);
            }
            public float Distance()
            {
                return (float)Math.Sqrt(x * x + y * y + z * z);
            }
        }

        private class Matrix
        {
            public float[] Values = new float[9];

            public Matrix(float[] values)
            {
                Values = values;
            }
            public Matrix(float v1, float v2, float v3, float v4, float v5, float v6, float v7, float v8, float v9)
            {
                Values[0] = v1;
                Values[1] = v2;
                Values[2] = v3;

                Values[3] = v4;
                Values[4] = v5;
                Values[5] = v6;

                Values[6] = v7;
                Values[7] = v8;
                Values[8] = v9;
            }

            public Matrix Multiply(Matrix other)
            {
                float[] result = new float[9];
                for (int row = 0; row < 3; row++)
                {
                    for (int col = 0; col < 3; col++)
                    {
                        result[row * 3 + col] = 0;
                        for (int i = 0; i < 3; i++)
                        {
                            result[row * 3 + col] += Values[row * 3 + i] * other.Values[i * 3 + col];
                        }
                    }
                }
                return new Matrix(result);
            }

            public Vector3f Transform(Vector3f inver)
            {
                return new Vector3f(
                    inver.x * Values[0] + inver.y * Values[3] + inver.z * Values[6],
                    inver.x * Values[1] + inver.y * Values[4] + inver.z * Values[7],
                    inver.x * Values[2] + inver.y * Values[5] + inver.z * Values[8]
                );
            }
        };

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

                public Vector3f Normalize()
                {
                    float len = (float)Math.Sqrt(x * x + y * y + z * z);
                    x /= len;
                    y /= len;
                    z /= len;
                    return new Vector3f(z, y, z);
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
            public ushort a = 0;
            /// <summary>
            /// Face Y
            /// </summary>
            public ushort b = 0;
            /// <summary>
            /// Face Z
            /// </summary>
            public ushort c = 0;

            public Face()
            {

            }

            public Face(Reader reader)
            {
                a = reader.ReadUInt16();
                b = reader.ReadUInt16();
                c = reader.ReadUInt16();
            }

            public void Write(Writer writer)
            {
                writer.Write(a);
                writer.Write(b);
                writer.Write(c);
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
        public ushort FaceCount;
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

        Bitmap buffer = new Bitmap(512, 512);

        public Model()
        {
            for (int a = 0; a < 256; a++)
            {
                MySin[a] = -Math.Sin(a * Math.PI / 128);
                MyCos[a] = Math.Cos(a * Math.PI / 128);
            }
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

            for (int a = 0; a < 256; a++)
            {
                MySin[a] = -Math.Sin(a * Math.PI / 128);
                MyCos[a] = Math.Cos(a * Math.PI / 128);
            }

            TexturePosCount = reader.ReadUInt16();

            for (int i = 0; i < TexturePosCount; i++)
            {
                TexturePositions.Add(new TexturePosition(reader));
            }

            FaceCount = reader.ReadUInt16();
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

            //Console.WriteLine("File Size: " + reader.BaseStream.Length + " Reader Pos: " + reader.BaseStream.Position + " Data Left: " + (reader.BaseStream.Length - reader.BaseStream.Position));

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

        private ushort ToRGB555(byte red, byte green, byte blue)
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
                builder.AppendLine(string.Format("f {0} {1} {2}", Faces[v].a + 1, Faces[v].b + 1, Faces[v].c + 1));
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
                vertices[0] = Vertices[Faces[v].a];
                vertices[1] = Vertices[Faces[v].b];
                vertices[2] = Vertices[Faces[v].c];

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
                    vertices[0] = Vertices[Faces[v].a];
                    vertices[1] = Vertices[Faces[v].b];
                    vertices[2] = Vertices[Faces[v].c];

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

        public Bitmap AsImage(float scaleX = 1, float scaleY = 1, float scaleZ = 1, int rx = 0, int ry = 0, int rz = 0, bool wireframe = false, uint defaultcolour = 0xFFFFFFFF)
        {
            buffer = new Bitmap(512, 512);

            int x = 256, y = 256;

            rx &= 0xFF;
            Matrix rotateX = new Matrix(
                1, 0, 0,
                0, (float)MyCos[rx], (float)MySin[rx],
                0, (float)-MySin[rx], (float)MyCos[rx]);

            ry &= 0xFF;
            Matrix rotateY = new Matrix(
                (float)MyCos[ry], 0, (float)-MySin[ry],
                0, 1, 0,
                (float)MySin[ry], 0, (float)MyCos[ry]);

            rz &= 0xFF;
            Matrix rotateZ = new Matrix(
                (float)MyCos[rz], (float)-MySin[rz], 0,
                (float)MySin[rz], (float)MyCos[rz], 0,
                0, 0, 1);

            Matrix transform = rotateX.Multiply(rotateY).Multiply(rotateZ);

            int size = 512 * 512;
            double[] zBuffer = new double[size];
            for (int q = 0; q < size; q++)
            {
                zBuffer[q] = -9999999999.9f;
            }

            int wHalf = 512 / 2;
            int hHalf = 512 / 2;

            for (int i = 0; i < Faces.Count; i++)
            {
                Face face = Faces[i];

                uint colour = defaultcolour;

                Vector3f xv = new Vector3f();
                Vector3f yv = new Vector3f();
                Vector3f zv = new Vector3f();

                xv = new Vector3f(Vertices[face.a].x, Vertices[face.a].y, Vertices[face.a].z);
                yv = new Vector3f(Vertices[face.b].x, Vertices[face.b].y, Vertices[face.b].z);
                zv = new Vector3f(Vertices[face.c].x, Vertices[face.c].y, Vertices[face.c].z);

                Vector3f v1 = transform.Transform(xv).Multiply(scaleX, scaleY, scaleZ);
                Vector3f v2 = transform.Transform(yv).Multiply(scaleX, scaleY, scaleZ);
                Vector3f v3 = transform.Transform(zv).Multiply(scaleX, scaleY, scaleZ);

                // Offset model
                v1.x += x;
                v1.y += y;
                v2.x += x;
                v2.y += y;
                v3.x += x;
                v3.y += y;

                Vector3f n1 = transform.Transform(Vertices[face.a].normal.Normalize());
                Vector3f n2 = transform.Transform(Vertices[face.b].normal.Normalize());
                Vector3f n3 = transform.Transform(Vertices[face.c].normal.Normalize());

                Vector3f varying_intensity = new Vector3f();
                Vector3f lightdir = new Vector3f(0.0f, -1.0f, 0.0f);

                varying_intensity.x = Math.Max(0.0f, lightdir.DotProduct(n1));
                varying_intensity.y = Math.Max(0.0f, lightdir.DotProduct(n2));
                varying_intensity.z = Math.Max(0.0f, lightdir.DotProduct(n3));

                double intensity;

                int minX = (int)Math.Max(0.0, Math.Ceiling(Math.Min(v1.x, Math.Min(v2.x, v3.x))));
                int maxX = (int)Math.Min(wHalf * 2 - 1.0, Math.Floor(Math.Max(v1.x, Math.Max(v2.x, v3.x))));
                int minY = (int)Math.Max(0.0, Math.Ceiling(Math.Min(v1.y, Math.Min(v2.y, v3.y))));
                int maxY = (int)Math.Min(hHalf * 2 - 1.0, Math.Floor(Math.Max(v1.y, Math.Max(v2.y, v3.y))));

                if (wireframe)
                {
                    intensity = Math.Min(1.0, Math.Max(0.0, varying_intensity.Distance()));

                    if (intensity > 0.5)
                    {
                        DrawLine((int)v1.x, (int)v1.y, (int)v2.x, (int)v2.y, ColourBlend(colour, 0xFFFFFF, intensity * 2.0 - 1.0));
                        DrawLine((int)v3.x, (int)v3.y, (int)v2.x, (int)v2.y, ColourBlend(colour, 0xFFFFFF, intensity * 2.0 - 1.0));
                    }
                    else
                    {
                        DrawLine((int)v1.x, (int)v1.y, (int)v2.x, (int)v2.y, ColourBlend(colour, 0x000000, (0.5 - intensity)));
                        DrawLine((int)v3.x, (int)v3.y, (int)v2.x, (int)v2.y, ColourBlend(colour, 0x000000, (0.5 - intensity)));
                    }
                }
                else
                {
                    double triangleArea = (v1.y - v3.y) * (v2.x - v3.x) + (v2.y - v3.y) * (v3.x - v1.x);

                    for (int t_y = minY; t_y <= maxY; t_y++)
                    {
                        for (int t_x = minX; t_x <= maxX; t_x++)
                        {
                            double b1 = ((t_y - v3.y) * (v2.x - v3.x) + (v2.y - v3.y) * (v3.x - t_x)) / triangleArea;
                            double b2 = ((t_y - v1.y) * (v3.x - v1.x) + (v3.y - v1.y) * (v1.x - t_x)) / triangleArea;
                            double b3 = ((t_y - v2.y) * (v1.x - v2.x) + (v1.y - v2.y) * (v2.x - t_x)) / triangleArea;
                            if (b1 >= 0 && b1 <= 1 && b2 >= 0 && b2 <= 1 && b3 >= 0 && b3 <= 1)
                            {
                                double depth = b1 * v1.z + b2 * v2.z + b3 * v3.z;
                                // b1, b2, b3 make up "bar"; a Vector3

                                // fragment
                                intensity = varying_intensity.DotProduct((float)b1, (float)b2, (float)b3);

                                int zIndex = t_y * wHalf * 2 + t_x;
                                if (zBuffer[zIndex] < depth)
                                {
                                    if (intensity > 0.5)
                                    {
                                        System.Drawing.Color c = System.Drawing.Color.FromArgb(0xFF, System.Drawing.Color.FromArgb((int)ColourBlend(colour, 0xFFFFFF, intensity * 2.0 - 1.0)));
                                        buffer.SetPixel(t_x, t_y, c);
                                    }
                                    else
                                    {
                                        System.Drawing.Color c = System.Drawing.Color.FromArgb(0xFF, System.Drawing.Color.FromArgb((int)ColourBlend(colour, 0x000000, (0.5 - intensity))));
                                        buffer.SetPixel(t_x, t_y, c);
                                    }
                                    zBuffer[zIndex] = depth;
                                }
                            }
                        }
                    }
                }
            }

            return buffer;
        }

        uint ColourBlend(uint colour1, uint colour2, double percent)
        {
            double inv = 1.0 - percent;
            int r = (int)((colour1 >> 16 & 0xFF) * inv + (colour2 >> 16 & 0xFF) * percent) << 16;
            int g = (int)((colour1 >> 8 & 0xFF) * inv + (colour2 >> 8 & 0xFF) * percent) << 8;
            int b = (int)((colour1 & 0xFF) * inv + (colour2 & 0xFF) * percent);
            return (uint)(r | g | b);
        }

        void DrawLine(int x0, int y0, int x1, int y1, uint col)
        {
            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;

            while (true)
            {
                System.Drawing.Color c = System.Drawing.Color.FromArgb(0xFF, System.Drawing.Color.FromArgb((int)col));
                buffer.SetPixel(x0, y0, c);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;

                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

    }
}
