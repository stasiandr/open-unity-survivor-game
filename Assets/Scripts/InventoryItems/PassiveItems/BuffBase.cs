using System;
using Contracts.InventorySystem;
using InventorySystem;
using Player;
using VContainer;

namespace InventoryItems.PassiveItems
{
    public class BuffBase<TDescriptor> : IInventoryItemBehaviour
    {
        protected readonly TDescriptor Descriptor;
        protected readonly InitializationData Data;
        protected PlayerModel PlayerModel;

        public BuffBase(IInventoryItemDescriptor descriptor, InitializationData data)
        {
            Descriptor = (TDescriptor)descriptor;
            Data = data;
        }

        [Inject]
        public void Inject(PlayerModel playerModel)
        {
            PlayerModel = playerModel;
        }

        public virtual void OnItemAdd()
        {
        }

        public virtual void Dispose()
        {
        }
    }
}