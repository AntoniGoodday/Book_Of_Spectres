using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{

    public StateMachine(State startingState) => ChangeState(startingState);

    public State CurrentState { get; private set; }

    public State PreviousState { get; private set; }

    public void ChangeState(State state)
    {
        if (CurrentState != null)
        {
            CurrentState.stage = State.EVENT.EXIT;

            PreviousState = CurrentState;
        }

        CurrentState = state;

        CurrentState.stage = State.EVENT.ENTER;
    }

    public void StateMachineTick()
    {
        CurrentState.Process();
    }

    
}
