using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventorySystem.HierarchyPattern
{
    public class HierarchyFactory
    {
        private readonly Dictionary<string, HierarchyObject> _allHierarchyObjects;

        public HierarchyFactory(AllHierarchyObjects allHierarchyObjects)
        {
            _allHierarchyObjects = allHierarchyObjects.Hierarchies
                .ToDictionary(h => h.ItemID, h => h);
        }

        public T Find<T>(string id) where T : HierarchyObject
        {
            if (!_allHierarchyObjects.TryGetValue(id, out var hierarchyObjectBase))
                throw new InventoryItemHierarchyNotFoundException(id);

            var hierarchyObject = hierarchyObjectBase as T;
            if (hierarchyObject == null)
                throw new InventoryItemHierarchyException(id);

            return hierarchyObject;
        }

        public T Instantiate<T>(string id) where T : HierarchyObject
        {
            return Object.Instantiate(Find<T>(id));
        }

        public T Instantiate<T>(string id, Transform parent, bool worldPositionStays = false) where T : HierarchyObject
        {
            return Object.Instantiate(Find<T>(id), parent, worldPositionStays);
        }
    }
}