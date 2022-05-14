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
        public class Vertex
        {
            /// <summary>
            /// Vertex X
            /// </summary>
            public float x = 0.0f;
            /// <summary>
            /// Vertex Y
            /// </summary>
            public float y = 0.0f;
            /// <summary>
            /// Vertex Z
            /// </summary>
            public float z = 0.0f;

            /// <summary>
            /// Vertex Normal X
            /// </summary>
            public float nx = 0.0f;
            /// <summary>
            /// Vertex Normal Y
            /// </summary>
            public float ny = 0.0f;
            /// <summary>
            /// Vertex Normal Z
            /// </summary>
            public float nz = 0.0f;

            public Vertex() { }
        }

        public class TextureUV
        {
            public float u = 0;
            public float v = 0;

            public TextureUV() { }
        }

        public class Frame
        {
            public List<Vertex> vertices = new List<Vertex>();

            public Frame() { }
        }

        /// <summary>
        /// the signature of the file format
        /// </summary>
        private static readonly byte[] signature = new byte[] { (byte)'M', (byte)'D', (byte)'L', 0 };

        /// <summary>
        /// internal "flags" value to contain the values for the different model flags
        /// </summary>
        private byte flags = 0;

        /// <summary>
        /// determines if the model uses vertex normals or not
        /// </summary>
        public bool hasNormals
        {
            get { return GetBit(flags, 0); }
            set { flags = (byte)SetBit(0, value, flags); }
        }
        /// <summary>
        /// determines if the model uses textures or not (this feature is not implimented in RSDKv5, so its basically useless)
        /// </summary>
        public bool hasTextures
        {
            get { return GetBit(flags, 1); }
            set { flags = (byte)SetBit(1, value, flags); }
        }
        /// <summary>
        /// determines if the model uses vertex colors or not
        /// </summary>
        public bool hasColors
        {
            get { return GetBit(flags, 2); }
            set { flags = (byte)SetBit(2, value, flags); }
        }
        /// <summary>
        /// is it using quads (4) or tris (3)
        /// </summary>
        public byte faceVertexCount = 3;
        /// <summary>
        /// the list of all the texture UV positions
        /// </summary>
        public List<TextureUV> textureUVs = new List<TextureUV>();
        /// <summary>
        /// a list of all the colors for each face
        /// </summary>
        public List<Color> colors = new List<Color>();
        /// <summary>
        /// the list of frames, used to animate the model
        /// </summary>
        public List<Frame> frames = new List<Frame>();
        /// <summary>
        /// the list of all the indices in the model
        /// </summary>
        public List<ushort> indices = new List<ushort>();

        private static bool GetBit(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private static int SetBit(int pos, bool set, int val)
        {
            if (set)
                val |= 1 << pos;
            if (!set)
                val &= ~(1 << pos);
            return val;
        }

        public Model() { }

        public Model(string filename) : this(new Reader(filename)) { }

        public Model(Stream stream) : this(new Reader(stream)) { }

        public Model(Reader reader)
        {
            Read(reader);
        }

        public void Read(Reader reader)
        {
            if (!reader.ReadBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid Model v5 signature");
            }

            flags = reader.ReadByte();
            faceVertexCount = reader.ReadByte();
            if (faceVertexCount != 3 && faceVertexCount != 4)
                throw new Exception("Detected Vertex Type wasn't Tris or Quads! RSDKv5 doesn't support other N-gons!");

            ushort vertexCount = reader.ReadUInt16();
            ushort frameCount = reader.ReadUInt16();

            textureUVs.Clear();
            if (hasTextures)
            {
                for (int t = 0; t < vertexCount; ++t)
                {
                    TextureUV uv = new TextureUV();
                    uv.u = reader.ReadSingle();
                    uv.v = reader.ReadSingle();
                    textureUVs.Add(uv);
                }
            }

            colors.Clear();
            if (hasColors)
            {
                for (int c = 0; c < vertexCount; ++c)
                {
                    Color color = new Color();
                    color.b = reader.ReadByte();
                    color.g = reader.ReadByte();
                    color.r = reader.ReadByte();
                    color.a = reader.ReadByte();
                    colors.Add(color);
                }
            }

            ushort indexCount = reader.ReadUInt16();
            indices.Clear();
            for (int i = 0; i < indexCount; ++i)
                indices.Add(reader.ReadUInt16());

            frames.Clear();
            for (int f = 0; f < frameCount; ++f)
            {
                Frame frame = new Frame();
                for (int v = 0; v < vertexCount; ++v)
                {
                    Vertex vert = new Vertex();
                    vert.x = reader.ReadSingle();
                    vert.y = reader.ReadSingle();
                    vert.z = reader.ReadSingle();

                    if (hasNormals)
                    {
                        vert.nx = reader.ReadSingle();
                        vert.ny = reader.ReadSingle();
                        vert.nz = reader.ReadSingle();
                    }
                    frame.vertices.Add(vert);
                }
                frames.Add(frame);
            }
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
            writer.Write(signature);
            writer.Write(flags);

            int vertCount = frames.Count >= 0 ? frames[0].vertices.Count : 0;
            writer.Write(faceVertexCount);
            writer.Write((ushort)vertCount);
            writer.Write((ushort)frames.Count);

            if (hasTextures)
            {
                for (int v = 0; v < vertCount; ++v)
                {
                    writer.Write(textureUVs[v].u);
                    writer.Write(textureUVs[v].v);
                }
            }

            if (hasColors)
            {
                for (int v = 0; v < vertCount; ++v)
                {
                    writer.Write(colors[v].b);
                    writer.Write(colors[v].g);
                    writer.Write(colors[v].r);
                    writer.Write(colors[v].a);
                }
            }

            writer.Write((ushort)indices.Count);
            for (int i = 0; i < indices.Count; ++i)
                writer.Write(indices[i]);

            for (int f = 0; f < frames.Count; ++f)
            {
                for (int v = 0; v < vertCount; ++v)
                {
                    writer.Write(frames[f].vertices[v].x);
                    writer.Write(frames[f].vertices[v].y);
                    writer.Write(frames[f].vertices[v].z);

                    if (hasNormals)
                    {
                        writer.Write(frames[f].vertices[v].nx);
                        writer.Write(frames[f].vertices[v].ny);
                        writer.Write(frames[f].vertices[v].nz);
                    }
                }
            }
            writer.Close();
        }

        public void WriteAsOBJ(string filename, int exportFrame = -1)
        {
            for (int f = (exportFrame < 0 ? 0 : exportFrame); f < frames.Count; ++f)
            {
                string path = filename;
                string extLess = path.Replace(Path.GetExtension(path), "");
                string streamName = extLess + (frames.Count > 1 ? (" Frame " + f + "") : "") + Path.GetExtension(path);

                StringBuilder builder = new StringBuilder();

                WriteMTL(streamName.Replace(Path.GetFileName(streamName), Path.GetFileNameWithoutExtension(streamName) + ".mtl"));

                if (hasColors)
                {
                    // Link Material
                    builder.AppendLine("mtllib " + Path.GetFileNameWithoutExtension(streamName) + ".mtl");
                    builder.AppendLine("");
                }

                // Object
                builder.AppendLine("o RSDKModelv5");

                builder.AppendLine("");
                for (int v = 0; v < frames[f].vertices.Count; ++v)
                    builder.AppendLine(string.Format("v {0} {1} {2}", frames[f].vertices[v].x, frames[f].vertices[v].y, frames[f].vertices[v].z));

                builder.AppendLine("");
                for (int v = 0; v < frames[f].vertices.Count; ++v)
                    builder.AppendLine(string.Format("vn {0} {1} {2}", frames[f].vertices[v].nx, frames[f].vertices[v].ny, frames[f].vertices[v].nz));

                builder.AppendLine("");
                for (int t = 0; t < textureUVs.Count; ++t)
                    builder.AppendLine(string.Format("vt {0} {1}", textureUVs[t].u, textureUVs[t].v));

                builder.AppendLine("");
                builder.AppendLine("usemtl None");
                builder.AppendLine("s off");
                builder.AppendLine("");

                for (int i = 0; i < indices.Count; i += faceVertexCount)
                {
                    List<ushort> verts = new List<ushort>();
                    for (int v = 0; v < faceVertexCount; ++v) verts.Add(indices[i + v]);

                    if (hasColors)
                        builder.AppendLine($"usemtl RSDKModelv5.Color.{verts[0]}");
                    builder.AppendLine(string.Format("f {0} {1} {2}", verts[0] + 1, verts[1] + 1, verts[2] + 1));
                    if (faceVertexCount == 4)
                        builder.AppendLine(string.Format("f {0} {1} {2}", verts[0] + 1, verts[2] + 1, verts[3] + 1));
                    builder.AppendLine("");
                }

                File.WriteAllText(streamName, builder.ToString());
            }
        }

        private void WriteMTL(string fileName)
        {
            StringBuilder builder = new StringBuilder();

            Writer writer = new Writer(fileName);
            for (int i = 0; i < colors.Count; ++i)
            {
                builder.AppendLine($"newmtl RSDKModelv5.Color.{i}");
                builder.AppendLine($"kd {(colors[i].r / 255f)} {(colors[i].g / 255f)} {(colors[i].b / 255f)}");
                builder.AppendLine("");
            }
            writer.Write(Encoding.ASCII.GetBytes(builder.ToString()));
            writer.Close();

        }

        public void WriteAsPLY(string filename, int exportFrame, bool binary = false)
        {
            for (int f = (exportFrame < 0 ? 0 : exportFrame); f < frames.Count; ++f)
            {
                string path = filename;
                string extLess = path.Replace(Path.GetExtension(path), "");
                string streamName = extLess + (frames.Count > 1 ? (" Frame " + f + "") : "") + Path.GetExtension(path);

                Writer writer = new Writer(streamName);

                // Header
                writer.WriteLine("ply");
                writer.WriteLine($"format {(binary ? "binary_little_endian" : "ascii")} 1.0");
                writer.WriteLine("comment Created by RSDK-Reverse");
                writer.WriteLine("element vertex " + frames[f].vertices.Count);

                writer.WriteLine("property float x");
                writer.WriteLine("property float y");
                writer.WriteLine("property float z");
                if (hasNormals)
                {
                    writer.WriteLine("property float nx");
                    writer.WriteLine("property float ny");
                    writer.WriteLine("property float nz");
                }
                if (hasTextures)
                {
                    writer.WriteLine("property float u");
                    writer.WriteLine("property float v");
                }

                if (hasColors)
                {
                    writer.WriteLine("property uint8 red");
                    writer.WriteLine("property uint8 green");
                    writer.WriteLine("property uint8 blue");
                    writer.WriteLine("property uint8 alpha");
                }

                writer.WriteLine("element face " + (indices.Count / faceVertexCount));
                writer.WriteLine("property list uint8 uint16 vertex_indices");
                writer.WriteLine("end_header");

                if (binary)
                {
                    for (int v = 0; v < frames[f].vertices.Count; ++v)
                    {
                        writer.Write(frames[f].vertices[v].x);
                        writer.Write(frames[f].vertices[v].y);
                        writer.Write(frames[f].vertices[v].z);

                        if (hasNormals)
                        {
                            writer.Write(frames[f].vertices[v].nx);
                            writer.Write(frames[f].vertices[v].ny);
                            writer.Write(frames[f].vertices[v].nz);
                        }

                        if (hasTextures)
                        {
                            writer.Write(textureUVs[v].u);
                            writer.Write(textureUVs[v].v);
                        }

                        if (hasColors)
                        {
                            writer.Write(colors[v].r);
                            writer.Write(colors[v].g);
                            writer.Write(colors[v].b);
                            writer.Write(colors[v].a);
                        }
                    }

                    for (int i = 0; i < indices.Count; i += faceVertexCount)
                    {
                        writer.Write(faceVertexCount);
                        for (int v = 0; v < faceVertexCount; ++v) writer.Write(indices[i + v]);
                    }
                }
                else
                {
                    for (int v = 0; v < frames[f].vertices.Count; ++v)
                    {
                        string vertex = "";

                        vertex += $"{frames[f].vertices[v].x} {frames[f].vertices[v].y} {frames[f].vertices[v].z}";

                        if (hasNormals)
                            vertex += $"{frames[f].vertices[v].nx} {frames[f].vertices[v].ny} {frames[f].vertices[v].nz}";

                        if (hasTextures)
                            vertex += $"{textureUVs[v].u} {textureUVs[v].v}";

                        if (hasColors)
                            vertex += $"{colors[v].r} {colors[v].g} {colors[v].b} {colors[v].a}";

                        writer.WriteLine(vertex);
                    }

                    for (int i = 0; i < indices.Count; i += faceVertexCount)
                    {
                        string face = "";

                        face += faceVertexCount + " ";

                        for (int v = 0; v < faceVertexCount; ++v)
                        {
                            face += indices[i + v];
                            if (v + 1 < faceVertexCount)
                                face += " ";
                        }

                        writer.WriteLine(face);
                    }
                }

                writer.Close();
            }
        }
    }
}
