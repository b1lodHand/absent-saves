using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable
{
    public bool Load();
    public bool Save();
}
