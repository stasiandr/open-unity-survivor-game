using System;
using UnityEngine;

namespace Contracts.InventorySystem
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InventoryItemClassAttribute : Attribute
    {
        public string ItemID { get; private set; }

        public InventoryItemClassAttribute(string itemID)
        {
            ItemID = itemID;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class InventoryItemIDAttribute : PropertyAttribute
    {
    }
}