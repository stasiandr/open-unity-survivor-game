using System;
using System.Diagnostics.CodeAnalysis;
using Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InventorySystem
{
    public class InventoryItemFactory
    {
        private readonly IObjectResolver _resolver;

        [Inject]
        public InventoryItemFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public InventoryItemBase Create([NotNull] IInventoryItemDescriptorBase descriptorBase, int level)
        {
            Debug.Assert(descriptorBase != null);
            
            if (descriptorBase is IInventoryItemGameObjectDescriptor goDescriptor)
            {
                var instance = goDescriptor.CreateItem(level);
                _resolver.InjectGameObject(instance);
                instance.transform.SetParent(WeaponContainerSingleton.Instance.transform, false);
                
                return new InventoryItemGameObject(goDescriptor, level, instance);
            }
            else if (descriptorBase is IInventoryItemDescriptor descriptor)
            {
                var instance = descriptor.CreateItem(level);
                _resolver.Inject(instance);
                return new InventoryItem(descriptor, level, instance);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}