using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pawn
{
    public abstract class Appearance : MonoBehaviour
    {
        private static readonly int SpeedAnimatorKey = Animator.StringToHash("speed");
        private static readonly int OrientationAnimatorKey = Animator.StringToHash("orientation");

        private Dictionary<string, Sprite> _currentSpriteSheets;
        private SpriteRenderer _spriteRenderer;
        
        private Animator _animator;
        private Vector2 _moveVector;
        private Vector2 _previousPosition;

        protected abstract void LoadSpriteSheet();

        protected virtual void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _animator = GetComponent<Animator>();

            _previousPosition = transform.position;
        }

        protected void LoadAndSetSpriteSheets(string loadFinalPath)
        {
            var sprites = Resources.LoadAll<Sprite>(loadFinalPath);

            if (sprites.Length < 1)
            {
                throw new NullReferenceException($"Could not load spritesheet -> {loadFinalPath}");
            }

            _currentSpriteSheets = sprites.ToDictionary(x => x.name, x => x);
            _spriteRenderer.sprite = sprites[0];
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
            _spriteRenderer.sprite = _currentSpriteSheets[_spriteRenderer.sprite.name];
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