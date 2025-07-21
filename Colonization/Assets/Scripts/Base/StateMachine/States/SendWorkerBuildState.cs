public class SendWorkerBuildState : State
{
    private Base _base;

    public SendWorkerBuildState(IStateChanger stateChanger, Base @base) : base(stateChanger)
    {
        _base = @base;
    }

    protected override void OnUpdate() => _base.SendWorkerToBuild();
}