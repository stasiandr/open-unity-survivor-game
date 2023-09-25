using Contracts;
using Contracts.InventorySystem;
using Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.InventoryView
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemName;

        public IInventoryItemDescriptor Descriptor { get; private set; }

        public InventoryItemView Construct(IInventoryItemDescriptor descriptor)
        {
            Descriptor = descriptor;

            itemImage.sprite = descriptor.ItemIcon;
            itemName.text = descriptor.ItemName;

            return this;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}