using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerCombatMovementState : PlayerCombatState
    {
        public PlayerCombatMovementState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
        }

        public override void Enter()
        {
            base.Enter();

            stateMachine.reusableData.isAiming = true;

            StartAnimation(stateMachine.player.animationData.aimParameterHash);

            stateMachine.reusableData.movementSpeedModifier = movementData.combatData.speedModifier;

            stateMachine.player.input.playerActions.Jump.Disable();
            stateMachine.player.input.playerActions.WalkToggle.Disable();
            stateMachine.player.input.playerActions.Sprint.Disable();
            stateMachine.player.input.playerActions.Dash.Disable();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.aimParameterHash);

            stateMachine.reusableData.isAiming = false;

            stateMachine.player.input.playerActions.Jump.Enable();
            stateMachine.player.input.playerActions.WalkToggle.Enable();
            stateMachine.player.input.playerActions.Sprint.Enable();
            stateMachine.player.input.playerActions.Dash.Enable();
        }
        public override void Update()
        {
            base.Update();

            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                return;
            }
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
        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            base.OnMovementCanceled(context);
        }

        protected override void OnShoot(InputAction.CallbackContext context)
        {
            base.OnShoot(context);

            if (!InventoryUI.instance.hasWeapon)
            {
                Debug.Log(InventoryUI.instance.hasWeapon);
                return;
            }

            stateMachine.player.InstantiateShoot();
        }
    }
}
