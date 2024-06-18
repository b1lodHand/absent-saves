using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.savesystem
{
    public interface ISaveable
    {
        public bool OnLoad();
        public bool OnSave();
    }
}