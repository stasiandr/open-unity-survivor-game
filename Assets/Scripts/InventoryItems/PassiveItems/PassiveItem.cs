using System;
using Global;
using Global.ItemsInterfaces;
using NaughtyAttributes;
using UnityEngine;

namespace InventoryItems.PassiveItems
{
    public abstract class PassiveItem<TData> : ScriptableObject, IInventoryItemDescriptor
        where TData : ILevelUpDescription
    {
        public TData[] data;

        [field: SerializeField] public string ID { get; private set; }

        [field: SerializeField]
        [field: ShowAssetPreview]
        public Sprite ItemIcon { get; set; }

        [field: SerializeField] public string ItemName { get; set; }

        [field: SerializeField] [field: Tag] public string[] Tags { get; set; }

        public int MaxItemLevel => data.Length - 1;

        public string GetLevelUpDescription(int newLevel)
        {
            return data[newLevel].LevelUpDescription;
        }

        public abstract IDisposable CreateItem(int level);
    }
}