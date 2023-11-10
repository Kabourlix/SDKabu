// Created by Kabourlix Cendrée on 29/10/2023

#nullable enable
using System;
using UnityEngine;

namespace SDKabu.KCharacter
{
    public class KHealthComp : IKHealth, IDisposable
    {
        #region Events

        /// <summary>
        /// Event fired when an actor dies, the parameter is the actor identifier.
        /// </summary>
        public event Action<IKActor>? OnActorDie;

        /// <summary>
        /// Event fired when an actor takes damage, the parameter is the actor identifier.
        /// </summary>
        public event Action<IKActor>? OnIncomingDamage;

        /// <summary>
        /// Fired when health changed (heal or damage), the parameter is the new health.
        /// </summary>
        public event Action<IKActor, int>? OnHealthChanged;

        /// <summary>
        /// Fired when max health changed, the parameter is the new max health.
        /// </summary>
        public event Action<IKActor, int>? OnMaxHealthChanged;

        #endregion


        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public float HealthPercentage => (float)Health / MaxHealth;

        private Func<int, int>? dmgTakenModifier = null;
        private Func<int, int>? healthGainedModifier = null;

        private readonly IKActor owner;

        public KHealthComp(int _maxHealth, IKActor _owner, Func<int, int>? _dmgFunc = null,
            Func<int, int>? _healFunc = null)
        {
            owner = _owner;
            dmgTakenModifier = _dmgFunc;
            healthGainedModifier = _healFunc;
            Init(_maxHealth);
        }

        public void Dispose()
        {
            Health = 0;
            MaxHealth = 0;
        }

        public void Init(int _maxHealth)
        {
            MaxHealth = _maxHealth;
            Health = _maxHealth;

            OnMaxHealthChanged?.Invoke(owner, MaxHealth);
        }

        public void TakeDamage(int _brutAmount)
        {
            if (Health <= 0)
            {
                return;
            }

            if (_brutAmount < 0)
            {
                Debug.LogError("Can't damage with a negative amount.");
                return;
            }

            int newAmount = _brutAmount;
            if (dmgTakenModifier != null)
            {
                newAmount = dmgTakenModifier(newAmount);
            }

            Health = Mathf.Clamp(Health - newAmount, 0, MaxHealth);
            OnHealthChanged?.Invoke(owner, Health);

            if (Health <= 0)
            {
                OnActorDie?.Invoke(owner);
            }
            else
            {
                OnIncomingDamage?.Invoke(owner); //Review : Might need to be refactored.
            }
        }

        public void Heal(int _brutAmount)
        {
            if (Health >= MaxHealth || Health == 0) //Can't heal dead people.
            {
                return;
            }

            if (_brutAmount < 0)
            {
                Debug.LogError("Can't heal with a negative amount.");
                return;
            }

            int newAmount = _brutAmount;
            if (healthGainedModifier != null)
            {
                newAmount = healthGainedModifier(newAmount);
            }

            Health = Mathf.Clamp(Health + newAmount, 0, MaxHealth);
            OnHealthChanged?.Invoke(owner, Health);
        }
    }

    public interface IKHealth<THealthType>
    {
        public THealthType Health { get; }
        public THealthType MaxHealth { get; }
        public float HealthPercentage { get; }

        public void TakeDamage(THealthType _brutAmount);
        public void Heal(THealthType _brutAmount);
    }

    public interface IKHealth : IKHealth<int>
    {
    }

    public interface IKHealthFloat : IKHealth<float>
    {
    }
}