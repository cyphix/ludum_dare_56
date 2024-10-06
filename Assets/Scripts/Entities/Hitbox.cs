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
    
    
    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
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