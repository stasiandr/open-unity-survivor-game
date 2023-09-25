using System;
using Contracts.InventorySystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class BaseDamageIncreaseBuff : PassiveItem<PlayerProperty<int>>
    {
        [InventoryItemClass("buff/base_damage_increase")]
        public class BaseDamageIncreaseBehaviour : BuffBase<BaseDamageIncreaseBuff>
        {
            [Preserve]
            public BaseDamageIncreaseBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) : base(
                descriptor, data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.BaseDamage.Value = Descriptor.data[Data.Level].Bonus;
            }
        }
    }
}