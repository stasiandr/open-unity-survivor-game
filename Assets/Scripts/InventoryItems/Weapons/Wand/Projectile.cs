using Interfaces;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace InventoryItems.Weapons.Wand
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField, GetComponent] private Rigidbody rb;

        public GameObject[] detachOnDestroy;
        
        public Projectile Construct(float speed, int damage, float duration)
        {
            rb.velocity = transform.forward * speed;
            
            gameObject.OnCollisionEnterAsObservable()
                .Where(col => col.collider.CompareTag("Enemy"))
                .Select(col => col.rigidbody.GetComponent<IDamagable>())
                .Subscribe(d =>
                {
                    d.DealDamage(damage);

                    foreach (var go in detachOnDestroy) go.transform.SetParent(null);

                    Destroy(gameObject);
                })
                .AddTo(this);
            
            Destroy(gameObject, duration);
            
            return this;
        }
    }
}