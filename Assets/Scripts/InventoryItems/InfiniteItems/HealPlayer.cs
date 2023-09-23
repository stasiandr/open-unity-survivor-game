using System;
using Global;
using Interfaces;
using Player;
using UnityEngine;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class HealPlayer : ScriptableObject, IInventoryItemDescriptor
    {
        public int heal;

        [field: SerializeField] public Sprite ItemIcon { get; set; }

        public string ItemName => "Heal";
        public string[] Tags => new[] { "Hidden" };
        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return $"Add +{heal} health";
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