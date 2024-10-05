using System;



public interface IState
{
    #region PROPERTIES
    
    public bool IsActive { get; }
    
    #endregion // PROPERTIES


    #region METHODS

    public void Enter();
    public void Exit();
    public StateType CheckTransitions();
    public void Update();
    public void FixedUpdate();

    #endregion // METHODS
}