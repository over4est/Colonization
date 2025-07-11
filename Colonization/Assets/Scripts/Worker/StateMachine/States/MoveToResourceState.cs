public class MoveToResourceState : State
{
    private Worker _worker;

    public MoveToResourceState(IStateChanger stateChanger ,Worker worker) : base(stateChanger)
    {
        _worker = worker;
    }

    protected override void OnUpdate() => _worker.MoveToResource();
}