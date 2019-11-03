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
        private static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static string path = projectDirectory + "/Saves/";

        public static void WriteToBinairyFile<T>(string fileName, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(path + fileName, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string fileName)
        {
            using(Stream stream = File.Open(path + fileName, FileMode.Open))
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

        public int rows;
        public int colums;

        public int timer;
        public bool playerTwo;

        public List<MemoryCard> GameCards;
        public List<MemoryCard> MemoryCards;
    }
}
