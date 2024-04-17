using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerLightLandingState : PlayerLandingState
    {
        public PlayerLightLandingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
        }

        public override void Enter()
        {
            //Para que no podamos movernos al aterrizar
            stateMachine.reusableData.movementSpeedModifier = 0;

            base.Enter();

            stateMachine.reusableData.currentJumpForce = airData.jumpData.stationaryForce;

            ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
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

        public override void OnAnimationTransitionEvent()
        {
            stateMachine.ChangeState(stateMachine.idlingState);
        }
    }
}
