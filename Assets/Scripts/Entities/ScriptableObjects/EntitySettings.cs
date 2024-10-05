using UnityEngine;



namespace Entities.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EntitySettings", menuName = "Entity Settings", order = 0)]
    public class EntitySettings : ScriptableObject
    {
        #region INSPECTOR FIELDS

        [SerializeField]
        private float _moveSpeed = 5f;

        #endregion // INSPECTOR FIELDS


        #region PROPERTIES
        
        public float MoveSpeed { get { return this._moveSpeed; } }
        
        #endregion // PROPERTIES
    }
}