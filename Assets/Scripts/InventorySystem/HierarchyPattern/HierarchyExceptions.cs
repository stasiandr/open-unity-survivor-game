using System;

namespace InventorySystem.HierarchyPattern
{
    public class InventoryItemHierarchyNotFoundException : Exception
    {
        public InventoryItemHierarchyNotFoundException(string id) : base(
            $"Item with id={id} does not have hierarchy object associated")
        {
        }
    }

    public class InventoryItemHierarchyException : Exception
    {
        public InventoryItemHierarchyException(string id) : base($"Item with id={id} has invalid hierarchy associated")
        {
        }
    }
}