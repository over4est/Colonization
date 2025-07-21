public class ToBuildBaseTransition : Transition
{
    private Worker _worker;

    public ToBuildBaseTransition(BuildBaseState buildBaseState, Worker worker) : base(buildBaseState)
    {
        _worker = worker;
    }

    protected override bool CanTransit() => _worker.HaveTargetFlag;
}