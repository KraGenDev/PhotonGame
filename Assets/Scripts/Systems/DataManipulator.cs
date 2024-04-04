using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DefaultNamespace.DTO;

namespace Systems
{
    public class DataManipulator
    {
        public static byte[] SerializeUserData(object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, userData);
                return memoryStream.ToArray();
            }
        }
        
        public static object DeserializeUserData(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return binaryFormatter.Deserialize(memoryStream);
            }
        }
        
        public static byte[] SerializeListToByteArray(object obj)
        {
            if (!(obj is List<UserData> userList))
                throw new ArgumentException("The input object is not a List<UserData>");

            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, userList);
                return memoryStream.ToArray();
            }
        }

        public static List<UserData> DeserializeListFromByteArray(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                return (List<UserData>)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}