using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace GenshintImpact2
{
    //Cada vez que se a�ada el componente Player, se a�adir� PlayerInput
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [SerializeField] public PlayerSO data;
        [SerializeField] public PlayerCapsuleColliderUtility colliderUtility;
        [SerializeField] public PlayerLayerData layerData;

        [SerializeField] public PlayerAnimationData animationData;
        public Rigidbody rb { get; private set; }
        public Animator animator { get; private set; }
        //La camara principal, no el cinemachine. Porque Cinemachine controla la c�mara principal
        public Transform cameraTransform { get; private set; }

      

        public PlayerInput input { get; private set; }
        private PlayerMovementSM movementStateMachine;
        public static Player instance;

        public GameObject cameraContainer;
        [SerializeField] public CinemachineVirtualCamera thirdPersonCam;
        [SerializeField] public CinemachineVirtualCamera firstPersonCam;

        [SerializeField] public GameObject CDInstance;
        [SerializeField] public GameObject weaponShootPosition;
        [SerializeField] float bulletSpeed;

        // Health
        [SerializeField] public float health;
        [SerializeField] public float maxHealth;

        private void Awake()
        {
            instance = this;
            Time.timeScale = 1f;
            //this representa la clase del player, la cual est� en el constructor de PlayerMovementSM
            movementStateMachine = new PlayerMovementSM(this);
            input = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody>();
            cameraTransform = Camera.main.transform;
            animationData.Initialize();
            animator = GetComponentInChildren<Animator>();


            FindCamera();

            DataManager.instance.LoadData();

            weaponShootPosition = GetWeapon();


        }

        //Se activa al cambiar un valor
        private void OnValidate()
        {
            colliderUtility.Initialize(gameObject);
            //Establece los datos del collider al empezar a jugar
            colliderUtility.CalculateCapsuleColliderDimensions();
        }

        private void Start()
        {
            //Cuando empieza el juego est� en idle
            movementStateMachine.ChangeState(movementStateMachine.idlingState);
        }

        private void OnTriggerEnter(Collider collider)
        {
            movementStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            movementStateMachine.OnTriggerExit(collider);
        }
        private void Update()
        {
            //Primero se actualiza el input antes del update
            movementStateMachine.HandleInput();
            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }

        private void OnEnable()
        {
            CameraSwitch.Register(thirdPersonCam);
            CameraSwitch.Register(firstPersonCam);
            CameraSwitch.SwitchCamera(thirdPersonCam);
            //Debug.Log("ActiveCamera" + CameraSwitch.activeCamera);
        }

        private void OnDisable()
        {
            CameraSwitch.Unregister(thirdPersonCam);
            CameraSwitch.Unregister(firstPersonCam);
        }

        public void OnMovementStateAnimationEnterEvent()
        {
            movementStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
        }

        public void OnMovementStateAnimationTransitionEvent()
        {
            movementStateMachine.OnAnimationTransitionEvent();
        }

        private void FindCamera()
        {
            cameraContainer = GameObject.Find("CameraContainer");
            thirdPersonCam = cameraContainer.transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
            firstPersonCam = cameraContainer.transform.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        }

        public GameObject GetWeapon()
        {
            GameObject playerRig = transform.GetChild(0).gameObject;
            GameObject mixamoHigs = playerRig.transform.GetChild(1).gameObject;
            GameObject mixamoRigSpine = mixamoHigs.transform.GetChild(2).gameObject;
            GameObject mixamoRigSpine1 = mixamoRigSpine.transform.GetChild(0).gameObject;
            GameObject mixamoRigSpine2 = mixamoRigSpine1.transform.GetChild(0).gameObject;
            GameObject mixamoRigLeftShoulder = mixamoRigSpine2.transform.GetChild(0).gameObject;
            GameObject mixamoRigLeftArm = mixamoRigLeftShoulder.transform.GetChild(0).gameObject;
            GameObject mixamoRigLeftForeArm = mixamoRigLeftArm.transform.GetChild(0).gameObject;
            GameObject mixamoRigLeftHand = mixamoRigLeftForeArm.transform.GetChild(0).gameObject;
            GameObject weaponShootPoint = mixamoRigLeftHand.transform.GetChild(0).gameObject;

            return weaponShootPoint;
        }

        public void InstantiateShoot()
        {
            if (weaponShootPosition == null)
            {
                return;
            }

            Instantiate(CDInstance, weaponShootPosition.transform.position, Quaternion.identity);
            CDInstance.GetComponent<Rigidbody>().velocity = weaponShootPosition.transform.forward * -bulletSpeed;


        }

        // Health
        public void HandleDamage(float damageTaken)
        {
            if (health > 0)
            {
                health -= damageTaken;
                UIManager.instance.UpdateHealthBar(-damageTaken / maxHealth);

                if (health <= 0)
                {
                    health = 0;
                    // Death
                }
            }
        }

    }
}
