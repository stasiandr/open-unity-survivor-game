using System;
using Player;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace InventoryItems.Weapons.Wand
{
    public class WandBehaviour : WeaponBehaviour<WandDescriptor.WandData>
    {
        [SerializeField] private Projectile projectile;

        private void Awake()
        {
            projectile.gameObject.SetActive(false);
        }

        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            SubscribeToPlayer(playerModel);

            Observable.Interval(TimeSpan.FromSeconds(RuntimeData.Interval), Scheduler.MainThreadFixedUpdate)
                .Subscribe(_ => InstantiateProjectile())
                .AddTo(this);
        }

        private void InstantiateProjectile()
        {
            for (var i = 0; i < RuntimeData.ProjectilesCount; i++)
            {
                var direction = Quaternion.Euler(0, Random.value * 360, 0) * (Vector3.forward + Vector3.up).normalized;
                var projectileDirection = Vector3.ProjectOnPlane(direction, Vector3.up);

                var pos = transform.parent.position + direction;

                Instantiate(projectile, pos, Quaternion.LookRotation(projectileDirection))
                    .Construct(RuntimeData.ProjectileSpeed, RuntimeData.AbilityDamage, 5)
                    .gameObject.SetActive(true);
            }
        }
    }
}