using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GenshintImpact2
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementSM stateMachine;

        protected PlayerGroundedData movementData;
        protected PlayerAirData airData;

        public PlayerMovementState(PlayerMovementSM playerMovementSM)
        {
            stateMachine = playerMovementSM;

            movementData = stateMachine.player.data.groundedData;

            airData = stateMachine.player.data.airData;

            InitializeData();
        }

        private void InitializeData()
        {
            SetBaseRotationData();
        }

        #region IStateMethods
        //Virtual permite cambiar lo que suceda dentro de la funci�n
        public virtual void Enter()
        {
            //Debug.Log("State: " + GetType().Name);

            AddInputActionsCallbacks();

        }

        public virtual void Exit()
        {
            RemoveInputActionsCallbacks();
        }
        public virtual void HandleInput()
        {
            ReadMovementInput();
        }

        public virtual void PhysicsUpdate()
        {
            Move();
        }

        public virtual void Update()
        {

        }

        public virtual void OnAnimationEnterEvent()
        {

        }

        public virtual void OnAnimationExitEvent()
        {

        }

        public virtual void OnAnimationTransitionEvent()
        {

        }

        public virtual void OnTriggerEnter(Collider collider)
        {
            if (stateMachine.player.layerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGround(collider);

                return;
            }

            if (stateMachine.player.layerData.IsItemLayer(collider.gameObject.layer))
            {
                OnItemPickUp(collider);

                return;
            }

        }

        public virtual void OnTriggerExit(Collider collider)
        {
            if (stateMachine.player.layerData.IsGroundLayer(collider.gameObject.layer))
            {
                OnContactWithGroundExited(collider);

                return;
            }
        }


        #endregion

        #region MainMethods
        /// <summary>
        /// Lee el input de movimiento
        /// </summary>
        private void ReadMovementInput()
        {
            stateMachine.reusableData.movementInput = stateMachine.player.input.playerActions.Movement.ReadValue<Vector2>();
        }

        private void Move()
        {
            //Significa que no nos estamos moviendo
            if (stateMachine.reusableData.movementInput == Vector2.zero || stateMachine.reusableData.movementSpeedModifier == 0f)
            {
                return;
            }

            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);

            float movementSpeed = GetMovementSpeed();

            //AddForce a�ade fuerza (quien lo dir�a). Sin esto seguir�a a�adiendo fuerza al personaje todo el rato
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            //AddForce hace el cambio en el siguiente PhysicsUpdate. Cambiar Velocity hace el cambio instant�neo
            //El tipo de fuerza VelocityChange no depende de la masa ni del tiempo

            //Es mejor dejar el Vector como �ltimo miembro de la multiplicaci�n en caso de tener m�s cosas multiplic�ndose
            //As� no se sobrecarga multiplicando vectores (m�s trabajo) y solo lo hace al final
            stateMachine.player.rb.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
        }

        /// <summary>
        /// El jugador se mover� hacia la suma del input de direcci�n y la rotaci�n de la c�mara
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }

        #region RotateRegion

        /// <summary>
        /// Actualiza la rotaci�n del personaje
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="shouldConsiderCameraRotation"></param>
        /// <returns></returns>
        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
        {
            float directionAngle = GetDirectionAngle(direction);

            //La rotaci�n de la c�mara solo se considera cuando el jugador se mueve 
            if (shouldConsiderCameraRotation)
            {
                directionAngle = AddCameraRotationToAngle(directionAngle);
            }

            if (directionAngle != stateMachine.reusableData.CurrentTargetRotation.y)
            {
                UpdateTargetRotationData(directionAngle);
            }

            return directionAngle;
        }

        /// <summary>
        /// Obtiene el �ngulo hacia el cual rotar� el personaje al moverse
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private float GetDirectionAngle(Vector3 direction)
        {
            //Usamos Atan2 para conseguir el �ngulo hacia el cual rotar� relativo al eje x.
            //Multiplicamos por Rad2Deg para pasar de radianes a grados
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            //Atan2 puede devolver �ngulos negativos (va de -180 a 180). Sumando 360 evitamos eso
            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        /// <summary>
        /// A�ade la rotaci�n de la c�mara al �ngulo el cual se mover� el jugador. El movimiento es relativo a la c�mara
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        private float AddCameraRotationToAngle(float angle)
        {
            //Usamos eulerAngles en vez de rotation porque este �ltimo devuelve un Quaternion
            //El eje y de la c�mara es su rotaci�n horizontal
            angle += stateMachine.player.cameraTransform.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }

        /// <summary>
        /// Actualiza el tiempo pasado y al volver a rotar hace que el targetRotation sea el nuevo �ngulo
        /// </summary>
        /// <param name="targetAngle"></param>
        private void UpdateTargetRotationData(float targetAngle)
        {
            stateMachine.reusableData.CurrentTargetRotation.y = targetAngle;

            stateMachine.reusableData.DampedTargetRotationPassedTime.y = 0f;
        }

        /// <summary>
        /// Devuelve la direcci�n hacia la que rota el personaje
        /// </summary>
        /// <param name="targetAngle"></param>
        /// <returns></returns>
        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            //Siempre queremos la rotaci�n del eje z
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        /// <summary>
        /// Rota al �ngulo deseado con un muy peque�o delay
        /// </summary>
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.player.rb.rotation.eulerAngles.y;

            //Si estamos ya en el �ngulo deseado no hace la rotaci�n
            if (currentYAngle == stateMachine.reusableData.CurrentTargetRotation.y)
            {
                return;
            }

            //El �ngulo con cierto delay en la rotaci�n
            //Si pasamos timeToReachTargetRotation a secas, tardar�a 0,14 para cada smooth que ejecuta. Con su vector .y tarda 0,14 para toda la rotaci�n
            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.reusableData.CurrentTargetRotation.y,
            ref stateMachine.reusableData.DampedTargetRotationCurrentVelocity.y, stateMachine.reusableData.TimeToReachTargetRotation.y - stateMachine.reusableData.DampedTargetRotationPassedTime.y);

            //Para que aumente y no sea 0 siempre
            stateMachine.reusableData.DampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            stateMachine.player.rb.MoveRotation(targetRotation);
        }

        #endregion

        #endregion
        protected float GetMovementSpeed(bool shouldConsiderSlopes = true)
        {
            float movementSpeed = movementData.baseSpeed * stateMachine.reusableData.movementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= stateMachine.reusableData.movementOnSlopesSpeedModifier;
            }

            return movementSpeed;
        }


        protected Vector3 GetPlayerHorizontalVelocity()
        {
            //Para acceder a la velocidad del rigidbody
            Vector3 playerHorizontalVelocity = stateMachine.player.rb.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }

        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3(0f, stateMachine.player.rb.velocity.y, 0f);
        }

        /// <summary>
        /// La direcci�n de movimiento
        /// </summary>
        /// <returns></returns>
        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(stateMachine.reusableData.movementInput.x, 0f, stateMachine.reusableData.movementInput.y);
        }

        /// <summary>
        /// Resetea la velocidad del rigidbody. De esta forma ser� un cambio instant�neo
        /// </summary>
        protected void ResetVelocity()
        {
            stateMachine.player.rb.velocity = Vector3.zero;
        }

        protected void ResetVerticalVelocity()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            stateMachine.player.rb.velocity = playerHorizontalVelocity;
        }
        protected virtual void AddInputActionsCallbacks()
        {
            stateMachine.player.input.playerActions.WalkToggle.started += OnWalkToggleStarted;
        }

        protected virtual void RemoveInputActionsCallbacks()
        {
            stateMachine.player.input.playerActions.WalkToggle.started -= OnWalkToggleStarted;
        }

        #region InputMethods

        /// <summary>
        /// Cambia entre caminar y correr
        /// </summary>
        /// <param name="context"></param>
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            stateMachine.reusableData.shouldWalk = !stateMachine.reusableData.shouldWalk;
        }

        /// <summary>
        /// Desacelera cuando dejamos de movernos
        /// </summary>
        protected void DecelerateHorizontally()
        {
            Vector3 playerHorizontalVelocity = GetPlayerHorizontalVelocity();

            //La desaceleraci�n ser� dependiente del tiempo, ya que se reducir� gardualmente.
            //Usamos Acceleration para acelerar en la direcci�n contraria. Por eso usamos -playerHorizontalVelocity. As� desacelera
            stateMachine.player.rb.AddForce(-playerHorizontalVelocity * stateMachine.reusableData.movementDecelerationForce, ForceMode.Acceleration);
        }

        protected void DecelerateVertically()
        {
            Vector3 playerVerticalVelocity = GetPlayerVerticalVelocity();

            stateMachine.player.rb.AddForce(-playerVerticalVelocity * stateMachine.reusableData.movementDecelerationForce, ForceMode.Acceleration);
        }

        //Sabremos si el jugador se mueve mirando la magnitud de su velocidad horizontal

        protected bool IsMovingHorizontally(float minimumMagnitude = 0.1f)
        {
            Vector3 playerHorizontaVelocity = GetPlayerHorizontalVelocity();

            //Para evitar obtener un valor y, ya que necesitamos un valor �nico que es la magnitud
            Vector2 playerHorizontalMovement = new Vector2(playerHorizontaVelocity.x, playerHorizontaVelocity.z);

            return playerHorizontalMovement.magnitude > minimumMagnitude;
        }

        protected bool IsMovingUp(float minimumVelocity = 0.1f)
        {
            return GetPlayerVerticalVelocity().y > minimumVelocity;
        }

        protected bool IsMovingDown(float minimumVelocity = 0.1f)
        {
            //As� podemos pasar una velocidad positiva como argumento mientras el m�todo comprueba si es negativo
            return GetPlayerVerticalVelocity().y < -minimumVelocity;
        }

        protected void SetBaseRotationData()
        {
            stateMachine.reusableData.rotationData = movementData.baseRotationData;
            stateMachine.reusableData.TimeToReachTargetRotation = stateMachine.reusableData.rotationData.targetRotationReachTime;
        }

        protected virtual void OnContactWithGround(Collider collider)
        {
        }

        protected virtual void OnItemPickUp(Collider collider)
        {
        }

        protected virtual void OnContactWithGroundExited(Collider collider)
        {
        }

        protected void OnPlayerDeath()
        {
            //Debug.Log(stateMachine.player.health);

            if (stateMachine.player.health <= 0.1f)
            {
                stateMachine.ChangeState(stateMachine.deadState);

                UIManager.instance.DiePanel();
            }
        }

        protected void StartAnimation(int animationHash)
        {
            stateMachine.player.animator.SetBool(animationHash, true);
        }

        protected void StopAnimation(int animationHash)
        {
            stateMachine.player.animator.SetBool(animationHash, false);
        }

        #endregion
    }
}
