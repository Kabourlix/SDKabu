// Created by Kabourlix Cendrée on 22/10/2023

using System;

namespace SDKabu.KUtils.StateMachine
{
    public interface IKStateMachine<in TStateType> where TStateType : Enum
    {
        public bool IsTransitionPossible(TStateType _previousState, TStateType _newState);

        public bool TryChangeState(TStateType _state);
        public void PauseGame(bool _pause);
    }

    public interface IKState<out TStateType> where TStateType : Enum
    {
        public void EnterState(IKStateMachine<TStateType> _stateMachine);
        public void ProcessState(IKStateMachine<TStateType> _stateMachine);
        public void ExitState(IKStateMachine<TStateType> _stateMachine);
    }
}