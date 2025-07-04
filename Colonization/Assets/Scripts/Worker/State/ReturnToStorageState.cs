public class ReturnToStorageState : IState
{
    private Worker _worker;

    public ReturnToStorageState(Worker worker)
    {
        _worker = worker;
    }

    public void Update()
    {
        if (_worker.IsHandsFull == false)
        {
            _worker.SetState(WorkerState.Wait);

            return;
        }

        _worker.MoveToStorage();
    }
}