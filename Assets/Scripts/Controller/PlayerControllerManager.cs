using Terresquall;
using UnityEngine;
using Model;

namespace Controller
{
    public class PlayerControllerManager : MonoBehaviour
    {
        private VirtualJoystick movementJoyStick, fireJoyStick;

        [Header("Character Setup")]
        public CharacterInfoBase characterInfo;
        public CharacterMovement movement;

        private Vector2 movementAxis;

        // Start is called before the first frame update
        void Start()
        {
            movementJoyStick = UiManager.instance.movementJs;
            fireJoyStick = UiManager.instance.fireJs;

            Initialize();
        }

        void Initialize()
        {
            movement = new CharacterMovement();
            movement.Initialize(GetComponent<Rigidbody2D>(), characterInfo.Speed);
        }

        // Update is called once per frame
        void Update()
        {
            movementAxis = movementJoyStick.GetAxis();
            movement.MoveCharacter(movementAxis);
        }
    }
}
