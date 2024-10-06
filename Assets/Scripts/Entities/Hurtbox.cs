using System;
using UnityEngine;



public class Hurtbox : MonoBehaviour
{
    #region INTERNAL FIELDS

    // Cached References
    private Collider _collider;
    private IDamager _damager;
    private GameObject _parentGo;
    
    #endregion // INTERNAL FIELDS


    #region PROPERTIES
    
    public IDamager Damager { get { return this._damager; } }
    
    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Awake()
    {
        this.Initialize();
    }

    private void Start()
    {
        this.Initialize();
    }
    
    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    private void CacheReferences()
    {
        if(this._parentGo == null)
        {
            Transform parentTransform = this.transform.parent;

            if(parentTransform != null)
            {
                if(parentTransform.gameObject.TryGetComponent<IDamager>(out IDamager damager))
                {
                    this._damager = damager;
                    this._parentGo = parentTransform.gameObject;
                }
                else
                {
                    Debug.LogError($"{nameof(Hurtbox)}'s parent GameObject does not have an IDamager.", this);
                    this.enabled = false;
                }
            }
            else
            {
                Debug.LogError($"{nameof(Hurtbox)} cannot be placed on a GameObject without a parent GameObject with IDamager.", this);
                this.enabled = false;
            }
        }

        this._collider ??= GetComponent<Collider>();
    }
    
    private void Initialize()
    {
        this.CacheReferences();

        if(this._collider != null)
        {
            this._collider.isTrigger = true;
        }
    }
    
    #endregion // CONSTRUCTOR METHODS
}