using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Contracts;
using Contracts.InventorySystem;
using UnityEngine;
using VContainer;

namespace InventorySystem
{
    public class InventoryItemFactory
    {
        public static Lazy<List<string>> AllItemTags { get; private set; } = CreateAllItemsTagsLazy();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            AllItemTags = CreateAllItemsTagsLazy();
        }

        private static Lazy<List<string>> CreateAllItemsTagsLazy()
        {
            return new Lazy<List<string>>(() => AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Select(t => t.GetCustomAttribute<InventoryItemClassAttribute>())
                .Where(a => a != null)
                .Select(a => a.ItemID)
                .ToList());
        }

        private readonly IObjectResolver _resolver;

        private readonly Dictionary<string, IInventoryItemDescriptor> _id2Descriptor;
        private readonly Dictionary<string, Type> _constructorData;

        [Inject]
        public InventoryItemFactory(IObjectResolver resolver, LevelSettings levelSettings)
        {
            _resolver = resolver;
            _id2Descriptor = levelSettings.AllItems.All
                .Where(i => i != null)
                .ToDictionary(i => i.ID, i => i);

            _constructorData = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.GetCustomAttribute<InventoryItemClassAttribute>() != null)
                .ToDictionary(t => t.GetCustomAttribute<InventoryItemClassAttribute>().ItemID, t => t);
        }

        public InventoryItem Create(string id, int level)
        {
            if (!_id2Descriptor.TryGetValue(id, out var descriptor))
                throw new InventoryItemNotFoundException(id);
            if (!_constructorData.TryGetValue(id, out var type))
                throw new InventoryItemNotFoundException(id);

            var instance = Activator.CreateInstance(type, descriptor, new InitializationData(level));

            if (instance == null)
                throw new InventoryItemInvalidConstructorException(id);

            var behaviour = instance as IInventoryItemBehaviour;
            if (behaviour == null)
                throw new InventoryItemInvalidInterfaceException(id);

            _resolver.Inject(instance);
            return new InventoryItem(descriptor, level, behaviour);
        }
    }
}