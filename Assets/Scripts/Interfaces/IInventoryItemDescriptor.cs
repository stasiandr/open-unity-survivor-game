using System;
using System.Linq;
using UnityEngine;
using VContainer;

namespace Interfaces
{
    public interface IInventoryItemDescriptorBase
    {
        Sprite ItemIcon { get; }
        string ItemName { get; }
        string[] Tags { get; }
        int MaxItemLevel { get; }
        string GetLevelUpDescription(int newLevel);

        
        bool ContainsTag(string tag) => Tags.Contains(tag);
        bool CanCreateItemWithLevel(int newLevel) => newLevel <= MaxItemLevel;
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