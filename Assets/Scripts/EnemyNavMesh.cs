using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GenshintImpact2
{
    public class EnemyNavMesh : MonoBehaviour
    {
        [SerializeField] Transform movePosition;
        private NavMeshAgent agent;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
                agent.destination = movePosition.position;
            }
            
        }
    }
}
