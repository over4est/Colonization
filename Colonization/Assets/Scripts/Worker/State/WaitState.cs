public class WaitState : IState
{
    private Worker _worker;

    public WaitState(Worker worker)
    {
        _worker = worker;
    }

    public void Update()
    {
        _worker.WaitForCommand();
    }
}