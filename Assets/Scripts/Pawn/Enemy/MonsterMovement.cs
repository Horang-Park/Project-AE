using Horang.HorangUnityLibrary.Utilities;
using UnityEngine;

namespace Pawn.Enemy
{
    public class MonsterMovement : Movement
    {
        public float stopRange = 1.0f;

        private Transform _targetTransform;

        protected override void Awake()
        {
            base.Awake();

            _targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            var magnitude = (_targetTransform.position - transform.position).magnitude;

            Log.Print($"{magnitude}");

            if (magnitude <= stopRange)
            {
                MoveVector = Vector2.zero;

                return;
            }

            MoveVector = (_targetTransform.position - transform.position).normalized;
        }
    }
}
