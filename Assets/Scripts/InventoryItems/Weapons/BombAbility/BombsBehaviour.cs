using System;
using Contracts.InventorySystem;
using InventorySystem;
using UniRx;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace InventoryItems.Weapons.BombAbility
{
    [InventoryItemClass(ItemID)]
    public class BombsBehaviour : WeaponBehaviour<BombsDescriptor.BombsData>
    {
        private BombsHierarchy _bombsHierarchy;
        private const string ItemID = "weapon/bombs";

        [Preserve]
        public BombsBehaviour(IInventoryItemDescriptor descriptor, InitializationData initializationData) : base(
            descriptor, initializationData)
        {
        }

        public override void OnItemAdd()
        {
            base.OnItemAdd();

            _bombsHierarchy = HierarchyFactory.Find<BombsHierarchy>(ItemID);

            Observable.Interval(TimeSpan.FromSeconds(RuntimeData.Interval), Scheduler.MainThreadFixedUpdate)
                .Subscribe(_ => InstantiateProjectile())
                .AddTo(Disposable);
        }

        private void InstantiateProjectile()
        {
            for (var i = 0; i < RuntimeData.ProjectilesCount; i++)
            {
                var direction = Quaternion.Euler(0, Random.value * 360, 0) *
                                (Vector3.forward + Vector3.up * 3).normalized;
                var pos = PlayerRouter.PlayerBase.position + direction * 2;

                Object.Instantiate(_bombsHierarchy.Bomb, pos, Quaternion.LookRotation(direction))
                    .Construct(RuntimeData.Radius, RuntimeData.AbilityDamage, 2.5f)
                    .gameObject.SetActive(true);
            }
        }
    }
}