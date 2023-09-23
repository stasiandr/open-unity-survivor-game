using System;
using UnityEngine;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class ProjectileCountBoost : PassiveItem<PlayerProperty<int>>
    {
        public override IDisposable CreateItem(int level)
        {
            return new GenericBuff(model => model.ExtraProjectiles.Value = data[level].Bonus);
        }
    }
}