using System;
using Contracts.InventorySystem;
using InventorySystem;
using UniRx;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace InventoryItems.Weapons.Wand
{
    [InventoryItemClass(ItemID)]
    public class WandBehaviour : WeaponBehaviour<WandDescriptor.WandData>
    {
        private const string ItemID = "weapon/wand";

        private WandHierarchy _hierarchyObject;

        public override void OnItemAdd()
        {
            base.OnItemAdd();
            _hierarchyObject = HierarchyFactory.Find<WandHierarchy>(ItemID);

            Observable.Interval(TimeSpan.FromSeconds(RuntimeData.Interval), Scheduler.MainThreadFixedUpdate)
                .Subscribe(_ => InstantiateProjectile())
                .AddTo(Disposable);
        }

        private void InstantiateProjectile()
        {
            for (var i = 0; i < RuntimeData.ProjectilesCount; i++)
            {
                var direction = Quaternion.Euler(0, Random.value * 360, 0) * (Vector3.forward + Vector3.up).normalized;
                var projectileDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

                var pos = PlayerRouter.WeaponsContainer.parent.position + direction;

                Object.Instantiate(_hierarchyObject.Projectile, pos, Quaternion.LookRotation(projectileDirection))
                    .Construct(RuntimeData.ProjectileSpeed, RuntimeData.AbilityDamage, 5)
                    .gameObject.SetActive(true);
            }
        }

        [Preserve]
        public WandBehaviour(IInventoryItemDescriptor descriptor, InitializationData initializationData) : base(
            descriptor, initializationData)
        {
        }
    }
}