using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class EnemyHealthBar : MonoBehaviour
    {
        public Camera camera;
        // Start is called before the first frame update
        void Start()
        {
            camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        }
    }
}
