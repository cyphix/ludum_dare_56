using System;



public interface IHealthManager
{
    #region PROPERTIES
    
    public bool IsInvulnerable { get; }
    
    #endregion // PROPERTIES
    
    
    #region METHODS

    public void TakeDamage(IDamager damager, bool invulIgnore = false);
    public void TakeDamage(string damagerName, int amount, bool invulIgnore = false);

    #endregion // METHODS
}