using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerRunData
    {
        [SerializeField][field: Range(1f, 2f)] public float speedModifier = 1f;
    }
}
