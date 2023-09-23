using Enemies;
using GameManagement;
using GameManagement.SelectionCanvas;
using InventorySystem;
using Player;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf();

            builder.RegisterComponentInHierarchy<PlayerRouter>().AsImplementedInterfaces();

            builder.Register<PlayerTargetAdapter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<AbilitySelectionCanvas>().AsSelf();

            builder.Register<Inventory>(Lifetime.Singleton).AsSelf();
            builder.Register<LevelUpService>(Lifetime.Singleton).AsSelf();

            builder.RegisterEntryPoint<GameService>();
        }
    }
}