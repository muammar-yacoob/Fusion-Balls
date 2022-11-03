using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class SessionController : NetworkBehaviour
    {
        private float spawnDelay = 1f;
        private float timer;
        private const string BallCount = "BallCount";

        private void Update()
        {
            if (CanUpdate)
            {
                int ballCount = FindObjectsOfType<Ball>().Length;
                UpdateSessionProps(ballCount);
            }

            timer += Time.deltaTime;
        }

        public bool CanUpdate => timer >= spawnDelay;

        private void UpdateSessionProps(int ballCount)
        {
            var customProps = new Dictionary<string, SessionProperty>()
            {
                { BallCount, ballCount }
            };
            Runner.SessionInfo.UpdateCustomProperties(customProps);
        }
    }
}
