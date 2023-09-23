using Global;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace InventoryItems.Weapons.Orb
{
    public class RotatingSphereWeapon : MonoBehaviour
    {
        public int damage;
        public float angularSpeed;

        /// <summary>
        /// Offset measured in degrees.
        /// </summary>
        public float offset;

        private Vector3 _localPositionOnStart;

        private void Start()
        {
            gameObject.OnTriggerEnterAsObservable()
                .Where(c => c.CompareTag("Enemy"))
                .Select(c => c.GetComponentInParent<IDamagable>())
                .Subscribe(hp => hp.DealDamage(damage))
                .AddTo(this);

            _localPositionOnStart = transform.localPosition;
        }

        // Yes, UniRx bug 
        private void LateUpdate()
        {
            transform.position = transform.parent.position +
                                 Quaternion.Euler(0, Time.time * 360 * angularSpeed + offset, 0) *
                                 _localPositionOnStart;
        }
    }
}