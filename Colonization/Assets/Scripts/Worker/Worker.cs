using System;
using UnityEngine;

[RequireComponent(typeof(WorkerMover), typeof(RandomPositioner), typeof(ResourcePicker))]
[RequireComponent(typeof(DistanceMeter), typeof(WorkerStateMachineFactory))]
public class Worker : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance;

    private Flag _currentTargetFlag;
    private RandomPositioner _randomPositioner;
    private Vector3 _currentBaseStorage;
    private Resource _currentTargetResource;
    private WorkerMover _mover;
    private WorkerStateMachine _machine;
    private Resource _resourceInHand;
    private DistanceMeter _distanceMeter;
    private ResourcePicker _resourcePicker;

    public event Action ResourceDelivered;
    public event Action<Vector3, Worker> FlagReached;

    public bool IsFree => _machine.CurrentState is WaitState;
    public bool HaveTargetFlag => _currentTargetFlag != null;
    public bool HaveTargetResource => _currentTargetResource != null;
    public bool IsHandsFull => _resourceInHand != null;

    private void Awake()
    {
        _machine = GetComponent<WorkerStateMachineFactory>().Create(this);
        _distanceMeter = GetComponent<DistanceMeter>();
        _randomPositioner = GetComponent<RandomPositioner>();
        _mover = GetComponent<WorkerMover>();
        _mover.enabled = false;
        _resourcePicker = GetComponent<ResourcePicker>();
    }

    private void Update()
    {
        _machine.Update();
    }

    public void MoveToFlag()
    {
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentTargetFlag.transform.position);

        if (sqrDistance <= _pickUpDistance)
        {
            FlagReached?.Invoke(_currentTargetFlag.transform.position, this);
            _currentTargetFlag.CallDisable();

            _currentTargetFlag = null;
        }
        else
            _mover.Move(_currentTargetFlag.transform.position);
    }

    public void WaitForCommand()
    {
        if (_mover.Agent.hasPath == false && _randomPositioner.TryGetRandomPositionInRadius(_currentBaseStorage, out Vector3 position))
            _mover.Move(position);
    }

    public void MoveToResource()
    {
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentTargetResource.transform.position);

        if (sqrDistance <= _pickUpDistance)
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
            _resourceInHand = null;
            _currentTargetResource = null;
        }
        else
            _mover.Move(_currentBaseStorage);
    }

    public void SetTargetFlag(Flag flag) => _currentTargetFlag = flag;

    public void SetStoragePosition(Vector3 position) => _currentBaseStorage = position;

    public void SetResource(Resource resource) => _currentTargetResource = resource;
}