using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GenshintImpact2
{
    public class EnemyFollowState : EnemyMovementState
    {

        public EnemyFollowState(EnemyMovementSM enemyMovementSM) : base(enemyMovementSM)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            stateMachine.enemy.agent.destination = stateMachine.enemy.movePosition.position;
        }
    }
}
