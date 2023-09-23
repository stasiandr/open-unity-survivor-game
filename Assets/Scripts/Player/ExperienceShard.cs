using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Player
{
    public class ExperienceShard : MonoBehaviour
    {
        [SerializeField] private new GameObject collider;
        [SerializeField] private Transform model;
        [SerializeField] private int experience;

        // ReSharper disable once Unity.IncorrectMethodSignature
        private async UniTaskVoid Start()
        {
            var targetPositionY = model.localPosition.y;
            model.localPosition -= Vector3.up;
            await transform.DOMoveY(targetPositionY, 1f).SetEase(Ease.OutBounce);
            
            gameObject.OnTriggerEnterAsObservable()
                .Where(c => c.CompareTag("Player"))
                .Subscribe(c =>
                {
                    c.GetComponentInParent<PlayerPresenter>().AddExperience(experience);
                    Destroy(gameObject);
                })
                .AddTo(this);
            collider.SetActive(true);
        }
    }
}