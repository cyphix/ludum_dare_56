using System;

using UnityEngine;
using UnityEngine.Events;

using Entities.ScriptableObjects;



[RequireComponent(
    typeof(HealthManager), typeof(PlayerCmdSys)
)]
public class PlayerCtl : MonoBehaviour, IDamager, IEntityCtl
{
    #region EVENTS

    public UnityEvent<int> ConsumeFoodEvent;
    
    #endregion // EVENTS
    
    
    #region INSPECTOR FIELDS

    [Header("Character")]
    [SerializeField]
    private int _starvationDamage = 1;
    [SerializeField]
    private int _attackDamage = 1;
    [SerializeField]
    private bool _causesKnockback = true;
    [SerializeField]
    private float _knockbackForce = 20f;
    
    [SerializeField]
    private EntitySettings _entitySettings;
    
    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS

    // Cached References
    private ICmdSystem _cmdSystem;
    private IEntityBody _entityBody;
    private IHealthManager _healthManager;
    private IStateMachine _sm;
    private IStomach _stomach;
    
    #endregion // INTERNAL FIELDS


    #region PROPERTIES
    
    public int AttackDamage { get { return this._attackDamage; } }
    public bool CauseKnockback { get { return this._causesKnockback; } }
    public string DamagerName { get { return this.name; } }
    public float KnockbackForce { get { return this._knockbackForce; } }
    public Vector3 Position { get { return this.transform.position; } }
    
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
        this._sm.ForceStateTransition(StateType.Death);
    }

    public void OnHit(IDamager damager)
    {
        if(this._debugLogging)
        {
            Debug.Log($"Hit [{damager.AttackDamage}] from [{damager.DamagerName}]");
        }

        if(this._healthManager.IsInvulnerable) { return; }

        if(damager.CauseKnockback)
        {
            Vector3 direction = (this.transform.position - damager.Position).normalized;
            this._entityBody.QueueKnockback(direction, damager.KnockbackForce);
        }
        
        this._healthManager.TakeDamage(damager);
    }

    public void OnStomachContentsChange(int stomachContents)
    {
        if(stomachContents < 0)
        {
            if(this._debugLogging)
            {
                Debug.Log($"[{this.name}] takes starvation damage of [{this._starvationDamage}]");
            }
            
            this._healthManager.TakeDamage("Starvation", this._starvationDamage, true);
        }
    }
    
    #endregion // EVENT METHODS


    #region METHODS

    public bool CanConsume()
    {
        if(this._stomach != null)
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
        if(this._debugLogging)
        {
            Debug.Log($"[{this.name}] consumed [{foodValue}] points of food.");
        }
        
        this.ConsumeFoodEvent.Invoke(foodValue);
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    #endregion // INTERNAL METHODS
}