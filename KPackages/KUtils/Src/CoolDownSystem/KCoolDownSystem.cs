// Copyrighted by team Rézoskour
// Created by Kabourlix Cendrée on 12

#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SDKabu.KUtils
{
    internal class KCoolDownSystem : MonoBehaviour, IKCoolDown
    {
        public event Action<string>? OnCoolDownStarted;
        public event Action<string>? OnCoolDownFinished;
        public event Action<string>? OnCoolDownCanceled;

        private readonly Dictionary<string, KCoolDown> registeredCoolDowns = new();

        public void Dispose()
        {
            foreach (KeyValuePair<string, KCoolDown> kv in registeredCoolDowns)
            {
                StopCoolDown(kv.Value);
            }

            registeredCoolDowns.Clear();
        }

        private void Update()
        {
            foreach (KeyValuePair<string, KCoolDown> kv in registeredCoolDowns)
            {
                if (!kv.Value.IsRunning)
                {
                    continue;
                }
                kv.Value.UpdateRemaining(Time.deltaTime);
                if (kv.Value.IsRunning)
                {
                    continue;
                }

                OnCoolDownFinished?.Invoke(kv.Key);
            }
        }


        public ReadOnlySpan<string> RegisteredCoolDowns
            => registeredCoolDowns.Keys.ToArray().AsSpan();

        public bool TryGetCoolDownRemainingTime(string _id, out float _remainingTime)
        {
            _remainingTime = -1;
            if (!IsCoolDownRegistered(_id))
            {
                Debug.LogError($"{_id} is not registered as a CoolDown");
                return false;
            }

            KCoolDown cd = registeredCoolDowns[_id];
            _remainingTime = cd.RemainingTime;
            return true;
        }

        public bool TryGetCoolDownRemainingTime(KCoolDown _cd, out float _remainingTime)
        {
            return TryGetCoolDownRemainingTime(_cd.Id, out _remainingTime);
        }

        #region CoolDown Registration

        public bool IsCoolDownRegistered(string _id)
        {
            return registeredCoolDowns.ContainsKey(_id);
        }

        public bool IsCoolDownRegistered(KCoolDown _cd)
        {
            return IsCoolDownRegistered(_cd.Id);
        }

        public bool TryRegisterCoolDown(string _id, float _duration)
        {
            return TryRegisterCoolDown(new KCoolDown(_id, _duration));
        }

        public bool TryRegisterCoolDown(KCoolDown _cd)
        {
            if (IsCoolDownRegistered(_cd))
            {
                Debug.LogWarning($"CoolDown {_cd.Id} is already registered");
                return false;
            }

            return registeredCoolDowns.TryAdd(_cd.Id, _cd);
        }

        public void UnregisterCoolDown(string _id)
        {
            if (!IsCoolDownRegistered(_id))
            {
                Debug.LogError($"{_id} is not registered as a CoolDown");
                return;
            }

            registeredCoolDowns.Remove(_id, out KCoolDown cd);
            StopCoolDown(cd);
        }

        public void UnregisterCoolDown(KCoolDown _cd) => UnregisterCoolDown(_cd.Id);

        #endregion // CoolDown Registration

        #region CoolDown flow

        public void StartCoolDown(string _id, bool _override = false)
        {
            if (!IsCoolDownRegistered(_id))
            {
                Debug.LogError($"{_id} is not registered as a CoolDown");
                return;
            }

            KCoolDown cd = registeredCoolDowns[_id];
            if (cd.Start(_override))
            {
                OnCoolDownStarted?.Invoke(_id);
            }
        }

        public void StartCoolDown(KCoolDown _cd, bool _override = false)
        {
            StartCoolDown(_cd.Id, _override);
        }

        public void PauseCoolDown(string _id, bool _pause)
        {
            if (!IsCoolDownRegistered(_id))
            {
                Debug.LogError($"{_id} is not registered as a CoolDown");
                return;
            }

            KCoolDown cd = registeredCoolDowns[_id];
            cd.Pause(_pause);
        }

        public void PauseCoolDown(KCoolDown _cd, bool _pause)
        {
            PauseCoolDown(_cd.Id, _pause);
        }

        public void StopCoolDown(string _id)
        {
            if (!IsCoolDownRegistered(_id))
            {
                Debug.LogError($"{_id} is not registered as a CoolDown");
                return;
            }

            KCoolDown cd = registeredCoolDowns[_id];
            cd.Stop();
            OnCoolDownCanceled?.Invoke(_id);
        }

        public void StopCoolDown(KCoolDown _cd) => StopCoolDown(_cd.Id);

        #endregion // CoolDown flow

        public bool UpdateCoolDownDuration(string _id, float _newDuration, bool _override = false)
        {
            if (!IsCoolDownRegistered(_id))
            {
                Debug.LogError($"{_id} is not registered as a CoolDown");
                return false;
            }

            KCoolDown cd = registeredCoolDowns[_id];
            return cd.UpdateDuration(_newDuration, _override);
        }
    }
}