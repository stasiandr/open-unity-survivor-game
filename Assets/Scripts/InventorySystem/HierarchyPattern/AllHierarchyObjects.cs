using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.HierarchyPattern
{
    [CreateAssetMenu]
    public class AllHierarchyObjects : ScriptableObject
    {
        [field: SerializeField] public List<HierarchyObject> Hierarchies { get; private set; }
    }
}