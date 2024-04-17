using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Permite a�adir cosas al capsule collider que sean espec�ficos del jugador
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
