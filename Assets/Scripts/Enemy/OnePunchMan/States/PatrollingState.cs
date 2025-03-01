using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class PatrollingState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;

        public PatrollingState(IStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter() { }

        public void Update() { }

        public void OnStateExit() { }
    }
}
