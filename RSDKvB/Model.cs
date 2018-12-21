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

        public class Colour
        {
            public byte b;
            public byte g;
            public byte r;
            public byte a;

            public Colour()
            {

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
        public class Vertex
        {
            public float x;
            public float y;
            public float z;

            public List<Normal> Normals = new List<Normal>();

            public class Normal
            {
                public float x;
                public float y;
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

            public Vertex(Reader reader, bool useNormals)
            {
                x = reader.ReadSingle();
                y = reader.ReadSingle();
                z = reader.ReadSingle();

                if (useNormals)
                {
                    Normals.Add(new Normal(reader));
                }
            }

            public void Write(Writer writer)
            {
                writer.Write(x);
                writer.Write(y);
                writer.Write(z);
            }

        }

        public List<Colour> Colours = new List<Colour>();

        public List<Vertex> Vertices = new List<Vertex>();

        public List<short> Faces = new List<short>();

        public short FaceCount;
        public ushort VertexCount;

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

            VertexCount = reader.ReadUInt16();

            for (int i = 0; i < VertexCount; i++)
            {
                reader.ReadSingle();
                reader.ReadSingle();
            }

            FaceCount = reader.ReadInt16();
            for (int i = 0; i < FaceCount; ++i)
                Faces.Add(reader.ReadInt16());

            var vc = reader.ReadInt16();

            if (vc == 1)
            {
                for (int i = 0; i < vc; i++)
                {
                    Vertices.Add(new Vertex(reader,true));
                }
            }
            else
            {
                for (int i = 0; i < vc; i++)
                {
                    Vertices.Add(new Vertex(reader, true));
                }
            }

            Console.WriteLine("File Size: " + reader.BaseStream.Length + " Reader Pos: " + reader.BaseStream.Position + " Data Left: " + (reader.BaseStream.Length - reader.BaseStream.Position));

        }
    }
}
