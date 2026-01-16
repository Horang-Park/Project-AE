using System;
using GlobalData;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stage
{
    public abstract class BaseResource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType = ResourceType.None;

        private Tilemap _tilemap;

        public virtual void RemoveResourceTile(Vector2 contactPoint, Vector3 playerDirection, Action<ResourceType> onRemoved)
        {
            var tilePosition = _tilemap.layoutGrid.WorldToCell(contactPoint);
            var targetTile = _tilemap.GetTile(tilePosition);

            if (!targetTile) // 지울 타일이 null이면 플레이어 진행 방향으로 삭제
            {
                tilePosition.x += (int)playerDirection.x;
                tilePosition.y += (int)playerDirection.y;
            }

            _tilemap.SetTile(tilePosition, null);

            onRemoved?.Invoke(resourceType);
        }

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }
    }
}