using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerRollingState : PlayerLandingState
    {
        private PlayerRollData rollData;
        public PlayerRollingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            rollData = movementData.rollData;
        }

        public override void Enter()
        {
            stateMachine.reusableData.movementSpeedModifier = movementData.rollData.speedModifier;

            base.Enter();

            StartAnimation(stateMachine.player.animationData.rollParameterHash);

            //Así no sprinteamos al entrar en el roll state
            stateMachine.reusableData.shouldSprint = false;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.rollParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            //Así aseguramos que solo rotamos cuando no llamemos el método para movernos
            if (stateMachine.reusableData.movementInput != Vector2.zero)
            {
                return;
            }

            RotateTowardsTargetRotation();
        }

        public override void OnAnimationTransitionEvent()
        {
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.mediumStoppingState);

                return;
            }

            OnMove();
        }

        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
        }
    }
}
