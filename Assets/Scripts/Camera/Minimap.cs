using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    public class Minimap : MonoBehaviour
    {
        public float angle;

        private Transform player;
        public Transform minimapOverlay;

        // Start is called before the first frame update
        void Start()
        {

            player = GameObject.FindWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = player.position + Vector3.up * 5f;

            RotateOverlay();

        }

        private void RotateOverlay()
        {
            minimapOverlay.localRotation = Quaternion.Euler(0, 0, player.eulerAngles.y - angle);
        }
    }
}
