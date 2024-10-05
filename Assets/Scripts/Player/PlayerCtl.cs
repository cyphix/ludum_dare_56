using System;
using UnityEngine;



public class PlayerCtl : MonoBehaviour
{
    #region INSPECTOR FIELDS

    [Header("State Machine")]
    [SerializeField]
    private StateType _startingState = StateType.Null;
    
    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS

    private StateMachine _sm;
    
    #endregion // INTERNAL FIELDS
    
    
    #region UNITY METHODS
    
    public void Start()
    {
        this._sm = new StateMachine(this._startingState);
        
        this.BuildStateMachine();
    }

    public void Update()
    {
        if(this._sm.IsStateMachineRunning)
        {
            this._sm.Update();
        }
    }

    public void FixedUpdate()
    {
        if(this._sm.IsStateMachineRunning)
        {
            this._sm.FixedUpdate();
        }
    }
    
    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    private void BuildStateMachine()
    {
        throw new NotImplementedException();
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region METHODS
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    #endregion // INTERNAL METHODS
}