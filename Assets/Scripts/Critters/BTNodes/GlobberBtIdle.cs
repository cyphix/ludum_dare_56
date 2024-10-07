using System;

using UnityEngine;
using UnityEngine.AI;



public class GlobberBtIdle : BTNode
{
    #region INSPECTOR FIELDS
    
    [SerializeField]
    private EntityDetector _entityDetector;
        
    #endregion // INSPECTOR FIELDS


    #region UNITY METHODS
    
    private void Start()
    {
        if(this._entityDetector == null)
        {
            Debug.LogError($"{DebugUtils.GameObjectNamePretty(this.gameObject)} is missing {nameof(IEntityDetector)}", this);
        }
    }
    
    #endregion // UNITY METHODS
    
    
    #region METHODS
    
    public override BTNodeState Evaluate()
    {
        if(this._entityDetector.HasSpottedEntities)
        {
            return BTNodeState.Failure;
        }

        return BTNodeState.Running;
    }
    
    #endregion // METHODS
}