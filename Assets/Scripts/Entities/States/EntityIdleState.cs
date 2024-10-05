using System;using UnityEngine;



[RequireComponent(typeof(EntityBody), typeof(Rigidbody))]
public class EntityIdleState : EntityState
{
    #region INTERNAL FIELDS
    
    // Cached References
    private ICmdSystem _cmdSystem;
    private IEntityBody _entityBody;
    
    #endregion // INTERNAL FIELDS


    #region UNITY METHODS
    
    private void Awake()
    {
        this.Initialize();
    }

    private void Start()
    {
        this.Initialize();
    }
    
    #endregion // UNITY METHODS
    
    
    #region CONSTRUCTORS

    private void CacheReferences()
    {
        this._cmdSystem ??= GetComponent<ICmdSystem>();
        this._entityBody ??= GetComponent<IEntityBody>();
    }

    private void Initialize()
    {
        this.CacheReferences();
        
        this.Type = StateType.Idle;
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