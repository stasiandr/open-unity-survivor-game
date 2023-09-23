using System;
using UnityEngine;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class MoveSpeed : PassiveItem<PlayerProperty<float>>
    {
        public override IDisposable CreateItem(int level)
        {
            return new GenericBuff(model => model.MoveSpeedPercent.Value = data[level].Bonus);
        }
    }
}