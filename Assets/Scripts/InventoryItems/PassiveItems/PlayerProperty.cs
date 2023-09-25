using System;
using Contracts.ItemsInterfaces;
using UnityEngine;

namespace InventoryItems.PassiveItems
{
    [Serializable]
    public struct PlayerProperty<T> : ILevelUpDescription where T : struct
    {
        [field: SerializeField] public T Bonus { get; private set; }

        [field: SerializeField] public string LevelUpDescription { get; private set; }
    }
}