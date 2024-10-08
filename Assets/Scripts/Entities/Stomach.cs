﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;



public class Stomach : MonoBehaviour, IStomach
{
    #region EVENTS

    /// <summary>
    /// Invoked to track the digestion process over time.
    /// </summary>
    /// <remarks>
    /// This event takes two <c>float</c> parameters:
    /// <list type="bullet">
    /// <item>
    /// <description>The first parameter represents the time left of the digestion process tick.</description>
    /// </item>
    /// <item>
    /// <description>The second parameter represents the total time required for the digestion process tick.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public UnityEvent<float, float> DigestionTimerEvent;
    /// <summary>
    /// Invoked when the contents of the stomach changes.
    /// </summary>
    /// <remarks>
    /// This event takes two <c>int</c> parameters:
    /// <list type="bullet">
    /// <item>
    /// <description>The first parameter represents the contents in the stomach.</description>
    /// </item>
    /// <item>
    /// <description>The second parameter represents the max amount the stomach can hold.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public UnityEvent<int, int> StomachContentsEvent;
    
    public UnityEvent<int, int> GoalEvent;
    public UnityEvent<string> WinEvent;
    
    #endregion // EVENTS
    
    
    #region INSPECTOR FIELDS

    [SerializeField]
    private int _stomachContents = 0;
    [SerializeField]
    private int _maxCapacity = 10;
    [SerializeField]
    private bool _canDigest = true;
    [SerializeField]
    private bool _canStarve = true;
    [SerializeField]
    [Tooltip("The time in seconds for digesting a point of food.")]
    private float _digestionTick = 10f;
    
    [Header("Win Condition - Doesn't Belong Here")]
    [SerializeField]
    private int _winFoodAmount = 5;

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
    public int TotalDigested { get; private set; } = 0;
    
    #endregion // PROPERTIES


    #region UNITY METHODS
    
    private void Start()
    {
        this.Initialize();
        
        this.StomachContentsEvent.Invoke(this._stomachContents, this._maxCapacity);
        this.GoalEvent.Invoke(this.TotalDigested, this._winFoodAmount);
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
            
            this.DigestionTimerEvent.Invoke(this._currentDigestionLeft, this._digestionTick);
        }
    }

    #endregion // UNITY METHODS


    #region CONSTRUCTOR METHODS

    private void Initialize()
    {
        this._currentDigestionLeft = this._digestionTick;

        this.Consume(0);
    }
    
    #endregion // CONSTRUCTOR METHODS


    #region METHODS

    public void Consume(int foodValue)
    {
        if(!this.CanConsume) { return; }
        
        this._stomachContents = this.IsStarving ? foodValue : this._stomachContents + foodValue ;
        
        if(this._stomachContents >= this._maxCapacity)
        {
            this._stomachContents = this._maxCapacity;
            this.CanConsume = false;
            
            if(this._debugLogging)
            {
                Debug.Log($"[{this.name}] is full and can't consume.", this);
            }
        }
        
        this.StomachContentsEvent.Invoke(this._stomachContents, this._maxCapacity);
    }
    
    #endregion // METHODS


    #region INTERNAL METHODS
    
    private void Digest()
    {
        if(!this._canStarve && this._stomachContents <= 0) { return; }
        
        if(this.CanDigest)
        {
            if(this._stomachContents > 0)
            {
                this.TotalDigested++;
                
                this.UpdateGoal();
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
            
            this.StomachContentsEvent.Invoke(this._stomachContents, this._maxCapacity);
        }
        else
        {
            Debug.LogWarning($"[{this.name}] attempted to digest even though it is set off.", this);
        }
    }

    private void UpdateGoal()
    {
        this.GoalEvent.Invoke(this.TotalDigested, this._winFoodAmount);
        
        this.WinCheck();
    }

    private void WinCheck()
    {
        if(this.TotalDigested >= this._winFoodAmount)
        {
            if(this._debugLogging)
            {
                Debug.Log($"Win with [{this.TotalDigested}] food.", this);
            }
            
            this.WinEvent.Invoke($"you ate your fill of {this._winFoodAmount}!");
        }
    }
    
    #endregion // INTERNAL METHODS
}