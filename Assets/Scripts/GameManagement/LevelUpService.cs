using System.Collections.Generic;
using System.Linq;
using Contracts;
using GameManagement.SelectionCanvas;
using Global;
using InventorySystem;
using UnityEngine;

namespace GameManagement
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class LevelUpService
    {
        private readonly AllInGameItems _allInGameItems;
        private readonly Inventory _inventory;
        private readonly InventoryItemFactory _factory;

        public IEnumerable<AbilityButtonModel> GenerateAvailableItems(int count)
        {
            var result = new List<AbilityButtonModel>();

            foreach (var item in _allInGameItems.Weapons)
            {
                var newLevel = _inventory.TryGetByPrototype(item, out var inventoryItem) ? inventoryItem.Level + 1 : 0;

                if (newLevel != 0 && !inventoryItem.Descriptor.CanCreateItemWithLevel(newLevel)) continue;

                result.Add(BuildButtonModel(item, newLevel));
            }

            foreach (var item in _allInGameItems.Buffs)
            {
                var newLevel = _inventory.TryGetByPrototype(item, out var inventoryItem) ? inventoryItem.Level + 1 : 0;

                if (newLevel != 0 && !inventoryItem.Descriptor.CanCreateItemWithLevel(newLevel)) continue;

                result.Add(BuildButtonModel(item, newLevel));
            }


            while (result.Count < count)
            {
                var item = _allInGameItems.InfiniteBuffs.GetRandom();
                result.Add(BuildButtonModel(item, 0));
            }

            return result.OrderBy(x => Random.Range(0, result.Count)).Take(count).ToArray();
        }

        private AbilityButtonModel BuildButtonModel(IInventoryItemDescriptorBase descriptor, int newLevel)
        {
            return new AbilityButtonModel
            {
                icon = descriptor.ItemIcon,
                name = descriptor.ItemName,
                description = descriptor.GetLevelUpDescription(newLevel),
                level = newLevel,
                createCallback = () =>
                {
                    _inventory.RemoveByPrototype(descriptor);
                    _inventory.Add(_factory.Create(descriptor, newLevel));
                }
            };
        }


        public LevelUpService(LevelSettings levelSettings, Inventory inventory, InventoryItemFactory factory)
        {
            _factory = factory;
            _allInGameItems = levelSettings.AllItems;
            _inventory = inventory;
        }
    }
}