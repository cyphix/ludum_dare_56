using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



[RequireComponent(typeof(SphereCollider))]
public class EntityDetector : MonoBehaviour, IEntityDetector
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private float _detectorRange = 2.5f;
    [SerializeField]
    private float _fovAngle = 60f;
    [SerializeField]
    private LayerMask _blockingMask;
    [SerializeField]
    private float _checkInterval = 0.2f;

    [Header("Sensor Options")]
    [SerializeField]
    private bool _canSpotFood = true;
    
    [Header("Debug")]
    [SerializeField]
    protected bool _debugLogging = false;
    [SerializeField]
    protected bool _debugVisuals = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS

    private List<Collider> _detectedList;
    private List<Collider> _fovList;
    private Queue<Collider> _addQueue;
    private Queue<Collider> _removeQueue;

    private bool _isScanning = false;
    
    // Cached References
    private SphereCollider _collider;
    
    // Tags
    private TagHandle _critterTag;
    private TagHandle _foodTag;
    private TagHandle _hazardTag;
    private TagHandle _playerTag;
    
    #endregion // INTERNAL FIELDS
    
    
    #region PROPERTIES

    public bool HasSpottedEntities { get; private set; } = false;

    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void OnEnable()
    {
        this.InitTagRefs();

        if(this._isScanning)
        {
            StartCoroutine(this.CheckIfDetectedInFOV());
        }
    }

    private void Awake()
    {
        this.Initialize();
    }
    
    private void Start()
    {
        this.Initialize();

        this._detectedList = new List<Collider>();
        this._fovList = new List<Collider>();
        this._addQueue = new Queue<Collider>();
        this._removeQueue = new Queue<Collider>();

        this._isScanning = true;
        StartCoroutine(this.CheckIfDetectedInFOV());
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

    private void InitTagRefs()
    {
        this._critterTag = TagHandle.GetExistingTag("Critter");
        this._foodTag = TagHandle.GetExistingTag("Food");
        this._hazardTag = TagHandle.GetExistingTag("Hazard");
        this._playerTag = TagHandle.GetExistingTag("Player");
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        if(this.Detectable(other))
        {
            this._addQueue.Enqueue(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(this.Detectable(other))
        {
            this._removeQueue.Enqueue(other);
        }
    }
    
    #endregion // EVENT METHODS


    #region COROUTINES

    private IEnumerator CheckIfDetectedInFOV()
    {
        while(this.enabled)
        {
            while(this._addQueue.Count > 0)
            {
                Collider other = this._addQueue.Dequeue();
                if(!this._detectedList.Contains(other))
                {
                    this._detectedList.Add(other);
                }
            }
            
            while(this._removeQueue.Count > 0)
            {
                Collider other = this._removeQueue.Dequeue();
                this._detectedList.Remove(other);
            }

            this._fovList.Clear();
            foreach(Collider other in this._detectedList)
            {
                if(other == null) { continue; }
                
                if(this.CheckInFOV(other))
                {
                    this._fovList.Add(other);
                }
            }
            
            if(this._debugLogging)
            {
                Debug.Log($"Detected in FOV Count: [{this._fovList.Count}]");
            }
            
            yield return new WaitForSeconds(this._checkInterval);
        }
    }
    
    #endregion // COROUTINES


    #region INTERNAL METHODS

    private bool CheckInFOV(Collider other)
    {
        Vector3 detectorPos = this.transform.position;
        Vector3 otherPos = other.transform.position;

        Vector3 directionToTarget = (otherPos - detectorPos).normalized;
        float distanceToTarget = Vector3.Distance(detectorPos, otherPos);
        float angleToTarget = Vector3.Angle(this.transform.forward, directionToTarget);

        if(!IsInFOV(
            this._fovAngle, angleToTarget,
            directionToTarget, this._detectorRange
        ))
        { return false; }

        if(Physics.Raycast(
               detectorPos, directionToTarget,
               distanceToTarget, this._blockingMask
           ))
        {
            if(this._debugVisuals)
            {
                Debug.DrawLine(detectorPos, otherPos, Color.red, 0.1f);
            }

            return false;
        }

        if(this._debugVisuals)
        {
            Debug.DrawLine(detectorPos, otherPos, Color.green, 0.1f);
        }

        return true;
    }

    private bool Detectable(Collider other)
    {
        return (
            (this._canSpotFood && other.CompareTag(this._foodTag)) ||
            other.CompareTag(this._critterTag) ||
            other.CompareTag(this._playerTag)
        );
    }

    #endregion // INTERNAL METHODS


    #region STATIC METHODS
    
    private static bool IsInFOV(float viewAngle, float angleToTarget, Vector3 directionToTarget, float viewDistance)
    {
        return (
            angleToTarget < (viewAngle / 2) &&
            directionToTarget.magnitude < viewDistance
        );
    }
    
    #endregion // STATIC METHODS


    #region GIZMOS METHODS
    
    private void OnDrawGizmos()
    {
        if(Application.isEditor && this._debugVisuals)
        {
            // Detection sphere
            this._collider ??= GetComponent<SphereCollider>();
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(this._collider.bounds.center, this._collider.radius);
            
            // FOV cone
            Gizmos.color = Color.red;

            Vector3 leftBoundary = Quaternion.Euler(0, -this._fovAngle / 2, 0) * transform.forward;
            Vector3 rightBoundary = Quaternion.Euler(0, this._fovAngle / 2, 0) * transform.forward;

            Gizmos.DrawRay(transform.position, leftBoundary * this._detectorRange);
            Gizmos.DrawRay(transform.position, rightBoundary * this._detectorRange);
        }
    }
    
    #endregion // GIZMOS METHODS
}