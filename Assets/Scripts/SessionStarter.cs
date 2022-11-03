using System;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public abstract class SessionStarter : MonoBehaviour
    {
        protected NetworkRunner runner;
        protected bool joiningSession;

        protected int gameSceneIndex = 2;

        protected void Awake()
        {
            runner = FindObjectOfType<NetworkRunner>();
            runner ??= gameObject.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
        }
        
        protected async void StartSession(GameMode mode, string sessionName = "Default")
        {
            joiningSession = true;
            
            var customProps = new Dictionary<string, SessionProperty>()
            {
                { "BallCount", 0 }
            };
            
            var result = await runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionName,
                //CustomLobbyName = "DEV",
                Scene = gameSceneIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                PlayerCount = 4,
                SessionProperties = customProps
            });

            if (!result.Ok)
            {
                Debug.LogError($"Failed to Start: {result.ShutdownReason}");
                joiningSession = false;
                return;
            }
        }

        private void OnDestroy() => runner.Disconnect(runner.LocalPlayer);
        private void OnApplicationQuit() => runner.Disconnect(runner.LocalPlayer);
    }
}