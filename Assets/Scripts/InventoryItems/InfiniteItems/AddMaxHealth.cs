using Contracts.InventorySystem;
using InventorySystem;
using NaughtyAttributes;
using Player;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class AddMaxHealth : ScriptableObject, IInventoryItemDescriptor
    {
        [field: SerializeField] public int AdditionMaxHealth { get; private set; }

        [field: SerializeField]
        [field: InventoryItemID]
        public string ID { get; private set; }

        [field: SerializeField] public Sprite ItemIcon { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }

        [field: SerializeField] [field: Tag] public string[] Tags { get; private set; }
        [field: SerializeField] public string LevelUpDescriptionFormat { get; private set; } = "Add +{0} max health";

        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return string.Format(LevelUpDescriptionFormat, AdditionMaxHealth);
        }


        [InventoryItemClass("pickups/add_max_health")]
        public class AddMaxHealthBehaviour : InfiniteItemBehaviourBase<AddMaxHealth>
        {
            [Preserve]
            public AddMaxHealthBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) : base(
                descriptor, data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.AddMaxHealth(Descriptor.AdditionMaxHealth);
            }
        }
    }
}