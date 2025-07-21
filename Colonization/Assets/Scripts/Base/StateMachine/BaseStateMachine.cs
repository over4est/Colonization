public class BaseStateMachine : IStateChanger
{
    private State _state;

    public void ChangeState(State state)
    {
        _state = state;
    }

    public void Update()
    {
        _state?.Update();
    }
}