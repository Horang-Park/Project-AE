using System;
using System.Linq;
using UnityEngine;

namespace Pawn.Enemy
{
    public class MonsterAppearance : Appearance
    {
        protected override void LoadSpriteSheet()
        {
            const string specificSpriteFolderPath = "Sprites/Monster Sprite Sheets";
            var sprites = Resources.LoadAll<Sprite>($"{specificSpriteFolderPath}/{gameObject.name}/spritesheet");

            if (sprites.Length < 1)
            {
                throw new NullReferenceException(
                    $"Could not load spritesheet -> {specificSpriteFolderPath}/{gameObject.name}/spritesheet");
            }

            CurrentSpriteSheets = sprites.ToDictionary(x => x.name, x => x);
            SpriteRenderer.sprite = sprites[0];
        }
    }
}
