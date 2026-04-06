using System;
using Models;
using UnityEngine;

namespace Stage
{
    public class TreeResource : BaseResource
    {
        public override void RemoveResourceTile(Vector2 contactPoint, Vector3 playerDirection, Action<ResourceType> onRemoved)
        {
            base.RemoveResourceTile(contactPoint, playerDirection, onRemoved);

            // todo: 아이템 뿌리기 기능 추가
        }
    }
}
