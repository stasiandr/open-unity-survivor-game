using System;
using Contracts.InventorySystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class ProjectileCountIncreaseBuff : PassiveItem<PlayerProperty<int>>
    {
        [InventoryItemClass("buff/projectile_count_increase")]
        public class ProjectileCountIncreaseBehaviour : BuffBase<ProjectileCountIncreaseBuff>
        {
            [Preserve]
            public ProjectileCountIncreaseBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) :
                base(descriptor, data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.ExtraProjectiles.Value = Descriptor.data[Data.Level].Bonus;
            }
        }
    }
}