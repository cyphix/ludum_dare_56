using UnityEngine;



public class Hazard : MonoBehaviour, IDamager
{
    #region INSPECTOR FIELDS

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
    public string DamagerName { get { return this.name; } }
    public float KnockbackForce { get { return this._knockbackForce; } }
    public Vector3 Position { get { return this.transform.position; } }
    
    #endregion // PROPERTIES
}