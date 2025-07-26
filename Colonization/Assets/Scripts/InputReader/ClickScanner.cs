using UnityEngine;

public class ClickScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _maxRaycastDistance = 100f;

    public void Scan(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out IClickable clickable))
        {
            clickable.OnClick();
        }
    }

    public bool TryGetMouseWorldPosition(Ray ray, out Vector3 position)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance, _groundLayer))
        {
            position = hit.point;

            return true;
        }

        position = Vector3.zero;

        return false;
    }
}