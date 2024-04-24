using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GenshintImpact2
{
    public class EnemyPatrolState : EnemyMovementState
    {
        public float range = 10;
        
        public EnemyPatrolState(EnemyMovementSM enemyMovementSM) : base(enemyMovementSM)
        {

        }


        public override void Update()
        {
            base.Update();

            //Si el enemigo ha alcanzado el destino genera un punto random en el rango y dentro de centrePoint

            if (stateMachine.enemy.agent.remainingDistance <= 0.01f)
            {
                Vector3 point;

                //Si encuentra un punto random, este será el nuevo punto al que ir
                if (HasFoundRandomPoint(stateMachine.enemy.centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.red, 10.0f);
                    //Instantiate(cube, point, Quaternion.identity);
                    stateMachine.enemy.agent.SetDestination(point);
                }
            }

        }

        /// <summary>
        /// Genera un punto random dentro de la esfera
        /// </summary>
        /// <param name="center"></param>
        /// <param name="range"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        bool HasFoundRandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * range; //Punto random en la esfera

            Debug.Log(randomPoint);

            //Instantiate(sphere, randomPoint, Quaternion.identity);

            NavMeshHit hit;

            //Busca una posición válida dentro del NavMesh y cercana al punto random. Si lo hace, lo devuelve como parámetro out.
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                //Instantiate(cilinder, hit.position, Quaternion.identity);
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }

        
    }
}
