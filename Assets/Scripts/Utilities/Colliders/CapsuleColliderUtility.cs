using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Recalcula la añtura del CapsuleCollider
    /// </summary>
    [Serializable]
    public class CapsuleColliderUtility
    {
        public CapsuleColliderData capsuleColliderData;
        [SerializeField] public DefaultColliderData defaultColliderData;
        [SerializeField] public SlopeData slopeData;

        public void Initialize(GameObject gameObject)
        {
            if(capsuleColliderData != null)
            {
                return;
            }

            capsuleColliderData = new CapsuleColliderData();
            capsuleColliderData.Initialize(gameObject);

            OnInitialize();
        }

        protected virtual void OnInitialize()
        {

        }

        /// <summary>
        public void CalculateCapsuleColliderDimensions()
        {
            SetCapsuleColliderRadius(defaultColliderData.radius);
            //Se multiplica por el porcentaje de altura del escalón. El 1f - es porque queremos quitar el porcentaje de la altura
            //Por ejemplo, quitando 25% de la altura significa que la nueva altura será 75% de su valor por defecto
            SetCapsuleColliderHeight(defaultColliderData.height * (1f - slopeData.stepHeightPercantage));
            SetCapsuleColliderRadius(defaultColliderData.radius);
            RecalculateCapsuleColliderCenter();

            //Evita problemas en caso de que el tamaño del CC se vuelva esférico al disminuir
            float halfColliderHeight = capsuleColliderData.collider.height / 2f;

            //Si la altura es el doble o menos del radio. Si dividimos la altura entre 2, obtenemos la mitad
            //Si la mitad de la altura es igual al radio, entonces la altura es el doble del radio
            //Si la mitad de la altura es menor al radio, la altura total es menor al doble del radio
            if(halfColliderHeight < capsuleColliderData.collider.radius)
            {
                SetCapsuleColliderRadius(halfColliderHeight);
            }

            capsuleColliderData.UpdateColliderData();
        }

        public void SetCapsuleColliderRadius(float radius)
        {
           capsuleColliderData.collider.radius = radius;
        }

        public void SetCapsuleColliderHeight(float height)
        {
            capsuleColliderData.collider.height = height;
        }
        /// <summary>
        /// Recalcula la altura, centro y radio del collider
        /// </summary>
        //Queremos que el CapsuleCollider se recentralice en la parte de arriba del personaje en vez del centro.
        //Esto se debe a que cambiar la altura del CC hace que su tamaño vaya de abajo a arriba
        public void RecalculateCapsuleColliderCenter()
        {
            //Diferencia entre el tamaño por defecto y la altura actual
            float colliderHeightDifference = defaultColliderData.height - capsuleColliderData.collider.height;

            //Añade la mita de esta diferencia en el centro
            Vector3 newColliderCenter = new Vector3(0f, defaultColliderData.centerY + (colliderHeightDifference/2f), 0f);

            capsuleColliderData.collider.center = newColliderCenter;


        }
    }
}
