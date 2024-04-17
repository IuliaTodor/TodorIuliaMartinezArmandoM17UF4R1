using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenshintImpact2
{
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")]
    public class PlayerSO : ScriptableObject
    {
        [SerializeField] public PlayerGroundedData groundedData;
        [SerializeField] public PlayerAirData airData;

    }
}
