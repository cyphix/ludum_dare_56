using UnityEngine;



public class Hazard : MonoBehaviour, IDamager
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private int _threatLevel = 1;

    [Header("Move to HazardSettings")]
    [SerializeField]
    private int _damage = 1;
    [SerializeField]
    private bool _causesKnockback = true;
    [SerializeField]
    private float _knockbackForce = 20f;
    
    #endregion // INSPECTOR FIELDS
    
    
    #region PROPERTIES
    
    public int AttackDamage { get { return this._damage; } }
    public bool CauseKnockback { get { return this._causesKnockback; } }
    public float KnockbackForce { get { return this._knockbackForce; } }
    
    public Vector3 Position { get { return this.transform.position; } }
    public string SourceName { get { return this.name; } }
    public int ThreatLevel { get { return this._threatLevel; } }
    public ThreatType ThreatType { get { return ThreatType.Hazard; } }
    
    #endregion // PROPERTIES
}