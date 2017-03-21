using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using ProtoBuf;

namespace SerialisationTests
{
    class Program
    {
        private static int arraySize = 1000;
        private static int timesSerialized = 1000;
        static readonly Stopwatch stopwatch = new Stopwatch();
        private static KinectData[] data;
        private static KinectData[] result = null;
        static byte[] serializedData = null;

        private static MemoryStream memoryStream = new MemoryStream();
        static readonly BinaryFormatter binaryFormatter = new BinaryFormatter();
        private static int sizeBinaryFormatter = 0;
        private static double timeBinaryFormatter = 0;

        private static string jsonDataString = null;
        private static int sizeJson = 0;
        private static double timeJson = 0;

        private static int sizeProtobuf = 0;
        private static double timeProtobuf = 0;

        static void Main(string[] args)
        {

            data = new KinectData[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                data[i] = new KinectData(i, new Position(1, 2, 3), "KinectData");
            }

            Console.WriteLine("Serialisation for " + arraySize + " array size");
            Console.WriteLine("Serialized " + timesSerialized + " times");
            Console.WriteLine("-------------------------------------------- \n");

            stopwatch.Reset();



            stopwatch.Start();
            for (int i = 0; i < timesSerialized; i++)
            {
                protobufSerialize();
                resetData();
            }
            stopwatch.Stop();
            timeProtobuf = stopwatch.ElapsedMilliseconds;
            stopwatch.Reset();

            Console.WriteLine("Protobuf average results");
            Console.WriteLine("_______________________");
            Console.WriteLine("Time: " + timeProtobuf / timesSerialized + " ms with " + sizeProtobuf / timesSerialized + " bytes \n \n");

            stopwatch.Start();
            for (int i = 0; i < timesSerialized; i++)
            {
                binaryFormatterSerialize();
                resetData();
            }
            stopwatch.Stop();
            timeBinaryFormatter = stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Reset();

            Console.WriteLine("BinaryFormatter average results");
            Console.WriteLine("_______________________");
            Console.WriteLine("Time: " + (timeBinaryFormatter / timesSerialized) + " ms with " + sizeBinaryFormatter / timesSerialized + " bytes \n \n");

            stopwatch.Start();
            for (int i = 0; i < timesSerialized; i++)
            {
                jsonSerialize();
                resetData();
            }
            stopwatch.Stop();
            timeJson = stopwatch.Elapsed.TotalMilliseconds;
            stopwatch.Reset();

            Console.WriteLine("Json average results");
            Console.WriteLine("_______________________");
            Console.WriteLine("Time: " + timeJson / timesSerialized + " ms with " + sizeJson / timesSerialized + " bytes \n \n");


            Console.ReadLine();

        }


        private static void protobufSerialize()
        {
            Serializer.Serialize(memoryStream, data);
            serializedData = memoryStream.ToArray();
            sizeProtobuf += serializedData.Length;


            memoryStream.Position = 0;

            result = Serializer.Deserialize<KinectData[]>(memoryStream);
        }

        private static void jsonSerialize()
        {
            jsonDataString = JsonConvert.SerializeObject(data);
            serializedData = Encoding.ASCII.GetBytes(jsonDataString);
            sizeJson += serializedData.Length;


            result = JsonConvert.DeserializeObject<KinectData[]>(Encoding.ASCII.GetString(serializedData));
        }

        private static void binaryFormatterSerialize()
        {
            binaryFormatter.Serialize(memoryStream, data);
            serializedData = memoryStream.ToArray();
            sizeBinaryFormatter += serializedData.Length;


            memoryStream.Position = 0;


            result = (KinectData[])binaryFormatter.Deserialize(memoryStream);
        }

        private static void resetData()
        {
            serializedData = null;
            result = null;
            memoryStream = null;
            memoryStream = new MemoryStream();
        }
    }
}
