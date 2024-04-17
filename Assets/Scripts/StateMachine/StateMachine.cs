using UnityEngine;

namespace GenshintImpact2
{
    /// <summary>
    /// Clase de la cual todos los estados heredan
    /// </summary>
    public abstract class StateMachine
    {
        //Protected permite que las clases que hereden de esta accedan a la variable
        protected IState currentState;

        /// <summary>
        /// Cambia el estado actual a otro
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(IState newState)
        {
            //Resetea los datos que deban serlo antes de cambiar de estado
            //El operador ? hace que se lance la función si currentState no es un null
            currentState?.Exit();

            currentState = newState;

            //Establece los datos que deban serlo en el nuevo estado
            currentState.Enter();
        }

        public void HandleInput()
        {
            currentState?.HandleInput();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }

        public void OnAnimationEnterEvent()
        {
            currentState?.OnAnimationExitEvent();
        }


        public void OnAnimationExitEvent()
        {
            currentState?.OnAnimationExitEvent();
        }

        public void OnAnimationTransitionEvent()
        {
            currentState?.OnAnimationTransitionEvent();
        }
        
        public void OnTriggerEnter(Collider collider)
        {
            currentState?.OnTriggerEnter(collider);
        }
        
        public void OnTriggerExit(Collider collider)
        {
            currentState?.OnTriggerExit(collider);
        }

    }
}
