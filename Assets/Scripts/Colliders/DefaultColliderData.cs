using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Clase necesaria para recalcular las nuevas dimensiones del capsule collider
    /// </summary>
    [Serializable]
    public class DefaultColliderData
    {
        //Estos son relativos a lo alto que sea el modelo del personaje
        [SerializeField] public float height = 1.25f;
        [SerializeField] public float centerY = 0.6f;
        [SerializeField] public float radius = 0.24f;
    }
}
