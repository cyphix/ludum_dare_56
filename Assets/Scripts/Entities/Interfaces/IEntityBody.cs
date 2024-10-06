using System;

using UnityEngine;



public interface IEntityBody
{
    #region PROPERTIES
    
    public bool HasQueuedKnockback { get; }
    
    #endregion // PROPERTIES
    
    
    #region METHODS

    public void DoKnockback();
    public void Move(Vector2 direction, float speed);
    public void QueueKnockback(Vector3 direction, float force);
    public void UpdateFacing(Vector2 direction);
    public void ZeroMove();

    #endregion // METHODS
}