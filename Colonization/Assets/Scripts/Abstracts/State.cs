using System.Collections.Generic;

public abstract class State
{
    private IStateChanger _stateChanger;
    private List<Transition> _transitions = new List<Transition>();

    public State(IStateChanger stateChanger)
    {
        _stateChanger = stateChanger;
    }

    public void AddTransition(Transition transition)
    {
        if (transition == null)
            return;

        if (_transitions.Contains(transition))
            return;

        _transitions.Add(transition);
    }

    public void Update()
    {
        foreach (Transition transition in _transitions)
        {
            if (transition.TryTransit(out State nextState) == false)
                continue;

            _stateChanger.ChangeState(nextState);
            return;
        }

        OnUpdate();
    }

    protected virtual void OnUpdate() { }
}