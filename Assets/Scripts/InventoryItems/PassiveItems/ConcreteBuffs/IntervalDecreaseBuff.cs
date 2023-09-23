using System;
using UnityEngine;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class IntervalDecreaseBuff : PassiveItem<PlayerProperty<float>>
    {
        public override IDisposable CreateItem(int level)
        {
            return new GenericBuff(model => model.IntervalDecrease.Value = data[level].Bonus);
        }
    }
}