using com.absence.savesystem.internals;
using com.absence.savesystem.internals.legacy;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace com.absence.savesystem.builtin.legacy
{
    /// <summary>
    /// The serializator that uses <see cref="BinaryFormatter"/> to save/load.
    /// </summary>
    public class BinarySerializatorLegacy : Serializator
    {
        /// <summary>
        /// The serializator that uses <see cref="BinaryFormatter"/> to save/load.
        /// </summary>
        /// <param name="fileName">File name to use.</param>
        public BinarySerializatorLegacy(string fileName) : base(fileName)
        {
        }

        public override bool Serialize(object dataToSerialize)
        {
            try
            {
                BinaryFormatter formatter = GetBinaryFormatter();

                if (!Directory.Exists(SaveLoadHandler.SaveDirectory)) Directory.CreateDirectory(SaveLoadHandler.SaveDirectory);

                var fullPath = SaveLoadHandler.SaveDirectory + FileName + ".save";
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
        public override bool Deserialize(out object data)
        {
            data = null;
            if (!File.Exists(p_fullPath)) return false;

            try
            {
                BinaryFormatter formatter = GetBinaryFormatter();
                using (FileStream fileToRead = File.OpenRead(p_fullPath))
                {
                    data = formatter.Deserialize(fileToRead);
                }
            }

            catch (Exception e)
            {
                Debug.LogError($"An error occured while loading the save file '{p_fullPath}': \n{e.Message}");
                return false;
            }

            return true;
        }

        private static BinaryFormatter GetBinaryFormatter()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            SurrogateSelector selector = new SurrogateSelector();

            SurrogateSelectorInitializator.InitializeSurrogatesForSelector(selector);

            formatter.SurrogateSelector = selector;

            return formatter;
        }
    }
}