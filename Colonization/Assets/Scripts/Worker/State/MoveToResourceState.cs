public class MoveToResourceState : IState
{
    private Worker _worker;

    public MoveToResourceState(Worker worker)
    {
        _worker = worker;
    }

    public void Update()
    {
        if (_worker.IsHandsFull)
        {
            _worker.SetState(WorkerState.ReturnToStorageState);

            return;
        }

        _worker.MoveToResource();
    }
}