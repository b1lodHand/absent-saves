using System.Linq;
using UnityEngine;

namespace com.absence.savesystem.internals
{
    /// <summary>
    /// This is the component responsible for sending messages to <see cref="ISaveMessageReceiver"/>
    /// instances. Don't use this manually.
    /// </summary>
    public class SaveMessageCaller : MonoBehaviour
    {
        public static SaveMessageCaller CreateNew(SaveMessageCallMode callMode)
        {
            var comp = new GameObject(nameof(SaveMessageCaller)).AddComponent<SaveMessageCaller>();
            comp.MessageCallMode = callMode;

            return comp;
        }
        public SaveMessageCallMode MessageCallMode { get; private set; }
        public bool Call()
        {
            bool result = true;
            switch (MessageCallMode)
            {
                case SaveMessageCallMode.Save:
                    result = CallForSave();
                    break;
                case SaveMessageCallMode.Load:
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