using System;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public abstract class StartMenuBase : MonoBehaviour
    {
        protected NetworkRunner runner;
        protected bool joiningSession;

        protected int gameSceneIndex = 0;

        protected void Awake()
        {
            runner = FindObjectOfType<NetworkRunner>();
            runner ??= gameObject.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
        }
        
        protected async void StartSession(GameMode mode, string sessionName = "Default")
        {
            joiningSession = true;
            var result = await runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionName,
                //CustomLobbyName = "DEV",
                Scene = gameSceneIndex,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>(),
                PlayerCount = 4
            });

            if (!result.Ok)
            {
                Debug.LogError($"Failed to Start: {result.ShutdownReason}");
                joiningSession = false;
                return;
            }
        }
    }
}