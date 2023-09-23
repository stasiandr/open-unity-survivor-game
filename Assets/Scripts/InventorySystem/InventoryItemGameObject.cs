using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InventorySystem
{
    public abstract class InventoryItemBase
    {
        public IInventoryItemDescriptorBase Descriptor { get; private set; }
        public int Level { get; private set; }

        public InventoryItemBase(IInventoryItemDescriptorBase descriptor, int level)
        {
            Descriptor = descriptor;
            Level = level;
        }

        public virtual void OnAdd() { }

        public virtual void OnRemove() { }
    }


    public class InventoryItemGameObject : InventoryItemBase
    {
        private readonly GameObject _gameObject;

        public InventoryItemGameObject(IInventoryItemDescriptorBase descriptor, int level, GameObject gameObject) : base(descriptor, level)
        {
            _gameObject = gameObject;
        }
        public override void OnRemove() => Object.Destroy(_gameObject);
    }

    public class InventoryItem : InventoryItemBase
    {
        private readonly IDisposable _disposable;

        public InventoryItem(IInventoryItemDescriptorBase descriptor, int level, IDisposable disposable) : base(descriptor, level)
            => _disposable = disposable;

        public override void OnRemove() => _disposable.Dispose();
    }
    
}