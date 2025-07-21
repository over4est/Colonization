public class BuildBaseState : State
{
    private Worker _worker;

    public BuildBaseState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
    {
        _worker = worker;
    }

    protected override void OnUpdate() => _worker.MoveToFlag();
}