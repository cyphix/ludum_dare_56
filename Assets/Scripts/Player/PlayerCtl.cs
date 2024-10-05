using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;



[RequireComponent(typeof(PlayerCmdSys))]
public class PlayerCtl : MonoBehaviour
{
    #region EVENTS

    public UnityEvent<int> HealthEvent;
    public UnityEvent<int> MaxHealthEvent;
    
    #endregion // EVENTS
    
    
    #region INSPECTOR FIELDS
    
    [Header("State Machine")]
    [SerializeField]
    private StateType _startingState = StateType.Null;

    [Header("Character")]
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private int _maxHealth = 5;

    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS

    private StateMachine _sm;
    
    // Cached References
    private ICmdSystem _cmdSystem;
    
    #endregion // INTERNAL FIELDS
    
    
    #region UNITY METHODS
    
    private void Awake()
    {
        this.Initialize();
    }

    public void Start()
    {
        this.Initialize();
        
        this._sm = new StateMachine(this._startingState);
        
        this.BuildStateMachine();

        if(this._sm.ValidateStateMachine())
        {
            this._sm.StartStateMachine();
        }
        
        this.MaxHealthEvent.Invoke(this._maxHealth);
        this.HealthEvent.Invoke(this._health);
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
            this._sm.AddState(state.Type, state);
        }
    }

    private void CacheReferences()
    {
        this._cmdSystem ??= GetComponent<ICmdSystem>();
    }

    private void Initialize()
    {
        this.CacheReferences();
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region EVENT METHODS

    public void OnHit(GameObject dmgSource, int dmg)
    {
        if(this._debugLogging)
        {
            Debug.Log($"Damage [{dmg}] from [{dmgSource.name}]");
        }
        
        this._health--;
        this.HealthEvent.Invoke(this._health);
    }
    
    #endregion // EVENT METHODS


    #region METHODS
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    #endregion // INTERNAL METHODS
}