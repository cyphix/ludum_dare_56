using System;

using UnityEngine;



[RequireComponent(typeof(Rigidbody))]
public class EntityBody : MonoBehaviour, IEntityBody
{
    #region INTERNAL FIELDS

    // Cached References
    private Rigidbody _rigidbody;
    
    #endregion // INTERNAL FIELDS


    #region UNITY METHODS
    private void Start()
    {
        this.CacheReferences();
    }
    #endregion // UNITY METHODS


    #region CONSTRUCTORS

    private void CacheReferences()
    {
        this._rigidbody = GetComponent<Rigidbody>();
    }
    
    #endregion // CONSTRUCTORS
    
    
    #region METHODS

    public void Move(Vector2 direction, float speed)
    {
        Vector2 normalized = direction.normalized;

        this._rigidbody.linearVelocity = new Vector3(normalized.x, 0f, normalized.y) * speed;
    }

    public void ZeroMove()
    {
        this._rigidbody.linearVelocity = Vector3.zero;
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    private void UpdateFacing(Vector3 direction)
    {
        throw new NotImplementedException();
    }
    
    #endregion // INTERNAL METHODS
}