using System.Collections.Generic;
using System.Linq;
using Fusion;
using UnityEngine;

namespace Spark.FusionTest
{
  public class PlayerSpawner : SimulationBehaviour, ISpawned
  {
    [SerializeField] private NetworkPrefabRef playerPrefab;
    private readonly Dictionary<PlayerRef, NetworkObject> spawnedCharacters = new();

    public void Spawned()
    {
      var pos = transform.position + Vector3.up;
      var rot = transform.rotation;
      var playerObject = Runner.Spawn(playerPrefab, pos, rot, Runner.LocalPlayer);
      Runner.SetPlayerObject(Runner.LocalPlayer, playerObject);
      spawnedCharacters.Add(Runner.LocalPlayer, playerObject);
      
      print($"Welcome to {Runner.SessionInfo.Name}. Player Count:{Runner.ActivePlayers.Count()}");
    }
  }
}
