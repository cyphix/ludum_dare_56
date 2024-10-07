using System;
using System.Collections.Generic;
using UnityEngine;



public interface IEntityDetector
{
    #region PROPERTIES
    
    public List<Collider> GetSpottedEntities { get; }
    public bool HasSpottedEntities { get; }
    
    #endregion // PROPERTIES
}