using UnityEngine;

public class AreaScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius;
    [SerializeField] private LayerMask _scanLayer;

    public bool IsAreaCorrect(Vector3 position)
    {
        Collider[] objects = Physics.OverlapSphere(position, _scanRadius, _scanLayer);

        return objects.Length == 0;
    }
}