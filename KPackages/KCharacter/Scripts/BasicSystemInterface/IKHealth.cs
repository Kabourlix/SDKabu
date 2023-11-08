// Copyright (c) Asobo Studio, All rights reserved. www.asobostudio.com


#nullable enable
using System;

namespace SDKabu.KCharacter
{
    public interface IKHealth<THealthType> //TODO : Modify it to make a class
    {
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
        public event Action<THealthType>? OnHealthChanged;

        /// <summary>
        /// Fired when max health changed, the parameter is the new max health.
        /// </summary>
        public event Action<THealthType>? OnMaxHealthChanged;

        public THealthType Health { get; }
        public THealthType MaxHealth { get; }
        public float HealthPercentage { get; }

        public void TakeDamage(THealthType _brutAmount, Func<THealthType, THealthType>? _amountModifierFunc = null);
        public void Heal(THealthType _brutAmount, Func<THealthType, THealthType>? _amountModifierFunc = null);
    }

    public interface IKHealth : IKHealth<int>
    {
    }

    public interface IKHealthFloat : IKHealth<float>
    {
    }
}