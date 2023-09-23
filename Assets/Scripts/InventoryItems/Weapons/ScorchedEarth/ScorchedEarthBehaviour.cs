using System;
using System.Linq;
using Global;
using Interfaces;
using Player;
using UniRx;
using UnityEngine;
using VContainer;

namespace InventoryItems.Weapons.ScorchedEarth
{
    public class ScorchedEarthBehaviour : WeaponBehaviour<ScorchedEarthDescriptor.ScorchedEarthData>
    {
        [SerializeField] private ParticleSystem tempView;

        private readonly Collider[] _results = new Collider[10];

        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            SubscribeToPlayer(playerModel);

            tempView.transform.localScale = Vector3.one * RuntimeData.Radius;

            Observable.Interval(TimeSpan.FromSeconds(RuntimeData.Interval))
                .Select(_ =>
                {
                    var size = Physics.OverlapSphereNonAlloc(transform.position, RuntimeData.Radius, _results);
                    return ..size;
                }).Subscribe(range =>
                {
                    foreach (var damagable in _results[range]
                                 .Where(col => col.CompareTag("Enemy"))
                                 .Select(col => col.attachedRigidbody.GetComponent<IDamagable>()))
                        damagable.DealDamage(RuntimeData.AbilityDamage);

                    tempView.Emit(1);
                }).AddTo(this);
        }
    }
}