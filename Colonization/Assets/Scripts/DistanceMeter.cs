using UnityEngine;
using UnityEngine.AI;

public class DistanceMeter : MonoBehaviour
{
    [SerializeField] private Transform _startMeasurePosition;

    public float MeasureDistance(Resource resource)
    {
        float distance = 0f;
        NavMeshPath path = new NavMeshPath();

        NavMesh.CalculatePath(_startMeasurePosition.position, resource.transform.position, NavMesh.AllAreas, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                distance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
        }

        return distance;
    }

    public float GetSqrDistance(Vector3 target)
    {
        return (target - transform.position).sqrMagnitude;
    }
}