using System;
using Contracts;
using Contracts.InventorySystem;
using Global;

namespace InventorySystem
{
    public class InventoryItem
    {
        public IInventoryItemDescriptor Descriptor { get; private set; }
        public int Level { get; private set; }

        public IDisposable Disposable { get; private set; }

        public InventoryItem(IInventoryItemDescriptor descriptor, int level, IInventoryItemBehaviour disposable)
        {
            Descriptor = descriptor;
            Level = level;
            Disposable = disposable;
        }

        public void OnAdd()
        {
            
        }

        public void OnRemove()
        {
            Disposable.Dispose();
        }
    }
}