using UnityEngine;

namespace Pawn.Monster
{
    public class MonsterMovement : Movement
    {
        public float Speed
        {
            set => moveSpeed = value;
        }
        
        public float StopRange
        {
            set => _stopRange = value;
        }
        
        private float _stopRange = 1.0f;
        private Transform _targetTransform;

        protected override void Awake()
        {
            base.Awake();

            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            var magnitude = (_targetTransform.position - transform.position).magnitude;

            if (magnitude <= _stopRange)
            {
                MoveVector = Vector2.zero;

                return;
            }

            MoveVector = (_targetTransform.position - transform.position).normalized;
        }
    }
}
