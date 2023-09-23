using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Player
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private Slider experience;
        [SerializeField] private Slider health;
        [SerializeField] private TMP_Text currentHealthText;
        [SerializeField] private TMP_Text currentLevelText;

        [Inject]
        public void Construct(PlayerModel playerModel)
        {
            playerModel.CurrentHealth
                .CombineLatest(playerModel.MaxHealth, (current, max) => (float)current / max)
                .Subscribe(val => health.value = val)
                .AddTo(this);

            playerModel.CurrentHealth
                .CombineLatest(playerModel.MaxHealth, (current, max) => $"{current}/{max}")
                .Subscribe(val => currentHealthText.text = $"Health: {val}")
                .AddTo(this);

            playerModel.CurrentExperienceAboveLastLevel
                .CombineLatest(playerModel.ExperienceToLevelUp, (current, cost) => (float)current / cost)
                .Subscribe(exp => experience.value = exp)
                .AddTo(this);

            playerModel.CurrentLevel
                .Select(lvl => $"Current level: {lvl}")
                .Subscribe(txt => currentLevelText.text = txt)
                .AddTo(this);
        }
    }
}