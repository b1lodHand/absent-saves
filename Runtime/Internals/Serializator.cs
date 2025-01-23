namespace com.absence.savesystem.internals
{
    /// <summary>
    /// This is the base class to derive from when creating custom logic for saving/loading.
    /// </summary>
    public abstract class Serializator
    {
        /// <summary>
        /// File name to use while saving/loading.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The full path calculated from the file name.
        /// </summary>
        protected string p_fullPath => SaveLoadHandler.SaveDirectory + FileName + ".save";

        /// <summary>
        /// This is the base class to derive from when creating custom logic for saving/loading.
        /// </summary>
        /// <param name="fileName">File name to use.</param>
        public Serializator(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Override to define logic for serialization (saving).
        /// </summary>
        /// <param name="dataToSerialize">Raw data to serialize.</param>
        /// <returns>Returns false if anything goes wrong, true otherwise.</returns>
        public abstract bool Serialize(object dataToSerialize);

        /// <summary>
        /// Override to define logic for deserialization (loading).
        /// </summary>
        /// <param name="data">Raw data acquired from the deserialization process.</param>
        /// <returns>Returns false if anything goes wrong, true otherwise.</returns>
        public abstract bool Deserialize(out object data);
    }
}
