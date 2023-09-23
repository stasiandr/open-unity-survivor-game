using Characters;
using Interfaces;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using VContainer;

namespace Enemies
{
    [RequireComponent(typeof(RockCharacterController))]
    public class EnemyBrains : MonoBehaviour
    {
        [SerializeField, GetComponent] private RockCharacterController controller;

        [SerializeField] private int damage;
        
        [Inject]
        public void Construct(IEnemyTarget target)
        {
            target.WorldPosition
                .Subscribe(pos => controller.Direction.Value = (pos - transform.position).normalized)
                .AddTo(this);

            gameObject.OnCollisionStayAsObservable()
                .Where(c => c.collider.CompareTag("Player"))
                .Select(c => c.rigidbody.GetComponent<IDamagable>())
                .Subscribe(hp => hp.DealDamage(damage))
                .AddTo(this);
        }
    }
}