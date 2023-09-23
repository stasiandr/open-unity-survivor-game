using System;
using Cysharp.Threading.Tasks;
using GameManagement.SelectionCanvas;
using Player;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace GameManagement
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GameService : IStartable, IDisposable
    {
        private readonly CompositeDisposable _lifetime = new();

        private readonly PlayerModel _playerModel;
        private readonly AbilitySelectionCanvas _abilitySelectionCanvas;

        public IReadOnlyReactiveProperty<int> PlayerLevel => _playerModel.CurrentLevel;

        public GameService(PlayerModel playerModel,
            AbilitySelectionCanvas abilitySelectionCanvas,
            LevelUpService levelUpService)
        {
            _playerModel = playerModel;
            _abilitySelectionCanvas = abilitySelectionCanvas;

            _playerModel.CurrentLevel
                .Skip(1)
                .Subscribe(_ => UniTask.Create(async () =>
                {
                    Time.timeScale = 0;
                    var playerSelected =
                        await _abilitySelectionCanvas.SelectAbility(levelUpService.GenerateAvailableItems(3));

                    playerSelected.createCallback();

                    Time.timeScale = 1;
                })).AddTo(_lifetime);
        }

        void IDisposable.Dispose()
        {
            _lifetime.Dispose();
        }

        void IStartable.Start()
        {
        }
    }
}