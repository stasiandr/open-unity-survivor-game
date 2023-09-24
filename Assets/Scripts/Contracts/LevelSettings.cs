using Global;
using TNRD;
using UnityEngine;

namespace Contracts
{
    public class LevelSettings : ScriptableObject
    {
        public IInventoryItemDescriptorBase StartingItem => startingItem.Value;
        [SerializeField] private SerializableInterface<IInventoryItemDescriptorBase> startingItem;

        [field: SerializeField] public AllInGameItems AllItems { get; private set; }
    }
}