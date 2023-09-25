using UniRx;
using UnityEngine;

namespace Contracts
{
    public interface IEnemyTarget
    {
        IReadOnlyReactiveProperty<Vector3> WorldPosition { get; }
    }
}