using System;



public interface IEntityCtl
{
    #region PROPERTIES
    
    #endregion // PROPERTIES
    
    
    #region METHODS

    public bool CanConsume();
    public void ConsumeFood(int foodValue);

    #endregion // METHODS
}