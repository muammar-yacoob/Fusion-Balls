using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Born.FusionTest
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button teacherButton;
        [SerializeField] private Button studentButton;

        private void Awake()
        {
            teacherButton.onClick.AddListener(() => SceneManager.LoadScene(1));
            studentButton.onClick.AddListener(() => SceneManager.LoadScene(2));
        }
    }
}