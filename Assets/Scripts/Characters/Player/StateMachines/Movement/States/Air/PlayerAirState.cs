using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerAirState : PlayerMovementState
    {
        public PlayerAirState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.airParameterHash);

            ResetSprintState();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.airParameterHash);
        }

        protected virtual void ResetSprintState()
        {
            stateMachine.reusableData.shouldSprint = false;
        }

        protected override void OnContactWithGround(Collider collider)
        {
            stateMachine.ChangeState(stateMachine.lightLandingState);
        }
    }
}
