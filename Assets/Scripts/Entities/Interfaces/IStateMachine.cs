using System;
using Random = UnityEngine.Random;



public interface IStateMachine
{
    #region PROPERTIES
    
    public StateType CurrentState { get; }
    public bool IsStateMachineRunning { get; }
    
    #endregion // PROPERTIES


    #region METHODS

    public void AddState(StateType stateType, IState state);
    public void FixedUpdate();
    public void RemoveState(StateType stateType);
    public void StartStateMachine();
    public void Update();
    public bool ValidateStateMachine();

    #endregion // METHODS
}