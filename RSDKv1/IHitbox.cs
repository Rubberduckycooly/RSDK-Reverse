using System;
using System.IO;

namespace RSDKv1
{
    public interface IHitbox : ICloneable, IBinarySerializable, ISaveChanges
    {
        int Left { get; set; }

        int Top { get; set; }

        int Right { get; set; }

        int Bottom { get; set; }
    }
}
