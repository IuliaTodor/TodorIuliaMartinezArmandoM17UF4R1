using UnityEngine;

namespace GenshintImpact2
{
    //Diferencia entre interface y abstract class. Las interface definen los m�todos, mientras que 
    //las abstract las implementan
    public interface IState
    {
        public void Enter();
        public void Exit();

        /// <summary>
        /// L�gica de leer input
        /// </summary>
        public void HandleInput();

        /// <summary>
        /// L�gica sin f�sicas
        /// </summary>
        public void Update();

        /// <summary>
        /// L�gica de f�sicas. Equivalente a FixedUpdate
        /// </summary>
        public void PhysicsUpdate();
        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        /// <summary>
        /// Puede usarse para transicionar a otros estados en cierto frame de la animaci�n
        /// </summary>
        public void OnAnimationTransitionEvent();
        public void OnTriggerEnter(Collider collider);
        public void OnTriggerExit(Collider collider);
    }
}
