using Terresquall;
using UnityEngine;
using Model;

namespace Controller
{
    public class PlayerControllerManager : MonoBehaviour
    {
        private VirtualJoystick movementJoyStick, fireJoyStick;

        public BoxCollider2D movementBounds;

        [Header("Character Setup")]
        public CharacterInfoBase characterInfo;
        public CharacterMovementController movement;
        public BulletFireController bulletFire;

        [Header("Prefabs")]
        public GameObject bulletPrefab;

        private Vector2 movementAxis;
        private Bounds bounds;

        private void Awake()
        {
            characterInfo.InitializePlayer();
        }

        // Start is called before the first frame update
        void Start()
        {
            movementJoyStick = UiManager.instance.movementJs;
            fireJoyStick = UiManager.instance.fireJs;

            Initialize();
        }

        private void OnDestroy()
        {
            if(movement != null)
            {
                movement.OnDestroy();
            }

            if(bulletFire != null)
            {
                bulletFire.OnDestroy();
            }
        }

        void Initialize()
        {
            movement = new CharacterMovementController();
            movement.Initialize(GetComponent<Rigidbody2D>(), characterInfo.Speed);

            bulletFire = new BulletFireController();
            bulletFire.Initialize(this.transform, bulletPrefab, 10.0f, fireJoyStick);

            if(movementBounds != null)
            {
                bounds = movementBounds.bounds;
            }
        }

        // Update is called once per frame
        void Update()
        {
            movementAxis = movementJoyStick.GetAxis();
            movement.MoveCharacter(movementAxis, bounds);

            bulletFire.Update(Time.deltaTime);
        }
    }
}
