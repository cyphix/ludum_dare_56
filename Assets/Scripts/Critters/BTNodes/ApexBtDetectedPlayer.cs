using System;

using UnityEngine;
using UnityEngine.AI;



public class ApexBtDetectedPlayer : BTNode
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private float _speed = 6f;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private EntityDetector _entityDetector;
        
    #endregion // INSPECTOR FIELDS


    #region UNITY METHODS
    private void Start()
    {
        if(this._entityDetector == null)
        {
            Debug.LogError($"{DebugUtils.GameObjectNamePretty(this.gameObject)} is missing {nameof(EntityDetector)}", this);
        }

        this._agent.speed = this._speed;
    }
    
    #endregion // UNITY METHODS
    
    
    #region METHODS
    
    public override BTNodeState Evaluate()
    {
        if(this._entityDetector.HasSpottedEntities)
        {
            Collider playerCollider = this._entityDetector.GetSpottedEntities[0];
            Vector3 direction = this.transform.position - playerCollider.transform.position;

            this._agent.SetDestination(playerCollider.transform.position);
            
            return BTNodeState.Running;
        }
        else
        {
            if(this.HasReachedDestination())
            {
                return BTNodeState.Success;
            }
            
            return BTNodeState.Running;
        }
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    bool HasReachedDestination()
    {
        if (!this._agent.pathPending && (this._agent.remainingDistance <= this._agent.stoppingDistance || this._agent.hasPath))
        {
            return true;
        }
        return false;
    }
    
    #endregion // INTERNAL METHODS
}