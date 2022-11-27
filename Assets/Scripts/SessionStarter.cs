using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Spark.FusionTest
{
    public abstract class SessionStarter : MonoBehaviour
    {
        protected NetworkRunner runner;
        private NetworkSceneManagerDefault netSceneManager;
        protected bool JoiningSession;

        private readonly int gameSceneIndex = 4;

        protected void Awake()
        {
            runner = FindObjectOfType<NetworkRunner>();
            runner ??= gameObject.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
            
            netSceneManager = FindObjectOfType<NetworkSceneManagerDefault>();
            netSceneManager ??= gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        protected async void StartSession(GameMode mode, string sessionName = "Default")
        {
            print("starting session "+sessionName);
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
                SceneManager = netSceneManager,
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


    }
}