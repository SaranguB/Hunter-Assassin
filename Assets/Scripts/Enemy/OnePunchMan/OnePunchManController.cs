using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;
using StatePattern.Player;
using System;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {

        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(OnePunchManStates.IDLE);
        }

        private void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);
     

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();

        }

        private void ResetTimer() => idleTimer = enemyScriptableObject.IdleTime;

        private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(Rotation.eulerAngles.y, targetRotation, enemyScriptableObject.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Data.RotationThreshold;

        private bool IsFacingPlayer(Quaternion desiredRotation) => Quaternion.Angle(Rotation, desiredRotation) < Data.RotationThreshold;

        private Quaternion CalculateRotationTowardsPlayer()
        {
            Vector3 directionToPlayer = target.Position - Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }
        
        private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(Rotation, desiredRotation, enemyScriptableObject.RotationSpeed / 30 * Time.deltaTime);

        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            isIdle = false;
            isRotating = false;
            isShooting = true;
            target = targetToSet;
            shootTimer = 0;
        }

        public override void PlayerExitedRange() 
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
        }
       
    }
    public enum OnePunchManStates
    {
        IDLE,
        ROTATING,
        SHOOTING
    }
}