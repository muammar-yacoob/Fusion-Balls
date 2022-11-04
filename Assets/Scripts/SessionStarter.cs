using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public abstract class SessionStarter : MonoBehaviour
    {
        protected NetworkRunner runner;
        protected bool JoiningSession;

        private readonly int gameSceneIndex = 3;

        protected void Awake()
        {
            runner = FindObjectOfType<NetworkRunner>();
            runner ??= gameObject.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
        }

        protected async void StartSession(GameMode mode, string sessionName = "Default")
        {
            JoiningSession = true;


            var customProps = new Dictionary<string, SessionProperty>()
            {
                { "BallCount", 0 }
            };

            var result = await runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionName,
                Scene = gameSceneIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                PlayerCount = 4,
                SessionProperties = customProps
            });

            if (!result.Ok)
            {
                Debug.LogError($"Failed to Start: {result.ShutdownReason}");
                JoiningSession = false;
                return;
            }

            print($"Loading Scene[{gameSceneIndex}]...");
            runner.SetActiveScene(gameSceneIndex);
        }

        private void OnApplicationQuit()
        {
            if(runner.IsServer)
                runner.Disconnect(runner.LocalPlayer);
        }
    }
}