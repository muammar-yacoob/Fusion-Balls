using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Born.FusionTest
{
    public class BackToMainMenuButton : MonoBehaviour
    {
        private void Start()
        {
            //if (App.Instance == null) return;
            var backButton = GetComponent<Button>();
            backButton.onClick.AddListener(()=>SceneManager.LoadScene(0));
        }
    }
}