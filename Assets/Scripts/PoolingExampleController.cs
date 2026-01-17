using Pool;
using UnityEngine;

public class PoolingExample : MonoBehaviour
{
    [Header("Pool Manager")]
    [SerializeField] private PoolManager poolManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject explosionPrefab;

    private void Update()
    {
        // 스페이스바로 총알 발사
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnBullet();
        }

        // E키로 폭발 효과
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnExplosion();
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = poolManager.GetFromPool(
            bulletPrefab, 
            transform.position, 
            transform.rotation
        );

        // 3초 후 자동 반환
        bullet.GetComponent<PooledGameObject>().ReturnToPool(3f);
    }

    private void SpawnExplosion()
    {
        GameObject explosion = poolManager.GetFromPool(
            explosionPrefab,
            transform.position + Vector3.up,
            Quaternion.identity
        );

        // 파티클 시스템이면 재생하고 자동 반환
        var ps = explosion.GetComponent<ParticleSystem>();
        if (ps != null)
        {
            ps.Play();
            explosion.GetComponent<PooledGameObject>().ReturnToPool(ps.main.duration);
        }
    }
}
