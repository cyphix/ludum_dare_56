using System;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class EntityDetector : MonoBehaviour, IEntityDetector
{
    #region INTERNAL FIELDS
    
    // Cached References
    private SphereCollider _collider;
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES

    public bool HasSpottedEntities { get; private set; } = false;

    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Start()
    {
        this.Initialize();
    }
    
    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    private void CacheReferences()
    {
        this._collider ??= GetComponent<SphereCollider>();
    }

    private void Initialize()
    {
        this.CacheReferences();
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Detect: [{other.name}]");
    }
    
    #endregion // EVENT METHODS
}