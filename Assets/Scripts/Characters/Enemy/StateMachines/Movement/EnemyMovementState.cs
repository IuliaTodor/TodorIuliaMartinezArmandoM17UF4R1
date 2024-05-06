using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

namespace GenshintImpact2
{
    public class EnemyMovementState : IState
    {
        protected EnemyMovementSM stateMachine;

        public EnemyMovementState(EnemyMovementSM enemyMovementSM)
        {
            stateMachine = enemyMovementSM;
        }

        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);
        }

        public virtual void Exit()
        {

        }

        public virtual void HandleInput()
        {

        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        { 

        }

        public virtual void OnAnimationTransitionEvent()
        {

        }

        public virtual void OnTriggerEnter(Collider collider)
        {

        }

        public virtual void OnTriggerExit(Collider collider)
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void OnFieldViewEnter()
        {
            stateMachine.ChangeState(stateMachine.followState);
        }

        public virtual void OnFieldViewExit()
        {
            stateMachine.ChangeState(stateMachine.patrolState);
        }

        public virtual void OnSetDestination()
        {
            
        }

        public virtual void OnHitboxEnter()
        {
           
        }

        public virtual void OnEnemyHealth()
        {
            if(stateMachine.enemy.health <= stateMachine.enemy.maxHealth/2)
            {
                stateMachine.ChangeState(stateMachine.patrolState);
            }
        }

    }
}
