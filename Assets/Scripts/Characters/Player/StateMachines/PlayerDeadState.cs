using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerDeadState : PlayerGroundedState
    {
        public PlayerDeadState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {

        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.deadParameterHash);

            stateMachine.reusableData.movementSpeedModifier = 0;

            stateMachine.player.input.playerActions.Movement.Disable();

            ResetVelocity();


        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.deadParameterHash);
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

        //public override void OnAnimationExitEvent()
        //{
        //    base.OnAnimationExitEvent();

        //    stateMachine.player.input.playerActions.Movement.Enable();

        //}

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            stateMachine.ChangeState(stateMachine.idlingState);
        }
    }
}
