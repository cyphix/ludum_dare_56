using System;

using UnityEngine;



public interface ICmdSystem
{
    #region PROPERTIES

    public Vector2 Move { get; }
    public float XMove { get; }
    public float YMove { get; }
    
    #endregion // PROPERTIES


    #region METHODS

    public bool IsMoving();

    public void Process();
    public void ProcessFixed();

    #endregion // METHODS
}