using System;



public interface ICmdSystem
{
    #region PROPERTIES
    
    public float XMove { get; }
    public float YMove { get; }
    
    #endregion // PROPERTIES


    #region METHODS

    public bool IsMoving();

    #endregion // METHODS
}