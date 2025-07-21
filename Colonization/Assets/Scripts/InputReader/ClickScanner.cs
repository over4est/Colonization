using UnityEngine;

public class ClickScanner : MonoBehaviour
{
    public void Scan(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out IClickable clickable))
        {
            clickable.OnClick();
        }
    }

    public bool TryGetMouseWorldPosition(Ray ray, out Vector3 position)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            position = hit.point;

            return true;
        }

        position = Vector3.zero;

        return false;
    }
}