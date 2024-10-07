using System;

using UnityEngine;



public interface IEntityCtl
{
    #region PROPERTIES
    
    public string SourceName { get; }
    public Vector3 Position { get; }
    
    #endregion // PROPERTIES
    
    
    #region METHODS

    public bool CanConsume();
    public void ConsumeFood(int foodValue);

    #endregion // METHODS
}