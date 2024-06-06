using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.absence.savesystem
{
    public interface ISaveable
    {
        public bool Load();
        public bool Save();
    }
}