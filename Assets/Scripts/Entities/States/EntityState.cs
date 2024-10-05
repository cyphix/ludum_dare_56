using Entities.ScriptableObjects;
using UnityEngine;



public abstract class EntityState : MonoBehaviour, IState
{
    #region INSPECTOR FIELDS

    [SerializeField]
    protected EntitySettings _settings;

    [Header("Settings")]
    [SerializeField]
    protected bool _useOverrideSettings = false;
    [SerializeField]
    protected bool _debugLogs = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES
    
    public bool IsActive { get; private set; }

    public StateType Type { get; protected set; }
    
    #endregion // PROPERTIES


    #region METHODS

    public abstract StateType CheckTransitions();
    
    public virtual void Enter()
    {
        this.IsActive = true;
        
        if(this._debugLogs)
        {
            Debug.Log($"State Entered: [{this.Type}]");
        }
    }

    public virtual void Exit()
    {
        this.IsActive = false;
        
        if(this._debugLogs)
        {
            Debug.Log($"State Exited: [{this.Type}]");
        }
    }
    
    public virtual void ProcessFixed() { }

    public virtual void Process() { }
    
    #endregion // METHODS
}