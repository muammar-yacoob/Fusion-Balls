using System;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class StartMenuLobby : StartMenuBase
    {
        private bool joiningLobby;
        private const string LobbyName = "DEV";
        private string sessionName = "Default";
        private void Awake()
        {
            base.Awake();
            StartLobby();
        }

        async void StartLobby()
        {
            joiningLobby = true;
            if (runner == null)
            {
                Debug.LogError("No Runner Detected");
                return;
            }

            //Join a lobby for the matchmaker
            var result = await runner.JoinSessionLobby(SessionLobby.Custom, LobbyName);

            if (!result.Ok)
            {
                Debug.LogError($"Failed to Join Lobby: {result.ShutdownReason}");
                return;
            }
            print($"Lobby {LobbyName} joined");
            joiningLobby = false;
        }

        private void OnGUI()
        {
            if (runner.SessionInfo.IsValid) return;
            if (joiningLobby)
            {
                GUI.Label(new Rect(10, 10, 120, 25), "Joining Lobby...");
                return;
            }

            if (joiningSession) return;
            JoinSessionUI();
        }
        
        private void JoinSessionUI()
        {
            sessionName = GUI.TextField(new Rect(10, 10, 120, 20), sessionName, 10);

            if (String.IsNullOrEmpty(sessionName)) return;
            if (GUI.Button(new Rect(10, 33, 120, 20), $"Start/Join {sessionName}"))
            {
                joiningSession = true;
                StartSession(GameMode.Shared, sessionName);
            }
        }
    }
}