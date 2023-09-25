using InventorySystem.HierarchyPattern;
using UnityEngine;

namespace InventoryItems.Weapons.Wand
{
    public class WandHierarchy : HierarchyObject
    {
        [field: SerializeField] public Projectile Projectile { get; private set; }
    }
}