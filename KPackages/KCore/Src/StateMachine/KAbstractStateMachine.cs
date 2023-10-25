// Created by Kabourlix Cendrée on 22/10/2023

#nullable enable

using System;
using UnityEngine;

namespace SDKabu.KCore.StateMachine
{
    public abstract class KAbstractStateMachine : MonoBehaviour
    {
        public IKState? CurrentState { get; protected set; }

        public virtual bool TryChangeState(IKState _newState)
        {
            if (CurrentState?.IsTransitionPossible(_newState) ?? true)
            {
                CurrentState?.Exit(this);
                CurrentState = _newState;
                CurrentState?.Enter(this);
                return true;
            }

            Debug.Log($"Transition impossible from {CurrentState?.Name} to {_newState.Name}");
            return false;
        }
    }
}