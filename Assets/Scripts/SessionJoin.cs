using System;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class SessionJoin : SessionStarter
    {
        private bool joiningLobby;
        private const string LobbyName = "DEV";
        
        private void Awake()
        {
            base.Awake();
            StartLobby();
            LobbyController.OnJoinSession += Handle_OnJoinSession;
        }

        private void OnDestroy() => LobbyController.OnJoinSession -= Handle_OnJoinSession;

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

        private void Update()
        {
            if (runner.SessionInfo.IsValid) return;
            if (joiningLobby)
            {
                //GUI.Label(new Rect(10, 10, 120, 25), "Joining Lobby...");
                return;
            }

            if (JoiningSession) return;
        }
        
        private void Handle_OnJoinSession(string sessionName)
        {
            if (String.IsNullOrEmpty(sessionName)) return;
            {
                JoiningSession = true;
                print($"joining {sessionName}...");
                StartSession(GameMode.Shared, sessionName);
            }
        }
    }
}