using Fusion;
using UnityEngine;

namespace Born.FusionTest
{
public class SimpleLobby : MonoBehaviour
{
    private NetworkRunner runner;

    private void Awake()
    {
        StartLobby("DEV");
        runner = GetComponent<NetworkRunner>();
    }

    async void StartLobby(string LobbyName)
    {
        var result = await runner.JoinSessionLobby(SessionLobby.Custom, LobbyName);

        if (!result.Ok)
        {
            Debug.LogError($"Failed to Join Lobby: {result.ShutdownReason}");
            
            return;
        }
        print($"Lobby {LobbyName} joined");
    }
}
}