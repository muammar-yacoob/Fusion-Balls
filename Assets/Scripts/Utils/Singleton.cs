using UnityEngine;

namespace Spark.Balls.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if(Instance != null) Destroy(gameObject);
            Instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }

        protected void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
    
}