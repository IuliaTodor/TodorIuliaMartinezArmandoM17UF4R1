using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerGroundedData
    {
        [SerializeField][Range(0f, 25f)] public float baseSpeed = 5f;
        [SerializeField][Range(0f, 5f)] public float groundToFallRayDistance = 1f;
        /// <summary>
        /// Permite establecer diferentes velocidades dependiendo del ángulo del suelo
        /// </summary>
        [SerializeField] public AnimationCurve slopeSpeedAngles;
        [SerializeField] public PlayerRotationData baseRotationData;
        [SerializeField] public PlayerWalkData walkData;
        [SerializeField] public PlayerRunData runData;
        [SerializeField] public PlayerSprintData sprintData;
        [SerializeField] public PlayerDashData dashData;
        [SerializeField] public PlayerStopData stopData;
        [SerializeField] public PlayerRollData rollData;
        [SerializeField] public PlayerCombatData combatData;
    }
}
