using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Memory_game
{
    public static class SaveAndLoad
    {
        private static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        public static string path = projectDirectory + "/Saves";

        //private GameData gameData;

        public static void WriteToBinairyFile<T>(T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(path, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>()
        {
            using(Stream stream = File.Open(path, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }

    [Serializable]
    public class SaveData
    {
        public string namePlayerOne;
        public string namePlayerTwo;

        public int scorePlayerOne;
        public int scorePlayerTwo;

        public float timer;

        public List<MemoryCard> GameCards;
        public List<MemoryCard> MemoryCards;
    }
}
