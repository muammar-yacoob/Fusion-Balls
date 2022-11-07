using TMPro;
using UnityEngine;


public class AppVersion : MonoBehaviour
{
    private void Awake() => GetComponent<TMP_Text>().text ="ver "+ Application.version;
}
