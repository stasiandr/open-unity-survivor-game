using System;
using Contracts.ItemsInterfaces;
using Global.ItemsInterfaces;
using UnityEngine;

namespace InventoryItems.Weapons.Orb
{
    [CreateAssetMenu]
    public class OrbDescriptor : WeaponDescriptor<OrbDescriptor.OrbData, OrbBehaviour>
    {
        [Serializable]
        public struct OrbData : ILevelUpDescription, IProjectileSpeed, IAbilityDamage, IProjectilesCount, ICloneable
        {
            [field: SerializeField] public float ProjectileSpeed { get; set; }

            [field: SerializeField] public int AbilityDamage { get; set; }

            [field: SerializeField] public int ProjectilesCount { get; set; }

            [field: SerializeField] public string LevelUpDescription { get; private set; }


            public object Clone()
            {
                return new OrbData
                {
                    ProjectileSpeed = ProjectileSpeed,
                    AbilityDamage = AbilityDamage,
                    ProjectilesCount = ProjectilesCount,
                    LevelUpDescription = LevelUpDescription
                };
            }
        }
    }
}