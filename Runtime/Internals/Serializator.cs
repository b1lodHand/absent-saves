namespace com.absence.savesystem.internals
{
    public abstract class Serializator
    {
        public string FileName { get; private set; }

        protected string p_fullPath => SaveLoadHandler.SaveDirectory + FileName + ".save";

        public Serializator(string fileName)
        {
            FileName = fileName;
        }

        public abstract bool Serialize(object dataToSerialize);
        public abstract bool Deserialize(out object data);
    }
}
