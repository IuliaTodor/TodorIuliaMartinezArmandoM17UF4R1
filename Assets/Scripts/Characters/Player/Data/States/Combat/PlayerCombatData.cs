using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerCombatData
    {
        [SerializeField][field: Range(0f, 1f)] public float speedModifier = 0.7f;
    }
}
