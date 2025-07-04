public class WorkerStateMachine
{
    private IState _state;
    private Worker _worker;
    private WorkerState _currentState;

    public WorkerState CurrentState => _currentState;

    public WorkerStateMachine(Worker worker)
    {
        _worker = worker;
    }

    public void SetState(WorkerState state)
    {
        switch (state)
        {
            case WorkerState.Wait:
                _state = new WaitState(_worker);
                _currentState = WorkerState.Wait; 
                break;
            case WorkerState.MoveToResource:
                _state = new MoveToResourceState(_worker);
                _currentState = WorkerState.MoveToResource;
                break;
            case WorkerState.ReturnToStorageState:
                _state = new ReturnToStorageState(_worker);
                _currentState = WorkerState.ReturnToStorageState;
                break;
        }
    }

    public void Update()
    {
        _state?.Update();
    }
}