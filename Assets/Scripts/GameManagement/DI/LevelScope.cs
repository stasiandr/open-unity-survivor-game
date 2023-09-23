using Enemies;
using GameManagement.SelectionCanvas;
using InventorySystem;
using Player;
using VContainer;
using VContainer.Unity;

namespace GameManagement.DI
{
    public class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<PlayerModel>(Lifetime.Singleton).AsSelf();
            
            builder.RegisterComponentInHierarchy<SimpleEnemyTarget>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<AbilitySelectionCanvas>().AsSelf();

            builder.Register<Inventory>(Lifetime.Singleton).AsSelf();
            builder.Register<LevelUpService>(Lifetime.Singleton).AsSelf();
            
            builder.RegisterEntryPoint<GameService>();
        }
    }
}