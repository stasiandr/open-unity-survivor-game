using Contracts;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class DamageTexts : MonoBehaviour
    {
        [SerializeField] private TMP_Text prefab;

        [SerializeField] private float randomRadius = 1;
        [SerializeField] private float randomUp = 1;
        [SerializeField] private float duration = 1;
        [SerializeField] private float easeOvershoot = 2;

        private Transform _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main!.transform;

            MessageBroker.Default
                .Receive<EnemyDamageReceived>()
                .Subscribe(data =>
                {
                    var instance = Instantiate(prefab, data.WorldPosition,
                        Quaternion.LookRotation(_mainCamera.forward));
                    instance.text = $"-{data.Damage}";

                    var endPosition = data.WorldPosition + Vector3.up * randomUp + Random.onUnitSphere * randomRadius;

                    instance.gameObject.SetActive(true);

                    instance.transform.DOMove(endPosition, duration).SetEase(Ease.OutBack).easeOvershootOrAmplitude =
                        easeOvershoot;

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