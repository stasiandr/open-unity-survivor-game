using Contracts.InventorySystem;
using UnityEngine;

namespace InventorySystem.HierarchyPattern
{
    public class HierarchyObject : MonoBehaviour
    {
        [field: SerializeField]
        [field: InventoryItemID]
        public string ItemID { get; private set; }
    }
}