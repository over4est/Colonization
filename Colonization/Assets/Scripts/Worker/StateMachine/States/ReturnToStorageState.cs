public class ReturnToStorageState : State
{
    private Worker _worker;

    public ReturnToStorageState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
    {
        _worker = worker;
    }

    protected override void OnUpdate() => _worker.MoveToStorage();

}