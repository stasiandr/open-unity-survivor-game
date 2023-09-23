using Interfaces;
using UniRx;
using UnityEngine;

namespace Enemies
{
    public class SimpleEnemyTarget : MonoBehaviour, IEnemyTarget
    {
        public IReadOnlyReactiveProperty<Vector3> WorldPosition => transform
            .ObserveEveryValueChanged(t => t.position).ToReadOnlyReactiveProperty();
    }
}