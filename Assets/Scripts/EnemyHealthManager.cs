using UnityEngine;

namespace Pool
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        private PooledGameObject _pooledGameObject;

        private void Awake()
        {
            _pooledGameObject = GetComponent<PooledGameObject>();
        }

        private void OnEnable()
        {
            // 풀에서 꺼내질 때마다 초기화
            health = 100f;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
        
            if (health <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // 사망 처리 후 풀에 반환
            if (_pooledGameObject != null)
            {
                _pooledGameObject.ReturnToPool();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
