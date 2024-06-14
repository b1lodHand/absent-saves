using UnityEngine;

namespace com.absence.savesystem
{
    public static class GameSaveHandler
    {
        private static string m_currentSaveName = string.Empty;
        public static string CurrentSaveName => m_currentSaveName;

        public static void NewGame(string saveName)
        {
            DataPipe.ResetData();
            m_currentSaveName = saveName;
            QuickSave();
        }

        public static bool QuickSave()
        {
            return Save(m_currentSaveName);
        }

        public static bool Save(string saveName)
        {
            if (string.IsNullOrEmpty(saveName)) throw new UnityException("Save name not valid!");

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Save);
            receiver.Call();

            BinarySerializator.Serialize(saveName, DataPipe.RetrieveData());

            return true;
        }

        public static bool Load(string saveName)
        {
            if (!BinarySerializator.Deserialize(saveName, out object data)) return false;

            DataPipe.SendData(data);
            m_currentSaveName = saveName;

            SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Load);
            receiver.Call();

            return true;
        }
    }
}