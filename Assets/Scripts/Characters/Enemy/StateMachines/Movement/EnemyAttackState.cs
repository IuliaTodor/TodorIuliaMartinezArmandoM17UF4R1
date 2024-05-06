using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyAttackState : EnemyMovementState
    {


        public EnemyAttackState(EnemyMovementSM enemyMovementSM) : base(enemyMovementSM)
        {
        }



        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // public override void OnAnimationTransitionEvent()
        // {
        //     base.OnAnimationTransitionEvent();
        //     Debug.Log("STATE MACHINE: " + stateMachine);
        //     Debug.Log("aigrjiorejerig");

        //     // animator.gameObject.transform.parent.GetComponent<Enemy>().HandleAttack();
        // }
    }
}
