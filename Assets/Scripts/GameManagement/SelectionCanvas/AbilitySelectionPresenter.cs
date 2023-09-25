using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nrjwolf.Tools.AttachAttributes;
using UniRx;
using UnityEngine;

namespace GameManagement.SelectionCanvas
{
    [RequireComponent(typeof(Canvas))]
    public class AbilitySelectionPresenter : MonoBehaviour
    {
        [SerializeField] [GetComponent] private Canvas canvas;

        [SerializeField] private AbilityButtonView abilityButton;


        private readonly List<AbilityButtonView> _lastViews = new();


        private AbilityButtonModel _playerSelected;

        public async UniTask<AbilityButtonModel> SelectAbility(IEnumerable<AbilityButtonModel> models)
        {
            _playerSelected = null;

            foreach (var model in models)
            {
                var view = Instantiate(abilityButton, abilityButton.transform.parent).Construct(model);

                view.Button.OnClickAsObservable()
                    .Select(_ => model)
                    .Subscribe(m => _playerSelected = m)
                    .AddTo(view);

                _lastViews.Add(view);
            }

            canvas.enabled = true;

            await UniTask.WaitUntil(() => _playerSelected != null);

            canvas.enabled = false;

            foreach (var view in _lastViews) view.Destroy();
            _lastViews.Clear();

            return _playerSelected;
        }
    }
}