using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerStoppingState : PlayerGroundedState
    {
        public PlayerStoppingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.reusableData.movementSpeedModifier = 0f;


            base.Enter();

            StartAnimation(stateMachine.player.animationData.stoppingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.stoppingParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            //Así el jugador rota hasta la dirección deseada al detenerse
            RotateTowardsTargetRotation();

            //Mira si nos estamos moviendo
            if (!IsMovingHorizontally())
            {
                return;
            }

            DecelerateHorizontally();
        }

        public override void OnAnimationTransitionEvent()
        {
            if(stateMachine.reusableData.isAiming)
            {
                return;
            }

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
    }
}
