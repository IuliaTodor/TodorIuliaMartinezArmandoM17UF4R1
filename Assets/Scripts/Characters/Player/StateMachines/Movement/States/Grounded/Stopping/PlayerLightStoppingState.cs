using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerLightStoppingState : PlayerStoppingState
    {
    public PlayerLightStoppingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.reusableData.movementDecelerationForce = movementData.stopData.lightDecelerationForce;

        stateMachine.reusableData.currentJumpForce = airData.jumpData.weakForce;
    }
}
}
