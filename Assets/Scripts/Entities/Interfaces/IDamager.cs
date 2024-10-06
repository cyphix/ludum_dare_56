using System;
using UnityEngine;



public interface IDamager
{
    #region PROPERTIES
    
    public int AttackDamage { get; }
    public bool CauseKnockback { get; }
    public string DamagerName { get; }
    public float KnockbackForce { get; }
    public Vector3 Position { get; }
    
    #endregion // PROPERTIES
}