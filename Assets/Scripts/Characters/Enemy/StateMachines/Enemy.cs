using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace GenshintImpact2
{

    public class Enemy : MonoBehaviour
    {
        private EnemyMovementSM enemyStateMachine;
        public static Enemy instance;

        public Rigidbody rb { get; private set; }
        public Animator animator { get; private set; }

        public NavMeshAgent agent;

        public Transform movePosition;

        public GameObject HealthBar;

        public float maxHealth;

        public float health;

        /// <summary>
        /// Centro de la zona de patrulla
        /// </summary>
        public Transform centrePoint;

        public GameObject cube;
        public GameObject sphere;
        public GameObject cilinder;




        private void Awake()
        {
            instance = this;
            enemyStateMachine = new EnemyMovementSM(this);
            rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();

        }

        void Start()
        {
            enemyStateMachine.ChangeState(enemyStateMachine.patrolState);
        }

        void Update()
        {
            enemyStateMachine.HandleInput();
            enemyStateMachine.Update();
        }
        private void FixedUpdate()
        {
            enemyStateMachine.PhysicsUpdate();
        }

        private void OnTriggerEnter(Collider collider)
        {
            enemyStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            enemyStateMachine.OnTriggerExit(collider);
        }

        public void OnMovementStateAnimationEnterEvent()
        {
            enemyStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            enemyStateMachine.OnAnimationExitEvent();
        }

        public void OnMovementStateAnimationTransitionEvent()
        {
            enemyStateMachine.OnAnimationTransitionEvent();
        }

        public void HandleDamage(float damageTaken)
        {
            if (health > 0)
            {
                health -= damageTaken;
                UIManager.instance.UpdateHealthBar(HealthBar.GetComponent<Slider>(), -damageTaken / maxHealth);

                if (health <= 0)
                {
                    health = 0;
                    // Death
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(new Vector3(centrePoint.position.x, centrePoint.position.y, centrePoint.position.z), 10f);
        }

    }
}
