using System;

using UnityEngine;



[RequireComponent(typeof(EntityBody), typeof(Rigidbody))]
public class EntityKnockbackState : EntityState
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private float _lockStateTime = 0.1f;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS

    private float _timer = 0f;
    
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
        
        this.Type = StateType.Knockback;
    }
    
    #endregion // CONSTRUCTORS


    #region METHODS
    public override void Enter()
    {
        base.Enter();

        this._timer = 0f;
        
        this._entityBody.DoKnockback();
    }

    public override StateType CheckTransitions()
    {
        if(this._timer >= this._lockStateTime)
        {
            if(this._cmdSystem.IsMoving())
            {
                return StateType.Move;
            }

            return StateType.Idle;
        }
        
        return StateType.Null;
    }

    public override void Process()
    {
        if(this.IsActive)
        {
            this._timer += Time.deltaTime;
        }
    }
    #endregion // METHODS
}