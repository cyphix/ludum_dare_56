using System;
using Entities.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;



[RequireComponent(
    typeof(HealthManager)
)]
public class CritterCtl : MonoBehaviour, IDamager, IEntityCtl
{
    #region EVENTS

    public UnityEvent<int> ConsumeFoodEvent;
    
    #endregion // EVENTS
    
    
    #region INSPECTOR FIELDS

    [Header("Temp - Needs to go to EntitySettings")]
    [SerializeField]
    protected int _attackDamage = 1;
    [SerializeField]
    protected bool _causesKnockback = true;
    [SerializeField]
    protected float _knockbackForce = 20f;
    [SerializeField]
    private int _threatLevel = 1;
    
    [Header("Settings")]
    [SerializeField]
    protected bool _needsFood = false;
    [SerializeField]
    protected EntitySettings _entitySettings;
    
    [Header("Debug")]
    [SerializeField]
    protected bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS

    // Cached References
    protected ICmdSystem _cmdSystem;
    protected IEntityBody _entityBody;
    protected IHealthManager _healthManager;
    protected IStateMachine _sm;
    protected IStomach _stomach;
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES
    
    // TODO
    public int AttackDamage { get { return this._attackDamage; } }
    public bool CauseKnockback { get { return this._causesKnockback; } }
    public float KnockbackForce { get { return this._knockbackForce; } }
    
    public Vector3 Position { get { return this.transform.position; } }
    public string SourceName { get { return this.name; } }
    public int ThreatLevel { get { return this._threatLevel; } }
    public ThreatType ThreatType { get { return ThreatType.Entity; } }
    
    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Awake()
    {
        this.Initialize();
    }

    public void Start()
    {
        this.Initialize();

        this.BuildStateMachine();
        if(this._sm.ValidateStateMachine())
        {
            this._sm.StartStateMachine();
        }
        else
        {
            Debug.LogError($"[{nameof(this._sm)}] failed to validate.");
        }
    }
    
    public void Update()
    {
        if(this._sm.IsStateMachineRunning)
        {
            this._sm.Process();
        }
    }

    public void FixedUpdate()
    {
        this._cmdSystem.ProcessFixed();
        
        if(this._sm.IsStateMachineRunning)
        {
            this._sm.ProcessFixed();
        }
    }
    
    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    private void BuildStateMachine()
    {
        IState[] states = this.GetComponents<IState>();

        foreach(IState state in states)
        {
            if(this._entitySettings != null)
            {
                state.SetSettings(this._entitySettings);
            }
            
            this._sm.AddState(state.Type, state);
        }
    }

    private void CacheReferences()
    {
        this._cmdSystem ??= GetComponent<ICmdSystem>();
        this._entityBody ??= GetComponent<IEntityBody>();
        this._healthManager ??= GetComponent<IHealthManager>();
        this._sm ??= GetComponent<IStateMachine>();
        this._stomach ??= GetComponent<IStomach>();
    }

    private void Initialize()
    {
        this.CacheReferences();
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region EVENT METHODS
    
    public void OnDeath(string damagerName)
    {
        if(this._debugLogging)
        {
            Debug.Log($"{DebugUtils.GameObjectNamePretty(this.gameObject)} Forcing Death state in StateMachine.");
        }
        
        this._sm.ForceStateTransition(StateType.Death);
    }
    
    public void OnHit(IDamager damager)
    {
        if(this._debugLogging)
        {
            Debug.Log($"Hit [{damager.AttackDamage}] from [{damager.SourceName}]");
        }

        if(this._healthManager.IsInvulnerable) { return; }

        if(damager.CauseKnockback)
        {
            Vector3 direction = (this.transform.position - damager.Position).normalized;
            this._entityBody.QueueKnockback(direction, damager.KnockbackForce);
        }
        
        this._healthManager.TakeDamage(damager);
    }
    
    #endregion // EVENT METHODS


    #region METHODS
    
    public bool CanConsume()
    {
        if(this._needsFood && this._stomach != null)
        {
            return this._stomach.CanConsume;
        }

        if(this._debugLogging)
        {
            Debug.LogWarning($"[{this.name}]'s Stomach component is missing!");
        }
        
        return false;
    }

    public void ConsumeFood(int foodValue)
    {
        if(!this._needsFood) { return; }
        
        if(this._debugLogging)
        {
            Debug.Log($"[{this.name}] consumed [{foodValue}] points of food.");
        }
        
        this.ConsumeFoodEvent.Invoke(foodValue);
    }
    
    #endregion // METHODS
}