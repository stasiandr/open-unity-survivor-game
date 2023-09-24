using System;
using Global;
using NaughtyAttributes;
using Player;
using UnityEngine;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class HealPlayer : ScriptableObject, IInventoryItemDescriptor
    {
        [field: SerializeField] private int heal;

        [field: SerializeField] public Sprite ItemIcon { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }
        
        [field: SerializeField] 
        [field: Tag]
        public string[] Tags { get; private set; }
        [field: SerializeField] public string LevelUpDescriptionFormat { get; private set; } = "Add +{0} health";
        
        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return string.Format(LevelUpDescriptionFormat, heal);
        }

        public IDisposable CreateItem(int level)
        {
            return new HealPlayerBehaviour(heal);
        }

        public class HealPlayerBehaviour : IDisposable
        {
            private readonly int _additionMaxHealth;

            public HealPlayerBehaviour(int additionMaxHealth)
            {
                _additionMaxHealth = additionMaxHealth;
            }

            [Inject]
            public void Inject(PlayerModel playerModel)
            {
                playerModel.Heal(_additionMaxHealth);
            }

            public void Dispose()
            {
            }
        }
    }
}