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
            get { return getBit(flags, 0); }
            set { flags = (byte)setBit(0, value, flags); }
        }
        /// <summary>
        /// determines if the model uses textures or not (this feature is not implimented in RSDKv5, so its basically useless)
        /// </summary>
        public bool hasTextures
        {
            get { return getBit(flags, 1); }
            set { flags = (byte)setBit(1, value, flags); }
        }
        /// <summary>
        /// determines if the model uses vertex colours or not
        /// </summary>
        public bool hasColours
        {
            get { return getBit(flags, 2); }
            set { flags = (byte)setBit(2, value, flags); }
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
        /// a list of all the colours for each face
        /// </summary>
        public List<Color> colours = new List<Color>();
        /// <summary>
        /// the list of frames, used to animate the model
        /// </summary>
        public List<Frame> frames = new List<Frame>();
        /// <summary>
        /// the list of all the indices in the model
        /// </summary>
        public List<ushort> indices = new List<ushort>();

        private static bool getBit(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private static int setBit(int pos, bool set, int val)
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
            read(reader);
        }

        public void read(Reader reader)
        {
            if (!reader.readBytes(4).SequenceEqual(signature))
            {
                reader.Close();
                throw new Exception("Invalid Model v5 signature");
            }

            flags = reader.ReadByte();
            faceVertexCount = reader.ReadByte();
            if (faceVertexCount != 3 && faceVertexCount != 4)
                throw new Exception("Detected Vertex Type wasn't Tris or Quads! RSDKv5 doesn't support other N-gons!");

            ushort vertexCount = reader.ReadUInt16();
            ushort frameCount  = reader.ReadUInt16();

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

            colours.Clear();
            if (hasColours)
            {
                for (int c = 0; c < vertexCount; ++c)
                {
                    Color colour = new Color();
                    colour.B = reader.ReadByte();
                    colour.G = reader.ReadByte();
                    colour.R = reader.ReadByte();
                    colour.A = reader.ReadByte();
                    colours.Add(colour);
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

        public void write(string filename)
        {
            using (Writer writer = new Writer(filename))
                write(writer);
        }

        public void write(System.IO.Stream stream)
        {
            using (Writer writer = new Writer(stream))
                write(writer);
        }

        public void write(Writer writer)
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

            if (hasColours)
            {
                for (int v = 0; v < vertCount; ++v)
                {
                    writer.Write(colours[v].B);
                    writer.Write(colours[v].G);
                    writer.Write(colours[v].R);
                    writer.Write(colours[v].A);
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

        public void writeAsOBJ(string filename, int exportFrame = -1)
        {
            for (int f = (exportFrame < 0 ? 0 : exportFrame); f < frames.Count; ++f)
            {
                string path = filename;
                string extLess = path.Replace(Path.GetExtension(path), "");
                string streamName = extLess + (frames.Count > 1 ? (" Frame " + f + "") : "") + Path.GetExtension(path);

                StringBuilder builder = new StringBuilder();

                writeMTL(streamName.Replace(Path.GetFileName(streamName), Path.GetFileNameWithoutExtension(streamName) + ".mtl"));

                if (hasColours)
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

                    if (hasColours)
                        builder.AppendLine($"usemtl RSDKModelv5.Colour.{verts[0]}");
                    builder.AppendLine(string.Format("f {0} {1} {2}", verts[0] + 1, verts[1] + 1, verts[2] + 1));
                    if (faceVertexCount == 4)
                        builder.AppendLine(string.Format("f {0} {1} {2}", verts[0] + 1, verts[2] + 1, verts[3] + 1));
                    builder.AppendLine("");
                }

                File.WriteAllText(streamName, builder.ToString());
            }
        }

        private void writeMTL(string fileName)
        {
            StringBuilder builder = new StringBuilder();

            Writer writer = new Writer(fileName);
            for (int i = 0; i < colours.Count; ++i)
            {
                builder.AppendLine($"newmtl RSDKModelv5.Colour.{i}");
                builder.AppendLine($"kd {(colours[i].R / 255f)} {(colours[i].G / 255f)} {(colours[i].B / 255f)}");
                builder.AppendLine("");
            }
            writer.Write(Encoding.ASCII.GetBytes(builder.ToString()));
            writer.Close();

        }
    }
}
