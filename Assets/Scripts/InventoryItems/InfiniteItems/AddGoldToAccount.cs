using Contracts.InventorySystem;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class AddGold : ScriptableObject, IInventoryItemDescriptor
    {
        [field: SerializeField] public int AdditionGold { get; private set; }

        [field: InventoryItemID]
        [field: SerializeField]
        public string ID { get; private set; }

        [field: SerializeField] public Sprite ItemIcon { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }

        [field: SerializeField] [field: Tag] public string[] Tags { get; private set; }
        [field: SerializeField] public string LevelUpDescriptionFormat { get; private set; } = "Add +{0} gold";

        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return string.Format(LevelUpDescriptionFormat, AdditionGold);
        }

        [InventoryItemClass("pickups/add_gold")]
        public class AddGoldBehaviour : InfiniteItemBehaviourBase<AddGold>
        {
            [Preserve]
            public AddGoldBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) : base(descriptor,
                data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.AddGold(Descriptor.AdditionGold);
            }
        }
    }
}