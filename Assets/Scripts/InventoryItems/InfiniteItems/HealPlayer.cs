using Contracts.InventorySystem;
using InventorySystem;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Scripting;

namespace InventoryItems.InfiniteItems
{
    [CreateAssetMenu]
    public class HealPlayer : ScriptableObject, IInventoryItemDescriptor
    {
        [field: SerializeField] public int Heal { get; private set; }

        [field: SerializeField]
        [field: InventoryItemID]
        public string ID { get; private set; }

        [field: SerializeField] public Sprite ItemIcon { get; private set; }

        [field: SerializeField] public string ItemName { get; private set; }

        [field: SerializeField] [field: Tag] public string[] Tags { get; private set; }
        [field: SerializeField] public string LevelUpDescriptionFormat { get; private set; } = "Add +{0} health";

        public int MaxItemLevel => int.MaxValue;

        public string GetLevelUpDescription(int newLevel)
        {
            return string.Format(LevelUpDescriptionFormat, Heal);
        }

        [InventoryItemClass("pickups/heal_player")]
        public class HealPlayerBehaviour : InfiniteItemBehaviourBase<HealPlayer>
        {
            [Preserve]
            public HealPlayerBehaviour(IInventoryItemDescriptor descriptor, InitializationData data) : base(descriptor,
                data)
            {
            }

            public override void OnItemAdd()
            {
                PlayerModel.Heal(Descriptor.Heal);
            }
        }
    }
}