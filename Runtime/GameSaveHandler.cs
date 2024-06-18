using com.absence.savesystem.internals;
using UnityEngine;

namespace com.absence.savesystem
{
    public static class GameSaveHandler
    {
        private static string m_currentSaveName = string.Empty;
        public static string CurrentSaveName => m_currentSaveName;

        public static void NewGame(string saveName, object defaultData)
        {
            m_currentSaveName = saveName;
            QuickSave(defaultData);
        }

        public static bool QuickSave(object dataToSave)
        {
            return Save(m_currentSaveName, dataToSave);
        }

        public static bool Save(string saveName, object dataToSave)
        {
            if (string.IsNullOrEmpty(saveName)) throw new UnityException("Save name not valid!");

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Save);
            receiver.Call();

            BinarySerializator.Serialize(saveName, dataToSave);

            return true;
        }

        public static bool Load(string saveName, out object data)
        {
            if (!BinarySerializator.Deserialize(saveName, out data)) return false;

            m_currentSaveName = saveName;

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Load);
            receiver.Call();

            return true;
        }
    }
}