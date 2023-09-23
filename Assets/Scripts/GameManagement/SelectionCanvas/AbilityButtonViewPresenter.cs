using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagement.SelectionCanvas
{
    public class AbilityButtonViewPresenter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image abilityImage;
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private TMP_Text abilityDescription;
        
        public AbilityButtonViewPresenter Construct(AbilitySelectionCanvas abilitySelectionCanvas,
            AbilityButtonModel model)
        {
            abilityImage.sprite = model.icon;
            abilityName.text = model.name;
            abilityDescription.text = model.description;
            button.OnClickAsObservable().Subscribe(_ => abilitySelectionCanvas.ClickOnAbility(model)).AddTo(this);

            gameObject.SetActive(true);
            return this;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}