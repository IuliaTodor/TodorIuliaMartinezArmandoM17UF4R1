using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerRollData
    {
        [SerializeField][Range(0f, 3f)] public float speedModifier = 1f;
    }
}
