using UnityEngine;



public abstract class BTNode : MonoBehaviour
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private BTNodeState _state;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region PROPERTIES
    
    public BTNodeState State { get { return this._state; } }
    
    #endregion // PROPERTIES


    #region METHODS

    public abstract BTNodeState Evaluate();

    #endregion // METHODS
}