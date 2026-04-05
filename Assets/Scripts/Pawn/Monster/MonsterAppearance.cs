namespace Pawn.Monster
{
    public class MonsterAppearance : Appearance
    {
        protected override void LoadSpriteSheet()
        {
            const string specificSpriteFolderPath = "Sprites/Monster Spritesheets";
            
            var finalPath = $"{specificSpriteFolderPath}/{gameObject.name}/spritesheet";
            
            LoadAndSetSpriteSheets(finalPath);
        }
    }
}
