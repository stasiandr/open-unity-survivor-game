using Contracts;
using UnityEngine;
using VContainer;

namespace Player
{
    public class PlayerPresenter : MonoBehaviour, IPlayerHealth
    {
        private PlayerModel _playerModel;

        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void DealDamage(int damage)
        {
            _playerModel.DealDamage(damage);
        }

        public void AddExperience(int experience)
        {
            _playerModel.AddExperience(experience);
        }
    }
}