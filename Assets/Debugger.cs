using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Debugger : NetworkBehaviour
{
    private void Awake()
    {
        if (Runner == null)
        {
            Debug.Log("No runner found. Loading first scene...");
            SceneManager.LoadScene(0);
        }
    }
}