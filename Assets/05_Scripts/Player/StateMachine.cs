using System.Collections.Generic;

namespace StateController
{
    public enum StateName
    {
        Idle = 100,
        Move,
        Jump,
        Crouch,
        Fire = 200,
        Reload,
        Melee,
        Throw
    }

    public class StateMachine
    {
        public BaseState CurrentState { get; private set; }
        private Dictionary<StateName, BaseState> states = new();

        public StateMachine(StateName stateName, BaseState state)
        {
            AddState(stateName, state);
            CurrentState = GetState(stateName);
        }

        public void AddState(StateName stateName, BaseState state)
        {
            states.TryAdd(stateName, state);
        }

        public BaseState GetState(StateName name)
        {
            return states.TryGetValue(name, out BaseState state) ? state : null;
        }

        public void DeleteState(StateName name)
        {
            states.Remove(name);
        }

        public void ChangeState(StateName nextState)
        {
            CurrentState?.OnExitState();
            CurrentState = states.TryGetValue(nextState, out BaseState newState) ? newState : null;
            CurrentState?.OnEnterState();
        }

        public void EnterState()
        {
            CurrentState?.OnEnterState();
        }

        public void UpdateState()
        {
            CurrentState?.OnUpdateState();
        }

        public void FixedUpdateState()
        {
            CurrentState?.OnFixedUpdateState();
        }

        public void ExitState()
        {
            CurrentState?.OnExitState();
        }
    }
}