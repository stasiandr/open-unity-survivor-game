using TNRD;
using UnityEngine;

namespace Global
{
    public class LevelSettings : ScriptableObject
    {
        public IInventoryItemDescriptorBase StartingItem => startingItem.Value;
        [SerializeField] private SerializableInterface<IInventoryItemDescriptorBase> startingItem;

    }
}