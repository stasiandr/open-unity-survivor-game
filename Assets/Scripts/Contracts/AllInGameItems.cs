using System;
using System.Collections.Generic;
using System.Linq;
using TNRD;
using UnityEngine;

namespace Global
{
    [Serializable]
    public class AllInGameItems
    {
        public IEnumerable<IInventoryItemDescriptorBase> Weapons => weapons.Select(si => si.Value);
        [SerializeField] private List<SerializableInterface<IInventoryItemDescriptorBase>> weapons;

        public IEnumerable<IInventoryItemDescriptorBase> Buffs => buffs.Select(si => si.Value);
        [SerializeField] private List<SerializableInterface<IInventoryItemDescriptorBase>> buffs;

        public IEnumerable<IInventoryItemDescriptorBase> InfiniteBuffs => infiniteBuffs.Select(si => si.Value);
        [SerializeField] private List<SerializableInterface<IInventoryItemDescriptorBase>> infiniteBuffs;

        public IEnumerable<IInventoryItemDescriptorBase> All => Weapons.Concat(Buffs).Concat(InfiniteBuffs);
    }
}