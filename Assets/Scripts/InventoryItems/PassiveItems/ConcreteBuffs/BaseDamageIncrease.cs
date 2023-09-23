using System;
using UnityEngine;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class BaseDamageIncrease : PassiveItem<PlayerProperty<int>>
    {
        public override IDisposable CreateItem(int level)
            => new GenericBuff(model => model.BaseDamage.Value = data[level].Bonus);
    }
}