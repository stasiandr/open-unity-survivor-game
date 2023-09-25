using InventorySystem.HierarchyPattern;
using UnityEngine;

namespace InventoryItems.Weapons.BombAbility
{
    public class BombsHierarchy : HierarchyObject
    {
        [field: SerializeField] public Bomb Bomb { get; private set; }
    }
}