public class WaitState : State
{
    private Worker _worker;

    public WaitState(IStateChanger stateChanger, Worker worker) : base(stateChanger)
    {
        _worker = worker;
    }

    protected override void OnUpdate() => _worker.WaitForCommand();
}