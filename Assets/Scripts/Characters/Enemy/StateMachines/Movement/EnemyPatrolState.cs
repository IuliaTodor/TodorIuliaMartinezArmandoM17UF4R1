using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyPatrolState : EnemyMovingState
    {
        private EnemyMovementSM enemyMovementSM;

        public EnemyPatrolState(EnemyMovementSM enemyMovementSM)
        {
            this.enemyMovementSM = enemyMovementSM;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
