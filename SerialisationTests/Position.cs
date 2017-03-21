using System;
using ProtoBuf;

namespace SerialisationTests
{
    [Serializable, ProtoContract]
    public class Position
    {
        [ProtoMember(1)]
        public float X;

        [ProtoMember(2)]
        public float Y;

        [ProtoMember(3)]
        public float Z;

        public Position(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Position()
        {
            
        }
    }
}
