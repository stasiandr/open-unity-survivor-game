using Characters;
using Global;
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
        [SerializeField] [GetComponent] private RockCharacterController controller;
        [SerializeField] private int damage = 1;

        [Inject]
        public void Construct(IEnemyTarget target)
        {
            target.WorldPosition
                .Subscribe(pos => controller.SetDirection((pos - transform.position).normalized))
                .AddTo(this);

            gameObject.OnCollisionStayAsObservable()
                .Where(c => c.collider.CompareTag("Player"))
                .Select(c => c.rigidbody.GetComponent<IDamagable>())
                .Subscribe(hp => hp.DealDamage(damage))
                .AddTo(this);
        }
    }
}