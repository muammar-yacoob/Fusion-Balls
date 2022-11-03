using System;
using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
    public class SimpleStartMenu : StartMenu
    {
        private string sessionName = "Default";
        private void OnGUI()
        {
            if (joiningSession)
            {
                GUI.Label(new Rect(10, 10, 120, 25), "Connecting...");
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
                StartSession(GameMode.Shared, sessionName);
                joiningSession = true;
            }
        }
    }
}