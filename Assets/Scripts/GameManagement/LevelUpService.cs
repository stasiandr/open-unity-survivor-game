using System.Collections.Generic;
using System.Linq;
using Contracts;
using Contracts.InventorySystem;
using GameManagement.SelectionCanvas;
using Global;
using InventorySystem;
using UnityEngine;
using VContainer;

namespace GameManagement
{
    public class LevelUpService
    {
        [Inject] private readonly LevelSettings _levelSettings;
        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly InventoryItemFactory _factory;

        public IEnumerable<AbilityButtonModel> GenerateAvailableItems(int count)
        {
            var result = new List<AbilityButtonModel>();

            foreach (var item in _levelSettings.AllItems.Weapons)
            {
                var newLevel = _inventory.TryGet(item.ID, out var inventoryItem) ? inventoryItem.Level + 1 : 0;

                if (newLevel != 0 && !inventoryItem.Descriptor.CanCreateItemWithLevel(newLevel)) continue;

                result.Add(BuildButtonModel(item, newLevel));
            }

            foreach (var item in _levelSettings.AllItems.Buffs)
            {
                var newLevel = _inventory.TryGet(item.ID, out var inventoryItem) ? inventoryItem.Level + 1 : 0;

                if (newLevel != 0 && !inventoryItem.Descriptor.CanCreateItemWithLevel(newLevel)) continue;

                result.Add(BuildButtonModel(item, newLevel));
            }


            while (result.Count < count)
            {
                var item = _levelSettings.AllItems.InfiniteBuffs.GetRandom();
                result.Add(BuildButtonModel(item, 0));
            }

            return result.OrderBy(x => Random.Range(0, result.Count)).Take(count).ToArray();
        }

        private AbilityButtonModel BuildButtonModel(IInventoryItemDescriptor descriptor, int newLevel)
        {
            return new AbilityButtonModel
            {
                Descriptor = descriptor,
                Level = newLevel,
            };
        }
    }
}