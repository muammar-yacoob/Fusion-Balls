using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class CubeAuthority : NetworkBehaviour
    {
        [SerializeField] private GameObject crown;

        public override void Spawned()
        {
            SwitchAuthority(Object.HasStateAuthority);
        }

        private void Update()
        {
            if (Object == null) return;
            if (crown == null) return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchAuthority(!Object.HasStateAuthority);
        }
        
        private void SwitchAuthority(bool state)
        {
            if(state) Object.RequestStateAuthority();
            else Object.ReleaseStateAuthority();
            
            crown.SetActive(state);
            print($"Has State: {state}");
        }
    }
}