using System.Collections.Generic;
using Nrjwolf.Tools.AttachAttributes;
using Player;
using UniRx;
using UnityEngine;
using VContainer;

namespace InventoryItems.Weapons.Orb
{
    public class OrbBehaviour : WeaponBehaviour<OrbDescriptor.OrbData>
    {
        [SerializeField] [GetComponentInChildren]
        private RotatingSphereWeapon rotatingSphereWeapon;

        private readonly List<RotatingSphereWeapon> _weapons = new();

        private void Awake()
        {
            rotatingSphereWeapon.gameObject.SetActive(false);
        }

        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            SubscribeToPlayer(playerModel);

            playerModel.PlayerStatsChanged.Subscribe(_ => CreateOrbs()).AddTo(this);
        }

        private void CreateOrbs()
        {
            foreach (var weapon in _weapons) Destroy(weapon.gameObject);
            _weapons.Clear();

            for (var i = 0; i < RuntimeData.ProjectilesCount; i++)
            {
                var weapon = Instantiate(rotatingSphereWeapon, rotatingSphereWeapon.transform.parent);

                weapon.damage = RuntimeData.AbilityDamage;
                weapon.angularSpeed = RuntimeData.ProjectileSpeed;
                weapon.offset = (float)i / RuntimeData.ProjectilesCount * 360;

                weapon.gameObject.SetActive(true);

                _weapons.Add(weapon);
            }
        }
    }
}