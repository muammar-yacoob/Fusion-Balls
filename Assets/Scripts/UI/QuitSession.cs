using UnityEngine;
using UnityEngine.UI;

namespace Born.FusionTest
{
    public class QuitSession : MonoBehaviour
    {
        private void Awake()
        {
            var quitButton = GetComponent<Button>();
            quitButton.onClick.AddListener(App.Instance.Disconnect);
        }
    }
}