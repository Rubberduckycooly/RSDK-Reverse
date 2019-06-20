using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RSDKv5
{
    public class Model
    {

        static double[] MyCos = new double[256];
        static double[] MySin = new double[256];

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

            public class Matrix
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

                public Vertex(Reader reader, bool useNormals)
                {
                    x = reader.ReadSingle();
                    y = reader.ReadSingle();
                    z = reader.ReadSingle();

                    if (isQuad)
                    {
                        w = reader.ReadSingle();
                    }

                    normal = new Normal();
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

            Bitmap buffer = new Bitmap(512, 512);

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

            public Bitmap AsImage(Model Parent, float scaleX = 1, float scaleY = 1, float scaleZ = 1, int rx = 0, int ry = 0, int rz = 0, bool wireframe = false, uint defaultcolour = 0xFFFFFFFF)
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

                int FaceID = 0;
                for (int i = 0; i < Parent.Faces.Count; i += Parent.FaceVerticiesCount, FaceID++)
                {
                    int[] face = new int[Parent.FaceVerticiesCount];

                    for (int f = 0; f < Parent.FaceVerticiesCount; f++)
                    {
                        face[f] = Parent.Faces[i + f];
                    }

                    uint colour = defaultcolour;

                    if (Parent.HasColours)
                    {
                        colour = (uint)((255 << 24) + (Parent.Colours[Parent.Faces[i]].r << 16) + (Parent.Colours[Parent.Faces[i]].g << 8) + Parent.Colours[Parent.Faces[i]].b);
                    }

                    Vector3f xv = new Vector3f();
                    Vector3f yv = new Vector3f();
                    Vector3f zv = new Vector3f();

                    if (Parent.FaceVerticiesCount == 4 && !wireframe)
                    {
                        xv = new Vector3f(Vertices[face[0]].x, Vertices[face[0]].y, Vertices[face[0]].z);
                        yv = new Vector3f(Vertices[face[2]].x, Vertices[face[2]].y, Vertices[face[2]].z);
                        zv = new Vector3f(Vertices[face[3]].x, Vertices[face[3]].y, Vertices[face[3]].z);

                        Vector3f q_v1 = transform.Transform(xv).Multiply(scaleX, scaleY, scaleZ);
                        Vector3f q_v2 = transform.Transform(yv).Multiply(scaleX, scaleY, scaleZ);
                        Vector3f q_v3 = transform.Transform(zv).Multiply(scaleX, scaleY, scaleZ);

                        // Offset model
                        q_v1.x += x;
                        q_v1.y += y;
                        q_v2.x += x;
                        q_v2.y += y;
                        q_v3.x += x;
                        q_v3.y += y;

                        Vector3f q_n1 = transform.Transform(Vertices[face[0]].normal.Normalize());
                        Vector3f q_n2 = transform.Transform(Vertices[face[2]].normal.Normalize());
                        Vector3f q_n3 = transform.Transform(Vertices[face[3]].normal.Normalize());

                        Vector3f q_varying_intensity = new Vector3f();
                        Vector3f q_lightdir = new Vector3f(0.0f, -1.0f, 0.0f);

                        q_varying_intensity.x = Math.Max(0.0f, q_lightdir.DotProduct(q_n1));
                        q_varying_intensity.y = Math.Max(0.0f, q_lightdir.DotProduct(q_n2));
                        q_varying_intensity.z = Math.Max(0.0f, q_lightdir.DotProduct(q_n3));

                        double q_intensity = q_varying_intensity.Distance();

                        int q_minX = (int)Math.Max(0.0, Math.Ceiling(Math.Min(q_v1.x, Math.Min(q_v2.x, q_v3.x))));
                        int q_maxX = (int)Math.Min(wHalf * 2 - 1.0, Math.Floor(Math.Max(q_v1.x, Math.Max(q_v2.x, q_v3.x))));
                        int q_minY = (int)Math.Max(0.0, Math.Ceiling(Math.Min(q_v1.y, Math.Min(q_v2.y, q_v3.y))));
                        int q_maxY = (int)Math.Min(hHalf * 2 - 1.0, Math.Floor(Math.Max(q_v1.y, Math.Max(q_v2.y, q_v3.y))));

                        double triangleArea = (q_v1.y - q_v3.y) * (q_v2.x - q_v3.x) + (q_v2.y - q_v3.y) * (q_v3.x - q_v1.x);

                        for (int q_y = q_minY; q_y <= q_maxY; q_y++)
                        {
                            for (int q_x = q_minX; q_x <= q_maxX; q_x++)
                            {
                                double b1 = ((q_y - q_v3.y) * (q_v2.x - q_v3.x) + (q_v2.y - q_v3.y) * (q_v3.x - q_x)) / triangleArea;
                                double b2 = ((q_y - q_v1.y) * (q_v3.x - q_v1.x) + (q_v3.y - q_v1.y) * (q_v1.x - q_x)) / triangleArea;
                                double b3 = ((q_y - q_v2.y) * (q_v1.x - q_v2.x) + (q_v1.y - q_v2.y) * (q_v2.x - q_x)) / triangleArea;
                                if (b1 >= 0 && b1 <= 1 && b2 >= 0 && b2 <= 1 && b3 >= 0 && b3 <= 1)
                                {
                                    double depth = b1 * q_v1.z + b2 * q_v2.z + b3 * q_v3.z;

                                    q_intensity = (double)q_varying_intensity.DotProduct((float)b1, (float)b2, (float)b3);

                                    int zIndex = q_y * wHalf * 2 + q_x;
                                    if (zBuffer[zIndex] < depth)
                                    {
                                        if (q_intensity > 0.5)
                                        {
                                            System.Drawing.Color c = System.Drawing.Color.FromArgb(0xFF, System.Drawing.Color.FromArgb((int)ColourBlend(colour, 0xFFFFFF, q_intensity * 2.0 - 1.0)));
                                            buffer.SetPixel(q_x, q_y, c);
                                        }
                                        else
                                        {
                                            System.Drawing.Color c = System.Drawing.Color.FromArgb(0xFF, System.Drawing.Color.FromArgb((int)ColourBlend(colour, 0x000000, (0.5 - q_intensity))));
                                            buffer.SetPixel(q_x, q_y, c);
                                        }
                                        zBuffer[zIndex] = depth;
                                    }
                                }
                            }
                        }
                    }

                    xv = new Vector3f(Vertices[face[0]].x, Vertices[face[0]].y, Vertices[face[0]].z);
                    yv = new Vector3f(Vertices[face[1]].x, Vertices[face[1]].y, Vertices[face[1]].z);
                    zv = new Vector3f(Vertices[face[2]].x, Vertices[face[2]].y, Vertices[face[2]].z);

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

                    Vector3f n1 = transform.Transform(Vertices[face[0]].normal.Normalize());
                    Vector3f n2 = transform.Transform(Vertices[face[1]].normal.Normalize());
                    Vector3f n3 = transform.Transform(Vertices[face[1]].normal.Normalize());

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

        /// <summary>
        /// the file's signtature
        /// </summary>
        public static readonly byte[] MAGIC = new byte[] { (byte)'M', (byte)'D', (byte)'L', (byte)'\0' };
        /// <summary>
        /// does it have Normals?
        /// </summary>
        public bool HasNormals = true;
        /// <summary>
        /// does it have Textures?
        /// </summary>
        public bool HasTextures = false;
        /// <summary>
        /// is it using Colours?
        /// </summary>
        public bool HasColours = true;
        /// <summary>
        /// is it using quads (4) or tris (3)
        /// </summary>
        public byte FaceVerticiesCount = 3; //Tri (3) or Quad (4)?
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
        /// a list of all the face
        /// </summary>
        public List<short> Faces = new List<short>();

        public Model()
        {
            AddFrame();
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

            for (int a = 0; a < 256; a++)
            {
                MySin[a] = -Math.Sin(a * Math.PI / 128);
                MyCos[a] = Math.Cos(a * Math.PI / 128);
            }

            byte flags = reader.ReadByte();
            //HasNormals = (flags & 0x00000001) != 0;
            //HasUnknown = (flags & 0x00000010) != 0;
            //HasColours = (flags & 0x00000100) != 0;

            HasNormals = GetBit(flags, 0);
            HasTextures = GetBit(flags, 1);
            HasColours = GetBit(flags, 2);

            Console.WriteLine("MDL READ: FLAGS:" + Pad(flags));

            string Pad(byte b)
            {
                return Convert.ToString(b, 2).PadLeft(8, '0');
            }


            FaceVerticiesCount = reader.ReadByte();
            if (FaceVerticiesCount != 3 && FaceVerticiesCount != 4)
                throw new Exception("Detected Vertex Type wasn't Tris or Quads! RSDKv5 doesn't support other N-gons!");

            ushort VertexCount = 0;
            ushort FramesCount = 0;
            short FaceCount = 0;

            VertexCount = reader.ReadUInt16();
            FramesCount = reader.ReadUInt16();

            Console.WriteLine("Frame Count: " + FramesCount + Environment.NewLine + "Vertex Count: " + VertexCount);

            if (HasTextures)
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

            //Console.WriteLine("MDL READ: FileSize: {0}, Position: 0x{1:X8}, DataLeft: {2}", reader.BaseStream.Length, reader.BaseStream.Position, reader.BaseStream.Length - reader.BaseStream.Position);
        }

        public void Write(Writer writer)
        {
            writer.Write(MAGIC);
            byte flags = 0;
            //flags |= (byte)(HasColours ? 0x00000001 : 0);
            //flags |= (byte)(HasUnknown ? 0x00000010 : 0);
            //flags |= (byte)(HasNormals ? 0x00000100 : 0);
            flags = (byte)SetBit(0, HasNormals, flags);
            flags = (byte)SetBit(1, HasTextures, flags);
            flags = (byte)SetBit(2, HasColours, flags);
            writer.Write(flags);

            //Console.WriteLine("MDL WRITE: FLAGS:" + Pad(flags));

            string Pad(byte b)
            {
                return Convert.ToString(b, 2).PadLeft(8, '0');
            }

            //Console.WriteLine("MDL WRITE: Flags: {0}, HasColours: {1}, HasUnknown: {2}, HasNormals: {3}", flags, HasColours, HasTextures, HasNormals);

            writer.Write(FaceVerticiesCount);
            writer.Write((ushort)Frames[0].Vertices.Count);
            writer.Write((ushort)Frames.Count);

            if (HasTextures)
                for (int i = 0; i < Frames[0].Vertices.Count; ++i)
                    TexturePositions[i].Write(writer);

            if (HasColours)
                for (int i = 0; i < Frames[0].Vertices.Count; ++i)
                    Colours[i].Write(writer);

            writer.Write((ushort)Faces.Count);
            for (int i = 0; i < Faces.Count; ++i)
                writer.Write(Faces[i]);

            for (int i = 0; i < Frames.Count; ++i)
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
                for (int v = 0; v < Faces.Count; v += FaceVerticiesCount)
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
            // Shows I been here lol - SS16
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

            writer.Write((int)(Faces.Count / FaceVerticiesCount));
            // Triangle

            var vertices = new Frame.Vertex[FaceVerticiesCount];
            for (int v = 0; v < Faces.Count; v += FaceVerticiesCount)
            {
                for (int ii = 0; ii < FaceVerticiesCount; ++ii)
                    vertices[ii] = Frames[0].Vertices[Faces[v + ii]];

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
            for (int i = 0; i < Frames.Count; i++)
            {
                string tmp = Path.GetExtension(filename);
                string tmp2 = filename.Replace(tmp, "");
                string streamName = tmp2 + " Frame " + i + tmp;
                using (var writer = new StreamWriter(File.Create(streamName)))
                {
                    writer.WriteLine("solid obj");
                    var vertices = new Frame.Vertex[FaceVerticiesCount];
                    for (int v = 0; v < Faces.Count; v += FaceVerticiesCount)
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
            Frames.Add(F);
        }

        public void DeleteFrame(int index)
        {
            int VertexCnt = Frames[index].Vertices.Count - 1;
            Frames.RemoveAt(index);
        }
    }
}
