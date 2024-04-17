using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [Serializable]
    public class PlayerLayerData
    {
        [SerializeField] public LayerMask groundLayer;
        [SerializeField] public LayerMask itemLayer;

        /// <summary>
        /// Comprueba si la layerMask contiene ciertos layer
        /// </summary>
        /// <param name="layerMask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public bool ContainsLayer(LayerMask layerMask, int layer)
        {
            //Mucha mierda de bitwise, layers y posiciones de bits
            return (1 << layer & layerMask) != 0;
        }
        
        public bool IsGroundLayer(int layer)
        {
            return ContainsLayer(groundLayer, layer);
        }
        public bool IsItemLayer(int layer)
        {
            return ContainsLayer(itemLayer, layer);
        }
    }
}
