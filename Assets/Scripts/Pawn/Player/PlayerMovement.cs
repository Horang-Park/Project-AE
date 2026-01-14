using UnityEngine;
using UnityEngine.InputSystem;

namespace Pawn.Player
{
    public class PlayerMovement : Movement
    {
        public InputActionAsset inputActions;

        private InputAction _moveAction;

        private void OnEnable()
        {
            inputActions.FindActionMap("Player").Enable();
        }

        private void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
        }

        protected override void Awake()
        {
            base.Awake();

            _moveAction = inputActions.FindAction("Move");
        }

        private void Update()
        {
            MoveVector =  _moveAction.ReadValue<Vector2>();
        }

        private void FixedUpdate()
        {
            Rigidbody2D.MovePosition(Rigidbody2D.position + MoveVector * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}