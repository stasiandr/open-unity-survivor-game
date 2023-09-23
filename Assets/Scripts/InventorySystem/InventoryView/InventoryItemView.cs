using Global;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem.InventoryView
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        [SerializeField] private TMP_Text itemName;

        public IInventoryItemDescriptorBase Descriptor { get; private set; }

        public InventoryItemView Construct(IInventoryItemDescriptorBase descriptor)
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