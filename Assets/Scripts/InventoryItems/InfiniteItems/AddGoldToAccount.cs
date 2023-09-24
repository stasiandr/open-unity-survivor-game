using System;
using Global;
using NaughtyAttributes;
using Player;
using UnityEngine;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class AddGold : ScriptableObject, IInventoryItemDescriptor
    {
        public int additionGold;

        [field: SerializeField] public Sprite ItemIcon { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }
        
        [field: SerializeField] 
        [field: Tag]
        public string[] Tags { get; private set; }
        [field: SerializeField] public string LevelUpDescriptionFormat { get; private set; } = "Add +{0} gold";
        
        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return string.Format(LevelUpDescriptionFormat, additionGold);
        }

        public IDisposable CreateItem(int level)
        {
            return new AddGoldBehaviour(additionGold);
        }

        public class AddGoldBehaviour : IDisposable
        {
            private readonly int _additionMaxHealth;

            public AddGoldBehaviour(int additionMaxHealth)
            {
                _additionMaxHealth = additionMaxHealth;
            }

            [Inject]
            public void Inject(PlayerModel playerModel)
            {
                playerModel.AddMaxHealth(_additionMaxHealth);
            }

            public void Dispose()
            {
            }
        }
    }
}