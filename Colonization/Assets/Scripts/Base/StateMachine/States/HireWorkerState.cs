public class HireWorkerState : State
{
    private Base _base;

    public HireWorkerState(IStateChanger stateChanger, Base @base) : base(stateChanger)
    {
        _base = @base;
    }

    protected override void OnUpdate() => _base.HireWorker();
}