using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using TNRD;
using UnityEngine;

namespace GameManagement.DI
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
    }
}