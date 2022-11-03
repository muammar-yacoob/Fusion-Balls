using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Born.FusionTest
{
    public class LobbyController : NetworkBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private RectTransform sessionsListUI;
        [SerializeField] private Button joinButtonPrefab;
        
        public override void Spawned() => Runner.AddCallbacks(this);
        public override void Despawned(NetworkRunner runner, bool hasState) => runner.RemoveCallbacks(this);

        public static event Action<string> OnJoinSession = delegate { };

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            Debug.Log($"Sessions List Updated: Count:{sessionList.Count}");
            ClearExistingButtons();
            if (sessionList.Count == 0) return;
            

            foreach (var session in sessionList)
            {
                if (!session.IsValid) return;

                var sessionRecord = new SessionDescriptor(session.Name, session.PlayerCount,session.MaxPlayers);
                Debug.Log(sessionRecord.ToString());
                
                var buttonObject = Instantiate(joinButtonPrefab,sessionsListUI);
                buttonObject.onClick.AddListener(() => OnJoinSessionButtonClicked(session.Name));
                
                var textObject = buttonObject.GetComponentInChildren<TMP_Text>();
                textObject.text = sessionRecord.ToString();
            }
        }

        private void ClearExistingButtons()
        {
            var oldButtons = sessionsListUI.GetComponentsInChildren<Button>();
            oldButtons.ToList().ForEach(b => Destroy(b.gameObject));
        }

        private void OnJoinSessionButtonClicked(string sessionName)
        {
            OnJoinSession?.Invoke(sessionName);
        }

        #region Other Callbacks
        public void OnConnectedToServer(NetworkRunner runner){}
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player){}
        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player){}
        public void OnInput(NetworkRunner runner, NetworkInput input){}
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input){}
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason){}
        public void OnDisconnectedFromServer(NetworkRunner runner){}
        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token){}
        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason){}
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message){}
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data){}
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken){}
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data){}
        public void OnSceneLoadDone(NetworkRunner runner){}
        public void OnSceneLoadStart(NetworkRunner runner){}
        #endregion
    }

    public class SessionDescriptor
    {
        public string SessionName { get; }
        public int MaxPlayers { get; }
        public int PlayerCount { get; }

        public SessionDescriptor(string sessionName, int playerCount,int maxPlayers)
        {
            SessionName = sessionName;
            MaxPlayers = maxPlayers;
            PlayerCount = playerCount;
        }

        public override string ToString() => ($"{SessionName}: [{PlayerCount}/{MaxPlayers}]");
    }
}
