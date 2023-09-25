using System;
using Contracts;
using Contracts.InventorySystem;
using Contracts.ItemsInterfaces;
using InventorySystem;
using InventorySystem.HierarchyPattern;
using Player;
using UniRx;
using VContainer;

namespace InventoryItems.Weapons
{
    public abstract class WeaponBehaviour<TData> : IInventoryItemBehaviour
        where TData : ICloneable, ILevelUpDescription
    {
        protected readonly CompositeDisposable Disposable;

        protected HierarchyFactory HierarchyFactory { get; private set; }
        protected IPlayerRouter PlayerRouter { get; private set; }

        protected TData RuntimeData { get; private set; }

        private readonly TData _baseData;

        public WeaponBehaviour(IInventoryItemDescriptor descriptor, InitializationData initializationData)
        {
            var weaponDescriptor = (WeaponDescriptor<TData>)descriptor;
            _baseData = weaponDescriptor.data[initializationData.Level];
            Disposable = new CompositeDisposable();
        }

        [Inject]
        public void Inject(PlayerModel playerModel, HierarchyFactory hierarchyFactory, IPlayerRouter playerRouter)
        {
            PlayerRouter = playerRouter;
            HierarchyFactory = hierarchyFactory;
            playerModel.PlayerStatsChanged
                .Subscribe(_ =>
                {
                    var baseData = _baseData.Clone();
                    playerModel.InjectPlayerStats(ref baseData);
                    RuntimeData = (TData)baseData;
                }).AddTo(Disposable);
        }

        public virtual void OnItemAdd()
        {
        }

        public virtual void Dispose()
        {
            Disposable?.Dispose();
        }
    }
}