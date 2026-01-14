using System;
using System.Collections.Generic;
using System.Linq;
using GlobalData;
using UnityEngine;
using Utilities;

namespace Pawn.Player
{
    public class PlayerAppearance : MonoBehaviour
    {
        public PlayerCharacterTypes characterType =  PlayerCharacterTypes.None;

        private static readonly int SpeedAnimatorKey = Animator.StringToHash("speed");
        private static readonly int OrientationAnimatorKey = Animator.StringToHash("orientation");

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _moveVector;
        private Vector2 _previousPosition;
        private Dictionary<string, Sprite> _currentSpriteSheets;

        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
            _spriteRenderer = this.GetComponent<SpriteRenderer>();

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

        private void LoadSpriteSheet()
        {
                const string specificSpriteFolderPath = "Sprites/Player Sprite Sheets";
                var currentCharacter = characterType.ToDescription();
                var sprites = Resources.LoadAll<Sprite>($"{specificSpriteFolderPath}/{currentCharacter}/spritesheet");

                if (sprites.Length < 1)
                {
                    throw new NullReferenceException(
                        $"Could not load spritesheet -> {specificSpriteFolderPath}/{currentCharacter}/spritesheet");
                }

                _currentSpriteSheets = sprites.ToDictionary(x => x.name, x => x);
                _spriteRenderer.sprite = sprites[0];
        }
    }
}
