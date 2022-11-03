using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

namespace Born.FusionTest
{
    public class QuitSession : NetworkBehaviour
    {

        public override void Spawned()
        {
            var quitButton = GetComponent<Button>();
            quitButton.onClick.AddListener(()=>Runner.Disconnect(Runner.LocalPlayer));
        }

    }
}
