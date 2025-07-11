using System;
using UnityEngine;

[RequireComponent(typeof(WorkerMover), typeof(RandomPositioner), typeof(ResourcePicker))]
[RequireComponent(typeof(DistanceMeter), typeof(WorkerStateMachineFactory))]
public class Worker : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance;

    private RandomPositioner _randomPositioner;
    private Vector3 _currentBaseStorage;
    private Resource _currentTargetResource;
    private WorkerMover _mover;
    private WorkerStateMachine _machine;
    private Resource _resourceInHand;
    private DistanceMeter _distanceMeter;
    private ResourcePicker _resourcePicker;

    public event Action ResourceDelivered;

    public bool HaveTargetResource => _currentTargetResource != null;
    public bool IsHandsFull => _resourceInHand != null;

    private void Awake()
    {
        _machine = GetComponent<WorkerStateMachineFactory>().Create(this);
        _distanceMeter = GetComponent<DistanceMeter>();
        _randomPositioner = GetComponent<RandomPositioner>();
        _mover = GetComponent<WorkerMover>();
        _resourcePicker = GetComponent<ResourcePicker>();
    }

    private void Update()
    {
        _machine.Update();
    }

    public void SetStoragePosition(Vector3 position)
    {
        _currentBaseStorage = position;
    }

    public void SetResource(Resource resource)
    {
        _currentTargetResource = resource;
    }

    public void WaitForCommand()
    {
        if (_mover.Agent.hasPath == false && _randomPositioner.TryGetRandomPositionInRadius(_currentBaseStorage, out Vector3 position))
        {
            _mover.Move(position);
        }
    }

    public void MoveToResource()
    {
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentTargetResource.transform.position);

        if (sqrDistance < _pickUpDistance)
            _resourceInHand = _resourcePicker.PickUpResource(_currentTargetResource);
        else
            _mover.Move(_currentTargetResource.transform.position);
    }

    public void MoveToStorage()
    {
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentBaseStorage);

        if (sqrDistance < _pickUpDistance)
        {
            ResourceDelivered?.Invoke();
            _resourceInHand.CallDispawn();
            _currentTargetResource = null;
            _resourceInHand = null;
        }
        else
            _mover.Move(_currentBaseStorage);
    }
}