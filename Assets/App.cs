using Spark.Balls.Utils;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Spark.FusionTest
{
    public class App :  Singleton<App>
    {
        [SerializeField] private SceneField mainMenuScene;
        [SerializeField] private SceneField teacherMenuScene;
        [SerializeField] private SceneField studentMenuScene;
        
        private NetworkRunner runner;
        private NetworkSceneManagerDefault netSceneManager;

        public string TeacherScene => teacherMenuScene.SceneName;
        public string StudentScene => studentMenuScene.SceneName;
        

        protected override void Awake()
        {
            base.Awake();
            runner = FindObjectOfType<NetworkRunner>();
            runner ??= gameObject.AddComponent<NetworkRunner>();
            runner.ProvideInput = true;
            
            netSceneManager = FindObjectOfType<NetworkSceneManagerDefault>();
            netSceneManager ??= gameObject.AddComponent<NetworkSceneManagerDefault>();
        }
        public void Disconnect()
        {
            print("quitting...");
            runner.Shutdown();
            SceneManager.LoadScene(mainMenuScene.SceneName);
        }
        private void OnApplicationQuit()
        {
            Disconnect();
        }
    }
}
