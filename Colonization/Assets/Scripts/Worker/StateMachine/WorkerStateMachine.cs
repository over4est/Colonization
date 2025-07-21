public class WorkerStateMachine : IStateChanger
{
    private State _state;

    public State CurrentState => _state;

    public void ChangeState(State state)
    {
        _state = state;
    }

    public void Update()
    {
        _state?.Update();
    }
}