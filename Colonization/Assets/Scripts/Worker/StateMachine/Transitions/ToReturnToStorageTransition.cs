public class ToReturnToStorageTransition : Transition
{
    private Worker _worker;

    public ToReturnToStorageTransition(ReturnToStorageState returnToStorageState, Worker worker) : base(returnToStorageState)
    {
        _worker = worker;
    }

    protected override bool CanTransit() => _worker.IsHandsFull;
}