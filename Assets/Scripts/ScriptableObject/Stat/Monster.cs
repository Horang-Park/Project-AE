using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObject.Stat
{
    [CreateAssetMenu(fileName = "MonsterStats", menuName = "Horang/Pawn/Create Monster Stats", order = 0)]
    public class Monster : UnityEngine.ScriptableObject
    {
        [System.Serializable]
        public class MonsterStat
        {
            public MonsterType monsterType;
            public float maxHp;
            public float attackDamage;
            public float attackSpeed;
            public float moveSpeed;
            public float stopRange;
        }

        public List<MonsterStat> monsterStatList = new();
    }
}