using Fusion;

namespace Born.FusionTest.Utils
{
    public abstract class NetworkSingleton<T> : NetworkBehaviour where T : NetworkBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake()
        {
            if(Instance != null) Destroy(gameObject);
            Instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        protected void OnApplicationQuit()
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
    
    
}