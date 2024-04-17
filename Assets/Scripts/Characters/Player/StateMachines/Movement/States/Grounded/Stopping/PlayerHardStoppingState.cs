using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerHardStoppingState : PlayerStoppingState
    {
        public PlayerHardStoppingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.hardStopParameterHash);

            stateMachine.reusableData.movementDecelerationForce = movementData.stopData.hardDecelerationForce;

            stateMachine.reusableData.currentJumpForce = airData.jumpData.strongForce;
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.hardStopParameterHash);
        }

        protected override void OnMove()
        {
            //Desde hard stopping no podemos transicionar a caminar
            if (stateMachine.reusableData.shouldWalk)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.runningState);
        }
    }
}
