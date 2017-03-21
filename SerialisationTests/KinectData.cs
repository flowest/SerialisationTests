using System;
using Newtonsoft.Json;
using ProtoBuf;

namespace SerialisationTests
{
    [Serializable, ProtoContract]
    public class KinectData : ISerializeObject
    {
        [ProtoMember(1)]
        public string dataType { get; set; }
        [ProtoMember(2)]
        public int kinectID;
        [ProtoMember(3)]
        public Position position;

        public KinectData(int kinectId, Position position, string dataType)
        {
            kinectID = kinectId;
            this.position = position;
            this.dataType = dataType;
        }

        public KinectData(KinectData kinectData)
        {
            this.position = kinectData.position;
            this.dataType = kinectData.dataType;
            this.position = kinectData.position;
        }

        public KinectData()
        {
            
        }
    }
}
