using System.Collections.Generic;
using UnityEngine;

public class ResourceScaner : MonoBehaviour
{
    [Header("Scan Settings")]
    [SerializeField] private float _scanDistance;
    [SerializeField] private LayerMask _layer;

    public bool Scan(Resource resourceSample, out List<Resource> results)
    {
        Collider[] tempResults = Physics.OverlapSphere(transform.position, _scanDistance, _layer.value);

        results = new List<Resource>();

        if (tempResults.Length > 0)
        {
            foreach (Collider collider in tempResults)
            {
                Resource currentResource = collider.GetComponent<Resource>();

                if (currentResource.GetType() == resourceSample.GetType())
                {
                    results.Add(currentResource);
                }
            }
        }

        return results.Count > 0;
    }

    public Resource Scan(Vector3 resourcePosition)
    {
        Collider[] tempResults = Physics.OverlapSphere(resourcePosition, _scanDistance, _layer.value);

        if (tempResults.Length > 0)
        {
            foreach(Collider collider in tempResults)
            {
                Resource resource = collider.GetComponent<Resource>();

                if (resource != null && collider.transform.position == resourcePosition)
                {
                    return resource;
                }
            }
        }

        return null;
    }
}