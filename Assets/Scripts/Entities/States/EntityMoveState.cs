﻿using System;
using UnityEngine;



[RequireComponent(typeof(EntityBody), typeof(Rigidbody))]
public class EntityMoveState : EntityState
{
    #region INSPECTOR FIELDS

    [Header("Settings Overrides")]
    [SerializeField]
    private float _moveSpeed = 3f;
    
    #endregion // INSPECTOR FIELDS
    
    
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
        
        if(this._entitySettings != null && !this._useOverrideSettings)
        {
            this._moveSpeed = this._entitySettings.MoveSpeed;
        }
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
        
        this.Type = StateType.Move;
    }
    
    #endregion // CONSTRUCTORS


    #region METHODS
    
    public override StateType CheckTransitions()
    {
        if(this._entityBody.HasQueuedKnockback)
        {
            return StateType.Knockback;
        }
        if(!this._cmdSystem.IsMoving())
        {
            return StateType.Idle;
        }
        
        return StateType.Null;
    }

    public override void Process()
    {
        if(this.IsActive)
        {
            this._entityBody.UpdateFacing(this._cmdSystem.Move);
        }
    }

    public override void ProcessFixed()
    {
        if(this.IsActive)
        {
            this._entityBody.Move(this._cmdSystem.Move, this._moveSpeed);
        }
    }
    #endregion // METHODS
}