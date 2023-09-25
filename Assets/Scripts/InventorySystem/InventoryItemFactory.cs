using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        private readonly Dictionary<string, IInventoryItemDescriptorBase> _id2Descriptor;
 
        [Inject]
        public InventoryItemFactory(IObjectResolver resolver, IPlayerRouter playerRouter, LevelSettings levelSettings)
        {
            _playerRouter = playerRouter;
            _resolver = resolver;
            _id2Descriptor = levelSettings.AllItems.All.ToDictionary(i => i.ID, i => i);
        }

        private InventoryItemBase Create([NotNull] IInventoryItemDescriptorBase descriptorBase, int level)
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

        public InventoryItemBase Create(string id, int level)
        {
            if (!_id2Descriptor.TryGetValue(id, out var descriptor)) 
                throw new NotSupportedException();

            return Create(descriptor, level);
        }
    }
}