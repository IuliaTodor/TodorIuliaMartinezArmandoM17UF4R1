using GenshintImpact2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GenshintImpact2
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject inventoryPanelDescription;
        [SerializeField] public Image itemIcon;
        [SerializeField] private Text itemName;
        [SerializeField] private Text itemDescription;

        [SerializeField] private Slot slotPrefabs;
        [SerializeField] private Transform container;

        public bool hasWeapon = false;

        //El index del slot que escogemos para mover
        public int initalSlotIndexToMove { get; private set; }
        public Slot selectedSlot { get; private set; } //El slot que tengamos seleccionado
        public List<Slot> slots = new List<Slot>();
        public static InventoryUI instance;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            InitializeInventory();
            initalSlotIndexToMove = -1;
        }

        private void Update()
        {
            UpdateSelectedSlot();
            if (Input.GetKeyUp(KeyCode.M))
            {
                if (selectedSlot != null)
                {
                    //Asignamos el índice del slot que estamos seleccionando
                    initalSlotIndexToMove = selectedSlot.index;
                }
            }
        }

        /// <summary>
        /// Añadimos los slots al inventario
        /// </summary>
        private void InitializeInventory()
        {
            for (int i = 0; i < Inventory.instance.slotsNumber; i++)
            {

                Slot newSlot = Instantiate(slotPrefabs, container);
                newSlot.index = i;
                slots.Add(newSlot);
            }
        }

        /// <summary>
        /// Actualizamos el slot seleccionado
        /// </summary>
        private void UpdateSelectedSlot()
        {
            GameObject GOSelected = EventSystem.current.currentSelectedGameObject; //El game object seleccionado

            //Si es null no hacemos nada
            if (GOSelected == null)
            {
                return;
            }

            Slot slot = GOSelected.GetComponent<Slot>(); //La referencia del slot seleccionado se guarda en la variable

            //SelectedSlot pasa a ser el slot que estamos seleccionando
            if (slot != null)
            {
                selectedSlot = slot;
            }

        }

        /// <summary>
        /// El item aparece en el inventario
        /// </summary>
        /// <param name="itemToAdd"></param>
        /// <param name="quantity"></param>
        /// <param name="itemIndex"></param>
        public void DrawItemOnInventory(Item itemToAdd, int quantity, int itemIndex)
        {
            Slot slot = slots[itemIndex];
            if (itemToAdd != null)
            {
                slot.ActivateSlotUI(true);
                slot.UpdateSlot(itemToAdd, quantity);
            }

            else
            {
                slot.ActivateSlotUI(false);
            }
        }

        private void UpdateInventoryDescription(int index)
        {
            if (Inventory.instance.items[index] != null)
            {
                itemIcon.sprite = Inventory.instance.items[index].image;
                itemName.text = Inventory.instance.items[index].itemName;
                Debug.Log(itemName.text);
                itemDescription.text = Inventory.instance.items[index].description;

                inventoryPanelDescription.SetActive(true);
            }

            else
            {
                inventoryPanelDescription.SetActive(false);
            }
        }

        /// <summary>
        /// Método que llamamos al usar el botón de Usar en el inventario
        /// </summary>
        public void UseItem()
        {
            //Si hay un objeto en ese slot, usamos su item
            if (selectedSlot != null)
            {
                selectedSlot.UseItemSlot();
                selectedSlot.SelectSlot();
            }
        }

        #region EventManager
        private void SlotInteractionResponse(SlotInteraction type, int index)
        {
            if (type == SlotInteraction.Click)
            {
                UpdateInventoryDescription(index);
            }
        }

        private void OnEnable()
        {
            Slot.eventSlotInteraction += SlotInteractionResponse;
        }

        private void OnDisable()
        {
            Slot.eventSlotInteraction -= SlotInteractionResponse;
        }
    }
    #endregion
}