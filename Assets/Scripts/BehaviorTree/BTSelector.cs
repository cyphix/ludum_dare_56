using System;
using System.Collections.Generic;
using UnityEngine;



public class BTSelector : BTNode 
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private List<BTNode> _children;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region METHODS
    
    public override BTNodeState Evaluate()
    {
        foreach(BTNode btNode in this._children)
        {
            BTNodeState result = btNode.Evaluate();

            if(result != BTNodeState.Failure)
            {
                return result;
            }
        }

        return BTNodeState.Failure;
    }
    
    #endregion // METHODS
}