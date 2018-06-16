using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSDKv3
{
    // NOTE!!!
    // IBinaryWriteSerializable is very different from ISaveChanges.
    // IBinaryWriteSerializable writes an object, saving all the necessary
    // information to re-create the original context.
    // ISaveChanges saves information without knowing the context, so the data
    // can be used only with a parent structure.
    // An example is RSDK5.AnimationEntry, where collisionBoxesCount is saved
    // with IBinaryWriteSerializable but not with ISaveChanges.

    public interface IBinaryReadSerializable
    {
        void Read(BinaryReader reader);
    }
    public interface IBinaryWriteSerializable
    {
        void Write(BinaryWriter writer);
    }

    public interface IBinarySerializable :
        IBinaryReadSerializable,
        IBinaryWriteSerializable
    {
    }

    public interface ISaveChanges
    {
        void SaveChanges(BinaryWriter writer);
    }

    public static class BinarySerializableExtension
    {
        public static void Read(this BinaryReader reader, IBinaryReadSerializable item)
        {
            item.Read(reader);
        }
        public static void Write(this BinaryWriter writer, IBinaryWriteSerializable item)
        {
            item.Write(writer);
        }
    }
}
