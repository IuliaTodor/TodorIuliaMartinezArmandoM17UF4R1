using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Referencia al CapsuleCollider y su centro en local space
    /// </summary>
    //Un local space son las propias coordenadas del objeto
    public class CapsuleColliderData
    {
        public CapsuleCollider collider {  get; private set; }
        public Vector3 colliderCenterInLocalSpace {  get; private set; }

        //Pars obtener la parte baja del collider
        public Vector3 colliderVerticalExtents {  get; private set; }

        public void Initialize(GameObject gameObject)
        {
            if (collider != null)
            {
                return;
            }

            collider = gameObject.GetComponent<CapsuleCollider>();

            UpdateColliderData();
        }

        public void UpdateColliderData()
        {

            colliderCenterInLocalSpace = collider.center;

            colliderVerticalExtents = new Vector3(0f, collider.bounds.extents.y, 0f);
        }
    }
}
