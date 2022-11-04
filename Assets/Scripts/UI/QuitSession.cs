using Fusion;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Born.FusionTest
{
    public class QuitSession : NetworkBehaviour
    {
        public override void Spawned()
        {
            var quitButton = GetComponent<Button>();
            quitButton.onClick.AddListener(() => SceneManager.LoadScene(0));
        }
    }
}