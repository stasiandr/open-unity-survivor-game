using System.Collections.Generic;
using UniRx;
using UnityEngine;
using VContainer;

namespace InventorySystem.InventoryView
{
    public class InventoryItemsListView : MonoBehaviour
    {
        public InventoryItemView prefab;

        private List<InventoryItemView> _viewInstances;

        private void Awake()
        {
            prefab.gameObject.SetActive(false);
            _viewInstances = new List<InventoryItemView>();
        }

        [Inject]
        public void Construct(Inventory inventory)
        {
            inventory.InventoryItems
                .ObserveAdd()
                .Where(addEvent => !addEvent.Value.Descriptor.ContainsTag("Hidden"))
                .Subscribe(addEvent =>
                {
                    var item = Instantiate(prefab, prefab.transform.parent).Construct(addEvent.Value.Descriptor);
                    _viewInstances.Add(item);
                    item.gameObject.SetActive(true);
                })
                .AddTo(this);

            inventory.InventoryItems
                .ObserveRemove()
                .Where(addEvent => !addEvent.Value.Descriptor.ContainsTag("Hidden"))
                .Subscribe(removeEvent =>
                {
                    var item = _viewInstances.Find(view => view.Descriptor == removeEvent.Value.Descriptor);
                    _viewInstances.Remove(item);
                    item.Destroy();
                })
                .AddTo(this);
        }
    }
}