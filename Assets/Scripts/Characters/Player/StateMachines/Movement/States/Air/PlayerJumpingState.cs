using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerJumpingState : PlayerAirState
    {
        private PlayerJumpData jumpData;    

        private bool shouldKeepRotating;
        
        private bool canStartFalling;

        public PlayerJumpingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            jumpData = airData.jumpData;
        }

        public override void Enter()
        {
            base.Enter();

            //No hay aceleración en el aire al saltar
            stateMachine.reusableData.movementSpeedModifier = 0f;

            //Hace que el salto sea menos floaty
            stateMachine.reusableData.movementDecelerationForce = jumpData.decelerationForce;

            stateMachine.reusableData.rotationData = jumpData.rotationData;

            shouldKeepRotating = stateMachine.reusableData.movementInput != Vector2.zero;

            Jump();
        }

        public override void Exit()
        {
            base.Exit();

            SetBaseRotationData();

            canStartFalling = false;
        }

        public override void Update()
        {
            base.Update();

            //Si la velocidad es negativa empieza a caer
            if (!canStartFalling && IsMovingUp(0f))
            {
                canStartFalling = true;
            }

            if (!canStartFalling || GetPlayerVerticalVelocity().y > 0)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.fallingState);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            //Rota según la dirección de movimiento si hay input
            if (shouldKeepRotating)
            {
                RotateTowardsTargetRotation();
            }

            if (IsMovingUp())
            {
                DecelerateVertically();
            }
        }

        private void Jump()
        {
            //Variable temporal. CurrentJF deberá ser cambiada dependiendo del ángulo de la cuesta y dirección de salto
            Vector3 jumpForce = stateMachine.reusableData.currentJumpForce;

            //Para que dirección de salto no dependa de los ejes
            Vector3 jumpDirection = stateMachine.player.transform.forward;

            if (shouldKeepRotating)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                //Cambia del forward vector a la dirección de rotación
                jumpDirection = GetTargetRotationDirection(stateMachine.reusableData.CurrentTargetRotation.y);
            }

            jumpForce.x *= jumpDirection.x;
            jumpForce.z *= jumpDirection.z;

            jumpForce = GetJumpForceOnSlope(jumpForce);

            //Así nuestra velocidad actual no influye en el salto
            ResetVelocity();

            stateMachine.player.rb.AddForce(jumpForce, ForceMode.VelocityChange);
        }

        /// <summary>
        /// Para saber si estamos en una cuesta y actualiza la fuerza de salto de ser el caso
        /// </summary>
        /// <param name="jumpForce"></param>
        /// <returns></returns>
        private Vector3 GetJumpForceOnSlope(Vector3 jumpForce)
        {
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.player.colliderUtility.capsuleColliderData.collider.bounds.center;

            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, jumpData.jumpToGroundRayDistance, stateMachine.player.layerData.groundLayer, QueryTriggerInteraction.Ignore))
            {
                float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

                if (IsMovingUp())
                {
                    float forceModifier = jumpData.jumpForceModifierOnSlopeUpwards.Evaluate(groundAngle);

                    jumpForce.x *= forceModifier;
                    jumpForce.z *= forceModifier;
                }

                if (IsMovingDown())
                {
                    float forceModifier = jumpData.jumpForceModifierOnSlopeDownwards.Evaluate(groundAngle);

                    jumpForce.y *= forceModifier;
                }
            }

            return jumpForce;
        }

        protected override void ResetSprintState()
        {
        }

        //protected override void OnMovementCanceled(InputAction.CallbackContext context)
        //{
        //}
    }
}
