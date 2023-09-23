using Global;
using UniRx;
using UnityEngine;
using VContainer;

namespace Enemies
{
    public class PlayerTargetAdapter : IEnemyTarget
    {
        [Inject]
        public PlayerTargetAdapter(IPlayerRouter router)
        {
            WorldPosition = router.PlayerBase
                .ObserveEveryValueChanged(t => t.position)
                .ToReadOnlyReactiveProperty();
        }

        public IReadOnlyReactiveProperty<Vector3> WorldPosition { get; }
    }
}