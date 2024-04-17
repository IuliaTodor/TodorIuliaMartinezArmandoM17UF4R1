using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerHardLandingState : PlayerLandingState
    {
        public PlayerHardLandingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.reusableData.movementSpeedModifier = 0f;

            base.Enter();

            StartAnimation(stateMachine.player.animationData.hardLandParameterHash);

            //Deshabilitamos el movimiento en lo que dura la animación
            
            stateMachine.player.input.playerActions.Movement.Disable();

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.hardLandParameterHash);

            stateMachine.player.input.playerActions.Movement.Enable();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (!IsMovingHorizontally())
            {
                return;
            }

            ResetVelocity();
        }

        public override void OnAnimationExitEvent()
        {
            stateMachine.player.input.playerActions.Movement.Enable();
        }

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.ChangeState(stateMachine.idlingState);
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.input.playerActions.Movement.started += OnMovementStarted;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.input.playerActions.Movement.started -= OnMovementStarted;
        }

        private void OnMovementStarted(InputAction.CallbackContext context)
        {
            OnMove();
        }

        protected override void OnMove()
        {
            if (stateMachine.reusableData.shouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.runningState);
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
        }
    }
}
