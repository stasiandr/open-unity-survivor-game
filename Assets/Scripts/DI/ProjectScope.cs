using Contracts;
using InventorySystem;
using InventorySystem.HierarchyPattern;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class ProjectScope : LifetimeScope
    {
        [field: SerializeField] private AllHierarchyObjects allHierarchyObjects;

        private PlayerInput _input;

        protected override void Configure(IContainerBuilder builder)
        {
            CreateInput();

            builder.RegisterInstance(_input).AsSelf();

            builder.Register<InventoryItemFactory>(Lifetime.Scoped).AsSelf();
            builder.RegisterInstance(new HierarchyFactory(allHierarchyObjects)).AsSelf();
        }

        private void CreateInput()
        {
            _input = new PlayerInput();
            _input.Enable();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _input.Disable();
            _input.Dispose();
            _input = null;
        }
    }
}