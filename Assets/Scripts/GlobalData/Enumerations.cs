using System.ComponentModel;

namespace GlobalData
{
    public enum PlayerCharacterType
    {
        None,
        [Description("1_The Assassin")]    Assassin,
        [Description("2_The Warrior")]     Warrior,
        [Description("3_The Paladin")]     Paladin,
        [Description("4_The Sorceress")]   Sorceress,
        [Description("5_The Archer")]      Archer,
        [Description("6_The Fighter")]     Fighter,
    }

    public enum ResourceType
    {
        None,
        Tree,
        Stone,
    }
}