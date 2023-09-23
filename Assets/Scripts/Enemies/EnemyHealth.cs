using Interfaces;
using UniRx;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour, IDamagable
    {
        [SerializeField] private int maxHealth;
        [SerializeField] private GameObject spawnOnDeath;
        
        public IntReactiveProperty CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = new IntReactiveProperty(maxHealth);

            CurrentHealth.Where(hp => hp <= 0).Subscribe(_ =>
            {
                if (spawnOnDeath != null) Instantiate(spawnOnDeath, transform.position, Quaternion.identity).SetActive(true);
                Destroy(gameObject);
            }).AddTo(this);
        }

        void IDamagable.DealDamage(int damage)
        {
            CurrentHealth.Value -= damage;
            MessageBroker.Default.Publish(new EnemyDamageReceived
            {
                Damage = damage,
                WorldPosition = transform.position,
            });
        }
    }
}