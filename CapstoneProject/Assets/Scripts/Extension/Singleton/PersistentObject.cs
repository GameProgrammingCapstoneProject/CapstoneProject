using System;
using UnityEngine;
namespace Core.Extension
{
    /// <summary>
    ///     A persistence object that dont be destroyed when loading another
    ///     scene, which can be inherited and make sure there's only one
    ///     instance in the scene.
    /// </summary>
    public class PersistentObject<T> : MonoBehaviour where T : Component
    {
        // Variables
        // ------------------------------------------------------------------------
        protected bool _enabled;
        protected static T _instance;
        protected static bool _isQuitting = false;

        // Property
        // ------------------------------------------------------------------------

        /// <summary>
        ///     The instance of this PersistentObject. Using Singleton Design Pattern.
        /// </summary>
        /// <value>The Instance</value>
        public static T Instance
        {
            get
            {
                if (_isQuitting) return null;
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        _instance = obj.AddComponent<T>();
                        // Setting name for better management
                        string[] typeName = obj.GetComponent<T>().ToString().Split('.', ')');
                        _instance.name = typeName[typeName.Length - 2];
                    }
                }
                return _instance;
            }
        }

        // Methods
        // -----------------------------------------------------------------------

        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;

            if (_instance == null)
            {
                // If this is the first instance, make it singleton.
                _instance = this as T;
                _enabled = true;
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void Start()
        {
            // If another Singleton already exists, destroy this one.
            if (_instance != null && _instance != this)
                Destroy(this);
        }

        protected virtual void OnApplicationQuit()
        {
            DestroyImmediate(this.gameObject);
        }

        protected void OnDestroy()
        {
            _isQuitting = true;
        }
    }
}
