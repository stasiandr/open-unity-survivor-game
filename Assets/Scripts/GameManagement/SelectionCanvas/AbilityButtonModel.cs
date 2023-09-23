using System;
using UnityEngine;

namespace GameManagement.SelectionCanvas
{
    [Serializable]
    public class AbilityButtonModel
    {
        public Sprite icon;
        public string name;
        public string description;
        public int level;
        public Action createCallback;
    }
}