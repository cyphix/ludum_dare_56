using System;

using UnityEngine;



public interface IEntityBody
{
    #region PROPERTIES
    
    #endregion // PROPERTIES


    #region METHODS

    public void Move(Vector2 direction, float speed);
    public void ZeroMove();

    #endregion // METHODS
}