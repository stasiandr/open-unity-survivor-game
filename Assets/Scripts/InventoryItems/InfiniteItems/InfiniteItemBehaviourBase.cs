using Contracts.InventorySystem;
using InventorySystem;
using Player;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    public abstract class InfiniteItemBehaviourBase<TDescriptor> : IInventoryItemBehaviour
        where TDescriptor : IInventoryItemDescriptor
    {
        protected readonly TDescriptor Descriptor;
        protected readonly InitializationData Data;
        protected PlayerModel PlayerModel;

        public InfiniteItemBehaviourBase(IInventoryItemDescriptor descriptor, InitializationData data)
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