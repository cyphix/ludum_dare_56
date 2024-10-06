using System;
using UnityEngine;



public class CritterCmdSys : MonoBehaviour, ICmdSystem
{
    #region PROPERTIES
    
    public Vector2 Move { get; }
    public float XMove { get; }
    public float YMove { get; }
    
    #endregion // PROPERTIES


    #region METHODS
    
    public bool IsMoving()
    {
        // TODO
        return false;
    }

    public void Process()
    {
        // TODO
    }

    public void ProcessFixed()
    {
        // TODO
    }
    
    #endregion // METHODS
}