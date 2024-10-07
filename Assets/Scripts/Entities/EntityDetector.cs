using System;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class EntityDetector : MonoBehaviour, IEntityDetector
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private float _detectorRange = 2.5f;
    
    [Header("Debug")]
    [SerializeField]
    protected bool _debugLogging = false;
    [SerializeField]
    protected bool _debugVisuals = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS
    
    // Cached References
    private SphereCollider _collider;
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES

    public bool HasSpottedEntities { get; private set; } = false;

    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Awake()
    {
        this.Initialize();
    }
    
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

        if(this._collider != null)
        {
            this._collider.isTrigger = true;
            this._collider.radius = this._detectorRange;
        }
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Detect: [{other.name}]");
    }
    
    #endregion // EVENT METHODS


    #region GIZMOS METHODS
    
    private void OnDrawGizmos()
    {
        if(Application.isEditor && this._debugVisuals)
        {
            if(!Application.isPlaying)
            {
                this.CacheReferences();
            }
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(this._collider.bounds.center, this._collider.radius);
        }
    }
    
    #endregion // GIZMOS METHODS
}