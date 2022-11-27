using Fusion;
using UnityEngine;

namespace Spark.FusionTest
{
    public class CubeRotation : NetworkBehaviour
    {
        [SerializeField] private float rotationSpeed = 1;
        
        public override void FixedUpdateNetwork()
        {
            var x = Input.GetAxisRaw("Horizontal");
            transform.Rotate(Vector3.up * rotationSpeed * x);
        }
    }
}