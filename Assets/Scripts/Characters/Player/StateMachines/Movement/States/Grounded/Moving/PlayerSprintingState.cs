using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerSprintingState : PlayerMovingState
    {
        private PlayerSprintData sprintData;
        private float startTime;
        
        //Si el botón de sprint no es mantenido el tiempo suficiente, pasará al running state
        private bool keepSprinting;
        private bool shouldResetSprintState;

        public PlayerSprintingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            sprintData = movementData.sprintData;
        }

        public override void Enter()
        {
            stateMachine.reusableData.movementSpeedModifier = sprintData.speedModifier;

            base.Enter();

            StartAnimation(stateMachine.player.animationData.sprintParameterHash);

            stateMachine.reusableData.currentJumpForce = airData.jumpData.strongForce;

            startTime = Time.time;

            shouldResetSprintState = true;

            if (!stateMachine.reusableData.shouldSprint)
            {
                keepSprinting = false;
            }

            AudioManager.instance.StopPlaying("Main");
            AudioManager.instance.Play("Sprint");
        }

        public override void Exit()
        {
            base.Exit();


            AudioManager.instance.StopPlaying("Sprint");
            AudioManager.instance.Play("Main");

            StopAnimation(stateMachine.player.animationData.sprintParameterHash);

            if (shouldResetSprintState)
            {

                keepSprinting = false;

                stateMachine.reusableData.shouldSprint = false;
            }
        }

        public override void Update()
        {
            base.Update();

            //Si es true no queremos transicionar a otro estado
            if (keepSprinting)
            {
                return;
            }

            //Significa que no ha pasado suficiente tiempo para cambiar de estado
            if (Time.time < startTime + sprintData.sprintToRunTime)
            {
                return;
            }

            StopSprinting();
        }

        /// <summary>
        /// Transiciona entre run y hard stop state
        /// </summary>
        private void StopSprinting()
        {
            if (stateMachine.reusableData.movementInput == Vector2.zero)
            {
                //if(stateMachine.reusableData.shouldDance)
                //{
                //    return;
                //}

                stateMachine.ChangeState(stateMachine.idlingState);

                return;
            }

            stateMachine.ChangeState(stateMachine.runningState);
        }

        protected override void AddInputActionsCallbacks()
        {
            base.AddInputActionsCallbacks();

            stateMachine.player.input.playerActions.Sprint.performed += OnSprintPerformed;
        }

        protected override void RemoveInputActionsCallbacks()
        {
            base.RemoveInputActionsCallbacks();

            stateMachine.player.input.playerActions.Sprint.performed -= OnSprintPerformed;
        }

        protected override void OnFall()
        {
            shouldResetSprintState = false;

            base.OnFall();
        }
        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            keepSprinting = true;

            stateMachine.reusableData.shouldSprint = true;
        }

        protected override void OnMovementCanceled(InputAction.CallbackContext context)
        {
            stateMachine.ChangeState(stateMachine.hardStoppingState);

            base.OnMovementCanceled(context);
        }

        //Para poder seguir sprintando tras saltar
        protected override void OnJumpStarted(InputAction.CallbackContext context)
        {
            shouldResetSprintState = false;

            base.OnJumpStarted(context);
        }

        //protected override void OnFall()
        //{
        //    shouldResetSprintState = false;

        //    base.OnFall();
        //}
    }
}
