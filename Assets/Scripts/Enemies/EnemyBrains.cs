using Characters;
using Contracts;
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

        // TODO: move to config
        [SerializeField] private int damage = 1;

        [Inject]
        public void Construct(IEnemyTarget target)
        {
            target.WorldPosition
                .Subscribe(pos => controller.SetDirection((pos - transform.position).normalized))
                .AddTo(this);


            gameObject.OnCollisionStayAsObservable()
                .Select(c => c.collider.GetComponentInParent<IPlayerHealth>())
                .Where(ph => ph != null)
                .Subscribe(hp => hp.DealDamage(damage))
                .AddTo(this);
        }
    }
}