using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class Cube : NetworkBehaviour
    {
        [SerializeField] private float rotationSpeed = 1;
        [SerializeField] private GameObject crown;

        private void Awake() => transform.position = Vector3.back;

        private void Update()
        {
            if (Object == null) return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                GetAuthority();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                ReleaseAuthority();
        }

        private void GetAuthority()
        {
            Object.RequestStateAuthority();
            crown.SetActive(true);
            print($"Has State: {Object.HasStateAuthority}");
        }
        
        private void ReleaseAuthority()
        {
            Object.ReleaseStateAuthority();
            crown.SetActive(false);
            print($"Has State: {Object.HasStateAuthority}");
        }

        public override void FixedUpdateNetwork()
        {
            var x = Input.GetAxisRaw("Horizontal");
            transform.Rotate(Vector3.up * rotationSpeed * x);
        }
    }
}