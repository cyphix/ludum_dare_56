using System;

using Entities.ScriptableObjects;



public interface IState
{
    #region PROPERTIES
    
    public bool IsActive { get; }
    
    public StateType Type { get; }
    
    #endregion // PROPERTIES


    #region METHODS

    public StateType CheckTransitions();
    public void Enter();
    public void Exit();
    public void ProcessFixed();
    public void Process();
    public void SetSettings(EntitySettings settings);

    #endregion // METHODS
}