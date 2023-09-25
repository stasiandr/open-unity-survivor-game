using System.Collections.Generic;
using Contracts.InventorySystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.Weapons.Orb
{
    [InventoryItemClass(ItemID)]
    public class OrbBehaviour : WeaponBehaviour<OrbDescriptor.OrbData>
    {
        private const string ItemID = "weapon/orb";

        private readonly List<RotatingSphereWeapon> _weapons = new();
        private OrbHierarchy _orbHierarchy;

        [Preserve]
        public OrbBehaviour(IInventoryItemDescriptor descriptor, InitializationData initializationData) : base(
            descriptor, initializationData)
        {
        }

        public override void OnItemAdd()
        {
            base.OnItemAdd();

            _orbHierarchy = HierarchyFactory.Find<OrbHierarchy>(ItemID);

            CreateOrbs();
        }

        private void CreateOrbs()
        {
            foreach (var weapon in _weapons) Object.Destroy(weapon.gameObject);
            _weapons.Clear();

            for (var i = 0; i < RuntimeData.ProjectilesCount; i++)
            {
                var weapon = Object.Instantiate(_orbHierarchy.RotatingSphereWeapon, PlayerRouter.WeaponsContainer);

                weapon.damage = RuntimeData.AbilityDamage;
                weapon.angularSpeed = RuntimeData.ProjectileSpeed;
                weapon.offset = (float)i / RuntimeData.ProjectilesCount * 360;

                weapon.gameObject.SetActive(true);

                _weapons.Add(weapon);
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            foreach (var weapon in _weapons) Object.Destroy(weapon.gameObject);
            _weapons.Clear();
        }
    }
}