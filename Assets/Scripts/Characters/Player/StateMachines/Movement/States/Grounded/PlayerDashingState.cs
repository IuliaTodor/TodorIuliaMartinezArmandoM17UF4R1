using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerDashingState : PlayerGroundedState
    {
        private PlayerDashData dashData;
        /// <summary>
        /// Momento en el que hace el dash
        /// </summary>
        private float startTime;
        private int consecutiveDashesUsed;
        /// <summary>
        /// Mira si deber�a rotar al moverse mientras dashea 
        /// </summary>
        private bool shouldKeepRotating;
        public PlayerDashingState(PlayerMovementSM playerMovementSM) : base(playerMovementSM)
        {
            dashData = movementData.dashData;
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.dashParameterHash);

            //Si entramos al estado mientras nos movemos, establecemos un SpeedModifier mayor
            stateMachine.reusableData.movementSpeedModifier = dashData.speedModifier;

            stateMachine.reusableData.currentJumpForce = airData.jumpData.strongForce;

            stateMachine.reusableData.rotationData = dashData.rotationData;

            //Si entramos mientras estamos quietos, a�adimos una fuerza para que el jugador se mueva
            Dash();

            //Ser� true si pulsamos tecla de movimiento o false si no pulsamos.
            shouldKeepRotating = stateMachine.reusableData.movementInput != Vector2.zero;

            UpdateConsecutiveDashes();

            startTime = Time.time;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if(!shouldKeepRotating)
            {
                return;
            }

            RotateTowardsTargetRotation();

        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.dashParameterHash);

            SetBaseRotationData();
        }

        public override void OnAnimationTransitionEvent()
        {
            base.OnAnimationTransitionEvent();

            if(stateMachine.reusableData.movementInput == Vector2.zero)
            {
                stateMachine.ChangeState(stateMachine.hardStoppingState);

                return;
            }
            stateMachine.ChangeState(stateMachine.sprintingState);

        }

        /// <summary>
        /// Fuerza en caso de que no haya movimiento y haya que transicionar al dash
        /// </summary>
        private void Dash()
        {

            //Solo necesitamos la rotaci�n horizontal
            Vector3 dashDirection = stateMachine.player.transform.forward;
            dashDirection.y = 0f;

            //As� el target direction ser� la forward direction del personaje. Sin considerar rotaci�n de la c�mara
            UpdateTargetRotation(dashDirection, false);
            //La fuerza ser� la direcci�n hacia la que mire el jugador multiplicado por la velocidad de movimiento

            if (stateMachine.reusableData.movementInput != Vector2.zero)
            {
                UpdateTargetRotation(GetMovementInputDirection());

                dashDirection = GetTargetRotationDirection(stateMachine.reusableData.CurrentTargetRotation.y);
            }

            stateMachine.player.rb.velocity = dashDirection * GetMovementSpeed(false);
        }

        private void UpdateConsecutiveDashes()
        {
            if(!IsConsecutive())
            {
               consecutiveDashesUsed = 0;
            }

            consecutiveDashesUsed++;

            if(consecutiveDashesUsed == dashData.consecutiveDashesLimitAmount)
            {
                consecutiveDashesUsed = 0;

                stateMachine.player.input.DisableActionForSeconds(stateMachine.player.input.playerActions.Dash, dashData.dashLimitReachedCooldown);
            }
        }

        /// <summary>
        /// Mira si el dash es consecutivo (puede hacer dos consecutivos)
        /// </summary>
        /// <returns></returns>
        private bool IsConsecutive()
        {
            //Comprueba si el tiempo es menor al tiempo en el que entr� en DashState + el tiempo para ser considerado dash consecutivo
            return Time.time < startTime + dashData.timeToBeConsideredConsecutive;
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.input.playerActions.Movement.performed += OnMovementPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();


            stateMachine.player.input.playerActions.Movement.performed -= OnMovementPerformed;
        }

        /// <summary>
        ///  Por si soltamos la tecla al empezar a dashear. As� rota de todas formas
        /// </summary>
        /// <param name="context"></param>
        private void OnMovementPerformed(InputAction.CallbackContext context)
        {
            shouldKeepRotating = true;
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {

        }

        protected override void OnDashStarted(InputAction.CallbackContext context)
        {
            
        }
    }
}
