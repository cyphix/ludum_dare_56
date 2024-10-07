using System;

using UnityEngine;
using UnityEngine.AI;



public class ApexBtPatrol : BTNode
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private GameObject[] _patrolPoints;
    [SerializeField]
    private EntityDetector _entityDetector;
    [SerializeField]
    private NavMeshAgent _agent;
    
    [Header("Debug")]
    [SerializeField]
    protected bool _debugLogging = false;
        
    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS

    private int _currentPoint = -1;
    
    #endregion // INTERNAL FIELDS


    #region UNITY METHODS
    
    private void Start()
    {
        if(this._entityDetector == null)
        {
            Debug.LogError($"{DebugUtils.GameObjectNamePretty(this.gameObject)} is missing {nameof(IEntityDetector)}", this);
        }
        
        this._agent.speed = this._speed;
    }
    
    #endregion // UNITY METHODS
    
    
    #region METHODS
    
    public override BTNodeState Evaluate()
    {
        if(this._entityDetector.HasSpottedEntities)
        {
            return BTNodeState.Failure;
        }

        if(this._patrolPoints.Length > 0 && this.HasReachedDestination())
        {
            if(this._currentPoint >= this._patrolPoints.Length - 1)
            {
                this._currentPoint = 0;
            }
            else
            {
                this._currentPoint++;
            }

            this._agent.SetDestination(this._patrolPoints[this._currentPoint].transform.position);

            if(this._debugLogging)
            {
                Debug.Log($"{DebugUtils.GameObjectNamePretty(this.gameObject)} Patrol point: {this._agent.destination}");
            }
        }

        return BTNodeState.Running;
    }
    
    #endregion // METHODS
    
    
    #region INTERNAL METHODS
    
    bool HasReachedDestination()
    {
        if (!this._agent.pathPending && (this._agent.remainingDistance <= this._agent.stoppingDistance))
        {
            return true;
        }
        return false;
    }
    
    #endregion // INTERNAL METHODS
}