using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;



public class HealthManager : MonoBehaviour, IHealthManager
{
    #region EVENTS
    
    public UnityEvent<string> DeathEvent;
    public UnityEvent<int> HealthEvent;
    public UnityEvent<int> MaxHealthEvent;
    
    #endregion // EVENTS


    #region INSPECTOR FIELDS
    
    [SerializeField]
    private int _health = 5;
    [SerializeField]
    private int _maxHealth = 5;
    [SerializeField]
    private bool _hasDamageCooldown = true;
    [SerializeField]
    private float _invulnerableTime = 1f;
    
    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;
    
    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS

    private float _invulTimer = 0f;
    
    #endregion // INTERNAL FIELDS


    #region PROPERTIES
    
    public bool IsInvulnerable { get; private set; }
    
    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Start()
    {
        this.MaxHealthEvent.Invoke(this._maxHealth);
        this.HealthEvent.Invoke(this._health);
    }

    private void Update()
    {
        if(!this.IsInvulnerable) return;
        
        this._invulTimer += Time.deltaTime;

        if(this._invulTimer >= this._invulnerableTime)
        {
            this.StopInvulnerability();
        }
    }
    
    #endregion // UNITY METHODS
    
    
    #region METHODS
    
    public void TakeDamage(IDamager damager, bool invulIgnore = false, bool noInvul = true)
    {
        this.TakeDamage(damager.SourceName, damager.AttackDamage, invulIgnore);
    }

    public void TakeDamage(string damagerName, int amount, bool invulIgnore = false, bool noInvul = true)
    {
        if(this.IsInvulnerable && !invulIgnore) { return; }
        
        if(this._debugLogging)
        {
            Debug.Log($"{DebugUtils.GameObjectNamePretty(this.gameObject)} Damage [{amount}] from [{damagerName}]");
        }
        
        this._health -= amount;

        this.HealthEvent.Invoke(this._health);
        if(this._health <= 0)
        {
            if(this._debugLogging)
            {
                Debug.Log($"{DebugUtils.GameObjectNamePretty(this.gameObject)} has died.");
            }
            
            this.DeathEvent.Invoke(damagerName);
        }

        if(!noInvul)
        {
            this.StartInvulnerability();
        }
    }
    
    #endregion // METHODS


    #region INTERNAL FIELDS

    private void StopInvulnerability()
    {
        if(this._debugLogging)
        {
            Debug.Log($"[{this.name}] is no longer invulnerable.");
        }
        
        this.IsInvulnerable = false;
        this._invulTimer = 0f;
    }

    private void StartInvulnerability()
    {
        if(this._debugLogging)
        {
            Debug.Log($"{DebugUtils.GameObjectNamePretty(this.gameObject)} is invulnerable.");
        }
        
        if(!this._hasDamageCooldown) { return; }
        
        this.IsInvulnerable = true;
        this._invulTimer = 0f;
    }
    
    #endregion // INTERNAL FIELDS
}