using System;
using Contracts;
using Contracts.InventorySystem;

namespace InventorySystem
{
    public class InventoryItem
    {
        public IInventoryItemDescriptor Descriptor { get; private set; }
        public int Level { get; private set; }

        public IInventoryItemBehaviour InventoryItemBehaviour { get; private set; }

        public InventoryItem(IInventoryItemDescriptor descriptor, int level,
            IInventoryItemBehaviour inventoryItemBehaviour)
        {
            Descriptor = descriptor;
            Level = level;
            InventoryItemBehaviour = inventoryItemBehaviour;
        }

        public void OnAdd()
        {
            InventoryItemBehaviour.OnItemAdd();
        }

        public void OnRemove()
        {
            InventoryItemBehaviour.Dispose();
        }
    }
}