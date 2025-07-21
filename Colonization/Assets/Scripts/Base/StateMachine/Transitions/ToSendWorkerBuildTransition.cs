public class ToSendWorkerBuildTransition : Transition
{
    private Base _base;

    public ToSendWorkerBuildTransition(SendWorkerBuildState buildBaseState, Base @base) : base(buildBaseState)
    {
        _base = @base;
    }

    protected override bool CanTransit() => _base.IsFlagPlaced == true;
}