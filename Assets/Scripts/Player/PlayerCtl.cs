using System;

using UnityEngine;
using UnityEngine.Events;

using Entities.ScriptableObjects;



[RequireComponent(
    typeof(HealthManager), typeof(PlayerCmdSys)
)]
public class PlayerCtl : CritterCtl
{
    #region INSPECTOR FIELDS

    [Header("Character")]
    [SerializeField]
    private int _starvationDamage = 1;
    
    #endregion // INSPECTOR FIELDS


    #region EVENT METHODS

    public void OnStomachContentsChange(int stomachContents)
    {
        if(stomachContents < 0)
        {
            if(this._debugLogging)
            {
                Debug.Log($"[{this.name}] takes starvation damage of [{this._starvationDamage}]");
            }
            
            this._healthManager.TakeDamage("Starvation", this._starvationDamage, true);
        }
    }
    
    #endregion // EVENT METHODS
}