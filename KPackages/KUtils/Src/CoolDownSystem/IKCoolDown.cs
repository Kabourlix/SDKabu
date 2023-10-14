// Copyrighted by team Rézoskour
// Created by Kabourlix Cendrée on 14

#nullable enable
using System;

namespace SDKabu.KUtils
{
    public interface IKCoolDown : IKService
    {
        public event Action<string>? OnCoolDownStarted;
        public event Action<string>? OnCoolDownFinished;
        public event Action<string>? OnCoolDownCanceled;

        public ReadOnlySpan<string> RegisteredCoolDowns { get; }

        public bool TryGetCoolDownRemainingTime(string _id, out float _remainingTime);
        public bool TryGetCoolDownRemainingTime(KCoolDown _cd, out float _remainingTime);
        public bool IsCoolDownRegistered(string _id);
        public bool IsCoolDownRegistered(KCoolDown _cd);

        public bool TryRegisterCoolDown(string _id, float _duration);
        public bool TryRegisterCoolDown(KCoolDown _cd);

        public void UnregisterCoolDown(string _id);
        public void UnregisterCoolDown(KCoolDown _cd);

        public void StartCoolDown(string _id, bool _override = false);
        public void StartCoolDown(KCoolDown _cd, bool _override = false);

        public void PauseCoolDown(string _id, bool _pause);
        public void PauseCoolDown(KCoolDown _cd, bool _pause);

        public void StopCoolDown(string _id);
        public void StopCoolDown(KCoolDown _cd);

        public bool UpdateCoolDownDuration(string _id, float _newDuration, bool _override = false);
    }
}