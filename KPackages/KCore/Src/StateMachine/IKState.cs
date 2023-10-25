// Created by Kabourlix Cendrée on 22/10/2023

#nullable enable
namespace SDKabu.KCore.StateMachine
{
    public interface IKState
    {
        public string Name { get; }
        public bool IsTransitionPossible(IKState? _formerState);

        public void Enter(KAbstractStateMachine _stateMachine);
        public void Process(KAbstractStateMachine _stateMachine);
        public void Exit(KAbstractStateMachine _stateMachine);
    }
}