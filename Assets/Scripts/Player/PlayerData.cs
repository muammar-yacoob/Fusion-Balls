using Spark.Balls.Utils;
using Fusion;
using UnityEngine;

namespace Spark.FusionTest.Player
{
    public class PlayerData : NetworkSingleton<PlayerData>
    {
        [Networked] public string Nickname { get; private set; }
        [Networked] public NetworkObject Instance { get; private set; }

        [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
        public void RPC_SetNick(string nickname) => Nickname = nickname;

        public override void Spawned()
        {
            if (Object.HasInputAuthority)
                RPC_SetNick(PlayerPrefs.GetString("Nickname"));

            Runner.SetPlayerObject(Object.InputAuthority, Object);
        }
        
        public PlayerData GetPlayerData(PlayerRef player)
        {
            if (Runner.TryGetPlayerObject(player, out NetworkObject netObject))
            {
                return netObject.GetComponent<PlayerData>();
            }
            else
            {
                Debug.LogError($"Player {player} not found");
                return null;
            }
        }
    }
}