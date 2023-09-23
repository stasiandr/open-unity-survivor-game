using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Extensions;
using Interfaces.PlayerStatsInterfaces;
using UniRx;

namespace Player
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PlayerModel
    {

        #region Health
        public IReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
        public IReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;
        public IReadOnlyReactiveProperty<bool> IsDead => _currentHealth
            .Select(val => val <= 0)
            .ToReadOnlyReactiveProperty();

        private readonly IntReactiveProperty _maxHealth;
        private readonly IntReactiveProperty _currentHealth;
        
        #endregion
        
        #region Experience

        private static List<int> ExperienceToNextLevelList => Enumerable.Repeat(8, 100).ToList();
        private static List<int> ExperienceThresholdsList => ExperienceToNextLevelList.CumulativeSum().ToList();
        
        
        /// <summary>
        /// Total experience that player collected throughout session
        /// </summary>
        public IReadOnlyReactiveProperty<int> CurrentTotalExperience => _currentTotalExperience;
        
        /// <summary>
        /// Current player level.
        /// </summary>
        public IReadOnlyReactiveProperty<int> CurrentLevel => CurrentTotalExperience
            .Select(exp => ExperienceThresholdsList.FindIndex(threshold => exp < threshold))
            .ToReadOnlyReactiveProperty();
        
        /// <summary>
        /// Player experience relative to last level cost. 
        /// </summary>
        public IReadOnlyReactiveProperty<int> CurrentExperienceAboveLastLevel => CurrentTotalExperience
            .Select(exp => CurrentLevel.Value == 0 ? exp : exp - ExperienceThresholdsList[CurrentLevel.Value - 1])
            .ToReadOnlyReactiveProperty();
        
        /// <summary>
        /// How much experience relative to last level cost player needs to level up.
        /// </summary>
        public IReadOnlyReactiveProperty<int> ExperienceToLevelUp => CurrentLevel
            .Select(lvl => ExperienceToNextLevelList[lvl])
            .ToReadOnlyReactiveProperty();
        
        /// <summary>
        /// How much total experience player needs to level up.
        /// </summary>
        public IReadOnlyReactiveProperty<int> TotalExperienceToLevelUp => CurrentLevel
            .Select(lvl => ExperienceThresholdsList[lvl])
            .ToReadOnlyReactiveProperty();
        

        private readonly IntReactiveProperty _currentTotalExperience;

        #endregion

        #region Other

        public IntReactiveProperty BaseDamage { get; private set; } = new(0);
        public IntReactiveProperty ExtraProjectiles { get; private set; } = new(0);
        public FloatReactiveProperty MoveSpeedPercent { get; private set; } = new(0);
        public FloatReactiveProperty ProjectileSpeed { get; private set; } = new(0);
        public FloatReactiveProperty IntervalDecrease { get; private set; } = new FloatReactiveProperty(0);


        public IObservable<Unit> PlayerStatsChanged => BaseDamage.Select(_ => Unit.Default)
            .Merge(ExtraProjectiles.Select(_ => Unit.Default))
            .Merge(MoveSpeedPercent.Select(_ => Unit.Default))
            .Merge(ProjectileSpeed.Select(_ => Unit.Default))
            .Merge(IntervalDecrease.Select(_ => Unit.Default));


        #endregion
        

        public PlayerModel()
        {
            _maxHealth = new IntReactiveProperty(100);
            _currentHealth = new IntReactiveProperty(_maxHealth.Value);

            _currentTotalExperience = new IntReactiveProperty(0);
        }

        public void DealDamage(int damage) => _currentHealth.Value -= damage;
        public void Heal(int health) => _currentHealth.Value += health;
        
        public void AddExperience(int experience) => _currentTotalExperience.Value += experience;

        public void AddMaxHealth(int additionMaxHealth) => _maxHealth.Value += additionMaxHealth;
        public void RemoveMaxHealth(int additionMaxHealth) => _maxHealth.Value -= additionMaxHealth;

        public void InjectPlayerStats(ref object target)
        {
            if (target is IAbilityDamage bd) bd.AbilityDamage += BaseDamage.Value;
            if (target is IMoveSpeed mp) mp.MoveSpeed += mp.MoveSpeed * MoveSpeedPercent.Value / 100;
            if (target is IProjectileSpeed pps) pps.ProjectileSpeed += pps.ProjectileSpeed * MoveSpeedPercent.Value / 100;
            if (target is IProjectilesCount ep) ep.ProjectilesCount += ExtraProjectiles.Value;
            if (target is IAbilityInterval ai) ai.Interval -= ai.Interval * IntervalDecrease.Value / 100;
        }
    }
}