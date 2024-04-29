using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GenshintImpact2
{
    public class ItemEffect : MonoBehaviour
    {
        [SerializeField] public Transform playerArm;
        [SerializeField] private Item itemInstance;
        [SerializeField] private int quantityToAdd;
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player"))
            {
                SetParent();
                InventoryUI.instance.hasWeapon = true;
                Inventory.instance.AddItem(itemInstance, quantityToAdd);
                //Destroy(gameObject);
            }
        }

        private void SetParent()
        {
            transform.SetParent(playerArm);
            transform.SetParent(playerArm, false);
            transform.position = playerArm.transform.position;
        }
    }
}
