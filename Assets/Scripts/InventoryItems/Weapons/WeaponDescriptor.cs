using System;
using Global;
using Global.ItemsInterfaces;
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

        public GameObject CreateItem(int level)
        {
            var go = Instantiate(prefab);
            go.PassData(data[level]);

            return go.gameObject;
        }
    }
}