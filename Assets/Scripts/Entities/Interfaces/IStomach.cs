using System;



public interface IStomach
{
    #region PROPERTIES
    
    public bool CanConsume { get; }
    public bool CanDigest { get; set; }
    public bool IsStarving { get; }
    
    public int StomachContents { get; }
    public int TotalDigested { get; }
    
    #endregion // PROPERTIES


    #region METHODS
    
    #endregion // METHODS
}