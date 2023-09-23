using Interfaces;
using InventorySystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameManagement.DI
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField]
        private AllInGameItems allItems;
        
        private PlayerInput _input;

        protected override void Configure(IContainerBuilder builder)
        {
            CreateInput();

            builder.RegisterInstance(_input).AsSelf();
            
            builder.RegisterInstance(allItems).AsSelf();

            builder.Register<InventoryItemFactory>(Lifetime.Scoped).AsSelf();
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