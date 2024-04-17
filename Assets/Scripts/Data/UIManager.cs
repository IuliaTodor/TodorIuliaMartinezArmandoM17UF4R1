using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace GenshintImpact2
{
    public class UIManager : MonoBehaviour
    {
        public PlayerInput input { get; private set; }
        public GameObject inventoryPanel;
        public GameObject descriptionPanel;
        public GameObject pausePanel;

        public GameObject diePanel;
        public AnimationClip deadClip;

        public static UIManager instance;

        public bool gameIsPaused = false;

        // Start is called before the first frame update
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            input = FindObjectOfType<PlayerInput>();
        }

        private void OnEnable()
        {
            AddInputActionsCallbacks();
        }

        private void OnDisable()
        {
            RemoveInputActionsCallbacks();
        }

        // Update is called once per frame
        void Update()
        {

        }
        protected void AddInputActionsCallbacks()
        {
            input.playerActions.ToggleInventory.started += OnInventoryToggle;

            input.playerActions.TogglePauseMenu.started += Pause;

            input.playerActions.Interact.started += Interact;
        }

        protected void RemoveInputActionsCallbacks()
        {
            input.playerActions.ToggleInventory.started -= OnInventoryToggle;

            input.playerActions.TogglePauseMenu.started -= Pause;

            input.playerActions.Interact.started -= Interact;
        }

        private void OnInventoryToggle(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            Debug.Log("inventory");

            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            descriptionPanel.SetActive(false);
        }

        public void Resume(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        void Pause(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            pausePanel.SetActive(true);
            input.playerActions.ToggleInventory.Disable();
            Time.timeScale = 0f;
            gameIsPaused = true;
            
        }

        public void DiePanel()
        {
            StartCoroutine(PlayerDie());
            //FindObjectOfType<AudioManager>().Play("OpenPauseMenu");
        }

        IEnumerator PlayerDie()
        {

            yield return new WaitForSeconds(deadClip.length);

            diePanel.SetActive(true);
            input.playerActions.ToggleInventory.Disable();
            Time.timeScale = 0f;
            gameIsPaused = true;

        }

        private void Interact(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            CallInteraction();
        }

        public void CallInteraction()
        {
            DataManager.instance.SaveData();
        }
    }
}
