using System;
using Contracts.ItemsInterfaces;
using Global.ItemsInterfaces;
using UnityEngine;

namespace InventoryItems.Weapons.BombAbility
{
    [CreateAssetMenu]
    public class BombDescriptor : WeaponDescriptor<BombDescriptor.BombData, BombBehaviour>
    {
        [Serializable]
        public struct BombData : ILevelUpDescription, ICloneable, IAbilityRadius, IAbilityDamage, IAbilityInterval, IProjectilesCount
        {
            [field: SerializeField] public float Interval { get; set; }
            [field: SerializeField] public int AbilityDamage { get; set; }
            [field: SerializeField] public float Radius { get; set; }
            [field: SerializeField] public int ProjectilesCount { get; set; }

            [field: SerializeField] public string LevelUpDescription { get; set; }

            public object Clone()
            {
                return new BombData
                {
                    Interval = Interval,
                    AbilityDamage = AbilityDamage,
                    Radius = Radius,
                    ProjectilesCount = ProjectilesCount,
                    LevelUpDescription = LevelUpDescription
                };
            }
        }
    }
}