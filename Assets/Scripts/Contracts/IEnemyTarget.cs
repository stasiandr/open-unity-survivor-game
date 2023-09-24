using UniRx;
using UnityEngine;

namespace Global
{
    public interface IEnemyTarget
    {
        IReadOnlyReactiveProperty<Vector3> WorldPosition { get; }
    }
}