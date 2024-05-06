using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GenshintImpact2
{
    public class EnemyFollowState : EnemyMovementState
    {
        Quaternion startRotation;

        float smooothRotationTime = 3f;
        [SerializeField] float stoppingDistance = 1f;


        public EnemyFollowState(EnemyMovementSM enemyMovementSM) : base(enemyMovementSM)
        {

        }

        public override void Enter()
        {
            base.Enter();            
        }

        public override void Update()
        {
            base.Update();

            OnSetDestination();

            if (stateMachine.enemy.agent.remainingDistance <= .1f)
                stateMachine.enemy.transform.rotation = Quaternion.Slerp(stateMachine.enemy.transform.rotation, startRotation, Time.deltaTime * smooothRotationTime);

            stateMachine.enemy.destination = stateMachine.enemy.target.transform.position;

            //Debug.Log("Target es: " + stateMachine.enemy.target.transform.position);
            //Debug.Log("Destination es: " + stateMachine.enemy.target.transform.position);

            stateMachine.enemy.agent.stoppingDistance = stoppingDistance;
            stateMachine.enemy.agent.SetDestination(stateMachine.enemy.destination);

            OnHitboxEnter();

            OnEnemyHealth();
        }

        public override void OnSetDestination()
        {
            base.OnSetDestination();

            if (!stateMachine.enemy.fieldOfView.IsTarget)
            {
                OnFieldViewExit();
            }
        }

        public override void OnFieldViewExit()
        {
            base.OnFieldViewExit();
        }

        public override void OnHitboxEnter()
        {
            base.OnHitboxEnter();

            if (stateMachine.enemy.hitBox.inAttackRange)
            {
                stateMachine.enemy.target.GetComponent<Player>().HandleDamage(0.05f);
                Debug.Log(stateMachine.enemy.target.GetComponent<Player>().health);
                //static.ChangeState(enemyStateMachine.attackState);
                // Mover a la enemyStateMachine
                //if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) animator.SetTrigger("Attack");
            }
        }

        public override void OnEnemyHealth()
        {
            base.OnEnemyHealth();
        }

        //public override void Enter()
        //{
        //    base.Enter();
        //}

        //public override void Update()
        //{
        //    stateMachine.enemy.agent.destination = stateMachine.enemy.movePosition.position;
        //}
    }
}
