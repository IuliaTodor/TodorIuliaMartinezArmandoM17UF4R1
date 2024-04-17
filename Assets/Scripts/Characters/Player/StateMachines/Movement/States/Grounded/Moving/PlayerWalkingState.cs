using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerWalkingState : PlayerMovingState
    {
        public PlayerWalkingState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
        }

        #region IState Methods

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.walkParameterHash);

            stateMachine.reusableData.movementSpeedModifier = movementData.walkData.speedModifier;
            stateMachine.reusableData.currentJumpForce = airData.jumpData.weakForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.walkParameterHash);
        }
        #endregion
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.lightStoppingState);

            base.OnMovementCanceled(context);
        }

        protected override void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.runningState);
        }
    }
}
