using System;using UnityEngine;



[RequireComponent(typeof(EntityBody), typeof(Rigidbody))]
public class PlayerIdleState : PlayerState
{
    #region INTERNAL FIELDS
    
    // Cached References
    private ICmdSystem _cmdSystem;
    private IEntityBody _entityBody;
    
    #endregion // INTERNAL FIELDS


    #region UNITY METHODS
    
    private void Start()
    {
        this.CacheReferences();
        
        this.Type = StateType.Idle;
    }
    
    #endregion // UNITY METHODS
    
    
    #region CONSTRUCTORS

    private void CacheReferences()
    {
        this._cmdSystem = GetComponent<ICmdSystem>();
        this._entityBody = GetComponent<IEntityBody>();
    }
    
    #endregion // CONSTRUCTORS


    #region METHODS
    public override void Enter()
    {
        base.Enter();
        
        // Stop the movement
        this._entityBody.ZeroMove();
    }

    public override StateType CheckTransitions()
    {
        if(this._cmdSystem.IsMoving())
        {
            return StateType.Move;
        }
        
        return StateType.Null;
    }
    
    #endregion // METHODS
}