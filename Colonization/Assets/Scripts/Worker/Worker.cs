using UnityEngine;

[RequireComponent(typeof(WorkerMover), typeof(RandomPositioner), typeof(ResourcePicker))]
[RequireComponent(typeof(DistanceMeter))]
public class Worker : MonoBehaviour
{
    [SerializeField] private float _pickUpDistance;

    private Flag _currentTargetFlag;
    private RandomPositioner _randomPositioner;
    private Vector3 _currentBaseStorage;
    private Resource _currentTargetResource;
    private WorkerMover _mover;
    private Resource _resourceInHand;
    private DistanceMeter _distanceMeter;
    private ResourcePicker _resourcePicker;

    public Vector3 CurrentBasePosition => _currentBaseStorage;
    public bool HaveResourceInHand => _resourceInHand != null;
    public bool HaveTargetFlag => _currentTargetFlag != null;
    public bool IsFree => _currentTargetFlag == null && _resourceInHand == null && _currentTargetResource == null;

    private void Awake()
    {
        _distanceMeter = GetComponent<DistanceMeter>();
        _randomPositioner = GetComponent<RandomPositioner>();
        _mover = GetComponent<WorkerMover>();
        _mover.enabled = false;
        _resourcePicker = GetComponent<ResourcePicker>();
    }

    private void Update()
    {
        if (_currentTargetFlag != null)
            MoveToFlag();
        else if (_currentTargetResource != null)
            MoveToResource();
        else if (_resourceInHand != null)
            MoveToStorage();
        else
            WaitForCommand();
    }

    public void SetTargetFlag(Flag flag) => _currentTargetFlag = flag;

    public void SetStoragePosition(Vector3 position) => _currentBaseStorage = position;

    public void SetResource(Resource resource) => _currentTargetResource = resource;

    public void FreeHands()
    {
        _resourceInHand.CallDispawn();
        _resourceInHand = null;
    }

    private void MoveToStorage() => _mover.Move(_currentBaseStorage);

    private void MoveToFlag() => _mover.Move(_currentTargetFlag.transform.position);

    private void WaitForCommand()
    {
        if (_mover.Agent.hasPath == false && _randomPositioner.TryGetRandomPositionInRadius(_currentBaseStorage, out Vector3 position))
            _mover.Move(position);
    }

    private void MoveToResource()
    {
        float sqrDistance = _distanceMeter.GetSqrDistance(_currentTargetResource.transform.position);

        if (sqrDistance <= _pickUpDistance)
        {
            _resourceInHand = _resourcePicker.PickUpResource(_currentTargetResource);
            _currentTargetResource = null;
        }
        else
            _mover.Move(_currentTargetResource.transform.position);
    }
}