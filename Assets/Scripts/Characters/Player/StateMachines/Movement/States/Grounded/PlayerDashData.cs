using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerDashData
    {
        [SerializeField][Range(1f, 3f)] public float speedModifier = 2f;
        [SerializeField] public PlayerRotationData rotationData;
        [SerializeField][Range(0f, 2f)] public float timeToBeConsideredConsecutive = 1f;
        [SerializeField][Range(1f, 10)] public float consecutiveDashesLimitAmount = 2f;
        [SerializeField][Range(0f, 5f)] public float dashLimitReachedCooldown = 1.75f;
    }
}
