using System;
using Contracts.InventorySystem;
using InventorySystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class MoveSpeedIncreaseBuff : PassiveItem<PlayerProperty<float>>
    {
        [InventoryItemClass("buff/move_speed_increase")]
        public class MoveSpeedIncreaseBehaviour : BuffBase<MoveSpeedIncreaseBuff>
        {
            [Preserve]
            public MoveSpeedIncreaseBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) : base(
                descriptor, data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.MoveSpeedPercent.Value = Descriptor.data[Data.Level].Bonus;
            }
        }
    }
}