using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerFallingData
    {
        [SerializeField][Range(0f, 10f)] public float fallSpeedLimit = 10f;
        [SerializeField][Range(0f, 100f)] public float minimumDistanceToBeConsideredHardFall = 3f;
    }
}
