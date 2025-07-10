using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class WorkerMover : MonoBehaviour
{
    private NavMeshAgent _agent;

    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void Move(Vector3 destination)
    {
        _agent.SetDestination(destination);
    }
}