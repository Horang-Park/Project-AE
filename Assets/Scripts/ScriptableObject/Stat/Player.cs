using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObject.Stat
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Horang/Pawn/Create Player Stats", order = 0)]
    public class Player : UnityEngine.ScriptableObject
    {
        [System.Serializable]
        public class PlayerStat
        {
            public PlayerCharacterType characterType;
            public float maxHp;
            public float attackDamage;
            public float attackSpeed;
            public float moveSpeed;
        }

        public List<PlayerStat> playerStatList = new();
    }
}