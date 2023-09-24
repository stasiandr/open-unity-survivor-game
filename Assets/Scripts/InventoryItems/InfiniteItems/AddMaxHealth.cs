using System;
using Global;
using NaughtyAttributes;
using Player;
using UnityEngine;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class AddMaxHealth : ScriptableObject, IInventoryItemDescriptor
    {
        public int additionMaxHealth;

        [field: SerializeField] public Sprite ItemIcon { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }
        
        [field: SerializeField] 
        [field: Tag]
        public string[] Tags { get; private set; }
        [field: SerializeField] public string LevelUpDescriptionFormat { get; private set; } = "Add +{0} max health";
        
        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return string.Format(LevelUpDescriptionFormat, additionMaxHealth);
        }

        public IDisposable CreateItem(int level)
        {
            return new AddMaxHealthBehaviour(additionMaxHealth);
        }

        public class AddMaxHealthBehaviour : IDisposable
        {
            private readonly int _additionMaxHealth;

            public AddMaxHealthBehaviour(int additionMaxHealth)
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