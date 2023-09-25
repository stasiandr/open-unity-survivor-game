using System;
using System.Collections.Generic;
using System.Linq;
using Contracts.InventorySystem;
using TNRD;
using UnityEngine;

namespace Contracts
{
    [Serializable]
    public class AllInGameItems
    {
        public IEnumerable<IInventoryItemDescriptor> Weapons => weapons.Select(si => si.Value);
        [SerializeField] private List<SerializableInterface<IInventoryItemDescriptor>> weapons;

        public IEnumerable<IInventoryItemDescriptor> Buffs => buffs.Select(si => si.Value);
        [SerializeField] private List<SerializableInterface<IInventoryItemDescriptor>> buffs;

        public IEnumerable<IInventoryItemDescriptor> InfiniteBuffs => infiniteBuffs.Select(si => si.Value);
        [SerializeField] private List<SerializableInterface<IInventoryItemDescriptor>> infiniteBuffs;

        public IEnumerable<IInventoryItemDescriptor> All => Weapons.Concat(Buffs).Concat(InfiniteBuffs);
    }
}