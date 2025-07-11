using UnityEngine;

public class WorkerStateMachineFactory : MonoBehaviour
{
    public WorkerStateMachine Create(Worker worker)
    {
        WorkerStateMachine stateMachine = new WorkerStateMachine();

        WaitState waitState = new WaitState(stateMachine, worker);
        MoveToResourceState moveToResourceState = new MoveToResourceState(stateMachine, worker);
        ReturnToStorageState returnToStorageState = new ReturnToStorageState(stateMachine, worker);

        ToWaitStateTransition toWaitStateTransition = new ToWaitStateTransition(waitState, worker);
        ToMoveToResourceTransition toMoveToResourceTransition = new ToMoveToResourceTransition(moveToResourceState, worker);
        ToReturnToStorageTransition toReturnToStorageTransition = new ToReturnToStorageTransition(returnToStorageState, worker);

        waitState.AddTransition(toMoveToResourceTransition);
        moveToResourceState.AddTransition(toReturnToStorageTransition);
        returnToStorageState.AddTransition(toWaitStateTransition);

        stateMachine.ChangeState(waitState);

        return stateMachine;
    }
}