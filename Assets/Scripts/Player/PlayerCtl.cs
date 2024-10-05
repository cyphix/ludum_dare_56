using System;
using Unity.VisualScripting;
using UnityEngine;



[RequireComponent(typeof(PlayerCmdSys))]
public class PlayerCtl : MonoBehaviour
{
    #region INSPECTOR FIELDS

    [Header("State Machine")]
    [SerializeField]
    private StateType _startingState = StateType.Null;
    
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


    #region METHODS
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    #endregion // INTERNAL METHODS
}