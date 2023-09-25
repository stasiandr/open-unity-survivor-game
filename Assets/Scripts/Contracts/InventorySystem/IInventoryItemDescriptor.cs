using System.Linq;
using UnityEngine;

namespace Contracts.InventorySystem
{
    public interface IInventoryItemDescriptor
    {
        string ID { get; }
        Sprite ItemIcon { get; }
        string ItemName { get; }
        string[] Tags { get; }
        int MaxItemLevel { get; }
        string GetLevelUpDescription(int newLevel);


        bool ContainsTag(string tag)
        {
            return Tags.Contains(tag);
        }

        bool CanCreateItemWithLevel(int newLevel)
        {
            return newLevel <= MaxItemLevel;
        }
    }
}