using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyAttack : MonoBehaviour
    {
        public bool inAttackRange = false;

        public GameObject target;
        private void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") 
            {
                inAttackRange = true;
                target = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.gameObject.tag == "Player") inAttackRange = false;
        }
    }
}
