using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nrjwolf.Tools.AttachAttributes;
using UnityEngine;

namespace GameManagement.SelectionCanvas
{
    [RequireComponent(typeof(Canvas))]
    public class AbilitySelectionCanvas : MonoBehaviour
    {
        [SerializeField] [GetComponent] private Canvas canvas;

        [SerializeField] private AbilityButtonViewPresenter abilityButton;


        private readonly List<AbilityButtonViewPresenter> _lastViews = new();


        private AbilityButtonModel _playerSelected;

        internal void ClickOnAbility(AbilityButtonModel model)
        {
            _playerSelected = model;
        }

        public async UniTask<AbilityButtonModel> SelectAbility(IEnumerable<AbilityButtonModel> models)
        {
            _playerSelected = null;

            foreach (var model in models)
                _lastViews.Add(Instantiate(abilityButton, abilityButton.transform.parent).Construct(this, model));

            canvas.enabled = true;

            await UniTask.WaitUntil(() => _playerSelected != null);

            canvas.enabled = false;

            foreach (var view in _lastViews) view.Destroy();
            _lastViews.Clear();

            return _playerSelected;
        }
    }
}