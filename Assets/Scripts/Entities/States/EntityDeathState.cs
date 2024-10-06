using System;using UnityEngine;



[RequireComponent(typeof(EntityBody), typeof(Rigidbody))]
public class EntityDeathState : EntityState
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private GameObject _corpsePrefab; 
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS
    
    // Cached References
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
        this._entityBody ??= GetComponent<IEntityBody>();
    }

    private void Initialize()
    {
        this.CacheReferences();
        
        this.Type = StateType.Death;
    }
    
    #endregion // CONSTRUCTORS


    #region METHODS
    public override StateType CheckTransitions()
    {
        return StateType.Null;
    }

    public override void Enter()
    {
        base.Enter();
        
        // Stop the movement
        this._entityBody.ZeroMove();
        
        this.CreateCorpse();
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS

    private void CreateCorpse()
    {
        if(this._corpsePrefab == null) { return; }

        GameObject prefab = Instantiate(
            this._corpsePrefab,
            this.transform.position,
            this.transform.rotation
        );

        if(this._debugLogging)
        {
            Debug.Log($"Created corpse [{prefab}] at [{prefab.transform.position}]");
        }
        
        Destroy(this.gameObject);
    }
    
    #endregion // INTERNAL METHODS
}