using InventorySystem.HierarchyPattern;
using UnityEngine;

namespace InventoryItems.Weapons.ScorchedEarth
{
    public class ScorchedEarthHierarchy : HierarchyObject
    {
        [field: SerializeField] public ParticleSystem PS { get; private set; }
    }
}