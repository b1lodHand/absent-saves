using com.absence.savesystem.internals;
using System;

namespace com.absence.savesystem
{
    /// <summary>
    /// The static class responsible for handling the saving/loading process.
    /// </summary>
    public static class SaveHandler
    {
        private static string m_currentSaveName = string.Empty;

        /// <summary>
        /// Name of the current save. Empty if no saves loaded.
        /// </summary>
        public static string CurrentSaveName => m_currentSaveName;

        /// <summary>
        /// Action which will get invoked right after game gets saved.
        /// </summary>
        public static Action OnSave = null;

        /// <summary>
        /// Action which will get invoked right after game gets loaded.
        /// </summary>
        public static Action OnLoad = null;

        /// <summary>
        /// Use to create a new save.
        /// </summary>
        /// <param name="saveName">Name of the new save.</param>
        /// <param name="defaultData">The default data for this new save.</param>
        /// <param name="onReload">Action which will get invoked when the game reloads the newly created save.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool NewGame(string saveName, object defaultData, Action<object> onReload)
        {
            if (!QuickSave(defaultData)) return false;

            return Load(saveName, onReload);
        }

        /// <summary>
        /// Use to save the currently loaded save quickly.
        /// </summary>
        /// <param name="dataToSave">The new data which will override the old one.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool QuickSave(object dataToSave)
        {
            return Save(m_currentSaveName, dataToSave);
        }

        /// <summary>
        /// Use to save the game.
        /// </summary>
        /// <param name="saveName">Name of the target save file.</param>
        /// <param name="dataToSave">Data to save.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool Save(string saveName, object dataToSave)
        {
            if (string.IsNullOrEmpty(saveName)) throw new Exception("Save name not valid!");

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Save);
            receiver.Call();

            BinarySerializator.Serialize(saveName, dataToSave);

            OnSave?.Invoke();
            return true;
        }

        /// <summary>
        /// Use to load a save.
        /// </summary>
        /// <param name="saveName">Name of the save which will get loaded.</param>
        /// <param name="handleData">Action for handling the loaded data.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool Load(string saveName, Action<object> handleData)
        {
            if (!BinarySerializator.Deserialize(saveName, out object data)) return false;

            m_currentSaveName = saveName;

            handleData.Invoke(data);

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Load);
            receiver.Call();

            OnLoad?.Invoke();
            return true;
        }
    }
}