using System;
using Contracts.ItemsInterfaces;
using Global.ItemsInterfaces;
using UnityEngine;

namespace InventoryItems.Weapons.Wand
{
    [CreateAssetMenu]
    public class WandDescriptor : WeaponDescriptor<WandDescriptor.WandData, WandBehaviour>
    {
        [Serializable]
        public struct WandData : ILevelUpDescription, IProjectileSpeed, IAbilityDamage, IAbilityInterval,
            IProjectilesCount, ICloneable
        {
            [field: SerializeField] public float ProjectileSpeed { get; set; }

            [field: SerializeField] public int AbilityDamage { get; set; }

            [field: SerializeField] public float Interval { get; set; }

            [field: SerializeField] public int ProjectilesCount { get; set; }


            [field: SerializeField] public string LevelUpDescription { get; private set; }

            public object Clone()
            {
                return new WandData
                {
                    ProjectileSpeed = ProjectileSpeed,
                    AbilityDamage = AbilityDamage,
                    Interval = Interval,
                    ProjectilesCount = ProjectilesCount,
                    LevelUpDescription = LevelUpDescription
                };
            }
        }
    }
}