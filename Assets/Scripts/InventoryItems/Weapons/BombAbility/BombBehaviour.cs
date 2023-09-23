using System;
using Player;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace InventoryItems.Weapons.BombAbility
{
    public class BombBehaviour : WeaponBehaviour<BombDescriptor.BombData>
    {
        [SerializeField] private Bomb projectile;

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
                var direction = Quaternion.Euler(0, Random.value * 360, 0) *
                                (Vector3.forward + Vector3.up * 3).normalized;
                var pos = transform.parent.position + direction * 2;

                Instantiate(projectile, pos, Quaternion.LookRotation(direction))
                    .Construct(RuntimeData.Radius, RuntimeData.AbilityDamage, 2.5f)
                    .gameObject.SetActive(true);
            }
        }
    }
}