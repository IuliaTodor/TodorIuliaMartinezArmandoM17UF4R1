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

        protected NavMeshAgent agent;

        public EnemyMovementState(EnemyMovementSM enemyMovementSM)
        {
            stateMachine = enemyMovementSM;
        }

        public virtual void Enter()
        {
            
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
    }
}
