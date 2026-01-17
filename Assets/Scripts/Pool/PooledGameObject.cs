using Horang.HorangUnityLibrary.Utilities;
using UnityEngine;

namespace Pool
{
    public class PooledGameObject : MonoBehaviour
    {
        private GameObject _prefab;
        private PoolManager _poolManager;

        public GameObject Prefab => _prefab;
        public PoolManager PoolManager => _poolManager;

        public void Initialize(GameObject prefab, PoolManager poolManager)
        {
            _prefab = prefab;
            _poolManager = poolManager;
        }

        public void ReturnToPool()
        {
            if (_prefab != null && _poolManager != null)
            {
                _poolManager.ReturnToPool(_prefab, gameObject);
            }
            else
            {
                Log.Print($"PooledObject on {gameObject.name} is not properly initialized. Destroying instead.", LogPriority.Warning);

                Destroy(gameObject);
            }
        }

        public void ReturnToPool(float delay)
        {
            if (delay <= 0)
            {
                ReturnToPool();
            }
            else
            {
                Invoke(nameof(ReturnToPool), delay);
            }
        }

        public void CancelScheduledReturn()
        {
            CancelInvoke(nameof(ReturnToPool));
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}
