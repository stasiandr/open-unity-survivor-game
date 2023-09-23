using System;
using Interfaces;
using Interfaces.AbilityInterfaces;
using NaughtyAttributes;
using UnityEngine;

namespace InventoryItems.PassiveItems
{
    public abstract class PassiveItem<TData> : ScriptableObject, IInventoryItemDescriptor 
        where TData : ILevelUpDescription
    {
        public TData[] data;

        [field: SerializeField, ShowAssetPreview]
        public Sprite ItemIcon { get; set; }
        
        [field: SerializeField]
        public string ItemName { get; set; }
        
        [field: SerializeField, Tag]
        public string[] Tags { get; set; }

        public int MaxItemLevel => data.Length - 1;
        
        public string GetLevelUpDescription(int newLevel)
            => data[newLevel].LevelUpDescription;

        public abstract IDisposable CreateItem(int level);
    }
}