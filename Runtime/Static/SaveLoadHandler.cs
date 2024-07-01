using com.absence.savesystem.internals;
using System;
using UnityEngine;

namespace com.absence.savesystem
{
    /// <summary>
    /// The static class responsible for handling the saving/loading process.
    /// </summary>
    public static class SaveLoadHandler
    {
        public static readonly string SaveDirectory = Application.persistentDataPath + "/saves/";
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
        public static bool NewGame(object defaultData, Action<object> onReload, Serializator serializator)
        {
            if (!Save(defaultData, serializator)) return false;

            return Load(onReload, serializator);
        }

        /// <summary>
        /// Use to save the currently loaded save quickly.
        /// </summary>
        /// <param name="dataToSave">The new data which will override the old one.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool QuickSave(object dataToSave, Serializator serializator)
        {
            return Save(dataToSave, serializator);
        }

        /// <summary>
        /// Use to save the game.
        /// </summary>
        /// <param name="saveName">Name of the target save file.</param>
        /// <param name="dataToSave">Data to save.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool Save(object dataToSave, Serializator serializator)
        {
            if (string.IsNullOrEmpty(serializator.FileName)) throw new Exception("Save name not valid!");

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Save);
            receiver.Call();

            if (!serializator.Serialize(dataToSave)) return false;

            OnSave?.Invoke();
            return true;
        }

        /// <summary>
        /// Use to load a save.
        /// </summary>
        /// <param name="saveName">Name of the save which will get loaded.</param>
        /// <param name="handleData">Action for handling the loaded data.</param>
        /// <returns>False if anything goes wrong. True otherwise.</returns>
        public static bool Load(Action<object> handleData, Serializator serializator)
        {
            if (!serializator.Deserialize(out object data)) return false;

            m_currentSaveName = serializator.FileName;

            handleData.Invoke(data);

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Load);
            receiver.Call();

            OnLoad?.Invoke();
            return true;
        }
    }
}