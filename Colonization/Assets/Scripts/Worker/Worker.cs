using System;
using UnityEngine;

[RequireComponent(typeof(WorkerMover), typeof(RandomPositioner), typeof(ResourcePicker))]
[RequireComponent(typeof(DistanceMeter))]
public class Worker : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance;

    private RandomPositioner _randomPositioner;
    private Vector3 _currentBaseStorage;
    private Vector3 _currentResource;
    private WorkerMover _mover;
    private WorkerStateMachine _machine;
    private Resource _resourceInHand;
    private DistanceMeter _distanceMeter;
    private ResourcePicker _resourcePicker;

    public event Action ResourceDelivered;

    public WorkerState CurrentState => _machine.CurrentState;
    public bool IsHandsFull => _resourceInHand != null;

    private void Awake()
    {
        _distanceMeter = GetComponent<DistanceMeter>();
        _randomPositioner = GetComponent<RandomPositioner>();
        _machine = new WorkerStateMachine(this);
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

    public void SetResourcePosition(Vector3 position)
    {
        _currentResource = position;
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
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentResource);

        if (sqrDistance < _pickUpDistance)
            PickUpResource(_currentResource);
        else
            _mover.Move(_currentResource);
    }

    public void MoveToStorage()
    {
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentBaseStorage);

        if (sqrDistance < _pickUpDistance)
        {
            ResourceDelivered?.Invoke();
            _resourceInHand.CallDispawn();
            _resourceInHand = null;
        }
        else
            _mover.Move(_currentBaseStorage);
    }

    public void SetState(WorkerState state)
    {
        _machine.SetState(state);
    }

    private void PickUpResource(Vector3 position)
    {
        _resourceInHand = _resourcePicker.PickUpResource(position);
    }
}