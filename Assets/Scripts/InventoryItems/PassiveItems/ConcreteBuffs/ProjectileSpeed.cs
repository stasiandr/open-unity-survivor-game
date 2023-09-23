using System;
using UnityEngine;

namespace InventoryItems.PassiveItems.ConcreteBuffs
{
    [CreateAssetMenu]
    public class ProjectileSpeed : PassiveItem<PlayerProperty<float>>
    {
        public override IDisposable CreateItem(int level)
        {
            return new GenericBuff(model => model.ProjectileSpeed.Value = data[level].Bonus);
        }
    }
}