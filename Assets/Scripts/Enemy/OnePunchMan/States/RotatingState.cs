

using UnityEngine;

namespace StatePattern.Enemy
{
    public class RotatingState : IState
    {
        public OnePunchManController owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private float targetRotation;

        public RotatingState(OnePunchManStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void OnStateEnter() => targetRotation = (owner.Rotation.eulerAngles.y + 180) % 360;

        public void Update()
        {
            owner.SetRotation(CalculateRotation());
            if (IsRotationComplete())
                stateMachine.ChangeState(OnePunchManStates.IDLE);
        }
        public void OnStateExit() => targetRotation = 0;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle
            (owner.Rotation.eulerAngles.y, targetRotation, owner.Data.RotationSpeed * Time.deltaTime);
      

        private bool IsRotationComplete() => 
            Mathf.Abs(Mathf.Abs(owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < 
            owner.Data.RotationThreshold;
    }
}
