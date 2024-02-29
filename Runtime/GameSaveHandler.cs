
using Unity.VisualScripting;
using UnityEngine;

public static class GameSaveHandler
{
    public static string CurrentSaveName;

    public static void NewGame(string saveName)
    {
        GameSaveData.Reset();
        CurrentSaveName = saveName;
        QuickSave();
    }

    public static void QuickSave()
    {
        Save(CurrentSaveName);
    }

    public static void Save(string saveName)
    {
        if (string.IsNullOrEmpty(saveName)) throw new UnityException("Save name not valid!");

        SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Save);
        receiver.Call();

        BinarySerializator.Serialize(saveName, GameSaveData.Current);
    }

    public static void Load(string saveName)
    {
        if (!BinarySerializator.Deserialize(saveName, out object data)) return;

        GameSaveData.Current = (GameSaveData)data;
        CurrentSaveName = saveName;

        SaveMessageCaller receiver = SaveMessageCaller.CreateNew(SaveMessageCaller.CallMode.Load);
        receiver.Call();
    }
}