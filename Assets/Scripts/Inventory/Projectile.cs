using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class Projectile : MonoBehaviour
    {
        private float startTime;
        [SerializeField] private float lifeTime = 3;
        void Start()
        {
            startTime = Time.time;
        }
        void Update()
        {
            if (Time.time - startTime > lifeTime)
            {
                Destroy(gameObject);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().HandleDamage(1);
            }
        }

    }
}
