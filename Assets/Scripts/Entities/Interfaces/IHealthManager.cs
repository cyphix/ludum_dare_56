using System;



public interface IHealthManager
{
    #region PROPERTIES
    
    public bool IsInvulnerable { get; }
    
    #endregion // PROPERTIES
    
    
    #region METHODS

    public void TakeDamage(IDamager damager, bool invulIgnore = false, bool noInvul = true);
    public void TakeDamage(string damagerName, int amount, bool invulIgnore = false, bool noInvul = true);

    #endregion // METHODS
}