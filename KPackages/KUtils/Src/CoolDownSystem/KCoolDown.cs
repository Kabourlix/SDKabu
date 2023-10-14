// Copyrighted by team Rézoskour
// Created by Kabourlix Cendrée on 14

#nullable enable
using System.Linq;
using UnityEngine;

namespace SDKabu.KUtils
{
    public struct KCoolDown
    {
        public readonly string Id;
        public float Duration { get; private set; }
        public float RemainingTime { get; private set; }
        public bool IsRunning { get; private set; }

        public KCoolDown(string _id, float _duration)
        {
            Id = _id;
            Duration = _duration;
            RemainingTime = 0;
            IsRunning = false;
        }

        /// <summary>
        /// Start the current cooldown if it is not started (_override = false)
        /// </summary>
        /// <param name="_override">Whether or not the timer can be set in any condition.</param>
        /// <returns>Whether or not the cooldown has been started.</returns>
        public bool Start(bool _override = false)
        {
            if (RemainingTime > 0 && !_override)
            {
                return false;
            }

            RemainingTime = Duration;
            IsRunning = true;
            return true;
        }

        public void UpdateRemaining(float _delta)
        {
            if (!IsRunning)
            {
                return;
            }

            RemainingTime -= _delta;
            if (RemainingTime > 0)
            {
                return;
            }

            RemainingTime = 0;
            IsRunning = false;
        }

        public void Pause(bool _pause)
        {
            if (RemainingTime <= 0)
            {
                Debug.LogWarning($"{Id} cannot be paused since it is not running.");
                IsRunning = false;
                return;
            }

            IsRunning = !_pause;
        }

        public void Stop()
        {
            RemainingTime = 0;
            IsRunning = false;
        }

        public bool UpdateDuration(float _newDuration, bool _override)
        {
            if (IsRunning && !_override)
            {
                Debug.LogError($"{Id} is running and cannot be updated");
                return false;
            }

            Duration = _newDuration;
            return true;
        }

        //Override == operator
        public static bool operator ==(KCoolDown _cd1, KCoolDown _cd2)
        {
            return _cd1.Id == _cd2.Id;
        }

        public static bool operator !=(KCoolDown _cd1, KCoolDown _cd2)
        {
            return !(_cd1 == _cd2);
        }
    }
}