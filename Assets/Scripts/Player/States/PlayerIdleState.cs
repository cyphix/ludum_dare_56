using System;using UnityEngine;



public class PlayerIdleState : MonoBehaviour, IState
{
    #region INTERNAL FIELDS
    
    // Cached References
    private ICmdSystem _cmdSystem;
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES
    
    public bool IsActive { get; private set; }

    public StateType Type { get; private set; }
    
    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Start()
    {
        this.Type = StateType.Idle;
    }
    
    #endregion // UNITY METHODS
    
    
    #region CONSTRUCTORS

    private void CacheReferences()
    {
        MonoUtils.CacheComponent(this, ref this._cmdSystem);
    }
    
    #endregion // CONSTRUCTORS


    #region METHODS
    
    public StateType CheckTransitions()
    {
        if(this._cmdSystem.IsMoving())
        {
            return StateType.Move;
        }
        
        return StateType.Null;
    }
    
    public void Enter()
    {
        this.IsActive = true;
        
        Debug.Log($"State Entered: [{nameof(PlayerIdleState)}]");
    }

    public void Exit()
    {
        this.IsActive = false;
        
        Debug.Log($"State Exited: [{nameof(PlayerIdleState)}]");
    }
    
    public void ProcessFixed()
    {
        throw new NotImplementedException();
    }

    public void Process()
    {
        throw new NotImplementedException();
    }
    
    #endregion // METHODS
}