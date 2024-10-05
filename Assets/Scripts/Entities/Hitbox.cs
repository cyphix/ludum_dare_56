using UnityEngine;
using UnityEngine.Events;



public class Hitbox : MonoBehaviour
{
    #region EVENTS

    public UnityEvent<GameObject, int> HitEvent;
    
    #endregion // EVENTS
    
    
    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
        {
            this.HitEvent.Invoke(other.gameObject, 1);
        }
    }
    
    #endregion // EVENT METHODS
}