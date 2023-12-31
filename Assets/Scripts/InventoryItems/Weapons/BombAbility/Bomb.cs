using System;
using System.Linq;
using Contracts;
using Cysharp.Threading.Tasks;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

namespace InventoryItems.Weapons.BombAbility
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] [GetComponent] private Rigidbody rb;

        public GameObject onExplosion;

        public Bomb Construct(float radius, int damage, float duration)
        {
            rb.velocity = transform.forward * 5;

            UniTask.Create(async () =>
            {
                await UniTask.Delay(TimeSpan.FromSeconds(duration));

                // ReSharper disable once Unity.PreferNonAllocApi
                foreach (var damagable in Physics.OverlapSphere(transform.position, radius)
                             .Select(c => c.GetComponentInParent<IEnemyHealth>())
                             .Where(ph => ph != null))
                    damagable.DealDamage(damage);

                Instantiate(onExplosion, transform.position, Quaternion.identity).transform.localScale =
                    Vector3.one * radius;

                Destroy(gameObject);
            });

            return this;
        }
    }
}