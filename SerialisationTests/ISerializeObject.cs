using ProtoBuf;

namespace SerialisationTests
{
    [ProtoContract]
    [ProtoInclude(500, typeof(KinectData))]
    public interface ISerializeObject
    {
        string dataType
        {
            get;
            set;
        }
    }
}
