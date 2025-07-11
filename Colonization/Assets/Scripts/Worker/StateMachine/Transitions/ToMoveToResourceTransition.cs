public class ToMoveToResourceTransition : Transition
{
    private Worker _worker;

    public ToMoveToResourceTransition(MoveToResourceState moveToResourceState, Worker worker) : base(moveToResourceState)
    {
        _worker = worker;
    }

    protected override bool CanTransit() => _worker.IsHandsFull == false && _worker.HaveTargetResource ;
}