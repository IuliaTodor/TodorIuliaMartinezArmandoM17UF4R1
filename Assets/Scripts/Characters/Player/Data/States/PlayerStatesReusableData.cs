using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Datos que tengan que reutilizarse entre estados
    /// </summary>
    public class PlayerStatesReusableData
    {
        //Todas las variables tienen public set, para establecer sus valores
        public Vector2 movementInput { get; set; }
        //Así editamos la velocidad sin tocar la velocidad base
        public float movementSpeedModifier { get; set; } = 1f;
        public float movementOnSlopesSpeedModifier { get; set; } = 1f;
        public float movementDecelerationForce { get; set; } = 1f;

        public bool shouldWalk { get; set; }
        public bool shouldSprint { get; set; }
        public bool shouldDance { get; set; }
        public bool isAiming { get; set; }

        //No se puede hacer un set a una propiedad Vector3 desde otra clase.
        private Vector3 currentTargetRotation;
        /// <summary>
        /// Tiempo que tarda en rotar el personaje
        /// </summary>
        private Vector3 timeToReachTargetRotation;
        private Vector3 dampedTargetRotationCurrentVelocity;
        private Vector3 dampedTargetRotationPassedTime;

        //Devuelve una referencia a la propiedad privada. Así podemos cambiar sus valores.
        public ref Vector3 CurrentTargetRotation
        {
            //No le damos un set porque es una referencia y ya lo podemos cambiar sin problemas
            get
            {
                return ref currentTargetRotation;
            }
        }

        public ref Vector3 TimeToReachTargetRotation
        {
            get
            {
                return ref timeToReachTargetRotation;
            }
        }

        public ref Vector3 DampedTargetRotationCurrentVelocity
        {
            get
            {
                return ref dampedTargetRotationCurrentVelocity;
            }
        }
        public ref Vector3 DampedTargetRotationPassedTime
        {
            get
            {
                return ref dampedTargetRotationPassedTime;
            }
        }

        public Vector3 currentJumpForce {  get; set; }

        public PlayerRotationData rotationData;


    }
}
