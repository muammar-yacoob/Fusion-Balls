using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class BallOwnership : NetworkBehaviour, IStateAuthorityChanged
    {

        public void StateAuthorityChanged()
        {
            print($"Ball is now owned by {Object.StateAuthority}");
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(TryGetComponent(typeof(PlayerMovement),out var player))
            {
                var playerRef = player.GetComponent<NetworkObject>().InputAuthority;
                transform.parent = player.transform;
                //Object.aut
                Object.ReleaseStateAuthority();
            }
            
        }
    }
}
