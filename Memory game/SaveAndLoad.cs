using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Memory_game
{
    /// <summary>
    /// Static class used to save and load data
    /// </summary>
    public static class SaveAndLoad
    {
        /// Get the path to the saves map
        private static string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        public static string path = projectDirectory + "/Saves/";

        /// <summary>
        /// Write the data from the objectToWrite to a binary file
        /// </summary>
        /// <typeparam name="T">The type of object you want to save</typeparam>
        /// <param name="fileName">string name of the file</param>
        /// <param name="objectToWrite">object instance, with all the data you want to save</param>
        /// <param name="append">bool used to check if you want to add or overwrite the current savefile</param>
        public static void WriteToBinairyFile<T>(string fileName, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(path + fileName, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        /// <summary>
        /// Read the data from a binary file
        /// </summary>
        /// <typeparam name="T">The type of object you want to read</typeparam>
        /// <param name="fileName">string name of the file</param>
        /// <returns>The data from the binary file in the type you gave</returns>
        public static T ReadFromBinaryFile<T>(string fileName)
        {
            using(Stream stream = File.Open(path + fileName, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }
    }

    /// <summary>
    /// Class used to save and read all the data used when you make a new game
    /// </summary>
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
