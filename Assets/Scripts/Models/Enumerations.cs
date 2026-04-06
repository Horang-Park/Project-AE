using System.ComponentModel;

namespace Models
{
    public enum PlayerCharacterType
    {
        None,
        [Description("1_The Guardian")]    Guardian,
        [Description("2_The Berserker")]   Berserker,
        [Description("3_The War Smith")]   WarSmith,
        [Description("4_The Reaper")]      Reaper,
        [Description("5_The Lancer")]      Lancer,
        [Description("6_The Gladiator")]   Gladiator,
    }

    public enum ResourceType
    {
        None,
        Tree,
        Stone,
    }
}