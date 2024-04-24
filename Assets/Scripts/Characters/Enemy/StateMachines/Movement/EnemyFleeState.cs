using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyFleeState : EnemyMovementState
    {
    private EnemyMovementSM enemyMovementSM;

    public EnemyFleeState(EnemyMovementSM enemyMovementSM) : base(enemyMovementSM)
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
