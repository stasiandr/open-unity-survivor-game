using System;
using Contracts;
using Cysharp.Threading.Tasks;
using GameManagement.SelectionCanvas;
using Global;
using InventorySystem;
using Player;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GameManagement
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GameService : IStartable, IDisposable
    {
        private readonly CompositeDisposable _lifetime = new();

        [Inject] private readonly PlayerModel _playerModel;
        [Inject] private readonly AbilitySelectionCanvas _abilitySelectionCanvas;
        [Inject] private readonly LevelUpService _levelUpService;
        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly LevelSettings _levelSettings;
        [Inject] private readonly InventoryItemFactory _factory;

        public IReadOnlyReactiveProperty<int> PlayerLevel => _playerModel.CurrentLevel;

        void IDisposable.Dispose()
        {
            _lifetime.Dispose();
        }

        void IStartable.Start()
        {
            _playerModel.CurrentLevel
                .Skip(1)
                .Subscribe(_ => UniTask.Create(async () =>
                {
                    Time.timeScale = 0;
                    var playerSelected =
                        await _abilitySelectionCanvas.SelectAbility(_levelUpService.GenerateAvailableItems(3));

                    playerSelected.createCallback();

                    Time.timeScale = 1;
                }).Forget())
                .AddTo(_lifetime);
            
            
            _inventory.Add(_factory.Create(_levelSettings.StartingItem, 0));
        }
    }
}