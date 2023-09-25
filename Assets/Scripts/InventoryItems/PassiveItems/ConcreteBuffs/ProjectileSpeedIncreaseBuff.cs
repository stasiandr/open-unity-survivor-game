using System;
using Contracts.InventorySystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class ProjectileSpeedIncreaseBuff : PassiveItem<PlayerProperty<float>>
    {
        [InventoryItemClass("buff/projectile_speed_increase")]
        public class ProjectileSpeedIncreaseBehaviour : BuffBase<ProjectileSpeedIncreaseBuff>
        {
            [Preserve]
            public ProjectileSpeedIncreaseBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) :
                base(descriptor, data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.ProjectileSpeed.Value = Descriptor.data[Data.Level].Bonus;
            }
        }
    }
}