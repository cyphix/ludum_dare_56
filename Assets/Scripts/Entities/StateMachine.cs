using System;
using System.Collections.Generic;

using UnityEngine;



public class StateMachine : MonoBehaviour, IStateMachine
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private StateType _startingState;

    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS

    private IState _currentState;
    private  Dictionary<StateType, IState> _states;
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES
    
    public bool IsStateMachineRunning { get; private set; }

    public StateType CurrentState { get; private set; } = StateType.Null;
    
    #endregion // PROPERTIES


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


    #region CONSTRUCTOR METHODS

    private void Initialize()
    {
        if(this._states == null)
        {
            this._states = new Dictionary<StateType, IState>();
        }
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region METHODS
    
    public void AddState(StateType stateType, IState state)
    {
        if(!this._states.TryAdd(stateType, state))
        {
            Debug.LogWarning($"The state [{stateType}] already exist in the state dictionary.");
        }
        else if(this._debugLogging)
        {
            Debug.Log($"Added [{stateType}] state to the state dictionary.");
        }
    }
    
    public void ProcessFixed()
    {
        if(this.IsStateMachineRunning)
        {
            this.CheckTransitions();

            if(this.CurrentState != StateType.Null)
            {
                this._currentState.ProcessFixed();
            }
        }
    }
    
    public void RemoveState(StateType stateType)
    {
        if(!this._states.Remove(stateType))
        {
            Debug.LogWarning($"Failed to remove [{stateType}] from the state dictionary.");
            return;
        }

        if(this._debugLogging)
        {
            Debug.Log($"Removed [{stateType}] from the state dictionary.");
        }
    }

    public void StartStateMachine()
    {
        if(!this._states.ContainsKey(this._startingState))
        {
            Debug.LogError($"This state dictionary does not contain the starting state [{this._startingState}].");
            this.IsStateMachineRunning = false;
            this.enabled = false;
            return;
        }

        this.IsStateMachineRunning = true;
        
        this.Transition(this._startingState);
    }
    
    public void Process()
    {
        if(this.IsStateMachineRunning)
        {
            if(this.CurrentState != StateType.Null)
            {
                this._currentState.Process();
            }
        }
    }

    public bool ValidateStateMachine()
    {
        return (
            this._startingState != StateType.Null &&
            this._states.ContainsKey(this._startingState) &&
            this._states.Count > 0
        );
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS

    private void CheckTransitions()
    {
        if(this.CurrentState == StateType.Null) { return; }

        StateType nextState = this._currentState.CheckTransitions();

        if(nextState != StateType.Null)
        {
            this.Transition(nextState);
        }
    }

    private IState GetStateReference(StateType state)
    {
        if(this._states.TryGetValue(state, out IState stateRef)) return stateRef;
        
        Debug.LogError($"The state [{state}] does not exist in the state dictionary.");
        this.IsStateMachineRunning = false;

        return null;
    }

    private void Transition(StateType nextState)
    {
        if(this.CurrentState != StateType.Null)
        {
            this._currentState.Exit();
        }
        this._currentState = this.GetStateReference(nextState);
        this.CurrentState = nextState;
        this._currentState?.Enter();
    }
    
    #endregion // INTERNAL METHODS
}