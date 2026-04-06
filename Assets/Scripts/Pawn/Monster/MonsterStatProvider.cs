using System.Collections.Generic;
using Models;
using Utilities;

namespace Pawn.Monster
{
    public class MonsterStatProvider
    {
        private readonly Dictionary<string, ScriptableObject.Stat.Monster.MonsterStat> _monsterStats = new();
        private ScriptableObject.Stat.Monster.MonsterStat _previousStat;

        public MonsterStatProvider(ScriptableObject.Stat.Monster monsterScriptableObject)
        {
            foreach (var monsterStat in monsterScriptableObject.monsterStatList)
            {
                _monsterStats.Add(monsterStat.monsterType.ToDescription(), monsterStat);
            }
        }

        public ScriptableObject.Stat.Monster.MonsterStat Get(MonsterType monsterType)
        {
            if (_previousStat is not null && monsterType.Equals(_previousStat.monsterType))
            {
                return _previousStat;
            }

            if (!_monsterStats.TryGetValue(monsterType.ToDescription(), out var stat))
            {
                return new ScriptableObject.Stat.Monster.MonsterStat();
            }

            _previousStat = stat;

            return stat;
        }
    }
}