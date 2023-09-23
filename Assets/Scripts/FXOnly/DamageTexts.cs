using DG.Tweening;
using Interfaces;
using TMPro;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FXOnly
{
    public class DamageTexts : MonoBehaviour
    {
        [SerializeField] private TMP_Text prefab;

        [SerializeField] private float randomRadius = 1;
        [SerializeField] private float randomUp = 1;
        [SerializeField] private float duration = 1;
        [SerializeField] private float easeOvershoot = 2;
        
        public void Start()
        {
            MessageBroker.Default
                .Receive<EnemyDamageReceived>()
                .Subscribe(data =>
                {
                    var instance = Instantiate(prefab, data.WorldPosition, Quaternion.identity);
                    instance.text = $"-{data.Damage}";

                    var endPosition = data.WorldPosition + Vector3.up * randomUp + Random.onUnitSphere * randomRadius;

                    instance.gameObject.SetActive(true);

                    instance.transform.DOMove(endPosition, duration).SetEase(Ease.OutBack).easeOvershootOrAmplitude = easeOvershoot;
                    
                    DOTween.Sequence()
                        .AppendInterval(duration / 2)
                        .Append(instance.DOFade(0, duration / 2))
                        .Play();

                    Destroy(instance.gameObject, duration + 0.1f);
                })
                .AddTo(this);
        }
    }
}