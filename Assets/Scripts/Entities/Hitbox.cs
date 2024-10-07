using System;
using UnityEngine;
using UnityEngine.Events;



public class Hitbox : MonoBehaviour
{
    #region EVENTS

    public UnityEvent<IDamager> HitEvent;
    
    #endregion // EVENTS
    
    
    #region INSPECTOR FIELDS
    
    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS
    
    // Cached References
    private TagHandle _hurtboxTag;
    
    #endregion // INTERNAL FIELDS


    #region UNITY METHODS
    
    private void OnEnable()
    {
        this._hurtboxTag = TagHandle.GetExistingTag("Hurtbox");
    }
    
    #endregion // UNITY METHODS
    
    
    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(this._hurtboxTag) && other.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            if(this._debugLogging)
            {
                Debug.Log($"Hitbox collided with [{other.name}].");
            }
            
            this.HitEvent.Invoke(hurtbox.Damager);
        }
    }
    
    #endregion // EVENT METHODS
}