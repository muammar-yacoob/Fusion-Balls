using System;
using Fusion;
using UnityEngine;

namespace Spark.FusionTest
{
    public class StartMenuSimple : SessionStarter
    {
        private string sessionName = "Default";
        private void OnGUI()
        {
            if (runner.SessionInfo.IsValid) return;
            if (JoiningSession)
            {
                GUI.Label(new Rect(10, 10, 120, 25), "Connecting...");
                return;
            }

            if (JoiningSession) return;
            JoinSessionUI();
        }
        
        private void JoinSessionUI()
        {
            sessionName = GUI.TextField(new Rect(10, 10, 120, 20), sessionName, 10);

            if (String.IsNullOrEmpty(sessionName)) return;
            if (GUI.Button(new Rect(10, 33, 120, 20), $"Start/Join {sessionName}"))
            {
                JoiningSession = true;
                StartSession(GameMode.Shared, sessionName);
            }
        }
    }
}