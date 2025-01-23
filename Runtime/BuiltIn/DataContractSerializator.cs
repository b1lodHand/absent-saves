using com.absence.savesystem.internals;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using UnityEngine;

namespace com.absence.savesystem.builtin
{
    /// <summary>
    /// This is the serializator that uses <see cref="DataContractSerializer"/> for saving/loading.
    /// </summary>
    public class DataContractSerializator : Serializator
    {
        private Type m_type;

        /// <summary>
        /// This is the serializator that uses <see cref="DataContractSerializer"/> for saving/loading.
        /// </summary>
        /// <param name="fileName">File name to use.</param>
        /// <param name="type">Real type of the raw data.</param>
        public DataContractSerializator(string fileName, Type type) : base(fileName)
        {
            m_type = type;
        }

        public override bool Deserialize(out object data)
        {
            data = null;
            if (!File.Exists(p_fullPath)) return false;

            try
            {
                using (FileStream stream = File.OpenRead(p_fullPath))
                {
                    DataContractSerializer serializer = new(m_type);
                    XmlDictionaryReader binaryReader = XmlDictionaryReader.CreateBinaryReader(stream, XmlDictionaryReaderQuotas.Max);

                    data = serializer.ReadObject(binaryReader);
                    binaryReader.Close();
                }

                return true;
            }

            catch (Exception e)
            {
                Debug.LogError($"Something went wrong while loading a save file: {e.ToString()}");
                return false;
            }
        }

        public override bool Serialize(object dataToSerialize)
        {
            if (!Directory.Exists(SaveLoadHandler.SaveDirectory)) Directory.CreateDirectory(SaveLoadHandler.SaveDirectory);
            if (File.Exists(p_fullPath)) File.Delete(p_fullPath);

            try
            {
                using (FileStream stream = File.Create(p_fullPath))
                {
                    DataContractSerializer serializer = new(m_type);
                    XmlDictionaryWriter binaryWriter = XmlDictionaryWriter.CreateBinaryWriter(stream);

                    serializer.WriteObject(binaryWriter, dataToSerialize);
                    binaryWriter.Flush();
                    binaryWriter.Close();
                }

                return true;
            }

            catch (Exception e)
            {
                Debug.LogError($"Something went wrong saving the game: {e.ToString()}");
                return false;
            }
        }
    }
}
