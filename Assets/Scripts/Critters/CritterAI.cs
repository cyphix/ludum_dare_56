using System;
using UnityEngine;
using UnityEngine.Serialization;



public class CritterAI : MonoBehaviour
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private BTNode _behaviorTree;
    
    [Header("Debug")]
    [SerializeField]
    protected bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS


    #region UNITY METHODS
    
    private void Start()
    {
        if(this._behaviorTree == null)
        {
            Debug.LogError($"{DebugUtils.GameObjectNamePretty(this.gameObject)} Missing {nameof(BTSelector)}");
            this.enabled = false;
            return;
        }
    }

    private void Update()
    {
        this._behaviorTree.Evaluate();
    }
    
    #endregion // UNITY METHODS
}