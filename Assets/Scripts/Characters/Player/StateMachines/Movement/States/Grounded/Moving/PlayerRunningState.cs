using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerRunningState : PlayerMovingState
    {
        private float startTime;
        private PlayerSprintData sprintData;

        public PlayerRunningState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
            sprintData = movementData.sprintData;
        }


        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.runParameterHash);

            stateMachine.reusableData.movementSpeedModifier = movementData.runData.speedModifier;


            stateMachine.reusableData.currentJumpForce = airData.jumpData.mediumForce;

            startTime = Time.time;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.runParameterHash);
        }
        public override void Update()
        {
            base.Update();

            //Significa que hemos entrado a través del sprinting state
            if (!stateMachine.reusableData.shouldWalk)
            {
                return;
            }

            if (Time.time < startTime + sprintData.runToWalkTime)
            {
                return;
            }

            StopRunning();
        }

        /// <summary>
        /// Transiciona a medium stop state
        /// </summary>
        private void StopRunning()
        {


            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                //if (stateMachine.reusableData.shouldDance)
                //{
                //    return;
                //}

                stateMachine.ChangeState(stateMachine.idlingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.walkingState);
        }

        #endregion
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.mediumStoppingState);

            base.OnMovementCanceled(context);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.walkingState);
        }
    }
}
