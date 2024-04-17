using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Permite añadir cosas al capsule collider que sean específicos del jugador
    /// </summary>
    [Serializable]
    public class PlayerCapsuleColliderUtility : CapsuleColliderUtility
    {
        [SerializeField] public PlayerTriggerColliderData triggerColliderData;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            triggerColliderData.Initialize();
        }
    }
}
