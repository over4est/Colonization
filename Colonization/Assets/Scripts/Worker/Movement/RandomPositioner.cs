using UnityEngine;
using UnityEngine.AI;

public class RandomPositioner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _maxDistance;

    public bool TryGetRandomPositionInRadius(Vector3 basePosition ,out Vector3 position)
    {
        Vector3 randomPositionOnSphere = Random.onUnitSphere * _radius;

        position = new Vector3(basePosition.x + randomPositionOnSphere.x, basePosition.y, basePosition.z + randomPositionOnSphere.z);

        if (NavMesh.SamplePosition(position, out NavMeshHit hit, _maxDistance, NavMesh.AllAreas))
        {
            position = hit.position;

            return true;
        }

        return false;
    }
}