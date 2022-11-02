using Fusion;
using UnityEditor;
using UnityEngine;

namespace Born.FusionTest
{
    public class PlayerSetup : NetworkBehaviour
    {
        private Material mat;

        public override void Spawned()
        {
            mat = GetComponentInChildren<Renderer>().material;
           
            if (Object.HasInputAuthority)
                SetupLocalPlayer();
            else
                SetupRemotePlayer();
            
#if UNITY_EDITOR
            SetupInEditor();
#endif
            
        }
    
        private void SetupLocalPlayer()
        {
            mat.color = Color.cyan;
        }

        private void SetupRemotePlayer()
        {
            mat.color = Color.red;
        }
        
        
#if UNITY_EDITOR
        public void SetupInEditor()
        {
            if (Object.HasInputAuthority)
            {
                gameObject.name += " - Mine";
                Selection.activeObject = transform;
            }
        }
#endif

    }
}
