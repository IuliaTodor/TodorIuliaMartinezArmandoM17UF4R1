using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            CameraSwitch.SwitchCamera(stateMachine.player.thirdPersonCam);

            StartAnimation(stateMachine.player.animationData.idleParameterHash);

            //No hace falta modificar esta variable al usar Exit(), ya que cada estado la modificará a un valor específico
            stateMachine.reusableData.movementSpeedModifier = 0;

            stateMachine.reusableData.currentJumpForce = airData.jumpData.stationaryForce;

            ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.idleParameterHash);
        }

        public override void Update()
        {
            base.Update();

            //Comprobamos si el input de movimiento es cero. Para saber si movernos o no
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                return;
            }

            OnMove();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if(!IsMovingHorizontally())
            {
                return;
            }

            ResetVelocity();
        }

        #endregion
    }
}
