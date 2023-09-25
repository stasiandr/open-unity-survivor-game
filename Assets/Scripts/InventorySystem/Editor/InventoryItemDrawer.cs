using Contracts.InventorySystem;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.Editor
{
    [CustomPropertyDrawer(typeof(InventoryItemIDAttribute))]
    public class InventoryItemDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var availableItems = InventoryItemFactory.AllItemTags.Value;
            var index = availableItems.IndexOf(property.stringValue);
            index = EditorGUI.Popup(position, label.text, index, availableItems.ToArray());
            property.stringValue = index == -1 ? "" : availableItems[index];
        }
    }
}