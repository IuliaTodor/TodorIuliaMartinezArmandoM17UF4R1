using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerMovingState : PlayerGroundedState
    {
        public PlayerMovingState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.movingParameterHash);
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.movingParameterHash);
        }
    }
}
