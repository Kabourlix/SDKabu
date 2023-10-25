// Created by Kabourlix Cendrée on 22/10/2023

#nullable enable

using System;
using SDKabu.KCore.StateMachine;

// Created by Kabourlix Cendrée on 22/10/2023

namespace Src.GameManager
{
    public class KBasicGameManager : KAbstractStateMachine, IKGameManager
    {
        public void Dispose()
        {
        }

        public event Action<IKState>? OnStateChanged;
    }
}