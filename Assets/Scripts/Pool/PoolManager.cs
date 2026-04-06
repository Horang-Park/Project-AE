using System.Collections.Generic;
using System.Linq;
using Horang.HorangUnityLibrary.Foundation;
using Horang.HorangUnityLibrary.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace Pool
{
    public abstract class PoolManager : MonoSingleton<PoolManager>
    {
        [Header("Configuration")]
        [SerializeField] private PoolSettings _settings;
        [Header("Global Settings")]
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private bool organizeHierarchy = true;

        private readonly Dictionary<GameObject, ObjectPool<GameObject>> _pools = new();
        private readonly Dictionary<GameObject, Transform> _poolParents = new();

        protected override void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            foreach (var setting in _settings.poolSettings.Where(setting => setting.prefab != null))
            {
                CreatePool(setting.prefab, setting.defaultCapacity, setting.maxSize);
            
                if (setting.preloadCount > 0)
                {
                    WarmUpPool(setting.prefab, setting.preloadCount);
                }
            }
        }

        public GameObject GetFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (prefab == null)
            {
                Log.Print("Prefab is null!", LogPriority.Error);

                return null;
            }

            if (!_pools.ContainsKey(prefab))
            {
                CreatePool(prefab, 10, 100);
            }

            var obj = _pools[prefab].Get();
            obj.transform.position = position;
            obj.transform.rotation = rotation;

            return obj;
        }

        public GameObject GetFromPool(GameObject prefab)
        {
            return GetFromPool(prefab, Vector3.zero, Quaternion.identity);
        }

        public GameObject GetFromPool(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            var obj = GetFromPool(prefab, position, rotation);

            if (obj != null && parent != null)
            {
                obj.transform.SetParent(parent);
            }

            return obj;
        }

        public void ReturnToPool(GameObject prefab, GameObject obj)
        {
            if (prefab == null || obj == null)
            {
                Log.Print("Prefab or object is null!", LogPriority.Error);

                return;
            }

            if (_pools.TryGetValue(prefab, out var pool))
            {
                pool.Release(obj);
            }
            else
            {
                Log.Print($"No pool found for prefab: {prefab.name}. Destroying object instead.", LogPriority.Warning);

                Destroy(obj);
            }
        }

        public void CreatePool(GameObject prefab, int capacity, int maxPoolSize)
        {
            if (prefab == null)
            {
                return;
            }

            if (_pools.ContainsKey(prefab))
            {
                return;
            }

            Transform parent = null;

            if (organizeHierarchy)
            {
                parent = new GameObject($"Pool_{prefab.name}").transform;
                parent.SetParent(transform);
                _poolParents[prefab] = parent;
            }

            var pool = new ObjectPool<GameObject>(
                createFunc: () => OnCreatePoolItem(prefab, parent),
                actionOnGet: OnGetFromPool,
                actionOnRelease: (obj) => OnReturnToPool(obj, parent),
                actionOnDestroy: OnDestroyPoolObject,
                collectionCheck: collectionCheck,
                defaultCapacity: capacity,
                maxSize: maxPoolSize
            );

            _pools[prefab] = pool;
        }

        public void WarmUpPool(GameObject prefab, int count)
        {
            if (!_pools.ContainsKey(prefab))
            {
                Log.Print($"Pool for {prefab.name} doesn't exist. Creating it first.", LogPriority.Warning);

                CreatePool(prefab, count, count * 2);
            }

            var temp = new List<GameObject>();

            for (var i = 0; i < count; i++)
            {
                temp.Add(_pools[prefab].Get());
            }

            foreach (var obj in temp)
            {
                _pools[prefab].Release(obj);
            }
        }

        public void ReturnAllToPool(GameObject prefab)
        {
            if (!_poolParents.ContainsKey(prefab))
            {
                return;
            }

            var allObjects = FindObjectsByType<GameObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var activeObjects = (from obj in allObjects
                let pooledObj = obj.GetComponent<PooledGameObject>()
                    where pooledObj != null && pooledObj.Prefab == prefab && obj.activeInHierarchy select obj)
                .ToList();

            foreach (var obj in activeObjects)
            {
                ReturnToPool(prefab, obj);
            }
        }

        public void ClearPool(GameObject prefab)
        {
            if (!_pools.TryGetValue(prefab, out var pool))
            {
                return;
            }

            pool.Clear();

            _pools.Remove(prefab);

            if (!_poolParents.TryGetValue(prefab, out var parent))
            {
                return;
            }

            Destroy(parent.gameObject);

            _poolParents.Remove(prefab);
        }

        public void ClearAllPools()
        {
            foreach (var pool in _pools.Values)
            {
                pool.Clear();
            }
            _pools.Clear();

            foreach (var parent in _poolParents.Values.Where(parent => parent != null))
            {
                Destroy(parent.gameObject);
            }

            _poolParents.Clear();
        }

        public bool HasPool(GameObject prefab)
        {
            return _pools.ContainsKey(prefab);
        }

        #region Pool callbacks
        private GameObject OnCreatePoolItem(GameObject prefab, Transform parent)
        {
            var obj = Instantiate(prefab);
            obj.name = prefab.name;
        
            if (organizeHierarchy && parent != null)
            {
                obj.transform.SetParent(parent);
            }

            var pooledObject = obj.GetComponent<PooledGameObject>();

            if (pooledObject == null)
            {
                pooledObject = obj.AddComponent<PooledGameObject>();
            }

            pooledObject.Initialize(prefab, this);
        
            return obj;
        }

        private void OnGetFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReturnToPool(GameObject obj, Transform parent)
        {
            obj.SetActive(false);
        
            if (organizeHierarchy && parent != null)
            {
                obj.transform.SetParent(parent);
            }
        }

        private void OnDestroyPoolObject(GameObject obj)
        {
            Destroy(obj);
        }
        #endregion

        private void OnDestroy()
        {
            ClearAllPools();
        }
    }
}
