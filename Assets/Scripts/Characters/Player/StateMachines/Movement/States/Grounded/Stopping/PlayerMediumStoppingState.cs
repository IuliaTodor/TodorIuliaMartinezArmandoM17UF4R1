using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerMediumStoppingState : PlayerStoppingState
    {
    public PlayerMediumStoppingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

            StartAnimation(stateMachine.player.animationData.mediumStopParameterHash);

            stateMachine.reusableData.movementDecelerationForce = movementData.stopData.mediumDecelerationForce;

            stateMachine.reusableData.currentJumpForce = airData.jumpData.mediumForce;
        }

    public override void Exit()
    {
        base.Exit();

        StopAnimation(stateMachine.player.animationData.mediumStopParameterHash);
    }
}
}
