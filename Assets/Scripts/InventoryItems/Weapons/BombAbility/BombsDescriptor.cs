using System;
using Contracts.ItemsInterfaces;
using UnityEngine;

namespace InventoryItems.Weapons.BombAbility
{
    [CreateAssetMenu]
    public class BombsDescriptor : WeaponDescriptor<BombsDescriptor.BombsData>
    {
        [Serializable]
        public struct BombsData : ILevelUpDescription, ICloneable, IAbilityRadius, IAbilityDamage, IAbilityInterval,
            IProjectilesCount
        {
            [field: SerializeField] public float Interval { get; set; }
            [field: SerializeField] public int AbilityDamage { get; set; }
            [field: SerializeField] public float Radius { get; set; }
            [field: SerializeField] public int ProjectilesCount { get; set; }

            [field: SerializeField] public string LevelUpDescription { get; set; }

            public object Clone()
            {
                return new BombsData
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