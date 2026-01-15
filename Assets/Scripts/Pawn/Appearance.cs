using System.Collections.Generic;
using UnityEngine;

namespace Pawn
{
    public abstract class Appearance : MonoBehaviour
    {
        private static readonly int SpeedAnimatorKey = Animator.StringToHash("speed");
        private static readonly int OrientationAnimatorKey = Animator.StringToHash("orientation");

        protected Dictionary<string, Sprite> CurrentSpriteSheets;
        protected SpriteRenderer SpriteRenderer;

        private Animator _animator;
        private Vector2 _moveVector;
        private Vector2 _previousPosition;

        protected abstract void LoadSpriteSheet();

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
            SpriteRenderer = this.GetComponent<SpriteRenderer>();

            _previousPosition = transform.position;
        }

        private void Start()
        {
            LoadSpriteSheet();

            _animator.SetFloat(SpeedAnimatorKey, 0);
            _animator.SetInteger(OrientationAnimatorKey, 4);
        }

        private void FixedUpdate()
        {
            _moveVector.x = transform.position.x - _previousPosition.x;
            _moveVector.y = transform.position.y - _previousPosition.y;

            _previousPosition = transform.position;

            UpdateAnimation();
        }

        private void LateUpdate()
        {
            SpriteRenderer.sprite = CurrentSpriteSheets[SpriteRenderer.sprite.name];
        }

        private void UpdateAnimation()
        {
            _animator.SetFloat(SpeedAnimatorKey, Mathf.Abs(_moveVector.x) + Mathf.Abs(_moveVector.y));

            switch (_moveVector.x)
            {
                case > 0:
                    _animator.SetInteger(OrientationAnimatorKey, 6);
                    break;
                case < 0:
                    _animator.SetInteger(OrientationAnimatorKey, 2);
                    break;
            }

            switch (_moveVector.y)
            {
                case > 0:
                    _animator.SetInteger(OrientationAnimatorKey, 0);
                    break;
                case < 0:
                    _animator.SetInteger(OrientationAnimatorKey, 4);
                    break;
            }
        }
    }
}