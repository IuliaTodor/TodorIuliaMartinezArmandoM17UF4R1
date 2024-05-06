using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class SlopeData
    {
        //Porcentaje que va de 0 a 100%(1f)
        [SerializeField][Range(0f, 1f)] public float stepHeightPercantage = 0.25f;
        /// <summary>
        /// Distancia a la que tiene que flotar el Ray
        /// </summary>
        [SerializeField][Range(0f, 5f)] public float floatRayDistance = 2f;
        /// <summary>
        /// Fuerza que se añade al subir cuestas
        /// </summary>
        [SerializeField][Range(0f, 50f)] public float stepReachForce = 25f;
    }
}
