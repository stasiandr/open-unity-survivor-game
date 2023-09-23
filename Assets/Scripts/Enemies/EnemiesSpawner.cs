using System;
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
        [SerializeField] private float distance;

        [Inject]
        public void Construct(IEnemyTarget target)
        {
            var targetPosition = target.WorldPosition; 

            Observable.Interval(TimeSpan.FromSeconds(.5f), Scheduler.MainThreadFixedUpdate)
                .Select(_ => targetPosition.Value + Quaternion.Euler(0, Random.value * 360, 0) * Vector3.forward * distance)
                .Subscribe(pos => Instantiate(enemy, pos, Quaternion.identity).SetActive(true))
                .AddTo(this);
        }
    }
}