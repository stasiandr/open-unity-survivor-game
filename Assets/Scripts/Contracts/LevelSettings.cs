using Contracts.InventorySystem;
using UnityEngine;

namespace Contracts
{
    public class LevelSettings : ScriptableObject
    {
        [InventoryItemID] public string startWithItemString;

        [field: SerializeField] public AllInGameItems AllItems { get; private set; }

        [field: SerializeField] public int ItemsPerSelection { get; private set; } = 3;
    }
}