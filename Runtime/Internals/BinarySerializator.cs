using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace com.absence.savesystem.internals
{
    internal class BinarySerializator
    {
        internal static readonly string SaveDirectory = Application.persistentDataPath + "/saves/";

        internal static bool Serialize(string fileName, object dataToSerialize)
        {
            try
            {
                BinaryFormatter formatter = GetBinaryFormatter();

                if (!Directory.Exists(SaveDirectory)) Directory.CreateDirectory(SaveDirectory);

                var fullPath = SaveDirectory + fileName + ".save";
                if (File.Exists(fullPath)) File.Delete(fullPath);

                using (FileStream fileToWrite = File.Create(fullPath))
                {
                    formatter.Serialize(fileToWrite, dataToSerialize);
                }
            }

            catch (Exception e)
            {
                Debug.LogError($"An error occurred while saving the game: \n{e.Message}");
                return false;
            }

            return true;
        }
        internal static bool Deserialize(string fileName, out object data)
        {
            data = null;
            var fullPath = SaveDirectory + fileName + ".save";
            if (!File.Exists(fullPath)) return false;

            try
            {
                BinaryFormatter formatter = GetBinaryFormatter();
                using (FileStream fileToRead = File.OpenRead(fullPath))
                {
                    data = formatter.Deserialize(fileToRead);
                }
            }

            catch (Exception e)
            {
                Debug.LogError($"An error occured while loading the save file '{fullPath}': \n{e.Message}");
                return false;
            }

            return true;
        }

        internal static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            SurrogateSelector selector = new SurrogateSelector();

            SurrogateSelectorInitializator.InitializeSurrogatesForSelector(selector);

            formatter.SurrogateSelector = selector;

            return formatter;
        }

    }
}