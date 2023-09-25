using System;
using Contracts.InventorySystem;

namespace InventorySystem
{
    public class InventoryItemNotFoundException : Exception
    {
        public InventoryItemNotFoundException(string id) : base($"Item with id={id} not found") { }
    }

    public class InventoryItemInvalidConstructorException : Exception
    {
        public InventoryItemInvalidConstructorException(string id) : base($"Item with id={id} don't have valid constructor") { }
    }

    public class InventoryItemInvalidInterfaceException : Exception
    {
        public InventoryItemInvalidInterfaceException(string id) : base($"Item with id={id} do not implement {nameof(IInventoryItemBehaviour)}") { }
    }
}