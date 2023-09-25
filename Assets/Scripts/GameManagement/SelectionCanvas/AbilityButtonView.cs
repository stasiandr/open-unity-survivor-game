using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagement.SelectionCanvas
{
    public class AbilityButtonView : MonoBehaviour
    {
        [field: SerializeField] public Button Button { get; private set; }
        [SerializeField] private Image abilityImage;
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private TMP_Text abilityDescription;

        public AbilityButtonView Construct(AbilityButtonModel model)
        {
            abilityImage.sprite = model.Descriptor.ItemIcon;
            abilityName.text = model.Descriptor.ItemName;
            abilityDescription.text = model.Descriptor.GetLevelUpDescription(model.Level);

            gameObject.SetActive(true);
            return this;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}