using System;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerCmdSys : MonoBehaviour, ICmdSystem
{
    #region INTERNAL FIELDS

    private ISActions _actions;
    private InputAction _moveAction;
    
    #endregion // INTERNAL FIELDS


    #region PROPERTIES
    
    public float XMove { get; private set; }
    public float YMove { get; private set; }
    
    #endregion // PROPERTIES
    
    
    #region UNITY METHODS
    
    private void Awake()
    {
        this._actions = new ISActions();

        this._moveAction = this._actions.Player.Move;
    }

    private void OnEnable()
    {
        this._actions.Enable();
    }

    private void OnDisable()
    {
        this._actions.Disable();
    }
    
    #endregion // UNITY METHODS


    #region METHODS
    
    public bool IsMoving()
    {
        return Mathf.Approximately(this.XMove, 0f) || Mathf.Approximately(this.YMove, 0f);
    }

    public void ProcessFixed()
    {
        Vector2 input = this._moveAction.ReadValue<Vector2>();
        this.XMove = input.x;
        this.YMove = input.y;
    }
    
    #endregion // METHODS
}