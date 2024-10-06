using Entities.ScriptableObjects;
using UnityEngine;
using UnityEngine.Serialization;



public abstract class EntityState : MonoBehaviour, IState
{
    #region INSPECTOR FIELDS

    [Header("Settings")]
    [SerializeField]
    protected bool _useOverrideSettings = false;
    [SerializeField]
    protected bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS

    protected EntitySettings _entitySettings;
    
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
        
        if(this._debugLogging)
        {
            Debug.Log($"State Entered: [{this.Type}]");
        }
    }

    public virtual void Exit()
    {
        this.IsActive = false;
        
        if(this._debugLogging)
        {
            Debug.Log($"State Exited: [{this.Type}]");
        }
    }
    
    public virtual void ProcessFixed() { }

    public virtual void Process() { }

    public void SetSettings(EntitySettings settings)
    {
        this._entitySettings = settings;
    }
    
    #endregion // METHODS
}