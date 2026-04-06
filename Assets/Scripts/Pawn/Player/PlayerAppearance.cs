using Models;
using Utilities;

namespace Pawn.Player
{
    public class PlayerAppearance : Appearance
    {
        private PlayerCharacterType _characterType;

        protected override void Awake()
        {
            base.Awake();

            _characterType = Models.Models.CurrentPlayerCharacterType;
        }

        protected override void LoadSpriteSheet()
        {
            const string specificSpriteFolderPath = "Sprites/Player Spritesheets";
            
            var currentCharacter = _characterType.ToDescription();
            var finalPath = $"{specificSpriteFolderPath}/{currentCharacter}/spritesheet";
            
            LoadAndSetSpriteSheets(finalPath);
        }
    }
}
