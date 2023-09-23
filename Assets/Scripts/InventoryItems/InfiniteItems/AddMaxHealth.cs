using System;
using Global;
using Player;
using UnityEngine;
using VContainer;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class AddMaxHealth : ScriptableObject, IInventoryItemDescriptor
    {
        public int additionMaxHealth;

        [field: SerializeField] public Sprite ItemIcon { get; set; }

        public string ItemName => "Max health increase";
        public string[] Tags => new[] { "Hidden" };
        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return $"Add +{additionMaxHealth} max health";
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