
using UnityEngine;

namespace StatePattern.Enemy
{
    public class IdleState : IState
    {
        public OnePunchManController owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private float timer;

        public IdleState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;


        public void OnStateEnter() => ResetTimer();
        private void ResetTimer() => timer = owner.Data.IdleTime;


        public void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                stateMachine.ChangeState(OnePunchManStates.ROTATING);
            }
        }

        public void OnStateExit()
        {
            timer = 0;
        }
    }
}


