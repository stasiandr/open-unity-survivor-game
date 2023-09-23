using Interfaces;
using InventorySystem;
using TNRD;
using UnityEngine;
using VContainer;

namespace GameManagement
{
    public class StartWithItem : MonoBehaviour
    {
        [SerializeField] private SerializableInterface<IInventoryItemDescriptorBase> descriptor;
        private Inventory _inventory;
        private InventoryItemFactory _factory;

        [Inject]
        public void Construct(Inventory inventory, InventoryItemFactory factory)
        {
            _factory = factory;
            _inventory = inventory;
        }

        private void Start() => _inventory.Add(_factory.Create(descriptor.Value, 0));
    }
}