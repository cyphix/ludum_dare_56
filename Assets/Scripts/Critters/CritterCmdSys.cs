using System;
using UnityEngine;



public class CritterCmdSys : MonoBehaviour, ICmdSystem
{
    #region PROPERTIES
    
    public Vector2 Move { get; } = Vector2.zero;
    public float XMove { get; } = 0f;
    public float YMove { get; } = 0f;
    
    #endregion // PROPERTIES


    #region METHODS
    
    public bool IsMoving()
    {
        // HACK
        return false;
    }

    public void Process()
    {
        // HACK
    }

    public void ProcessFixed()
    {
        // HACK
    }
    
    #endregion // METHODS
}