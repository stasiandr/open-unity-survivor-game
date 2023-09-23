using System;
using Player;
using UniRx;
using UnityEngine;

namespace InventoryItems.Weapons
{
    public abstract class WeaponBehaviour<TData> : MonoBehaviour where TData : ICloneable
    {
        private TData _baseData;

        public void PassData(TData data)
        {
            _baseData = data;
        }

        protected void SubscribeToPlayer(PlayerModel playerModel)
        {
            playerModel.PlayerStatsChanged
                .Subscribe(_ =>
                {
                    var baseData = _baseData.Clone();
                    playerModel.InjectPlayerStats(ref baseData);
                    RuntimeData = (TData)baseData;
                }).AddTo(this);
        }

        protected TData RuntimeData { get; private set; }
    }
}