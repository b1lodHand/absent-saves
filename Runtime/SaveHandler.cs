using com.absence.savesystem.internals;
using System;
using UnityEngine;

namespace com.absence.savesystem
{
    public static class SaveHandler
    {
        private static string m_currentSaveName = string.Empty;
        public static string CurrentSaveName => m_currentSaveName;

        public static Action OnSave = null;
        public static Action OnLoad = null;

        public static bool NewGame(string saveName, object defaultData)
        {
            m_currentSaveName = saveName;
            return QuickSave(defaultData);
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

            OnSave?.Invoke();
            return true;
        }

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