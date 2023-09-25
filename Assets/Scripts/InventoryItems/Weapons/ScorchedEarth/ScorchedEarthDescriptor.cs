using System;
using Contracts.ItemsInterfaces;
using UnityEngine;

namespace InventoryItems.Weapons.ScorchedEarth
{
    [CreateAssetMenu]
    public class
        ScorchedEarthDescriptor : WeaponDescriptor<ScorchedEarthDescriptor.ScorchedEarthData>
    {
        [Serializable]
        public struct ScorchedEarthData : ILevelUpDescription, IAbilityDamage, IAbilityInterval, IAbilityRadius,
            ICloneable
        {
            [field: SerializeField] public int AbilityDamage { get; set; }

            [field: SerializeField] public float Interval { get; set; }

            [field: SerializeField] public float Radius { get; set; }


            [field: SerializeField] public string LevelUpDescription { get; private set; }

            public object Clone()
            {
                return new ScorchedEarthData
                {
                    AbilityDamage = AbilityDamage,
                    Interval = Interval,
                    Radius = Radius,
                    LevelUpDescription = LevelUpDescription
                };
            }
        }
    }
}