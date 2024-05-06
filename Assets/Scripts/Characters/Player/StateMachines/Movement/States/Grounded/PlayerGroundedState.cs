using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerGroundedState : PlayerMovementState
    {
        private SlopeData slopeData;
        public PlayerGroundedState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
            slopeData = stateMachine.player.colliderUtility.slopeData;
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.groundedParameterHash);

            UpdateShouldSprintState();
        }

        public override void Exit()
        {
            base.Exit();


            StopAnimation(stateMachine.player.animationData.groundedParameterHash);
        }
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            FloatCapsule();
            OnPlayerDeath();
        }

        /// <summary>
        /// Método usado para añadir una fuerza a nuestro CapsuleCollider. Este está subido para poder subir cuestas, y se le añade una fuerza
        /// Para mantenerlo flotando y que no se hunda en el suelo
        /// </summary>
        private void FloatCapsule()
        {
            Vector3 capsuleColliderCenterWorldSpace = stateMachine.player.colliderUtility.capsuleColliderData.collider.bounds.center;

            //El rayo que irá desde el centro del CC al suelo para mantenerlo flotando
            //Vector3.down siempre está en WorldSpace
            Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterWorldSpace, Vector3.down);

            //QueryTriggerInteraction.Ignore ignorará objetos con el layer, pero que son triggers. Porque no queremos considerar triggers como algo en lo que caminar
            if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.floatRayDistance, stateMachine.player.layerData.groundLayer, QueryTriggerInteraction.Ignore))
            {
                //Ángulo del suelo tomando la normal del rayo y y el vector hacia arriba
                float groundAngle = Vector3.Angle(hit.normal, Vector3.up);

                float slopeSpeedModifier = SetSlopeSpeedModifierOnAngle(groundAngle);

                if (slopeSpeedModifier == 0f)
                {
                    return;
                }

                //Distancia del centro de la cápsula al suelo
                float distanceToFloatingPoint = stateMachine.player.colliderUtility.capsuleColliderData.colliderCenterInLocalSpace.y * stateMachine.player.transform.localScale.y - hit.distance;

                if (distanceToFloatingPoint == 0f)
                {
                    return;
                }

                //La cantidad que tiene que levantar la fuerza al jugador del suelo
                float amountToList = distanceToFloatingPoint * slopeData.stepReachForce - GetPlayerVerticalVelocity().y;

                Vector3 liftForce = new Vector3(0f, amountToList, 0f);

                stateMachine.player.rb.AddForce(liftForce, ForceMode.VelocityChange);
            }
        }

        /// <summary>
        /// Cambia la velocidad al ir por una cuesta para que el jugador vaya más lento
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private float SetSlopeSpeedModifierOnAngle(float angle)
        {
            //Toma los valores en el AnimationCurve configurado en el insepctor
            float slopeSpeedModifier = movementData.slopeSpeedAngles.Evaluate(angle);

            stateMachine.reusableData.movementOnSlopesSpeedModifier = slopeSpeedModifier;

            return slopeSpeedModifier;
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.input.playerActions.Movement.canceled += OnMovementCanceled;

            stateMachine.player.input.playerActions.Dash.started += OnDashStarted;

            stateMachine.player.input.playerActions.Jump.started += OnJumpStarted;

            stateMachine.player.input.playerActions.ChangeCamera.started += OnCameraToggle;

            stateMachine.player.input.playerActions.DamageEnemy.started += OnEnemyDamaged;

            stateMachine.player.input.playerActions.Shoot.started += OnShoot;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.input.playerActions.Movement.canceled -= OnMovementCanceled;

            stateMachine.player.input.playerActions.Dash.started -= OnDashStarted;

            stateMachine.player.input.playerActions.Jump.started -= OnJumpStarted;

            stateMachine.player.input.playerActions.ChangeCamera.started -= OnCameraToggle;

            stateMachine.player.input.playerActions.DamageEnemy.started -= OnEnemyDamaged;

            stateMachine.player.input.playerActions.Shoot.started -= OnShoot;
        }

        protected virtual void OnShoot(InputAction.CallbackContext context)
        {
            if(!stateMachine.reusableData.isAiming)
            {
                return;
            }
        }

        //protected void OnDie(InputAction.CallbackContext context)
        //{
        //    stateMachine.ChangeState(stateMachine.deadState);

        //    UIManager.instance.DiePanel();
        //}

        protected void OnEnemyDamaged(InputAction.CallbackContext context)
        {
            stateMachine.player.target.GetComponent<Enemy>().HandleDamage(1);
        }

        protected virtual void OnJumpStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.jumpingState);
        }

        protected virtual void OnDashStarted(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.dashingState);
        }
        protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
        {
            if (stateMachine.reusableData.shouldDance)
            {
                return;
            }

            if (stateMachine.reusableData.isAiming)
            {
                return;
            }

            stateMachine.ChangeState(stateMachine.idlingState);
        }

        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            base.OnWalkToggleStarted(context);

            stateMachine.ChangeState(stateMachine.runningState);
        }

        protected virtual void OnCameraToggle(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.combatMovementState);
        }

        protected virtual void OnMove()
        {
            if (stateMachine.reusableData.shouldSprint)
            {
                stateMachine.ChangeState(stateMachine.sprintingState);

                return;
            }

            if (stateMachine.reusableData.shouldWalk)
            {
                stateMachine.ChangeState(stateMachine.walkingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.runningState);
        }

        protected void UpdateShouldSprintState()
        {
            if (!stateMachine.reusableData.shouldSprint)
            {
                return;
            }

            if (stateMachine.reusableData.movementInput != Vector2.zero)
            {
                return;
            }

            //Cuando entramos a groundedState, si shouldSprint es true y no hay tecla de movimiento pulsada, no transicionaremos a SprintState 
            stateMachine.reusableData.shouldSprint = false;
        }

        protected override void OnContactWithGroundExited(Collider collider)
        {
            base.OnContactWithGroundExited(collider);

            if (IsThereGroundUnderneath())
            {
                return;
            }

            //Detecta a cuanta distancia estamos del suelo
            Vector3 capsuleColliderCenterInWorldSpace = stateMachine.player.colliderUtility.capsuleColliderData.collider.bounds.center;

            //Obtenemos parte baja del collider
            Ray downwardsRayFromCapsuleBottom = new Ray(capsuleColliderCenterInWorldSpace - stateMachine.player.colliderUtility.capsuleColliderData.colliderVerticalExtents, Vector3.down);

            // out _ es un discard para elr ayo, indicando que no hay nada. Porque esto sirve para ver si no hay suelo
            if (!Physics.Raycast(downwardsRayFromCapsuleBottom, out _, movementData.groundToFallRayDistance, stateMachine.player.layerData.groundLayer, QueryTriggerInteraction.Ignore))
            {
                OnFall();
            }
        }

        private bool IsThereGroundUnderneath()
        {
            BoxCollider groundCheckCollider = stateMachine.player.colliderUtility.triggerColliderData.groundCheckCollider;

            Vector3 groundColliderCenterInWorldSpace = groundCheckCollider.bounds.center;

            //Busca los colliders tocando una cierta zona
            Collider[] overlappedGroundColliders = Physics.OverlapBox(groundColliderCenterInWorldSpace, 
            stateMachine.player.colliderUtility.triggerColliderData.groundCheckColliderVerticalExtents, 
            groundCheckCollider.transform.rotation, stateMachine.player.layerData.groundLayer, QueryTriggerInteraction.Ignore);

            return overlappedGroundColliders.Length > 0;
        }

        protected virtual void OnFall()
        {
            stateMachine.ChangeState(stateMachine.fallingState);
        }

        protected override void OnItemPickUp(Collider collider)
        {
            base.OnItemPickUp(collider);  
            
            stateMachine.ChangeState(stateMachine.dancingState);
        }
        
    }
}
