using GenshintImpact2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerJumpData
    {
        //Distancai del salto al rayo
        [field: SerializeField][field: Range(0f, 5f)] public float jumpToGroundRayDistance = 2f;

        [field: SerializeField] public AnimationCurve jumpForceModifierOnSlopeUpwards;

        [field: SerializeField] public AnimationCurve jumpForceModifierOnSlopeDownwards;

        [field: SerializeField] public PlayerRotationData rotationData;
        //Fuerzas de salto dependiendo de en qué estado salta
        [field: SerializeField] public Vector3 stationaryForce;
        [field: SerializeField] public Vector3 weakForce;
        [field: SerializeField] public Vector3 mediumForce;
        [field: SerializeField] public Vector3 strongForce;

        [field: SerializeField][field: Range(0f, 10f)] public float decelerationForce = 1.5f;
    }
}
