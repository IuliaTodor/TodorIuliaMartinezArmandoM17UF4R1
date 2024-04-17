using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class PlayerFallingState : PlayerAirState
    {
        private PlayerFallingData fallData;

        private Vector3 playerPositionOnEnter;

        public PlayerFallingState(PlayerMovementSM playerMovementStateMachine) : base(playerMovementStateMachine)
        {
            fallData = airData.fallData;
        }

        public override void Enter()
        {
            base.Enter();

            StartAnimation(stateMachine.player.animationData.fallParameterHash);

            stateMachine.reusableData.movementSpeedModifier = 0f;

            playerPositionOnEnter = stateMachine.player.transform.position;

            ResetVerticalVelocity();
        }

        public override void Exit()
        {
            base.Exit();

            StopAnimation(stateMachine.player.animationData.fallParameterHash);
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            LimitVerticalVelocity();
        }

        /// <summary>
        /// Limita la velocidad de caída
        /// </summary>
        private void LimitVerticalVelocity()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            //Si hemos aclanzado el límite de velocidad
            if (playerVerticalVelocity.y >= -fallData.fallSpeedLimit)
            {
                return;
            }

            Vector3 limitedVelocityForce = new Vector3(0f, -fallData.fallSpeedLimit - playerVerticalVelocity.y, 0f);

            stateMachine.player.rb.AddForce(limitedVelocityForce, ForceMode.VelocityChange);
        }

        protected override void ResetSprintState()
        {
        }

       /// <summary>
       /// Transiciona a un estado de caída diferente dependiendo de la altura. Entre hard y roll depende de si nos movemos durante la caída o no
       /// </summary>
       /// <param name="collider"></param>
       protected override void OnContactWithGround(Collider collider)
       {
           //Para saber cuanta distancia ha caído
           float fallDistance = playerPositionOnEnter.y - stateMachine.player.transform.position.y;

           if (fallDistance < fallData.minimumDistanceToBeConsideredHardFall)
           {
               stateMachine.ChangeState(stateMachine.lightLandingState);

               return;
           }

           if (stateMachine.reusableData.shouldWalk && !stateMachine.reusableData.shouldWalk || stateMachine.reusableData.movementInput == Vector2.zero)
           {
               stateMachine.ChangeState(stateMachine.hardLandingState);

               return;
           }

           stateMachine.ChangeState(stateMachine.rollingState);

       }
    }
}
