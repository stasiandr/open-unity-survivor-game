using System;
using Global;
using Player;
using UnityEngine;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class AddGold : ScriptableObject, IInventoryItemDescriptor
    {
        public int additionGold;

        [field: SerializeField] public Sprite ItemIcon { get; set; }

        public string ItemName => "Gold";
        public string[] Tags => new[] { "Hidden" };
        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return $"Add +{additionGold} gold";
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