using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerTriggerColliderData
    {
        [SerializeField] public BoxCollider groundCheckCollider;

        public Vector3 groundCheckColliderVerticalExtents { get; private set; }

        public void Initialize()
        {
            groundCheckColliderVerticalExtents = groundCheckCollider.bounds.extents;
        }
    }
}
