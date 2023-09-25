using System;
using System.Linq;
using Contracts;
using Contracts.InventorySystem;
using InventorySystem;
using Player;
using UniRx;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace InventoryItems.Weapons.ScorchedEarth
{
    [InventoryItemClass(ScorchedEarth)]
    public class ScorchedEarthBehaviour : WeaponBehaviour<ScorchedEarthDescriptor.ScorchedEarthData>
    {
        private const string ScorchedEarth = "weapon/scorched_earth";
        private ScorchedEarthHierarchy _scorchedEarthHierarchy;

        private readonly Collider[] _results = new Collider[10];

        [Preserve]
        public ScorchedEarthBehaviour(IInventoryItemDescriptor descriptor, InitializationData initializationData) :
            base(descriptor, initializationData)
        {
        }

        public override void OnItemAdd()
        {
            _scorchedEarthHierarchy =
                HierarchyFactory.Instantiate<ScorchedEarthHierarchy>(ScorchedEarth, PlayerRouter.WeaponsContainer);
            _scorchedEarthHierarchy.PS.transform.localScale = Vector3.one * RuntimeData.Radius;

            Observable.Interval(TimeSpan.FromSeconds(RuntimeData.Interval))
                .Select(_ =>
                {
                    var size = Physics.OverlapSphereNonAlloc(_scorchedEarthHierarchy.transform.position,
                        RuntimeData.Radius, _results);
                    return ..size;
                }).Subscribe(range =>
                {
                    foreach (var damagable in _results[range]
                                 .Select(c => c.GetComponentInParent<IEnemyHealth>())
                                 .Where(ph => ph != null))
                        damagable.DealDamage(RuntimeData.AbilityDamage);

                    _scorchedEarthHierarchy.PS.Emit(1);
                }).AddTo(Disposable);
        }

        public override void Dispose()
        {
            base.Dispose();
            Object.Destroy(_scorchedEarthHierarchy.gameObject);
        }
    }
}