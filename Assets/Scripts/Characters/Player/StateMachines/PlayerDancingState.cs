using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerDancingState : PlayerGroundedState
    {
        public PlayerDancingState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {

        }

        public override void Enter()
        {
            base.Enter();

            AudioManager.instance.StopPlaying("Main");
            AudioManager.instance.Play("Dance");

            stateMachine.reusableData.shouldDance = true;

            StartAnimation(stateMachine.player.animationData.dancingParameterHash);

            stateMachine.reusableData.movementSpeedModifier = 0;

            stateMachine.player.input.playerActions.Movement.Disable();

            ResetVelocity();


        }

        public override void Exit()
        {
            base.Exit();


            AudioManager.instance.StopPlaying("Dance");
            AudioManager.instance.Play("Main");

            StopAnimation(stateMachine.player.animationData.dancingParameterHash);
            stateMachine.reusableData.shouldDance = false;
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
            base.OnAnimationExitEvent();

            stateMachine.player.input.playerActions.Movement.Enable();
       
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            stateMachine.ChangeState(stateMachine.idlingState);
        }
    }
}
