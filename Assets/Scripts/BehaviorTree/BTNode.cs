using UnityEngine;



public abstract class BTNode : MonoBehaviour
{
    #region PROPERTIES
    
    public BTNodeState State { get; protected set; }
    
    #endregion // PROPERTIES


    #region METHODS

    public abstract BTNodeState Evaluate();

    #endregion // METHODS
}