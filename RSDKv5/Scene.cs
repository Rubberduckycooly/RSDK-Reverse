using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RSDKv5
{
    public class Scene
    {   
        public static readonly byte[] MAGIC = new byte[] { (byte)'S', (byte)'C', (byte)'N', (byte)'\0' };

        public SceneEditorMetadata EditorMetadata;

        public List<SceneLayer> Layers = new List<SceneLayer>();
        public List<SceneObject> Objects = new List<SceneObject>();


        public Scene()
        {
            EditorMetadata = new SceneEditorMetadata();
        }

        public Scene(string filename) : this(new Reader(filename))
        {

        }

        public Scene(Stream stream) : this(new Reader(stream))
        {

        }

        private Scene(Reader reader)
        {
            // Load scene
            if (!reader.ReadBytes(4).SequenceEqual(MAGIC))
                throw new Exception("Invalid scene file header magic");

            EditorMetadata = new SceneEditorMetadata(reader);
            
            byte layers_count = reader.ReadByte();
            for (int i = 0; i < layers_count; ++i)
                Layers.Add(new SceneLayer(reader));

            byte objects_count = reader.ReadByte();
            for (int i = 0; i < objects_count; ++i)
                Objects.Add(new SceneObject(reader));
        }

        public void Write(string filename)
        {
            using (Writer writer = new Writer(filename))
                this.Write(writer);
        }

        public void Write(Stream stream)
        {
            using (Writer writer = new Writer(stream))
                this.Write(writer);
        }

        internal void Write(Writer writer)
        {
            writer.Write(MAGIC);

            EditorMetadata.Write(writer);

            writer.Write((byte)Layers.Count);
            foreach (SceneLayer layer in Layers)
                layer.Write(writer);

            writer.Write((byte)Objects.Count);
            foreach (SceneObject obj in Objects)
                obj.Write(writer);
        }
    }
}
