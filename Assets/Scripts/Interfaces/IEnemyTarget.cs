using UniRx;
using UnityEngine;

namespace Interfaces
{
    public interface IEnemyTarget
    {
        IReadOnlyReactiveProperty<Vector3> WorldPosition { get; }
    }
}