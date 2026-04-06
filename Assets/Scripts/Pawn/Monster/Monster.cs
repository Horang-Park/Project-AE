using Horang.HorangUnityLibrary.Utilities;
using Models;
using UnityEngine;

namespace Pawn.Monster
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private MonsterType monsterType;
        
        private MonsterAppearance _monsterAppearance;
        private MonsterMovement _monsterMovement;
        private MonsterStatProvider _monsterStatProvider;
        
        private void Awake()
        {
            _monsterAppearance = gameObject.AddComponent<MonsterAppearance>();
            _monsterMovement = gameObject.AddComponent<MonsterMovement>();

            var scriptable = Resources.Load<ScriptableObject.Stat.Monster>("ScriptableObjects/MonsterStats");

            if (scriptable == null)
            {
                Log.Print("Could not load monster stats!", LogPriority.Error);

                return;
            }
            
            _monsterStatProvider = new MonsterStatProvider(scriptable);
        }

        private void OnEnable()
        {
            _monsterMovement.Speed = _monsterStatProvider.Get(monsterType).moveSpeed;
            _monsterMovement.StopRange = _monsterStatProvider.Get(monsterType).stopRange;
        }
    }
}