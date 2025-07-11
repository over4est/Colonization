public class ToWaitStateTransition : Transition
{
    private Worker _worker;

    public ToWaitStateTransition(WaitState waitState, Worker worker) : base(waitState)
    {
        _worker = worker;
    }

    protected override bool CanTransit() => _worker.IsHandsFull == false;
}