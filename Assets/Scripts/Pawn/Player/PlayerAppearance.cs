using System;
using System.Linq;
using GlobalData;
using UnityEngine;
using Utilities;

namespace Pawn.Player
{
    public class PlayerAppearance : Appearance
    {
        private PlayerCharacterType _characterType;

        protected override void Awake()
        {
            base.Awake();

            _characterType = Stores.CurrentPlayerCharacterType;
        }

        protected override void LoadSpriteSheet()
        {
            const string specificSpriteFolderPath = "Sprites/Player Sprite Sheets";
            var currentCharacter = _characterType.ToDescription();
            var sprites = Resources.LoadAll<Sprite>($"{specificSpriteFolderPath}/{currentCharacter}/spritesheet");

            if (sprites.Length < 1)
            {
                throw new NullReferenceException($"Could not load spritesheet -> {specificSpriteFolderPath}/{currentCharacter}/spritesheet.");
            }

            CurrentSpriteSheets = sprites.ToDictionary(x => x.name, x => x);
            SpriteRenderer.sprite = sprites[0];
        }
    }
}
