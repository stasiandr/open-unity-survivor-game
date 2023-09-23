using System;
using Interfaces;
using Interfaces.AbilityInterfaces;
using NaughtyAttributes;
using UnityEngine;

namespace InventoryItems.Weapons
{
    public abstract class WeaponDescriptor<TData, TBehaviour> : ScriptableObject, IInventoryItemGameObjectDescriptor
        where TBehaviour : WeaponBehaviour<TData>
        where TData : ILevelUpDescription, ICloneable
    {
        public TData[] data;

        public TBehaviour prefab;

        [field: SerializeField, ShowAssetPreview]
        public Sprite ItemIcon { get; set; }
        
        [field: SerializeField]
        public string ItemName { get; set; }
        
        [field: SerializeField, Tag]
        public string[] Tags { get; set; }
        
        public int MaxItemLevel => data.Length - 1;

        public string GetLevelUpDescription(int newLevel)
            => data[newLevel].LevelUpDescription;

        public GameObject CreateItem(int level)
        {
            var go = Instantiate(prefab);
            go.PassData(data[level]);

            return go.gameObject;
        }
    }
}