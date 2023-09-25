using InventorySystem.HierarchyPattern;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

namespace InventoryItems.Weapons.Orb
{
    public class OrbHierarchy : HierarchyObject
    {
        [field: SerializeField] public RotatingSphereWeapon RotatingSphereWeapon { get; private set; }
    }
}