using System;
using System.Diagnostics.CodeAnalysis;
using Contracts;
using Global;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InventorySystem
{
    public class InventoryItemFactory
    {
        private readonly IObjectResolver _resolver;
        private readonly IPlayerRouter _playerRouter;

        [Inject]
        public InventoryItemFactory(IObjectResolver resolver, IPlayerRouter playerRouter)
        {
            _playerRouter = playerRouter;
            _resolver = resolver;
        }

        public InventoryItemBase Create([NotNull] IInventoryItemDescriptorBase descriptorBase, int level)
        {
            Debug.Assert(descriptorBase != null);

            if (descriptorBase is IInventoryItemGameObjectDescriptor goDescriptor)
            {
                var instance = goDescriptor.CreateItem(level);
                _resolver.InjectGameObject(instance);

                instance.transform.SetParent(_playerRouter.WeaponsContainer, false);

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