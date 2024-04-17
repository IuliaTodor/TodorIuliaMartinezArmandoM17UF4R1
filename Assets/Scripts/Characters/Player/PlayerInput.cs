using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerInput : MonoBehaviour
    {
       public PlayerInputActions inputActions {  get; private set; }
        /// <summary>
        /// El mapa de inputs
        /// </summary>
        public PlayerInputActions.PlayerActions playerActions { get; private set; }

        private void Awake()
        {
            inputActions = new PlayerInputActions();

            //Player es la variable del ActionMap
            playerActions = inputActions.Player;
        }

        private void OnEnable()
        {
            inputActions.Enable();
            
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void DisableActionForSeconds(InputAction action, float seconds)
        {
            //Las corutinas solo pueden llamarse en MonoBehaviour
            StartCoroutine(DisableAction(action, seconds));
        }

        /// <summary>
        /// Deshabilitamos la acción, esperamos unos segundos y la habilitamos de nuevo
        /// </summary>
        /// <param name="action"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        private IEnumerator DisableAction(InputAction action, float seconds)
        {
            action.Disable();

            yield return new WaitForSeconds(seconds);

            action.Enable();
        }
    }
}
