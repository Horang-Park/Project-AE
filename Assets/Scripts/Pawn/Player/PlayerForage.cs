using GlobalData;
using Horang.HorangUnityLibrary.Utilities;
using Stage;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pawn.Player
{
    public class PlayerForage : MonoBehaviour
    {
        public InputActionAsset inputActions;

        private InputAction _interactAction;
        private PlayerMovement _playerMovement;

        private void OnEnable()
        {
            inputActions.FindActionMap("Player").Enable();
        }

        private void OnDisable()
        {
            inputActions.FindActionMap("Player").Disable();
        }

        private void Awake()
        {
            _interactAction = inputActions.FindAction("Interact");
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if (!_interactAction.WasPressedThisFrame())
            {
                return;
            }

            var direction = _playerMovement.moveDirection.normalized;

            if (Mathf.Abs(direction.x) < 1.0f && Mathf.Abs(direction.y) < 1.0f) // 대각선
            {
                return;
            }

            var layerMask = LayerMask.GetMask("Resource");
            var hit = Physics2D.Raycast(transform.position, direction, direction.magnitude, layerMask);

            if (!hit)
            {
                return;
            }

            var component = hit.collider.TryGetComponent(out BaseResource resource);

            if (!component)
            {
                return;
            }

            resource.RemoveResourceTile(hit.centroid, direction, Foraged);
        }

        private void Foraged(ResourceType resourceType)
        {
            Log.Print($"foraged resource type: {resourceType}");
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            var direction = _playerMovement.moveDirection.normalized;

            if (Mathf.Abs(direction.x) < 1.0f && Mathf.Abs(direction.y) < 1.0f)
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, direction);
        }
#endif
    }
}