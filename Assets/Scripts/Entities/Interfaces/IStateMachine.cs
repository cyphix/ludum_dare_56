using System;

using UnityEngine;



public interface IStateMachine
{
    #region PROPERTIES
    
    public bool IsStateMachineRunning { get; }
    
    public StateType CurrentState { get; }
    
    #endregion // PROPERTIES


    #region METHODS

    public void AddState(StateType stateType, IState state);
    public void ForceStateTransition(StateType stateType);
    public void ProcessFixed();
    public void RemoveState(StateType stateType);
    public void StartStateMachine();
    public void Process();
    public bool ValidateStateMachine();

    #endregion // METHODS
}