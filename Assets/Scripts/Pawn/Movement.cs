using UnityEngine;

namespace Pawn
{
    public abstract class Movement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        protected Vector2 MoveVector;

        private Rigidbody2D _rigidbody2D;

        protected virtual void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + MoveVector * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}