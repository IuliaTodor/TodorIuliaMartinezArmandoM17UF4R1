using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerCombatState : PlayerGroundedState
    {
        public PlayerCombatState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
        }

        public override void Enter()
        {
            base.Enter();

            CameraSwitch.SwitchCamera(stateMachine.player.firstPersonCam);
        }

        protected override void OnCameraToggle(InputAction.CallbackContext context)
        {
            base.OnCameraToggle(context);

            stateMachine.ChangeState(stateMachine.idlingState);
        }
    }
}
