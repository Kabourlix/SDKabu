// Created by Kabourlix Cendrée on 22/10/2023

#nullable enable

using System;
using SDKabu.KCore;
using SDKabu.KCore.StateMachine;

namespace Src.GameManager
{
    public interface IKGameManager : IKService
    {
        public event Action<IKState> OnStateChanged;
        public IKState? CurrentState { get; }
        public bool TryChangeState(IKState _newState);
    }
}