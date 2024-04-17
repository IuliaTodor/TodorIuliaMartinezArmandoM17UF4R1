using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerStopData
{
    [field: SerializeField][field: Range(0f, 15f)] public float lightDecelerationForce = 5f;
    [field: SerializeField][field: Range(0f, 15f)] public float mediumDecelerationForce = 6.5f;
    [field: SerializeField][field: Range(0f, 15f)] public float hardDecelerationForce = 5f;
}
}
