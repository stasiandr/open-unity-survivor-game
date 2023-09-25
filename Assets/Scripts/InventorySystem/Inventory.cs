using System.Linq;
using Global;
using UniRx;

namespace InventorySystem
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Inventory
    {
        public IReadOnlyReactiveCollection<InventoryItemBase> InventoryItems => _inventoryItems;
        private readonly ReactiveCollection<InventoryItemBase> _inventoryItems = new();

        public void Add(InventoryItemBase item)
        {
            item.OnAdd();
            _inventoryItems.Add(item);
        }

        public void Remove(InventoryItemBase item)
        {
            _inventoryItems.Remove(item);
            item.OnRemove();
        }

        public void Remove(string id)
        {
            if (TryGet(id, out var inventoryItem))
                Remove(inventoryItem);
        }

        public bool TryGet(string id, out InventoryItemBase inventoryItem)
        {
            inventoryItem = _inventoryItems.FirstOrDefault(i => i.Descriptor.ID == id);
            return inventoryItem != null;
        }
    }
}