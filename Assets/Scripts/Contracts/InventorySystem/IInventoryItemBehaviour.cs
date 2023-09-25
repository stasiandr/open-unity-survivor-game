using System;

namespace Contracts.InventorySystem
{
    public interface IInventoryItemBehaviour : IDisposable
    {
        void OnItemAdd();
    }
}