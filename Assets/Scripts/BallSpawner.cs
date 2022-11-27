using System.Collections.Generic;
using Fusion;
using UnityEngine;

namespace Spark.FusionTest
{
    public class BallSpawner : SimulationBehaviour
    {
        [SerializeField] private NetworkPrefabRef ballPrefab;
        [SerializeField] private float spawnDelay = 1f;
        
        private float timer;
        private List<NetworkObject> ballObjects = new();

        public override void FixedUpdateNetwork()
        {
            if (Runner.State != NetworkRunner.States.Running) return;
            if (Object.HasStateAuthority == false) return;

            if (CanSpawn) Spawn();
            timer += Time.deltaTime;
        }


        public bool CanSpawn => timer >= spawnDelay; 

        private void Spawn()
        {
            timer = 0;
            var pos = Random.insideUnitSphere * Random.Range(0.1f,10);
            pos.y = 8;
            var ballObject = Runner.Spawn(ballPrefab,pos,Quaternion.identity);
            ballObjects.Add(ballObject);
        }
    }
}
