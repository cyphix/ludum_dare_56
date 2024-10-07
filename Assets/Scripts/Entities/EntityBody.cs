using System;

using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class EntityBody : MonoBehaviour, IEntityBody
{
    #region INSPECTOR FIELDS
    
    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region INTERNAL FIELDS

    private float _knockbackForce = 0f;
    private Vector3 _knockbackDirection;

    // Cached References
    private Rigidbody _rigidbody;
    
    #endregion // INTERNAL FIELDS


    #region PROPERTIES
    
    public bool HasQueuedKnockback { get; private set; }
    
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


    #region CONSTRUCTORS

    private void CacheReferences()
    {
        if(this._rigidbody == null)
        {
            this._rigidbody = GetComponent<Rigidbody>();
        }
    }

    private void Initialize()
    {
        this.CacheReferences();
    }
    
    #endregion // CONSTRUCTORS
    
    
    #region METHODS

    public void DoKnockback()
    {
        if(this._debugLogging)
        {
            Debug.Log($"[{this.name}] knockback executing: direction>[{this._knockbackDirection}], force>[{this._knockbackForce}]");
        }

        Vector3 kb = this._knockbackDirection * this._knockbackForce;
        kb.y = 0f;
        this._rigidbody.AddForce(kb, ForceMode.Impulse);

        this.HasQueuedKnockback = false;
    }

    public void Move(Vector2 direction, float speed)
    {
        Vector2 normalized = direction.normalized;

        this._rigidbody.linearVelocity = new Vector3(normalized.x, 0f, normalized.y) * speed;
    }
    
    public void QueueKnockback(Vector3 direction, float force)
    {
        this.HasQueuedKnockback = true;
        this._knockbackDirection = direction;
        this._knockbackForce = force;
    }
    
    public void UpdateFacing(Vector2 direction)
    {
        if(!(direction.sqrMagnitude > 0.01f)) return;
        
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            
        this._rigidbody.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    }

    public void ZeroMove()
    {
        this._rigidbody.linearVelocity = Vector3.zero;
    }
    
    #endregion // METHODS
}