using StatePattern.Enemy;

namespace StatePattern.StateMachine
{
    public interface IStateMachine
    {
        public void ChangeState(States newState);
    }
}
