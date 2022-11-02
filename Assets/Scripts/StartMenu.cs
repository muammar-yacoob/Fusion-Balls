using System;
using System.Linq;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Born.FusionTest
{
    public class StartMenu : MonoBehaviour
    {
        private NetworkRunner runner;

        [SerializeField] private bool joinLobby;
        private const string LobbyName = "DEV";
        private bool lobbyJoined;
        private bool joiningSession;
        
        private string sessionName = "Default";
        private bool sessionStarted;

        private int gameSceneIndex = 1;


        private void Awake()
        {
            runner = FindObjectOfType<NetworkRunner>();
            runner ??= gameObject.AddComponent<NetworkRunner>();

            runner.ProvideInput = true;
            
            if(joinLobby)
                StartLobby();
        }
        async void StartLobby()
        {
            if (runner == null)
            {
                Debug.LogError("No Runner Detected");
                return;
            }

            //Join a lobby for the machmaker
            var result = await runner.JoinSessionLobby(SessionLobby.Custom, LobbyName);

            if (!result.Ok)
            {
                Debug.LogError($"Failed to Join Lobby: {result.ShutdownReason}");
                return;
            }

            lobbyJoined = true;
            print($"Lobby {LobbyName} joined");
        }

        private void OnGUI()
        {
            DrawHud();
        }

        private void DrawHud()
        {
            if(sessionStarted) return;
            
            if (joinLobby && !lobbyJoined || joiningSession)
            {
                GUI.Label(new Rect(10, 10, 120, 20), "Connecting...");
                return;
            }

            if (sessionStarted) return;

            sessionName = GUI.TextField(new Rect(10, 10, 120, 20), sessionName, 10);

            if (String.IsNullOrEmpty(sessionName)) return;
            if (GUI.Button(new Rect(10, 33, 120, 20), $"Start/Join {sessionName}"))
            {
                StartSession(GameMode.Shared);
            }
        }

        private async void StartSession(GameMode mode)
        {
            var sceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>();
            //gameSceneIndex = SceneManager.GetActiveScene().buildIndex;
            joiningSession = true;
            
            var result = await runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                CustomLobbyName = LobbyName,
                SessionName = sessionName,
                Scene = gameSceneIndex,
                SceneManager = sceneManager,
                PlayerCount = 4
            });

            if (!result.Ok)
            {
                Debug.LogError($"Failed to Start: {result.ShutdownReason}");
                sessionStarted = false;
                joiningSession = false;
                return;
            }
            sessionStarted = true;
        }
    }
}