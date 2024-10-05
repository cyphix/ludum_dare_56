using System;

using UnityEngine;



public static class MonoUtils
{
    /// <summary>
    /// Caches a component on a <see cref="GameObject"/> with minimal checks.
    /// If the reference already holds a value, the method returns <c>true</c> 
    /// without searching for the component again.
    /// </summary>
    /// <remarks>
    /// This method is intended to fail gracefully. If the component cannot be 
    /// found, the calling object will deactivate to prevent further errors. 
    /// For more control, consider using
    /// <see cref="TryCacheComponent{T}(MonoBehaviour, ref T)" />.
    /// </remarks>
    /// <param name="mono">The <see cref="MonoBehaviour"/> invoking this method.</param>
    /// <param name="component">A reference to store the cached component.</param>
    /// <typeparam name="T">The type of component to cache.</typeparam>
    /// <returns><c>true</c> if the component is successfully cached; otherwise, <c>false</c>.</returns>
    public static bool CacheComponent<T>(MonoBehaviour mono, ref T component) where T : class
    {
        if(mono == null)
        {
            // The MonoBehaviour is null, this is almost always the users fault
            Debug.LogError($"The {nameof(MonoBehaviour)} is not valid!");
            return false;
        }
        // The component is already cached
        if(component != null) { return true; }

        if(!mono.TryGetComponent<T>(out component))
        {
            // The component couldn't be found
            Debug.LogError($"{mono.name} is missing component [{nameof(T)}]", mono);
            // Turn the component off since it requires this.
            mono.enabled = false;
            return false;
        }

        return true;
    }
}