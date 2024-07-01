using System.Linq;
using UnityEngine;

namespace com.absence.savesystem.internals
{
    internal class SaveMessageCaller : MonoBehaviour
    {
        internal enum CallMode
        {
            Save = 0,
            Load = 1,
        }

        internal CallMode MessageCallMode { get; private set; }

        internal static SaveMessageCaller CreateNew(CallMode callMode)
        {
            var comp = new GameObject(nameof(SaveMessageCaller)).AddComponent<SaveMessageCaller>();
            comp.MessageCallMode = callMode;

            return comp;
        }

        internal bool Call()
        {
            bool result = true;
            switch (MessageCallMode)
            {
                case CallMode.Save:
                    result = CallForSave();
                    break;
                case CallMode.Load:
                    result = CallForLoad();
                    break;
                default:
                    result = false;
                    break;
            }

            Destroy(gameObject);
            return result;
        }

        private bool CallForSave()
        {
            bool success = true;
            var foundObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveMessageReceiver>().ToList();

            foundObjects.ForEach(o =>
            {
                if (!o.OnSave()) success = false;
            });

            return success;
        }

        private bool CallForLoad()
        {
            bool success = true;
            var foundObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISaveMessageReceiver>().ToList();

            foundObjects.ForEach(o =>
            {
                if (!o.OnLoad()) success = false;
            });

            return success;
        }
    }

}