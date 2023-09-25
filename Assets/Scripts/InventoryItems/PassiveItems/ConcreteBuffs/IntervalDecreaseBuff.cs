using System;
using Contracts.InventorySystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class IntervalDecreaseBuff : PassiveItem<PlayerProperty<float>>
    {
        [InventoryItemClass("buff/interval_decrease")]
        public class IntervalDecreaseBehaviour : BuffBase<IntervalDecreaseBuff>
        {
            [Preserve]
            public IntervalDecreaseBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) : base(
                descriptor, data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.IntervalDecrease.Value = Descriptor.data[Data.Level].Bonus;
            }
        }
    }
}