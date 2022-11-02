using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class Cube : NetworkBehaviour
    {
        [SerializeField] private float rotationSpeed = 1;

        private void Awake() => transform.position = Vector3.back;

        private void Update()
        {
            if (Object == null) return;
            
            if(Input.GetKeyDown(KeyCode.Alpha1))
                Object.RequestStateAuthority();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                Object.ReleaseStateAuthority();
        }

        public override void FixedUpdateNetwork()
        {
            var x = Input.GetAxisRaw("Horizontal");
            transform.Rotate(Vector3.up * rotationSpeed * x);
            print($"Has State: {Object.HasStateAuthority}");
        }
    }
}
