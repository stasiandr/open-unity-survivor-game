using System;
using System.Linq;
using UnityEngine;

namespace Global
{
    public interface IInventoryItemDescriptorBase
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

    public interface IInventoryItemGameObjectDescriptor : IInventoryItemDescriptorBase
    {
        GameObject CreateItem(int level);
    }

    public interface IInventoryItemDescriptor : IInventoryItemDescriptorBase
    {
        IDisposable CreateItem(int level);
    }
}