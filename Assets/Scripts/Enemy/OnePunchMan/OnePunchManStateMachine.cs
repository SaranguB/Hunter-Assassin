
using System;
using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine
    {
        private OnePunchManController owner;

        protected Dictionary<OnePunchManStates, IState> states = new Dictionary<OnePunchManStates, IState>();
        private IState currentState;

        public OnePunchManStateMachine(OnePunchManController owner)
        {
            this.owner = owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            states.Add(OnePunchManStates.IDLE, new IdleState(this));
            states.Add(OnePunchManStates.ROTATING, new RotatingState(this));
            states.Add(OnePunchManStates.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach (IState state in states.Values)
            {
                state.owner = owner;
            }
        }

        public void Update() => currentState?.Update();

        public void ChangeState(OnePunchManStates newstate) => ChangeState(states[newstate]);

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
    }
}