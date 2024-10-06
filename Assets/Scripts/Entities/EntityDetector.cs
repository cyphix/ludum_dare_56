using UnityEngine;



public class EntityDetector : MonoBehaviour, IEntityDetector
{
    #region PROPERTIES

    public bool HasSpottedEntities { get; private set; } = false;

    #endregion // PROPERTIES
}