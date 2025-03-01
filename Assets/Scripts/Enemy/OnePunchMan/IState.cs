using UnityEngine;

namespace StatePattern.Enemy
{
    public interface IState
    {
        public OnePunchManController owner { get; set; }

        public void OnStateEnter();

        public void Update();

        public void OnStateExit();

    }
}
