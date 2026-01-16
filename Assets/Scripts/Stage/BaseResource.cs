using System;
using GlobalData;
using Horang.HorangUnityLibrary.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Stage
{
    public abstract class BaseResource : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType = ResourceType.None;

        private Tilemap _tilemap;

        public void RemoveResourceTile(Vector2 contactPoint, Action<ResourceType> onRemoved)
        {
            var tilePosition = _tilemap.layoutGrid.WorldToCell(contactPoint);
            var tp = _tilemap.WorldToCell(contactPoint);

            _tilemap.SetTile(tp, null);

            onRemoved?.Invoke(resourceType);
        }

        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
        }
    }
}