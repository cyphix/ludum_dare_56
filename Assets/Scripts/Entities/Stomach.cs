using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;



public class Stomach : MonoBehaviour, IStomach
{
    #region EVENTS

    public UnityEvent<float> DigestionTimerEvent;
    public UnityEvent<int> StomachContentsEvent;
    
    #endregion // EVENTS
    
    
    #region INSPECTOR FIELDS

    [SerializeField]
    private int _stomachContents = 0;
    [SerializeField]
    private int _maxCapacity = 10;
    [SerializeField]
    private bool _canDigest = true;
    [SerializeField]
    [Tooltip("The time in seconds for digesting a point of food.")]
    private float _digestionTick = 10f;

    [Header("Debug")]
    [SerializeField]
    private bool _debugLogging = false;

    #endregion // INSPECTOR FIELDS


    #region INTERNAL FIELDS

    private float _currentDigestionLeft;
    
    #endregion // INTERNAL FIELDS


    #region PROPERTIES

    public bool CanConsume { get; private set; } = true;
    public bool CanDigest
    {
        get { return this._canDigest; }
        set { this._canDigest = value; }
    }
    public bool IsStarving { get { return this._stomachContents < 0; } }
    
    public int StomachContents { get { return this._stomachContents; } }
    public int TotalDigested { get; private set; }
    
    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Start()
    {
        this.Initialize();
    }

    private void Update()
    {
        if(this.CanDigest)
        {
            this._currentDigestionLeft -= Time.deltaTime;

            if(this._currentDigestionLeft <= 0f)
            {
                this.Digest();

                this._currentDigestionLeft = this._digestionTick - this._currentDigestionLeft;
            }
            
            this.DigestionTimerEvent.Invoke(this._currentDigestionLeft);
        }
    }

    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    private void Initialize()
    {
        this._currentDigestionLeft = this._digestionTick;
<<<<<<< HEAD
        this.StomachContentsEvent.Invoke(this._stomachContents);
=======

        this.Consume(0);
>>>>>>> fb97b3136224fd58ec7e9739e6008b4ea455c17f
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region METHODS

    public void Consume(int foodValue)
    {
        if(!this.CanConsume) { return; }
        
        this._stomachContents = this.IsStarving ? foodValue : this._stomachContents + foodValue ;
<<<<<<< HEAD
        this.StomachContentsEvent.Invoke(this._stomachContents);
=======
        
        if(this._stomachContents >= this._maxCapacity)
        {
            this._stomachContents = this._maxCapacity;
            this.CanConsume = false;
            
            if(this._debugLogging)
            {
                Debug.Log($"[{this.name}] is full and can't consume.", this);
            }
        }
>>>>>>> fb97b3136224fd58ec7e9739e6008b4ea455c17f
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    private void Digest()
    {
        if(this.CanDigest)
        {
            if(this._stomachContents > 0)
            {
                this.TotalDigested++;
            }
            this._stomachContents--;

            if(this._stomachContents < this._maxCapacity)
            {
                this.CanConsume = true;

                if(this._debugLogging)
                {
                    Debug.Log($"[{this.name}] can consume again.", this);
                }
            }

            if(this._debugLogging)
            {
                Debug.Log($"Stomach contents digested, current total: [{this._stomachContents}]");
            }
            
            this.StomachContentsEvent.Invoke(this._stomachContents);
        }
        else
        {
            Debug.LogWarning($"[{this.name}] attempted to digest even though it is set off.", this);
        }
    }
    
    #endregion // INTERNAL METHODS
}