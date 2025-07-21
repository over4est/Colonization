using UnityEngine;

public class BaseStateMachineFactory : MonoBehaviour
{
    public BaseStateMachine Create(Base @base)
    {
        BaseStateMachine stateMachine = new BaseStateMachine();

        HireWorkerState hireWorkerState = new HireWorkerState(stateMachine, @base);
        SendWorkerBuildState sendWokerBuildState = new SendWorkerBuildState(stateMachine, @base);

        ToSendWorkerBuildTransition toBuildBaseTransition = new ToSendWorkerBuildTransition(sendWokerBuildState, @base);
        ToHireWorkerTransition toHireWorkerTransition = new ToHireWorkerTransition(hireWorkerState, @base);

        hireWorkerState.AddTransition(toBuildBaseTransition);
        sendWokerBuildState.AddTransition(toHireWorkerTransition);

        stateMachine.ChangeState(hireWorkerState);

        return stateMachine;
    }
}