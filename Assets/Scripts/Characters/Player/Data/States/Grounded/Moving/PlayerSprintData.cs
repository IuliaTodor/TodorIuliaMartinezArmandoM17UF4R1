using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerSprintData
    {
        [field: SerializeField][field: Range(1f, 3f)] public float speedModifier = 1.7f;
        //Tiempo para pasar entre sprint y run state
        [field: SerializeField][field: Range(0f, 5f)] public float sprintToRunTime = 1f;
        [field: SerializeField][field: Range(0f, 2f)] public float runToWalkTime = 0.5f;
    }
}
