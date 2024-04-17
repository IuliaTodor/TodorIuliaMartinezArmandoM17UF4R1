using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerAirData
    {
        [SerializeField] public PlayerJumpData jumpData;
        [SerializeField] public PlayerFallingData fallData;
    }
}
