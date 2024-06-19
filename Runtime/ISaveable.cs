namespace com.absence.savesystem
{
    /// <summary>
    /// Any component that implements this interface will get notified when game saved/loaded.
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// The method gets called on load.
        /// </summary>
        /// <returns>False if anything went wrong. True otherwise.</returns>
        public bool OnLoad();

        /// <summary>
        /// The method gets called on save.
        /// </summary>
        /// <returns>False if anything went wrong. True otherwise.</returns>
        public bool OnSave();
    }
}