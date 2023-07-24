using System;
using UnityEngine;

#nullable enable

namespace SDKabu.KCharacter
{
    public class KHealthComponent
    {
        #region Fields

        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; }

        public float HealthPercentage
        {
            get
            {
                if (MaxHealth != 0) return Health / MaxHealth;
                
                Debug.Log("Max Health is not set.");
                return -1;
            }
        }

        #endregion // Fields


        #region Events

        public event Action<float>? OnHealthChanged;
        public event Action<float>? OnMaxHealthChanged;
        public event Action? OnDeath;

        #endregion // Events
        
        public KHealthComponent(int _maxHealth)
        {
            MaxHealth = _maxHealth;
            Health = _maxHealth;
        }
        
        
        public void ModifyMaxHealth(int _maxHealth)
        {
            MaxHealth = _maxHealth;
            OnMaxHealthChanged?.Invoke(MaxHealth);
        }


        private void ModifyHealth(float _brutValue, Func<float,float> _valueModifierFunc)
        {
            if ((_brutValue < 0f && Health <= 0f) || (_brutValue >= 0f && Health >= MaxHealth))
            {
                return;
            }
            
            var value = _valueModifierFunc(_brutValue);

            Health = Mathf.Clamp(Health + value, 0f, MaxHealth);
            OnHealthChanged?.Invoke(HealthPercentage);
            if(Health <= 0f)
            {
                OnDeath?.Invoke();
            }
        }

        protected virtual float ComputeRealDamage(float _brutValue)
        {
            return _brutValue;
        }
        
        protected virtual float ComputeRealHeal(float _brutValue)
        {
            return _brutValue;
        }
        
        /// <summary>
        /// Use this method to deal damage to the character.
        /// This does nothing if Health is already at 0.
        /// </summary>
        /// <param name="_brutValue">A positive brut value of the damage to deal.</param>
        public void Damage(float _brutValue)
        {
            if (_brutValue < 0)
            {
                Debug.Log("Damage value must be positive.");
                return;
            }

            ModifyHealth(-1f*_brutValue, ComputeRealDamage);
        }

        /// <summary>
        /// Use this method to heal the character.
        /// This does nothing if Health is already at MaxHealth.
        /// </summary>
        /// <param name="_brutValue">A positive brut value of the health to heal.</param>
        public void Heal(float _brutValue)
        {
            if(_brutValue < 0)
            {
                Debug.Log("Heal value must be positive.");
                return;
            }
            
            ModifyHealth(_brutValue, ComputeRealHeal);
        }

    }
}