using UnityEngine;

public class WorkerStateMachineFactory : MonoBehaviour
{
    public WorkerStateMachine Create(Worker worker)
    {
        WorkerStateMachine stateMachine = new WorkerStateMachine();

        WaitState waitState = new WaitState(stateMachine, worker);
        MoveToResourceState moveToResourceState = new MoveToResourceState(stateMachine, worker);
        ReturnToStorageState returnToStorageState = new ReturnToStorageState(stateMachine, worker);
        BuildBaseState buildBaseState = new BuildBaseState(stateMachine, worker);

        ToWaitStateTransition toWaitStateTransition = new ToWaitStateTransition(waitState, worker);
        ToMoveToResourceTransition toMoveToResourceTransition = new ToMoveToResourceTransition(moveToResourceState, worker);
        ToReturnToStorageTransition toReturnToStorageTransition = new ToReturnToStorageTransition(returnToStorageState, worker);
        ToBuildBaseTransition toBuildBaseTransition = new ToBuildBaseTransition(buildBaseState, worker);

        waitState.AddTransition(toBuildBaseTransition);
        waitState.AddTransition(toMoveToResourceTransition);
        moveToResourceState.AddTransition(toReturnToStorageTransition);
        returnToStorageState.AddTransition(toWaitStateTransition);
        buildBaseState.AddTransition(toWaitStateTransition);

        stateMachine.ChangeState(waitState);

        return stateMachine;
    }
}