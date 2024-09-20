using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    private Vector3 _lastPosition;

    [SerializeField] private LayerMask placementLayerMask;

    public Vector3 GetSelectedMapposition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, placementLayerMask))
        {
            _lastPosition = hit.point;
        }

        return _lastPosition;
    }

    public bool IsOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
