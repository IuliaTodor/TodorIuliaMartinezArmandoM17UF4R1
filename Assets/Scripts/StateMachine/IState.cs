using UnityEngine;

namespace GenshintImpact2
{
    //Diferencia entre interface y abstract class. Las interface definen los métodos, mientras que 
    //las abstract las implementan
    public interface IState
    {
        public void Enter();
        public void Exit();

        /// <summary>
        /// Lógica de leer input
        /// </summary>
        public void HandleInput();

        /// <summary>
        /// Lógica sin físicas
        /// </summary>
        public void Update();

        /// <summary>
        /// Lógica de físicas. Equivalente a FixedUpdate
        /// </summary>
        public void PhysicsUpdate();
        public void OnAnimationEnterEvent();
        public void OnAnimationExitEvent();
        /// <summary>
        /// Puede usarse para transicionar a otros estados en cierto frame de la animación
        /// </summary>
        public void OnAnimationTransitionEvent();
        public void OnTriggerEnter(Collider collider);
        public void OnTriggerExit(Collider collider);
    }
}
