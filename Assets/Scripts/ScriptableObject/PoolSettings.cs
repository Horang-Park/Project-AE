using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "GameObjectPoolSettings", menuName = "Horang/Pool/Create GameObject Pool Settings", order = 0)]
    public class PoolSettings : UnityEngine.ScriptableObject
    {
        [System.Serializable]
        public class PoolSetting
        {
            public string poolName;
            public GameObject prefab;
            public int defaultCapacity = 10;
            public int maxSize = 100;
            public int preloadCount = 0;
        }

        public List<PoolSetting> poolSettings = new();
    }
}
