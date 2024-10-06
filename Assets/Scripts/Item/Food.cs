using System;
using UnityEngine;



public class Food : MonoBehaviour
{
    #region INSPECTOR FIELDS

    [SerializeField]
    private int _foodValue = 1;
    [SerializeField]
    private float _rotTime = 15f;

    [Header("Debug")]
    [SerializeField]
    private bool _canRot = true;
    [SerializeField]
    private bool _debugLogging = false;

    #endregion // INSPECTOR FIELDS


    #region UNITY METHODS
    
    private void Start()
    {
        this.transform.localScale *= this._foodValue;
    }

    public void Update()
    {
        if(this._canRot)
        {
            this._rotTime -= Time.deltaTime;

            if(this._rotTime <= 0f)
            {
                this.Rot();
            }
        }
    }
    
    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    public void CreateFood(int foodValue=1, float rotTime=15f, bool canRot=true)
    {
        this._foodValue = foodValue;
        this._rotTime = rotTime;
        this._canRot = canRot;
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region EVENT METHODS
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<IEntityCtl>(out IEntityCtl entityCtl))
        {
            if(!entityCtl.CanConsume())
            {
                if(this._debugLogging)
                {
                    Debug.Log($"[{other.name}] cannot consume [{this.name}].", this);
                }
                return;
            }
            entityCtl.ConsumeFood(this._foodValue);
            
            if(this._debugLogging)
            {
                Debug.Log($"[{this.name}]: [{other.name}] consumed the food.");
            }
            
            Destroy(this.gameObject);
        }
    }
    
    #endregion // EVENT METHODS


    #region INTERNAL METHODS

    private void Rot()
    {
        Destroy(this.gameObject);
    }
    
    #endregion // INTERNAL METHODS
}