using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyFollowState : MonoBehaviour
    {
    private EnemyMovementSM enemyMovementSM;

    public EnemyFollowState(EnemyMovementSM enemyMovementSM)
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
