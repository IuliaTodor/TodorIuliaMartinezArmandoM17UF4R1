using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyMovementSM : StateMachine
    {
        public Enemy enemy { get; }
        public EnemyPatrolState patrolState { get; }
        public EnemyAttackState attackState { get; }
        public EnemyFleeState fleeState { get; }
        public EnemyFollowState followState { get; }
       
        public EnemyMovementSM(Enemy enemyReference)
        {
            enemy = enemyReference;

            patrolState = new EnemyPatrolState(this);

            attackState = new EnemyAttackState(this);

            fleeState = new EnemyFleeState(this);

            followState = new EnemyFollowState(this);
            
        }
    }
}
