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
            //transform.eulerAngles = new Vector3(0, 0, 270);
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
