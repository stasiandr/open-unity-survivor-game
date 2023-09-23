using System;
using Global;
using Interfaces;
using UniRx;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemy;
        [SerializeField] private float distance = 10;
        [SerializeField] private float interval = .5f;

        [Inject]
        public void Construct(IEnemyTarget target)
        {
            var targetPosition = target.WorldPosition;

            Observable.Interval(TimeSpan.FromSeconds(interval), Scheduler.MainThreadFixedUpdate)
                .Select(_ => targetPosition.Value + RandomOnUnitCircle3D() * distance)
                .Subscribe(pos => Instantiate(enemy, pos, Quaternion.identity).SetActive(true))
                .AddTo(this);
        }

        private static Vector3 RandomOnUnitCircle3D()
        {
            return Quaternion.Euler(0, Random.value * 360, 0) * Vector3.forward;
        }
    }
}