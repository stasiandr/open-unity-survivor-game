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
        [SerializeField] private int experience = 1;

        private void Start()
        {
            var targetPositionY = model.localPosition.y;
            model.localPosition -= Vector3.up;

            transform.DOMoveY(targetPositionY, 1f).SetEase(Ease.OutBounce).onComplete += () =>
            {
                gameObject.OnTriggerEnterAsObservable()
                    .Where(c => c.CompareTag("Player"))
                    .Subscribe(c =>
                    {
                        c.GetComponentInParent<PlayerPresenter>().AddExperience(experience);
                        Destroy(gameObject);
                    })
                    .AddTo(this);
                collider.SetActive(true);
            };
        }
    }
}