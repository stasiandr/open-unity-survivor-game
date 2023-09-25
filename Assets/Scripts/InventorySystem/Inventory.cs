using System.Linq;
using Global;
using UniRx;

namespace InventorySystem
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Inventory
    {
        public IReadOnlyReactiveCollection<InventoryItem> InventoryItems => _inventoryItems;
        private readonly ReactiveCollection<InventoryItem> _inventoryItems = new();

        public void Add(InventoryItem item)
        {
            item.OnAdd();
            _inventoryItems.Add(item);
        }

        public void Remove(InventoryItem item)
        {
            _inventoryItems.Remove(item);
            item.OnRemove();
        }

        public void Remove(string id)
        {
            if (TryGet(id, out var inventoryItem))
                Remove(inventoryItem);
        }

        public bool TryGet(string id, out InventoryItem inventoryItem)
        {
            inventoryItem = _inventoryItems.FirstOrDefault(i => i.Descriptor.ID == id);
            return inventoryItem != null;
        }
    }
}