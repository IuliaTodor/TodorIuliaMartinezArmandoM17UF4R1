using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class SaveGameItem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                UIManager.instance.CallInteraction();
            }
        }
    }
}
