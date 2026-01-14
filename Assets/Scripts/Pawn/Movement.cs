using UnityEngine;

namespace Pawn
{
    public abstract class Movement : MonoBehaviour
    {
        public float moveSpeed = 5f;

        protected Vector2 MoveVector;
        protected Rigidbody2D Rigidbody2D;

        protected virtual void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}