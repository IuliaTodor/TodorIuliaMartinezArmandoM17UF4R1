using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody rb;
        [SerializeField] private float speed = 5;
        private float startTime;
        [SerializeField] private float lifeTime = 3;
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            rb.velocity = new Vector3(-speed, 0, 0);
            startTime = Time.time;
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
        void Update()
        {
            if (Time.time - startTime > lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
