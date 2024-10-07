using System;
using UnityEngine;



public interface IDamager
{
    #region PROPERTIES
    
    public int AttackDamage { get; }
    public bool CauseKnockback { get; }
    public float KnockbackForce { get; }
    
    public string SourceName { get; }
    public Vector3 Position { get; }
    public int ThreatLevel { get; }
    public ThreatType ThreatType { get; }
    
    #endregion // PROPERTIES
}