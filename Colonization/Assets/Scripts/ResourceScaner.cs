using System.Collections.Generic;
using UnityEngine;

public class ResourceScaner : MonoBehaviour
{
    [Header("Scan Settings")]
    [SerializeField] private float _scanDistance;
    [SerializeField] private LayerMask _layer;

    public bool Scan(out List<Resource> results)
    {
        Collider[] tempResults = Physics.OverlapSphere(transform.position, _scanDistance, _layer.value);

        results = new List<Resource>();

        if (tempResults.Length > 0)
        {
            foreach (Collider collider in tempResults)
            {
                if (collider.TryGetComponent(out Resource currentResource))
                {
                    results.Add(currentResource);
                }
            }
        }

        return results.Count > 0;
    }
}