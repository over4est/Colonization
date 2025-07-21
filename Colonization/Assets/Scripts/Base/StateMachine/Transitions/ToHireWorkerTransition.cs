public class ToHireWorkerTransition : Transition
{
    private Base _base;

    public ToHireWorkerTransition(HireWorkerState farmResourceState, Base @base) : base (farmResourceState)
    {
        _base = @base;
    }

    protected override bool CanTransit() => _base.IsFlagPlaced == false;
}